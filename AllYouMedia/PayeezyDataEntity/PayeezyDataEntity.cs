using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Payeezy;
using System.Configuration;
using System.Net;
using System.IO;

namespace DataLayer
{
    public class PayeezyDataEntity
    {
        private string _APIExactID = ConfigurationSettings.AppSettings["PayeezyAPIExactID"].ToString();
        private string _APIPassword = ConfigurationSettings.AppSettings["PayeezyAPIPassword"].ToString();      

        #region PaymentCreating()
        public bool PaymentCreating(string card_Number, string cardHoldersName, string amount, string expiry_Date, string cvv, out string message)
        {
            return PaymentCreating(card_Number, cardHoldersName, amount, expiry_Date, cvv, "", out message);
        }
        public bool PaymentCreating(string card_Number, string cardHoldersName, string amount, string expiry_Date, string cvv, string currency, out string message)
        {
            bool isSuccess = false;
            message = string.Empty;

            try
            {
                ServiceSoapClient ws = new ServiceSoapClient();
                Transaction txn = new Transaction();

                txn.ExactID = _APIExactID;
                txn.Password = _APIPassword;

                txn.Transaction_Type = "00";
                txn.Card_Number = card_Number;
                txn.CardHoldersName = cardHoldersName;
                txn.DollarAmount = amount;
                txn.Expiry_Date = expiry_Date;
                txn.CVDCode = cvv;
                if (currency != "") txn.Currency = currency;

                TransactionResult result = ws.SendAndCommit(txn);

                if (result.Transaction_Approved)
                {
                    isSuccess = true;
                    message = "Your transaction has been approved!";
                }
                else
                {
                    message = (result.Bank_Message != "") ? result.Bank_Message : result.EXact_Message;
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (HttpWebResponse error_response = (HttpWebResponse)ex.Response)
                    {
                        using (StreamReader reader = new StreamReader(error_response.GetResponseStream()))
                        {
                            string remote_ex = reader.ReadToEnd();
                            message = remote_ex;
                        }
                    }
                }
                else
                {
                    message = ex.Message;
                }
            }

            return isSuccess;
        }
        #endregion
    }
}
