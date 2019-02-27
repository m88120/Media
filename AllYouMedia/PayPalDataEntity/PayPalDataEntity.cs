using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayPal;
using PayPal.Api;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data;
using System.Configuration;

namespace DataLayer
{
    public class PayPalDataEntity
    {
        #region PayPalDataEntity
        public PayPalDataEntity()
        {

        }
        #endregion


        #region GetConfig()
        private Dictionary<string, string> GetConfig()
        {
            Dictionary<string, string> configMap = new Dictionary<string, string>();

            configMap.Add("mode", ConfigurationSettings.AppSettings["PaypalEndpoints"].ToString());

            return configMap;
        }
        #endregion

        #region GetAccessToken()
        private string GetAccessToken()
        {
            return new OAuthTokenCredential(ConfigurationSettings.AppSettings["PaypalClientID"].ToString(), ConfigurationSettings.AppSettings["PaypalSecretID"].ToString(), GetConfig()).GetAccessToken();
        }
        #endregion

        #region GetAPIContext()
        private APIContext GetAPIContext()
        {
            APIContext apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();

            return apiContext;
        }
        #endregion

        #region GetAPIContext
        private APIContext GetAPIContext(string orderNo)
        {
            APIContext apiContext = new APIContext(GetAccessToken(), orderNo);
            apiContext.Config = GetConfig();

            return apiContext;
        }
        #endregion

        #region DeleteActivePlan()
        private void DeleteActivePlan()
        {
            this.DeleteActivePlan(this.GetAPIContext());
                
        }
        private void DeleteActivePlan(APIContext apiContext)
        {
            PlanList planList = Plan.List(apiContext, "0", "ACTIVE", "10", "yes");

            if (planList != null && planList.plans != null && planList.plans.Count > 0)
            {
                foreach (Plan plan in planList.plans)
                {
                    plan.Delete(apiContext);
                }
            }
        }
        #endregion

        #region GetActivePlan()
        private Plan GetActivePlan(APIContext apiContext, string netTotal, string currency, string cancel_url, string return_url)
        {
            PlanList planList = Plan.List(apiContext, "0", "ACTIVE", "1", "no");

            if (planList != null && planList.plans != null && planList.plans.Count > 0)
            {
                return planList.plans[planList.plans.Count - 1];
            }
            else
            {
                Currency amount = new Currency();
                amount.currency = currency;
                amount.value = netTotal;

                PaymentDefinition paymentDefinitions = new PaymentDefinition();
                paymentDefinitions.name = "InewHub Business Partner Subscription";
                paymentDefinitions.type = "REGULAR";
                paymentDefinitions.frequency = "MONTH";
                paymentDefinitions.frequency_interval = "1";
                paymentDefinitions.amount = amount;
                paymentDefinitions.cycles = "12";

                List<PaymentDefinition> paymentDefinitionsList = new List<PaymentDefinition>();
                paymentDefinitionsList.Add(paymentDefinitions);

                MerchantPreferences merchantPreferences = new MerchantPreferences();
                merchantPreferences.cancel_url = cancel_url;
                merchantPreferences.return_url = return_url;
                merchantPreferences.auto_bill_amount = "YES";
                //merchantPreferences.setup_fee = amount;

                Plan plan = new Plan();
                plan.name = "InewHub Business Partner Subscription";
                plan.description = "9.99 Subscription";
                plan.payment_definitions = paymentDefinitionsList;
                plan.merchant_preferences = merchantPreferences;
                plan.type = "fixed";

                Plan createdplan = plan.Create(apiContext);

                Patch patch = new Patch();
                patch.op = "replace";
                patch.path = "/";
                patch.value = new Plan() { state = "ACTIVE" };

                PatchRequest patchRequest = new PatchRequest();
                patchRequest.Add(patch);

                createdplan.Update(apiContext, patchRequest);

                return createdplan;
            }
        }
        #endregion


        #region PaymentExecuteWithPayPal()
        public DataTable PaymentExecuteWithPayPal(out object message)
        {
            DataTable dtTran = new DataTable();
            dtTran.Columns.Add(new DataColumn("TransactionID", typeof(string)));
            dtTran.Columns.Add(new DataColumn("TotalAmount", typeof(string)));
            dtTran.Columns.Add(new DataColumn("Currency", typeof(string)));
            dtTran.Columns.Add(new DataColumn("Date", typeof(string)));

            message = "";
            HttpContext CurrContext = HttpContext.Current;

            if (CurrContext.Request.Params["PayerID"] != null)
            {
                try
                {
                    APIContext apiContext = this.GetAPIContext();

                    Payment pymnt = new Payment();
                    if (CurrContext.Request.Params["guid"] != null)
                    {
                        pymnt.id = (string)CurrContext.Session[CurrContext.Request.Params["guid"]];
                    }
                    try
                    {
                        PaymentExecution pymntExecution = new PaymentExecution();
                        pymntExecution.payer_id = CurrContext.Request.Params["PayerID"];

                        Payment executedPayment = pymnt.Execute(apiContext, pymntExecution);

                        if (executedPayment != null)
                        {
                            if (executedPayment.state.ToLower() == "approved")
                            {
                                Sale sale = executedPayment.transactions[0].related_resources[0].sale;
                                dtTran.Rows.Add(sale.id, sale.amount.total, sale.amount.currency, sale.create_time);
                            }
                            else
                            {
                                if (executedPayment.failed_transactions != null && executedPayment.failed_transactions.Count > 0)
                                {
                                    message = executedPayment.failed_transactions[0].message;
                                }
                                else
                                {
                                    message = "Fatal error found. Failed transactions is also null!";
                                }
                            }
                        }
                        else
                        {
                            message = "Fatal error found. Executed Payment can not be null!";
                        }
                    }
                    catch (PaymentsException ex)
                    {
                        message = "<span style='font-size:20px;'>Payment failed!</span><br/><br/>";

                        if (ex.Message != "") message = message + ex.Message + "<br />";
                        if (ex.Details != null && ex.Details.message != "") message = message + ex.Details.message + "<br />";
                    }
                }
                catch (Exception ex2)
                {
                    message = "<span style='font-size:20px;'>Payment failed!</span><br/><br/>";

                    if (ex2.Message != "") message = message + ex2.Message;
                }
            }
            else
            {
                message = "Sorry! This is not a valid request.";
            }

            return dtTran;
        }
        #endregion

        #region PaymentSubscriptionExecuteWithPayPal()
        public DataTable PaymentSubscriptionExecuteWithPayPal(out object message)
        {
            DataTable dtTran = new DataTable();
            dtTran.Columns.Add(new DataColumn("PlanID", typeof(string)));
            dtTran.Columns.Add(new DataColumn("AgreementID", typeof(string)));
            dtTran.Columns.Add(new DataColumn("Token", typeof(string)));
            dtTran.Columns.Add(new DataColumn("Amount", typeof(string)));
            dtTran.Columns.Add(new DataColumn("Date", typeof(string)));

            message = "";
            HttpContext CurrContext = HttpContext.Current;

            string token = (CurrContext.Request.Params["token"] != null) ? CurrContext.Request.Params["token"].ToString().Trim() : "";

            if (token != "")
            {
                try
                {
                    APIContext apiContext = this.GetAPIContext();
                    try
                    {
                        string _loginName = CurrContext.Session["LoginName"].ToString();

                        if (_loginName == HttpContext.Current.User.Identity.Name)
                        {
                            Agreement executedAgreement = Agreement.Execute(apiContext, token);

                            Agreement createdAgreement = Agreement.Get(apiContext, executedAgreement.id);

                            if (createdAgreement != null)
                            {
                                string _planID = "";
                                string _amount = "";

                                Plan plan = Plan.Get(apiContext, CurrContext.Session["PlanID"].ToString());

                                if (plan != null)
                                {
                                    _planID = plan.id;

                                    if (plan.payment_definitions != null && plan.payment_definitions.Count > 0)
                                        _amount = plan.payment_definitions[0].amount.value;
                                }

                                dtTran.Rows.Add(_planID, createdAgreement.id, token, _amount, createdAgreement.start_date);
                            }
                            else
                            {
                                message = "Fatal error found. Executed agreement can not be null!";
                            }
                        }
                        else
                        {
                            message = "Fatal error found. Current user does not matched to this agreement!";
                        }
                    }
                    catch (PaymentsException ex)
                    {
                        message = "<span style='font-size:20px;'>Payment failed!</span><br/><br/>";

                        if (ex.Message != "") message = message + ex.Message + "<br />";
                        if (ex.Details != null && ex.Details.message != "") message = message + ex.Details.message + "<br />";
                    }
                }
                catch (Exception ex2)
                {
                    message = "<span style='font-size:20px;'>Payment failed!</span><br/><br/>";

                    if (ex2.Message != "") message = message + ex2.Message;
                }
            }
            else
            {
                message = "Sorry! This is not a valid request.";
            }

            return dtTran;
        }
        #endregion


        #region PaymentSubscriptionCreatingWithPayPal()
        public bool PaymentSubscriptionCreatingWithPayPal(string netTotal, string currency, string cancel_url, string return_url, out string message, out string redirectLink)
        {
            bool isSuccess = false;
            redirectLink = "";

            HttpContext CurrContext = HttpContext.Current;

            try
            {
                APIContext apiContext = this.GetAPIContext();
                try
                {
                    Plan plan = this.GetActivePlan(apiContext, netTotal, currency, cancel_url, return_url);

                    if (plan.id != "")
                    {
                        Payer payr = new Payer();
                        payr.payment_method = "paypal";

                        Agreement agreement = new Agreement();
                        agreement.name = netTotal + " Subscription";
                        agreement.description = "InewHub " + netTotal + " Subscription";
                        agreement.start_date = DateTime.Now.AddMonths(1).GetDateTimeFormats(new System.Globalization.CultureInfo("en-US"))[105] + "Z";
                        agreement.plan = new Plan() { id = plan.id };
                        agreement.payer = payr;

                        Agreement createdAgreement = agreement.Create(apiContext);

                        message = JObject.Parse(createdAgreement.ConvertToJson()).ToString(Formatting.Indented);

                        var links = createdAgreement.links.GetEnumerator();

                        while (links.MoveNext())
                        {
                            Links lnk = links.Current;
                            if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                            {
                                redirectLink = lnk.href;
                            }
                        }
                        CurrContext.Session.Add("LoginName", HttpContext.Current.User.Identity.Name);
                        CurrContext.Session.Add("PlanID", plan.id);
                        isSuccess = true;
                    }
                    else
                    {
                        message = "Fatal error found!";
                        isSuccess = false;
                    }
                }
                catch (PaymentsException ex)
                {
                    message = "<span style='font-size:20px;'>Payment failed!</span><br/><br/>";

                    if (ex.Message != "") message = message + ex.Message + "<br />";

                    if (ex.Details != null)
                    {
                        if (ex.Details.message != "") message = message + ex.Details.message + "<br />";

                        if (ex.Details.details != null)
                        {
                            foreach (ErrorDetails error in ex.Details.details)
                            {
                                if (error.issue != "") message = message + error.field + ": " + error.issue + "<br />";
                            }
                        }
                    }

                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                isSuccess = false;
            }

            return isSuccess;
        }
        #endregion

        #region PaymentCreatingWithPayPal()
        public bool PaymentCreatingWithPayPal(DataTable paymentItems, string shippingAmount, string handlingAmount, string subTotal, string netTotal, string currency, string cancel_url, string return_url, string transactionDescription, out string message, out string redirectLink)
        {
            bool isSuccess = false;
            redirectLink = "";
            HttpContext CurrContext = HttpContext.Current;

            List<Item> itms = new List<Item>();

            foreach (DataRow row in paymentItems.Rows)
            {
                Item item = new Item();
                item.name = row["Name"].ToString();
                item.currency = row["Currency"].ToString();
                item.price = row["Price"].ToString();
                item.quantity = row["Quantity"].ToString();

                itms.Add(item);
            }

            ItemList itemList = new ItemList();
            itemList.items = itms;

            Payer payr = new Payer();
            payr.payment_method = "paypal";

            Random rndm = new Random();
            var guid = Convert.ToString(rndm.Next(100000));

            RedirectUrls redirUrls = new RedirectUrls();
            redirUrls.cancel_url = cancel_url + (cancel_url.Contains("?") ? "&" : "?") + "guid=" + guid;
            redirUrls.return_url = return_url + (return_url.Contains("?") ? "&" : "?") + "guid=" + guid;

            Details details = new Details();
            details.tax = "0";
            details.shipping = shippingAmount;
            details.handling_fee = handlingAmount;
            details.subtotal = subTotal;

            Amount amnt = new Amount();
            amnt.currency = currency;
            amnt.total = netTotal;
            amnt.details = details;

            List<Transaction> transactionList = new List<Transaction>();
            Transaction tran = new Transaction();
            tran.description = transactionDescription;
            tran.amount = amnt;
            tran.item_list = itemList;

            transactionList.Add(tran);

            Payment pymnt = new Payment();
            pymnt.intent = "sale";
            pymnt.payer = payr;
            pymnt.transactions = transactionList;
            pymnt.redirect_urls = redirUrls;

            try
            {
                APIContext apiContext = this.GetAPIContext();
                Payment createdPayment = pymnt.Create(apiContext);

                message = JObject.Parse(createdPayment.ConvertToJson()).ToString(Formatting.Indented);

                var links = createdPayment.links.GetEnumerator();

                while (links.MoveNext())
                {
                    Links lnk = links.Current;
                    if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                    {
                        redirectLink = lnk.href;
                    }
                }
                CurrContext.Session.Add(guid, createdPayment.id);
                isSuccess = true;
            }
            catch (ConnectionException ex)
            {
                message = ex.Message;
                isSuccess = false;
            }
            catch (InvalidCredentialException ex)
            {
                message = ex.Message;
                isSuccess = false;
            }
            catch (PayPalException ex)
            {
                message = ex.Message;
                isSuccess = false;
            }

            return isSuccess;
        }
        #endregion

        #region PaymentCreatingWithPayPalCreditCard()
        public DataTable PaymentCreatingWithPayPalCreditCard(string orderno, DataTable paymentItems, string shippingAmount, string subTotal, string netTotal, string currency, string first_name, string last_name, string number, string type, int expire_month, int expire_year, string transactionDescription, out object message)
        {
            DataTable dtTran = new DataTable();
            dtTran.Columns.Add(new DataColumn("TransactionID", typeof(string)));
            dtTran.Columns.Add(new DataColumn("TotalAmount", typeof(string)));
            dtTran.Columns.Add(new DataColumn("Currency", typeof(string)));
            dtTran.Columns.Add(new DataColumn("Date", typeof(string)));

            HttpContext CurrContext = HttpContext.Current;

            List<Item> itms = new List<Item>();

            foreach (DataRow row in paymentItems.Rows)
            {
                Item item = new Item();
                item.name = row["Name"].ToString();
                item.currency = row["Currency"].ToString();
                item.price = row["Price"].ToString();
                item.quantity = row["Quantity"].ToString();

                itms.Add(item);
            }

            ItemList itemList = new ItemList();
            itemList.items = itms;

            CreditCard crdtCard = new CreditCard();
            //crdtCard.cvv2 = cvv2;
            crdtCard.expire_month = expire_month;
            crdtCard.expire_year = expire_year;
            crdtCard.first_name = first_name;
            crdtCard.last_name = last_name;
            crdtCard.number = number;
            crdtCard.type = type;

            Details details = new Details();
            details.tax = "0";
            details.shipping = shippingAmount;
            details.subtotal = subTotal;

            Amount amnt = new Amount();
            amnt.currency = currency;
            amnt.total = netTotal;
            amnt.details = details;

            List<Transaction> transactionList = new List<Transaction>();
            Transaction tran = new Transaction();
            tran.description = transactionDescription;
            tran.amount = amnt;
            tran.item_list = itemList;

            transactionList.Add(tran);

            List<FundingInstrument> fundingInstrumentList = new List<FundingInstrument>();
            FundingInstrument fundInstrument = new FundingInstrument();
            fundInstrument.credit_card = crdtCard;

            fundingInstrumentList.Add(fundInstrument);

            Payer payr = new Payer();
            payr.funding_instruments = fundingInstrumentList;
            payr.payment_method = "credit_card";

            Payment pymnt = new Payment();
            pymnt.intent = "sale";
            pymnt.payer = payr;
            pymnt.transactions = transactionList;

            try
            {
                APIContext apiContext = this.GetAPIContext();
                Payment createdPayment = pymnt.Create(apiContext);

                if (createdPayment.state.ToLower() == "approved")
                {
                    Sale sale = createdPayment.transactions[0].related_resources[0].sale;
                    dtTran.Rows.Add(sale.id, sale.amount.total, sale.amount.currency, sale.create_time);

                    message = "Status is approved.";
                }
                else
                {
                    message = "Status is pending.";
                }
            }
            catch (PayPalException ex)
            {
                message = ex.Message;
            }

            return dtTran;
        }
        #endregion

        #region PaymentItems()
        public DataTable PaymentItems()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Currency", typeof(string)));
            dt.Columns.Add(new DataColumn("Price", typeof(string)));
            dt.Columns.Add(new DataColumn("Quantity", typeof(string)));

            return dt;
        }
        #endregion
    }
}
