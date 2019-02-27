using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;

namespace BusinessEntity.ConcreateEntity
{
    public class ShoppingDataEntity
    {
        private DataEntity _de;

        #region ShoppingDataEntity
        public ShoppingDataEntity()
        {
            _de = new DataEntity();
        }
        #endregion

        #region GetShoppingNetAmount
        public double GetShoppingNetAmount(string UID)
        {
            _de.ParaNameArray("@UID");
            return Convert.ToDouble(_de.ExecuteScalarFunction("SELECT dbo.GetShoppingNetAmount(@UID)", UID));
        }
        #endregion

        #region GetShoppingNetAmountPaidUser
        public double GetShoppingNetAmountPaidUser(string Reg_User_LoginName)
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return Convert.ToDouble(_de.ExecuteScalarFunction("SELECT dbo.GetShoppingNetAmountPaidUser(@Reg_User_LoginName)", Reg_User_LoginName));
        }
        #endregion

        #region Shopping_Select_Cart
        public DataTable Shopping_Select_Cart(DataTable dt, string Reg_User_LoginName)
        {
            DataSet ds = new DataSet();
            ds.Merge(dt);

            _de.ParaNameArray("@XML", "@Reg_User_LoginName");
            return _de.ExecuteDataTable("Shopping_Select_Cart", ds.GetXml(), Reg_User_LoginName);
        }
        #endregion

        #region Shopping_Update_ShippingValue
        public int Shopping_Update_ShippingValue(string Reg_User_LoginName, decimal Shopping_Cart_Shipping)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Shopping_Cart_Shipping");
            return _de.ExecuteNonQuery("Shopping_Update_ShippingValue", Reg_User_LoginName, Shopping_Cart_Shipping);
        }
        #endregion

        #region Shopping Cart Method
        public DataTable Shop_GetCartItems(DataTable dt)
        {
            DataSet ds = new DataSet();
            ds.Merge(dt);

            _de.ParaNameArray("@XML", "@Reg_User_LoginName");
            return _de.ExecuteDataTable("Com_Shop_GetCartItems", ds.GetXml(), HttpContext.Current.User.Identity.Name);
        }
        public int Shop_DeleteCartItem(DataTable dt, int index, string Shopping_CartItem_Item_UID, out object message)
        {
            int attactedRow = 0;

            if (dt.Rows.Count > index)
            {
                dt.Rows.RemoveAt(index);

                attactedRow = 1;
            }

            if (HttpContext.Current.User.IsInRole("User"))
            {
                _de.ParaNameArray("@Reg_User_LoginName", "@Shopping_CartItem_Product_UID");
                attactedRow = _de.ExecuteNonQuery("Com_Shop_DeleteCartItem", HttpContext.Current.User.Identity.Name, Shopping_CartItem_Item_UID);
            }

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
        public int Shopping_Set_CartItem(DataTable dt, int index, string Shopping_CartItem_Item_UID, int Shopping_CartItem_Qty, out object message)
        {
            int attactedRow = 0;

            if (dt.Rows.Count > index)
            {
                dt.Rows[index]["Cart_Qty"] = Shopping_CartItem_Qty.ToString();

                attactedRow = 1;
            }

            if (HttpContext.Current.User.IsInRole("User"))
            {
                _de.ParaNameArray("@Reg_User_LoginName", "@Shopping_CartItem_Product_UID", "@Shopping_CartItem_Qty");
                attactedRow = _de.ExecuteNonQuery("Com_Shop_SetCartItem", HttpContext.Current.User.Identity.Name, Shopping_CartItem_Item_UID, Shopping_CartItem_Qty);
            }

            if (attactedRow > 0)
            {
                message = "Product quantity has been updated on your shopping cart!";
            }
            else
            {
                message = "Product already deleted. Please re-open this page!";
            }

            return attactedRow;
        }
        #endregion

        #region Shopping_Select_CartItemCount
        public int Shopping_Select_CartItemCount(string Reg_User_LoginName)
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return Convert.ToInt32(_de.ExecuteScalarFunction("SELECT dbo.Shopping_Select_CartItemCount(@Reg_User_LoginName)", Reg_User_LoginName));
        }
        #endregion

        #region Shopping_Insert_CartItem
        public int Shopping_Insert_CartItem(string Reg_User_LoginName, string Shopping_CartItem_UID, int Shopping_CartItem_Qty, string Shopping_CartItem_Size, string Shopping_CartItem_Color)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Shopping_CartItem_UID", "@Shopping_CartItem_Qty", "@Shopping_CartItem_Size", "@Shopping_CartItem_Color");
            return _de.ExecuteNonQuery("Shopping_Insert_CartItem", Reg_User_LoginName, Shopping_CartItem_UID, Shopping_CartItem_Qty, Shopping_CartItem_Size, Shopping_CartItem_Color);
        }
        #endregion

        #region Shopping_Delete_CartItem
        public int Shopping_Delete_CartItem(DataTable dt, string Reg_User_LoginName, string Shopping_CartItem_Item_UID, out object message)
        {
            int attactedRow = 0;
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                if (dr["Cart_UID"].ToString() == Shopping_CartItem_Item_UID)
                {
                    dr.Delete();
                    attactedRow = 1;
                }
            }
            dt.AcceptChanges();

            if (HttpContext.Current.User.Identity.Name.ToString() != "")
            {
                _de.ParaNameArray("@Reg_User_LoginName", "@Shopping_CartItem_Item_UID");
                attactedRow = _de.ExecuteNonQuery("Shopping_Delete_CartItem", Reg_User_LoginName, Shopping_CartItem_Item_UID);
            }

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

        #region Shopping_Update_CartItem
        public int Shopping_Update_CartItem(DataTable dt, string Reg_User_LoginName, string Shopping_CartItem_Item_UID, int Shopping_CartItem_Qty, out object message)
        {
            int attactedRow = 0;
           
            foreach (DataRow row in dt.Rows)
            {
                if (row["Cart_UID"].ToString() == Shopping_CartItem_Item_UID)
                {
                    row.SetField("Cart_Qty", Shopping_CartItem_Qty);
                    attactedRow = 1;
                }
            }
            dt.AcceptChanges();

            if (HttpContext.Current.User.Identity.Name.ToString() != "")
            {
                _de.ParaNameArray("@Reg_User_LoginName", "@Shopping_CartItem_Item_UID", "@Shopping_CartItem_Qty");
                attactedRow = _de.ExecuteNonQuery("Shopping_Update_CartItem", Reg_User_LoginName, Shopping_CartItem_Item_UID, Shopping_CartItem_Qty);
            }

            if (attactedRow > 0)
            {
                message = "Product quantity has been updated on your shopping cart!";
            }
            else
            {
                message = "Product already deleted. Please re-open this page!";
            }

            return attactedRow;
        }
        #endregion

        #region Shopping_Select_DeliveryAddress
        public DataTable Shopping_Select_DeliveryAddress(string Reg_User_LoginName)
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Shopping_Select_DeliveryAddress", Reg_User_LoginName);
        }
        #endregion

        #region Shopping_Update_DeliveryAddress
        public int Shopping_Update_DeliveryAddress(string Reg_User_LoginName, string Reg_UserAddress_Country, string Reg_UserAddress_State, string Reg_UserAddress_ADDRESS, string Reg_UserAddress_City, string Reg_UserAddress_ZipCode, string Reg_UserAddress_Mobile, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Reg_UserAddress_Country", "@Reg_UserAddress_State", "@Reg_UserAddress_ADDRESS", "@Reg_UserAddress_City", "@Reg_UserAddress_ZipCode", "@Reg_UserAddress_Mobile");
            return _de.ExecuteNonQuery("Shopping_Update_DeliveryAddress", "@Message", out message, Reg_User_LoginName, Reg_UserAddress_Country, Reg_UserAddress_State, Reg_UserAddress_ADDRESS, Reg_UserAddress_City, Reg_UserAddress_ZipCode, Reg_UserAddress_Mobile);
        }
        #endregion

        #region Shopping_Insert_PaymentPaypal()
        public DataTable Shopping_Insert_PaymentPaypal(string Reg_User_LoginName, string Shopping_PurchasePayment_Gateway, string Shopping_PurchasePayment_TransactionID, double PaidAmount, string Shopping_PurchasePayment_Date, string Shopping_PurchasePayment_Status, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Shopping_PurchasePayment_Gateway", "@Shopping_PurchasePayment_TransactionID", "@PaidAmount", "@Shopping_PurchasePayment_Date", "@Shopping_PurchasePayment_Status");
            return _de.ExecuteDataTable("Shopping_Insert_PaymentPaypal", "@Message", out message, Reg_User_LoginName, Shopping_PurchasePayment_Gateway, Shopping_PurchasePayment_TransactionID, PaidAmount, Shopping_PurchasePayment_Date, Shopping_PurchasePayment_Status);
        }
        #endregion

        #region Shopping_Insert_PaymentAuthorizeNet()
        public DataTable Shopping_Insert_PaymentAuthorizeNet(string Reg_User_LoginName, string Shopping_PurchasePayment_Gateway, string Shopping_PurchasePayment_TransactionID, double PaidAmount, string Shopping_PurchasePayment_Date, string Shopping_PurchasePayment_Status, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Shopping_PurchasePayment_Gateway", "@Shopping_PurchasePayment_TransactionID", "@PaidAmount", "@Shopping_PurchasePayment_Date", "@Shopping_PurchasePayment_Status");
            return _de.ExecuteDataTable("Shopping_Insert_PaymentAuthorizeNet", "@Message", out message, Reg_User_LoginName, Shopping_PurchasePayment_Gateway, Shopping_PurchasePayment_TransactionID, PaidAmount, Shopping_PurchasePayment_Date, Shopping_PurchasePayment_Status);
        }
        public DataTable Shopping_Insert_GuestPayment(string Shopping_GuestCart_UID, string Shopping_PurchasePayment_Gateway, string Shopping_PurchasePayment_TransactionID, double PaidAmount, string Shopping_PurchasePayment_Date, string Shopping_PurchasePayment_Status, out object message)
        {
            _de.ParaNameArray("@Shopping_GuestCart_UID", "@Shopping_PurchasePayment_Gateway", "@Shopping_PurchasePayment_TransactionID", "@PaidAmount", "@Shopping_PurchasePayment_Date", "@Shopping_PurchasePayment_Status");
            return _de.ExecuteDataTable("Shopping_Insert_GuestPayment", "@Message", out message, Shopping_GuestCart_UID, Shopping_PurchasePayment_Gateway, Shopping_PurchasePayment_TransactionID, PaidAmount, Shopping_PurchasePayment_Date, Shopping_PurchasePayment_Status);
        }
        #endregion

        #region Shopping_Select_PurchaseHistory
        public DataTable Shopping_Select_PurchaseHistory(string Reg_User_LoginName)
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Shopping_Select_PurchaseHistory", Reg_User_LoginName);
        }
        #endregion

        #region Shopping_Delete_UserHistory
        public int Shopping_Delete_UserHistory(string Shopping_UID)
        {
            _de.ParaNameArray("@Shopping_UID");
            return _de.ExecuteNonQuery("Shopping_Delete_UserHistory", Shopping_UID);
        }
        #endregion

        #region Shopping_Select_PurchaseHistory_Admin
        public DataTable Shopping_Select_PurchaseHistory_Admin(string Shopping_FromDate, string Shopping_ToDate, String Reg_User_LoginName)
        {
            _de.ParaNameArray("@Shopping_FromDate", "@Shopping_ToDate", "@Reg_User_LoginName");
            return _de.ExecuteDataTable("Shopping_Select_PurchaseHistory_Admin", Shopping_FromDate, Shopping_ToDate, Reg_User_LoginName);
        }
        #endregion

        #region Shopping_Delete_UserHistory_Admin
        public int Shopping_Delete_UserHistory_Admin(string Shopping_UID)
        {
            _de.ParaNameArray("@Shopping_UID");
            return _de.ExecuteNonQuery("Shopping_Delete_UserHistory_Admin", Shopping_UID);
        }
        #endregion

        #region Shopping_Select_ProductInvoice
        public DataTable Shopping_Select_ProductInvoice(string Reg_User_LoginName, string Shopping_Purchase_OrderNumber)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Shopping_Purchase_OrderNumber");
            return _de.ExecuteDataTable("Shopping_Select_ProductInvoice", Reg_User_LoginName, Shopping_Purchase_OrderNumber);
        }
        #endregion

        #region Shopping_Select_ProductInvoiceAdmin
        public DataTable Shopping_Select_ProductInvoiceAdmin(string Shopping_Purchase_OrderNumber)
        {
            _de.ParaNameArray("@Shopping_Purchase_OrderNumber");
            return _de.ExecuteDataTable("Shopping_Select_ProductInvoiceAdmin", Shopping_Purchase_OrderNumber);
        }
        #endregion

        #region Shopping_ProductCartItemInvoice
        public DataTable Shopping_ProductCartItemInvoice(string Reg_User_LoginName, string Shopping_Purchase_OrderNumber)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Shopping_Purchase_OrderNumber");
            return _de.ExecuteDataTable("Shopping_ProductCartItemInvoice", Reg_User_LoginName, Shopping_Purchase_OrderNumber);
        }
        #endregion

        #region Shopping_ProductCartItemInvoiceADMIN
        public DataTable Shopping_ProductCartItemInvoiceADMIN(string Shopping_Purchase_OrderNumber)
        {
            _de.ParaNameArray("@Shopping_Purchase_OrderNumber");
            return _de.ExecuteDataTable("Shopping_ProductCartItemInvoiceAdmin", Shopping_Purchase_OrderNumber);
        }
        #endregion

        #region Shopping_Select_EventTicketPurchaseHistory
        public DataTable Shopping_Select_EventTicketPurchaseHistory(string Reg_User_LoginName)
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Shopping_Select_EventTicketPurchaseHistory", Reg_User_LoginName);
        }
        #endregion

        #region Shopping_Select_EventTicketInvoice
        public DataTable Shopping_Select_EventTicketInvoice(string Reg_User_LoginName, string Event_Cart_UID)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Event_Cart_UID");
            return _de.ExecuteDataTable("Shopping_Select_EventTicketInvoice", Reg_User_LoginName, Event_Cart_UID);
        }
        public DataTable Shopping_Select_VisitorEventTicketInvoice(string Event_Cart_UID)
        {
            _de.ParaNameArray("@Event_Cart_UID");
            return _de.ExecuteDataTable("Shopping_Select_VisitorEventTicketInvoice", Event_Cart_UID);
        }
        public DataTable Shopping_Select_ShopInvoice(string Shopping_PurchaseGuestPayment_UID)
        {
            _de.ParaNameArray("@Shopping_PurchaseGuestPayment_UID");
            return _de.ExecuteDataTable("Shopping_Select_ShopInvoice", Shopping_PurchaseGuestPayment_UID);
        }
        #endregion

        #region Shopping_EventCartItemInvoice
        public DataTable Shopping_EventCartItemInvoice(string Reg_User_LoginName, string Event_Cart_UID)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Event_Cart_UID");
            return _de.ExecuteDataTable("Shopping_EventCartItemInvoice", Reg_User_LoginName, Event_Cart_UID);
        }

        public DataTable Shopping_VisitorEventCartItemInvoice(string Event_Cart_UID)
        {
            _de.ParaNameArray("@Event_Cart_UID");
            return _de.ExecuteDataTable("Shopping_VisitorEventCartItemInvoice", Event_Cart_UID);
        }
        #endregion

        #region Shopping_Select_EventTicketRefferalReport
        public DataTable Shopping_Select_EventTicketRefferalReport()
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Shopping_Select_EventTicketRefferalReport", HttpContext.Current.User.Identity.Name);
        }
        #endregion

        #region Shopping_Insert_FeePaidUser()
        public DataTable Shopping_Insert_FeePaidUser(string Reg_User_LoginName, string Shopping_PurchasePayment_Gateway, string Shopping_PurchasePayment_TransactionID, Decimal PaidAmount, string Shopping_PurchasePayment_Date, string Shopping_PurchasePayment_Status, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Shopping_PurchasePayment_Gateway", "@Shopping_PurchasePayment_TransactionID", "@PaidAmount", "@Shopping_PurchasePayment_Date", "@Shopping_PurchasePayment_Status");
            return _de.ExecuteDataTable("Shopping_Insert_FeePaidUser", "@Message", out message, Reg_User_LoginName, Shopping_PurchasePayment_Gateway, Shopping_PurchasePayment_TransactionID, PaidAmount, Shopping_PurchasePayment_Date, Shopping_PurchasePayment_Status);
        }
        #endregion

        #region Shopping_Insert_GuestCart()
        public DataTable Shopping_Insert_GuestCart(string Shopping_GuestCart_ItemUID, string Shopping_GuestCart_FullName, string Shopping_GuestCart_Email, double Shopping_GuestCart_Amount, int Shopping_GuestCart_Qty, double Shopping_GuestCart_Rate, string Shopping_GuestCart_Country, string Shopping_GuestCart_State, string Shopping_GuestCart_City, string Shopping_GuestCart_Address, string Shopping_GuestCart_ZipCode)
        {
            _de.ParaNameArray("@Shopping_GuestCart_ItemUID", "@Shopping_GuestCart_FullName", "@Shopping_GuestCart_Email", "@Shopping_GuestCart_Amount", "@Shopping_GuestCart_Qty", "@Shopping_GuestCart_Rate", "@Shopping_GuestCart_Country", "@Shopping_GuestCart_State", "@Shopping_GuestCart_City", "@Shopping_GuestCart_Address" , "@Shopping_GuestCart_ZipCode");
            return _de.ExecuteDataTable("Shopping_Insert_GuestCart", Shopping_GuestCart_ItemUID, Shopping_GuestCart_FullName, Shopping_GuestCart_Email, Shopping_GuestCart_Amount, Shopping_GuestCart_Qty, Shopping_GuestCart_Rate, Shopping_GuestCart_Country, Shopping_GuestCart_State, Shopping_GuestCart_City, Shopping_GuestCart_Address, Shopping_GuestCart_ZipCode);
        }
        #endregion

        public DataTable Shopping_Update_ShippingDetail(string Shopping_Shipping_PurchaseUID, string Shopping_Shipping_CourierName, string Shopping_Shipping_TrackingNumber, DateTime Shopping_Shipping_Date, DateTime Shopping_Shipping_ExpectedDeliveryDate, string CustomerLoginName, string ProductName, out object message)
        {
            _de.ParaNameArray("@Shopping_Shipping_PurchaseUID", "@Shopping_Shipping_CourierName", "@Shopping_Shipping_TrackingNumber", "@Shopping_Shipping_Date", "@Shopping_Shipping_ExpectedDeliveryDate", "@CustomerLoginName", "@ProductName", "@Reg_User_LoginName");
            return _de.ExecuteDataTable("Shopping_Update_ShippingDetail", "@Message", out message, Shopping_Shipping_PurchaseUID, Shopping_Shipping_CourierName, Shopping_Shipping_TrackingNumber, Shopping_Shipping_Date, Shopping_Shipping_ExpectedDeliveryDate, CustomerLoginName, ProductName, HttpContext.Current.User.Identity.Name);
        }
    }
}
