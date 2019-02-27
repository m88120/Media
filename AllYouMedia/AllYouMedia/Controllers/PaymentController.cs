using AllYouMedia.Models;
using BusinessEntity.ConcreateEntity;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AllYouMedia.Controllers
{
    [AllowAnonymous]
    public class PaymentController : Controller
    {
        #region PaymentProcessPP()
        public ActionResult PaymentProcessPP(string uid = "", string fuid = "", string fee = "", string CUID = "", string R_uid = "", string R_fuid = "")
        {
            decimal payAmount = 0;
            decimal EventPayAmount = 0;
            if (CUID == "")
            {
                payAmount = Convert.ToDecimal(fee);
            }
            else if (CUID != "")
            {
                EventPayAmount = Convert.ToDecimal(fee);
            }
            if (payAmount > 0 && uid != "")
            {
                string currencyCode = ConfigurationSettings.AppSettings["PaypalCurrencyCode"].ToString();

                PayPalDataEntity pd = new PayPalDataEntity();
                DataTable paymentItems = pd.PaymentItems();
                decimal _shipping = 0;
                decimal _handling = 0;
                decimal _subTotal = payAmount;
                decimal _netTotal = _handling + _subTotal;

                string _websiteUrl = Request.Url.Scheme + "://" + Request.Url.Authority;
                string _cancelUrl = _websiteUrl + "/Payment/PaymentFailed";
                string _returnUrl = _websiteUrl + "/Payment/PaymentSuccessPP?uid=" + uid;

                paymentItems.Rows.Add("Upgrade", currencyCode, _subTotal.ToString(), "1");

                string message = string.Empty;
                string redirectLink = string.Empty;

                if (pd.PaymentCreatingWithPayPal(paymentItems, _shipping.ToString(new System.Globalization.CultureInfo("en-US")), _handling.ToString(new System.Globalization.CultureInfo("en-US")), _subTotal.ToString(new System.Globalization.CultureInfo("en-US")), _netTotal.ToString(new System.Globalization.CultureInfo("en-US")), currencyCode, _cancelUrl, _returnUrl, "Upgrade Amount ", out message, out redirectLink))
                {
                    Response.Redirect(redirectLink);
                }
                else
                {
                    Response.Write(message);
                    Response.End();
                }
            }
            else if (payAmount > 0 && fuid != "")
            {
                string currencyCode = ConfigurationSettings.AppSettings["PaypalCurrencyCode"].ToString();

                PayPalDataEntity pd = new PayPalDataEntity();
                DataTable paymentItems = pd.PaymentItems();

                decimal _shipping = 0;
                decimal _handling = 0;
                decimal _subTotal = payAmount;
                decimal _netTotal = _handling + _subTotal;

                string _websiteUrl = Request.Url.Scheme + "://" + Request.Url.Authority;
                string _cancelUrl = _websiteUrl + "/Payment/PaymentFailed";
                string _returnUrl = _websiteUrl + "/Payment/PaymentSuccessPP?fuid=" + fuid;

                paymentItems.Rows.Add("Upgrade", currencyCode, _subTotal.ToString(), "1");

                string message = string.Empty;
                string redirectLink = string.Empty;

                if (pd.PaymentCreatingWithPayPal(paymentItems, _shipping.ToString(new System.Globalization.CultureInfo("en-US")), _handling.ToString(new System.Globalization.CultureInfo("en-US")), _subTotal.ToString(new System.Globalization.CultureInfo("en-US")), _netTotal.ToString(new System.Globalization.CultureInfo("en-US")), currencyCode, _cancelUrl, _returnUrl, "Upgrade Amount ", out message, out redirectLink))
                {
                    Response.Redirect(redirectLink);
                }
                else
                {
                    Response.Write(message);
                    Response.End();
                }
            }
            else if (EventPayAmount > 0 && CUID != "")
            {
                string currencyCode = ConfigurationSettings.AppSettings["PaypalCurrencyCode"].ToString();

                PayPalDataEntity pd = new PayPalDataEntity();
                DataTable paymentItems = pd.PaymentItems();

                decimal _shipping = 0;
                decimal _handling = 0;
                decimal _subTotal = Convert.ToDecimal(EventPayAmount);
                decimal _netTotal = _handling + _subTotal;

                string _websiteUrl = Request.Url.Scheme + "://" + Request.Url.Authority;
                string _cancelUrl = _websiteUrl + "/Payment/PaymentFailed";
                string _returnUrl = _websiteUrl + "/Payment/PaymentSuccessPP?CUID=" + CUID;

                paymentItems.Rows.Add("Purchase Event Ticket", currencyCode, _subTotal.ToString(), "1");

                string message = string.Empty;
                string redirectLink = string.Empty;

                if (pd.PaymentCreatingWithPayPal(paymentItems, _shipping.ToString(new System.Globalization.CultureInfo("en-US")), _handling.ToString(new System.Globalization.CultureInfo("en-US")), _subTotal.ToString(new System.Globalization.CultureInfo("en-US")), _netTotal.ToString(new System.Globalization.CultureInfo("en-US")), currencyCode, _cancelUrl, _returnUrl, "Event Ticket Amount ", out message, out redirectLink))
                {
                    Response.Redirect(redirectLink);
                }
                else
                {
                    Response.Write(message);
                    Response.End();
                }
            }
            else if (payAmount > 0 && R_uid != "")
            {
                string currencyCode = ConfigurationSettings.AppSettings["PaypalCurrencyCode"].ToString();

                PayPalDataEntity pd = new PayPalDataEntity();
                DataTable paymentItems = pd.PaymentItems();

                decimal _shipping = 0;
                decimal _handling = 0;
                decimal _subTotal = Convert.ToDecimal(payAmount);
                decimal _netTotal = _handling + _subTotal;

                string _websiteUrl = Request.Url.Scheme + "://" + Request.Url.Authority;
                string _cancelUrl = _websiteUrl + "/Payment/PaymentFailed";
                string _returnUrl = _websiteUrl + "/Payment/PaymentSuccessPP?R_uid=" + R_uid;

                paymentItems.Rows.Add("Purchase Event Ticket", currencyCode, _subTotal.ToString(), "1");

                string message = string.Empty;
                string redirectLink = string.Empty;

                if (pd.PaymentCreatingWithPayPal(paymentItems, _shipping.ToString(new System.Globalization.CultureInfo("en-US")), _handling.ToString(new System.Globalization.CultureInfo("en-US")), _subTotal.ToString(new System.Globalization.CultureInfo("en-US")), _netTotal.ToString(new System.Globalization.CultureInfo("en-US")), currencyCode, _cancelUrl, _returnUrl, "Registration", out message, out redirectLink))
                {
                    Response.Redirect(redirectLink);
                }
                else
                {
                    Response.Write(message);
                    Response.End();
                }
            }
            else if (payAmount > 0 && R_fuid != "")
            {
                string currencyCode = ConfigurationSettings.AppSettings["PaypalCurrencyCode"].ToString();

                PayPalDataEntity pd = new PayPalDataEntity();
                DataTable paymentItems = pd.PaymentItems();

                decimal _shipping = 0;
                decimal _handling = 0;
                decimal _subTotal = Convert.ToDecimal(payAmount);
                decimal _netTotal = _handling + _subTotal;

                string _websiteUrl = Request.Url.Scheme + "://" + Request.Url.Authority;
                string _cancelUrl = _websiteUrl + "/Payment/PaymentFailed";
                string _returnUrl = _websiteUrl + "/Payment/PaymentSuccessPP?R_fuid=" + R_fuid;

                paymentItems.Rows.Add("Purchase Event Ticket", currencyCode, _subTotal.ToString(), "1");

                string message = string.Empty;
                string redirectLink = string.Empty;

                if (pd.PaymentCreatingWithPayPal(paymentItems, _shipping.ToString(new System.Globalization.CultureInfo("en-US")), _handling.ToString(new System.Globalization.CultureInfo("en-US")), _subTotal.ToString(new System.Globalization.CultureInfo("en-US")), _netTotal.ToString(new System.Globalization.CultureInfo("en-US")), currencyCode, _cancelUrl, _returnUrl, "Registration", out message, out redirectLink))
                {
                    Response.Redirect(redirectLink);
                }
                else
                {
                    Response.Write(message);
                    Response.End();
                }
            }
            else
            {
                return RedirectToAction("PaymentFailed", "Payment");
            }

            return View();
        }
        #endregion

        #region PaymentFailed()
        public ActionResult PaymentFailed()
        {
            return View();
        }
        #endregion

        #region PaymentSuccessPP()
        public ActionResult PaymentSuccessPP(string uid = "", string fuid = "", string CUID = "", string R_uid = "", string R_fuid = "")
        {
            object message = string.Empty;
            DataTable dt = new PayPalDataEntity().PaymentExecuteWithPayPal(out message);

            if (dt.Rows.Count > 0)
            {
                message = string.Empty;
                string _date = Convert.ToDateTime(dt.Rows[0]["Date"]).ToString();

                if (uid != "")
                {
                    this.SetPaymentDone(new RoleDataEntity().Role_InsertNewUserWithPayment(uid, "P", dt.Rows[0]["TransactionID"].ToString(), Convert.ToDecimal(dt.Rows[0]["TotalAmount"]), _date, out message), message);
                }
                else if (fuid != "")
                {
                    this.SetPaymentDone(new RoleDataEntity().Role_InsertNewMediaPromoterWithPayment(fuid, "P", dt.Rows[0]["TransactionID"].ToString(), Convert.ToDecimal(dt.Rows[0]["TotalAmount"]), _date, out message), message);
                }
                else if (R_uid != "")
                {
                    this.SetPaymentDone(new RoleDataEntity().Role_InsertNewUserWithPayment(R_uid, "P", dt.Rows[0]["TransactionID"].ToString(), Convert.ToDecimal(dt.Rows[0]["TotalAmount"]), _date, out message), message);
                }
                else if (R_fuid != "")
                {
                    this.SetPaymentDone(new RoleDataEntity().Role_InsertNewMediaPromoterWithPayment(R_fuid, "P", dt.Rows[0]["TransactionID"].ToString(), Convert.ToDecimal(dt.Rows[0]["TotalAmount"]), _date, out message), message);
                }
                else if (CUID != "")
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        this.SetPaymentDone(new EventDataEntity().Event_Insert_EventUser(User.Identity.Name, CUID, "P", dt.Rows[0]["TransactionID"].ToString(), Convert.ToDecimal(dt.Rows[0]["TotalAmount"]), _date, "Y", "", out message), message);
                    }
                    else
                    {
                        this.SetEventPaymentDone(new EventDataEntity().Event_Insert_EventVisitorAfterPayment(CUID, "P", dt.Rows[0]["TransactionID"].ToString(), Convert.ToDouble(dt.Rows[0]["TotalAmount"]), _date, out message), message);
                    }
                }
            }
            else
            {
                TempData["Message1"] = "<div class='alert alert-danger'>" + message + "</div>";
            }
            return View();
        }
        #endregion

        #region SetPaymentDone()
        public void SetPaymentDone(DataTable dt, object message)
        {
            if (dt.Rows.Count > 0)
            {
                _CodeClass.SendMail(dt.Rows[0]["MAIL"].ToString(), dt.Rows[0]["MAIL SUBJECT"].ToString(), dt.Rows[0]["MAIL BODY"].ToString());

                TempData["Message1"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
            }
            else
            {
                TempData["Message1"] = "<div class='alert alert-danger'>" + message + "</div>";
            }
        }
        public void SetPaymentDone(DataTable dt, object message, string _returnUrl)
        {
            if (dt.Rows.Count > 0)
            {
                _CodeClass.SendMail(dt.Rows[0]["MAIL"].ToString(), dt.Rows[0]["MAIL SUBJECT"].ToString(), dt.Rows[0]["MAIL BODY"].ToString());

                TempData["Message1"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                Session["Message1"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                Response.Redirect(_returnUrl);
            }
            else
            {
                TempData["Message1"] = "<div class='alert alert-danger'>" + message + "</div>";
                Response.Redirect("~/Payment/PaymentFailed");
            }
        }

        public void SetEventPaymentDone(DataTable dt, object message)
        {
            if (dt.Rows.Count > 0)
            {
                string _emailaddress = System.Configuration.ConfigurationManager.AppSettings["mailaddress_admin"].ToString();
                string EUID = dt.Rows[0]["EUID"].ToString();
                _CodeClass.SendMail(dt.Rows[0]["MAIL"].ToString(), dt.Rows[0]["MAIL SUBJECT"].ToString(), dt.Rows[0]["MAIL BODY"].ToString());              

                TempData["Message1"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";

                Response.Redirect("~/Event/EventTicketInvoice?EUID=" + EUID);
            }
            else
            {
                TempData["Message1"] = "<div class='alert alert-danger'>" + message + "</div>";
                Response.Redirect("~/Payment/PaymentFailed");
            }
        }

        public void SetShopPaymentDone(DataTable dt, object message, string _returnUrl)
        {
            if (dt.Rows.Count > 0)
            {
                string _emailaddress = System.Configuration.ConfigurationManager.AppSettings["mailaddress_admin"].ToString();
                string UID = dt.Rows[0]["UID"].ToString();
                _CodeClass.SendMail(dt.Rows[0]["MAIL"].ToString(), dt.Rows[0]["MAIL SUBJECT"].ToString(), dt.Rows[0]["MAIL BODY"].ToString());
                _CodeClass.SendMail(_emailaddress, dt.Rows[0]["ADMIN MAIL SUBJECT"].ToString(), dt.Rows[0]["ADMIN MAIL BODY"].ToString());

                TempData["Message1"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                if (User.Identity.IsAuthenticated)
                {
                    Response.Redirect(_returnUrl);
                }
                else
                {
                    Response.Redirect("~/Product/ShoppingInvoice?SUID=" + UID);
                }
            }
            else
            {
                TempData["Message1"] = "<div class='alert alert-danger'>" + message + "</div>";
                Response.Redirect("~/Payment/PaymentFailed");
            }
        }

        #endregion

        #region Checkout
        public ActionResult Checkout(string CUID = "", string Fee = "", string Amount = "")
        {
            CheckoutModel model = new CheckoutModel();
            if (Session["hdnReff"] != null)
            {
                model.hdnReff = Session["hdnReff"].ToString();
                Session["hdnReff"] = Session["hdnReff"].ToString();
            }
            else
            {
                model.hdnReff = "";
                Session["hdnReff"] = null;
            }
            ViewBag.PaidUser = true;
            ViewBag.UnPaidUser = true;
            if (CUID != "")
            {
                Double Fees = Convert.ToDouble(Fee);
                if (Fees > 0)
                {
                    model.hdnUID = CUID;
                    model.hdnPUID = "";
                    model.Fee = _CodeClass.GetCurrency(Fees);
                    model.hdnFee = Fees.ToString();
                    model.hdnAmount = "";
                }
                else
                {
                    return RedirectToAction("Index", "Event");
                }
            }
            else if (CUID == "" && Amount != "")
            {
                if (User.Identity.IsAuthenticated)
                {
                    Double Amounts = new ShoppingDataEntity().GetShoppingNetAmountPaidUser(User.Identity.Name);
                    if (Amounts > 0)
                    {
                        model.hdnPUID = "";
                        model.hdnUID = "";
                        model.Fee = _CodeClass.GetCurrency(Amounts);
                        model.hdnAmount = Amounts.ToString();
                        model.hdnFee = "";
                    }
                    else
                    {
                        return RedirectToAction("Index", "Product");
                    }
                } 
                else
                {
                    model.hdnPUID = "";
                    model.hdnUID = "";
                    model.Fee = _CodeClass.GetCurrency(Amount);
                    model.hdnAmount = Amount.ToString();
                    model.hdnFee = "";
                    ViewBag.UnPaidUser = false;
                    ViewBag.PaidUser = true;
                }               
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            if (User.Identity.IsAuthenticated)
            {
                DataTable dt = new RoleDataEntity().Role_Select_UserDetail(User.Identity.Name);
                if (dt.Rows.Count > 0)
                {
                    model.Name = dt.Rows[0]["Name"].ToString();
                    model.Email = dt.Rows[0]["Email"].ToString();
                    model.City = dt.Rows[0]["City"].ToString();
                    model.Address = dt.Rows[0]["Address"].ToString();
                    model.Zip = dt.Rows[0]["Zip Code"].ToString();
                    model.Country = dt.Rows[0]["Country1"].ToString();
                    model.State = dt.Rows[0]["State1"].ToString();
                }
                var Country = PopulateCountry();
                ViewBag.PaidUser = false;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Checkout(CheckoutModel model, FormCollection form)
        {
            var PaymentOption = Request.Form["PaymentOption"];
            var hdnFee = Request.Form["hdnFee"];
            var hdnAmount = Request.Form["hdnAmount"];
            var hdnUID = Request.Form["hdnUID"];
            var hdnPUID = Request.Form["hdnPUID"];
            if(model.hdnReff == null)
            {
                model.hdnReff = "";
            }
            if (User.Identity.IsAuthenticated)
            {
                if (hdnFee != "")
                {
                    return RedirectToAction("PaymentProcessPZ", "Payment", new { @CUID = hdnUID, @fee = hdnFee });
                }
                else if (hdnAmount != "")
                {
                    return RedirectToAction("PaymentProcessPZ", "Payment", new { @Amount = hdnAmount });
                }
            }
            else if (model.LoginName != null && model.Password != null)
            {
                object message = string.Empty;
                if (new RoleDataEntity().Role_Select_UserLogin(model.LoginName, model.Password, out message) > 0)
                {
                    FormsAuthentication.SetAuthCookie(model.LoginName, false);

                    if (hdnFee != "")
                    {
                        return RedirectToAction("PaymentProcessPZ", "Payment", new { @CUID = hdnUID, @fee = hdnFee });
                    }
                    else if (hdnAmount != "")
                    {
                        return RedirectToAction("PaymentProcessPZ", "Payment", new { @Amount = hdnAmount });
                    }
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger' role='alert'>" + message.ToString() + "</div>";
                }
            }
            else if (model.Name != null && model.Email != null && model.Zip != null)
            {
                string UID = "";
                if (hdnFee != "")
                {
                    DataTable dt = new EventDataEntity().Event_Insert_EventVisitor_Temp(model.Name, model.Country, model.State, model.City, model.Zip, model.Address, model.Email, Convert.ToDecimal(hdnFee), hdnUID, model.hdnReff);
                    if (dt.Rows.Count > 0)
                    {
                        UID = dt.Rows[0]["UID"].ToString();
                        return RedirectToAction("PaymentProcessPZ", "Payment", new { @CUID = UID, @fee = hdnFee });
                    }
                }
                else if (hdnAmount != "" && hdnPUID != "")
                {
                    TempData["Message"] = "<div class='alert alert-danger'> Please Login First!</div>";
                }
            }
            return RedirectToAction("Index", "Product");
        }
        #endregion

        #region PopulateCountry()
        public List<SelectListItem> PopulateCountry()
        {
            DataTable dtCountry = new MasterDataEntity().Master_Select_CountryDDL();
            List<SelectListItem> ddlCountry = new List<SelectListItem>();
            ddlCountry.Add(new SelectListItem { Text = "--Select Country--", Value = "" });
            foreach (DataRow dr in dtCountry.Rows)
            {
                ddlCountry.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                ViewData["Country"] = ddlCountry;
            }
            return ddlCountry;
        }
        #endregion

        #region PopulateState(Country_UID)
        public List<SelectListItem> PopulateState(string Country_UID)
        {
            List<SelectListItem> ddlState = new List<SelectListItem>();
            ddlState.Add(new SelectListItem { Text = "--Select State--", Value = "" });
            if (Country_UID.ToString() != "")
            {
                DataTable dtState = new MasterDataEntity().Master_Select_StateDDL(Country_UID.ToString());
                if (dtState.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtState.Rows)
                    {
                        ddlState.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                    }
                }
            }
            return ddlState;
        }
        #endregion

        #region PaymentProcessPZ()
        public ActionResult PaymentProcessPZ(string U_uid = "", string U_fuid = "", string fee = "0", string CUID = "", string R_uid = "", string R_fuid = "", string PUID = "", string Amount = "0")
        {
            PaymentProcessModel model = new PaymentProcessModel();
            if (Amount != "")
            {
                model.Fee = Amount;
                model.hdnFee = Convert.ToDecimal(Amount);
            }
            else if (CUID == "")
            {
                model.Fee = fee;
                model.hdnFee = Convert.ToDecimal(fee);
            }
            else if (CUID != "")
            {
                model.Fee = fee;
                model.hdnFee = Convert.ToDecimal(fee);
            }
            if (null != Session["hdnReff"])
            { Session["hdnReff"] = Session["hdnReff"].ToString(); }
            else { Session["hdnReff"] = ""; }

            Session["UID"] = "";
            Session["FUID"] = "";
            Session["R_UID"] = "";
            Session["R_FUID"] = "";
            Session["CUID"] = "";
            Session["PUID"] = "";
            if (U_uid != "") { Session["U_UID"] = U_uid; model.hdnUID = U_uid; }
            else if (U_fuid != "") { Session["U_FUID"] = U_fuid; model.hdnUID = U_fuid; }
            else if (R_uid != "") { Session["R_UID"] = R_uid; model.hdnUID = R_uid; }
            else if (R_fuid != "") { Session["R_FUID"] = R_fuid; model.hdnUID = R_fuid; }
            else if (CUID != "") { Session["CUID"] = CUID; model.hdnUID = CUID; }
            else if (PUID != "") { Session["PUID"] = PUID; model.hdnUID = PUID; }
            else if (Amount != "") { model.hdnFee = Convert.ToDecimal(Amount); }
            else { TempData["Message1"] = "<div class='alert alert-danger'>Something Went Wrong! Try Again</div>"; Response.Redirect("~/Payment/PaymentFailed"); }
            return View(model);
        }

        [HttpPost]
        public ActionResult PaymentProcessPZ(PaymentProcessModel model)
        {
            string payAmount = Convert.ToString(model.hdnFee);
            string uid = model.hdnUID;

            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority;

            string _returnUrl = baseUrl + "/Payment/PaymentSuccessPZ";

            _returnUrl = _returnUrl + "?uid=" + uid;

            string message = string.Empty;
            object message1 = string.Empty;
            string transactionID = string.Empty;
            string hdnReff = "";
            if (Session["hdnReff"].ToString() != null)
            { hdnReff = Session["hdnReff"].ToString(); }
            else { hdnReff = ""; }
            PayeezyDataEntity pz = new PayeezyDataEntity();

            if (pz.PaymentCreating(model.CardNumber, model.CardHoldersName, payAmount, model.ExpiryDate, model.CVV, out message))
            {
                string _date = DateTime.Now.ToString();
                if (Session["U_UID"].ToString() != "")
                {
                    this.SetPaymentDone(new RoleDataEntity().Role_InsertUpgradeUserWithPayment(Session["U_UID"].ToString(), "P", "123456789", Convert.ToDecimal(payAmount), _date, out message1), message, _returnUrl);
                }
                else if (Session["U_FUID"].ToString() != "")
                {
                    this.SetPaymentDone(new RoleDataEntity().Role_InsertUpgradeMediaPromoterWithPayment(Session["U_FUID"].ToString(), "P", "123456789", Convert.ToDecimal(payAmount), _date, out message1), message, _returnUrl);

                }
                else if (Session["R_UID"].ToString() != "")
                {
                    this.SetPaymentDone(new RoleDataEntity().Role_InsertNewUserWithPayment(Session["R_UID"].ToString(), "P", "123456789", Convert.ToDecimal(payAmount), _date, out message1), message, _returnUrl);

                }
                else if (Session["R_FUID"].ToString() != "")
                {
                    this.SetPaymentDone(new RoleDataEntity().Role_InsertNewMediaPromoterWithPayment(Session["R_FUID"].ToString(), "P", "123456789", Convert.ToDecimal(payAmount), _date, out message1), message, _returnUrl);

                }
                else if (Session["CUID"].ToString() != "")
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        this.SetEventPaymentDone(new EventDataEntity().Event_Insert_EventUser(User.Identity.Name, Session["CUID"].ToString(), "P", "123456789", Convert.ToDecimal(payAmount), _date, "Y", hdnReff, out message1), message);
                    }
                    else
                    {
                        this.SetEventPaymentDone(new EventDataEntity().Event_Insert_EventVisitorAfterPayment(Session["CUID"].ToString(), "P", "123456789", Convert.ToDouble(payAmount), _date, out message1), message);
                    }
                }
                else
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        this.SetShopPaymentDone(new ShoppingDataEntity().Shopping_Insert_PaymentAuthorizeNet(User.Identity.Name, "P", "123456789", Convert.ToDouble(payAmount), _date, "S", out message1), message, _returnUrl);
                    }
                    else if (Session["PUID"].ToString() != "")
                    {
                        //this.SetShopPaymentDone(new ShoppingDataEntity().Shopping_Insert_GuestPayment(Session["PUID"].ToString(), "P", "123456789", Convert.ToDouble(payAmount), _date, "S", out message1), message, _returnUrl);
                        TempData["Message1"] = "<div class='alert alert-danger'> Please Login First!</div>";
                    }
                }
            }
            else
            {
                TempData["Message1"] = "<div class='alert alert-danger'>" + message + "</div>";
                Response.Redirect("~/Payment/PaymentFailed");
            }
            return View(model);
        }
        #endregion

        #region PaymentSuccessPZ()
        public ActionResult PaymentSuccessPZ(string uid = "")
        {
            if (TempData["Message1"].ToString() != "")
            {
                TempData["Message1"] = TempData["Message1"].ToString();
                Session["Message1"] = "";
            }
            else
            {
                TempData["Message1"] = Session["Message1"].ToString();
            }
            return View();
        }
        #endregion
    }
}