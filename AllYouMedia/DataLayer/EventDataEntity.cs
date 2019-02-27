using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;

namespace BusinessEntity.ConcreateEntity
{
    public class EventDataEntity
    {
        private DataEntity _de;

        #region EventDataEntity
        public EventDataEntity()
        {
            _de = new DataEntity();
        }
        #endregion

        #region Event_Select_EventDDL
        public DataTable Event_Select_EventDDL()
        {
            return _de.ExecuteDataTable("Event_Select_EventDDL");
        }
        #endregion

        #region GetEventCount
        public object GetTotalEventCount()
        {
            return _de.ExecuteScalarFunction("SELECT dbo.GetTotalEventCount()");
        }
        public object GetSelectedEventCount(string Event_Performer_Category)
        {
            _de.ParaNameArray("@Event_Performer_Category");
            return _de.ExecuteScalarFunction("SELECT dbo.GetSelectedEventCount(@Event_Performer_Category)", Event_Performer_Category);
        }
        #endregion

        #region Event_Select_EventDetailWithUID
        public DataTable Event_Select_EventDetailWithUID(string Event_UID)
        {
            _de.ParaNameArray("@Event_UID");
            return _de.ExecuteDataTable("Event_Select_EventDetailWithUID", Event_UID);
        }
        public DataTable Event_Select_EventMainDetailWithUID(string Event_UID)
        {
            _de.ParaNameArray("@Event_UID");
            return _de.ExecuteDataTable("Event_Select_EventMainDetailWithUID", Event_UID);
        }
        public DataTable Event_Select_EventDayDetailWithUID(string Event_UID)
        {
            _de.ParaNameArray("@Event_UID");
            return _de.ExecuteDataTable("Event_Select_EventDayDetailWithUID", Event_UID);
        }

        public DataTable Event_Select_EventDetailOnRegister(string Event_UID)
        {
            _de.ParaNameArray("@Event_UID");
            return _de.ExecuteDataTable("Event_Select_EventDetailOnRegister", Event_UID);
        }
        #endregion

        #region Event_Select_EventList
        public DataTable Event_Select_EventList()
        {

            return _de.ExecuteDataTable("Event_Select_EventList");
        }
        public DataTable Event_Select_EventListDU()
        {

            return _de.ExecuteDataTable("Event_Select_EventListDU");
        }
        public DataTable Event_Select_EventListDD()
        {

            return _de.ExecuteDataTable("Event_Select_EventListDD");
        }
        public DataTable Event_Select_EventListAtoZ()
        {

            return _de.ExecuteDataTable("Event_Select_EventListAtoZ");
        }
        public DataTable Event_Select_EventListZtoA()
        {

            return _de.ExecuteDataTable("Event_Select_EventListZtoA");
        }
        #endregion

        #region Event_Select_EventList
        public DataTable Event_Select_EventList(string Event_UID)
        {
            _de.ParaNameArray("@Event_UID");
            return _de.ExecuteDataTable("Event_Select_EventList", Event_UID);
        }
        #endregion

        #region Event_Select_Cart
        public DataTable Event_Select_Cart(DataTable dt, string Event_CartItem_UID)
        {
            DataSet ds = new DataSet();
            ds.Merge(dt);

            _de.ParaNameArray("@XML", "@Event_CartItem_UID");
            return _de.ExecuteDataTable("Event_Select_Cart", ds.GetXml(), Event_CartItem_UID);
        }
        #endregion

        #region Event_InsertCartItem
        public DataTable Event_InsertCartItem(string Event_UID, int Event_Qty)
        {
            _de.ParaNameArray("Event_UID", "Event_Qty");
            return _de.ExecuteDataTable("Event_InsertCartItem", Event_UID, Event_Qty);
        }
        #endregion

        #region Event_UpdateCartItem
        public DataTable Event_UpdateCartItem(DataTable dt, string EventCart_UID, int Event_Qty)
        {                    
            _de.ParaNameArray("@EventCart_UID", "@Event_Qty");
            return _de.ExecuteDataTable("Event_UpdateCartItem", EventCart_UID, Event_Qty);
        }
        #endregion

        #region GetEventNetAmount
        public double GetEventNetAmount(string UID)
        {
            _de.ParaNameArray("@UID");
            return Convert.ToDouble(_de.ExecuteScalarFunction("SELECT dbo.GetEventNetAmount(@UID)", UID));
        }
        #endregion

        #region Event_Insert_EventVisitor()
        public DataTable Event_Insert_EventVisitor(string Event_Cart_Visitor_FName, string Event_Cart_Visitor_LName, string Event_Cart_Visitor_Country, string Event_Cart_Visitor_State, string Event_Cart_Visitor_City, string Event_Cart_Visitor_ZipCode, string Event_Cart_Visitor_Address, string Event_Cart_Visitor_Email, string Event_Cart_Visitor_Event_UID, string Event_Cart_PaymentGateway, string Event_Cart_Visitor_TransactionID, Double Event_Cart_PaidAmount, string Event_Cart_PaidDate, out object message)
        {
            _de.ParaNameArray("@Event_Cart_Visitor_FName", "@Event_Cart_Visitor_LName", "@Event_Cart_Visitor_Country", "@Event_Cart_Visitor_State", "@Event_Cart_Visitor_City", "@Event_Cart_Visitor_ZipCode", "@Event_Cart_Visitor_Address", "@Event_Cart_Visitor_Email", "@Event_Cart_Visitor_Event_UID", "@Event_Cart_PaymentGateway", "@Event_Cart_Visitor_TransactionID", "@Event_Cart_PaidAmount", "@Event_Cart_PaidDate");
            return _de.ExecuteDataTable("Event_Insert_EventVisitor_AfterPayment", "@Message", out message, Event_Cart_Visitor_FName, Event_Cart_Visitor_LName, Event_Cart_Visitor_Country, Event_Cart_Visitor_State, Event_Cart_Visitor_City, Event_Cart_Visitor_ZipCode, Event_Cart_Visitor_Address, Event_Cart_Visitor_Email, Event_Cart_Visitor_Event_UID, Event_Cart_PaymentGateway, Event_Cart_Visitor_TransactionID, Event_Cart_PaidAmount, Event_Cart_PaidDate);
        }
        #endregion

        #region Event_Insert_EventUser()
        public DataTable Event_Insert_EventUser(string Reg_User_LoginName, string Event_Cart_User_Event_UID, string Shopping_PurchasePayment_Gateway, string Shopping_PurchasePayment_TransactionID, Decimal PaidAmount, string Shopping_PurchasePayment_Date, string Shopping_PurchasePayment_Status, string Event_Cart_RefferedBy, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Event_Cart_User_Event_UID", "@Shopping_PurchasePayment_Gateway", "@Shopping_PurchasePayment_TransactionID", "@PaidAmount", "@Shopping_PurchasePayment_Date", "@Shopping_PurchasePayment_Status", "@Event_Cart_RefferedBy");
            return _de.ExecuteDataTable("Event_Insert_EventUser_AfterPayment", "@Message", out message, Reg_User_LoginName, Event_Cart_User_Event_UID, Shopping_PurchasePayment_Gateway, Shopping_PurchasePayment_TransactionID, PaidAmount, Shopping_PurchasePayment_Date, Shopping_PurchasePayment_Status, Event_Cart_RefferedBy);
        }
        #endregion

        #region Event_Select_CategoryDDL
        public DataTable Event_Select_CategoryDDL()
        {
            return _de.ExecuteDataTable("Event_Select_CategoryDDL");
        }
        #endregion

        #region Event_Select_EventList
        public DataTable Event_Select_SelectedEventList(string Event_Performer_Category)
        {
            _de.ParaNameArray("@Event_Performer_Category");
            return _de.ExecuteDataTable("Event_Select_SelectedEventList", Event_Performer_Category);
        }
        #endregion

        #region Event_Insert_EventVisitor_Temp()
        public DataTable Event_Insert_EventVisitor_Temp(string Event_Cart_Visitor_Name, string Event_Cart_Visitor_Country, string Event_Cart_Visitor_State, string Event_Cart_Visitor_City, string Event_Cart_Visitor_ZipCode, string Event_Cart_Visitor_Address, string Event_Cart_Visitor_Email, Decimal Event_Cart_Visitor_Fee, string Event_Cart_Visitor_Event_UID, string Event_Cart_Visitor_RefferedBy)
        {
            _de.ParaNameArray("@Event_Cart_Visitor_Name", "@Event_Cart_Visitor_Country", "@Event_Cart_Visitor_State", "@Event_Cart_Visitor_City", "@Event_Cart_Visitor_ZipCode", "@Event_Cart_Visitor_Address", "@Event_Cart_Visitor_Email", "@Event_Cart_Visitor_Fee", "@Event_Cart_Visitor_Event_UID", "@Event_Cart_Visitor_RefferedBy");
            return _de.ExecuteDataTable("Event_Insert_EventVisitor_Temp", Event_Cart_Visitor_Name, Event_Cart_Visitor_Country, Event_Cart_Visitor_State, Event_Cart_Visitor_City, Event_Cart_Visitor_ZipCode, Event_Cart_Visitor_Address, Event_Cart_Visitor_Email, Event_Cart_Visitor_Fee, Event_Cart_Visitor_Event_UID, Event_Cart_Visitor_RefferedBy);
        }
        #endregion

        #region Event_Insert_EventVisitorAfterPayment()
        public DataTable Event_Insert_EventVisitorAfterPayment(string Event_Cart_Temp_UID, string Shopping_PurchasePayment_Gateway, string Shopping_PurchasePayment_TransactionID, double PaidAmount, string Shopping_PurchasePayment_Date, out object message)
        {
            _de.ParaNameArray("@Event_Cart_Temp_UID", "@Shopping_PurchasePayment_Gateway", "@Shopping_PurchasePayment_TransactionID", "@PaidAmount", "@Shopping_PurchasePayment_Date");
            return _de.ExecuteDataTable("Event_Insert_EventVisitor_Payment", "@Message", out message, Event_Cart_Temp_UID, Shopping_PurchasePayment_Gateway, Shopping_PurchasePayment_TransactionID, PaidAmount, Shopping_PurchasePayment_Date);
        }
        #endregion

        #region Event_Insert_VisitorPerformerDetail()
        public int Event_Insert_VisitorPerformerDetail(string Event_Performer_Temp_Event_BUID, string Event_Performer_Temp_Name, string Event_Performer_Temp_Category, string Event_Performer_Temp_TimeFrom, string Event_Performer_Temp_TimeTo, string Event_Performer_Temp_Email, string Event_Performer_Temp_Mobile, string Event_Performer_Temp_Message, out object message)
        {
            _de.ParaNameArray("@Event_Performer_Temp_Event_BUID", "@Event_Performer_Temp_Name", "@Event_Performer_Temp_Category", "@Event_Performer_Temp_TimeFrom", "@Event_Performer_Temp_TimeTo", "@Event_Performer_Temp_Email", "@Event_Performer_Temp_Mobile", "@Event_Performer_Temp_Message");
            return _de.ExecuteNonQuery("Event_Insert_VisitorPerformerDetail", "@Message", out message, Event_Performer_Temp_Event_BUID, Event_Performer_Temp_Name, Event_Performer_Temp_Category, Event_Performer_Temp_TimeFrom, Event_Performer_Temp_TimeTo, Event_Performer_Temp_Email, Event_Performer_Temp_Mobile, Event_Performer_Temp_Message);
        }
        #endregion

        #region Event_Delete_CartItem
        public int Event_Delete_CartItem(DataTable dt, string Event_CartItem_UID, out object message)
        {
            int attactedRow = 0;
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                if (dr["Cart_UID"].ToString() == Event_CartItem_UID)
                    dr.Delete();
                attactedRow = 1;
            }
            dt.AcceptChanges();

            _de.ParaNameArray("@Event_CartItem_UID");
            attactedRow = _de.ExecuteNonQuery("Event_Delete_CartItem", Event_CartItem_UID);

            if (attactedRow > 0)
            {
                message = "Product has been removed from your shopping cart!";
            }
            else
            {
                message = "Product already deleted. Please re-open this page!";
            }

            return attactedRow;
        }
        #endregion


        
    }
}
