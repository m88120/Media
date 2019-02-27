using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessEntity.ConcreateEntity
{
    public class IncomeDataEntity
    {
        private DataEntity _de;

        #region IncomeDataEntity
        public IncomeDataEntity()
        {
            _de = new DataEntity();
        }
        #endregion

        #region Payout_Select_Level_Date
        public DataTable Payout_Select_Level_Date()
        {
            return _de.ExecuteDataTable("Payout_Select_Level_Date");
        }
        #endregion

        #region Payout_Insert_Level
        public int Payout_Insert_Level(out object message)
        {
            return _de.ExecuteNonQuery("Payout_Insert_Level", "Message", out message);
        }
        #endregion

        #region Income_Report_Referral
        public DataTable Income_Report_Referral(string DateFrom, string DateTo)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@DateFrom", "@DateTo");
            return _de.ExecuteDataTable("Income_Report_Referral", HttpContext.Current.User.Identity.Name, DateFrom, DateTo);
        }
        public DataTable Income_Report_ReferralAdmin(string Reg_User_LoginName, string DateFrom, string DateTo)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@DateFrom", "@DateTo");
            return _de.ExecuteDataTable("Income_Report_ReferralAdmin", Reg_User_LoginName, DateFrom, DateTo);
        }
        #endregion

        #region Income_Report_SelfPurchase
        public DataTable Income_Report_SelfPurchase(string DateFrom, string DateTo)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@DateFrom", "@DateTo");
            return _de.ExecuteDataTable("Income_Report_SelfPurchase", HttpContext.Current.User.Identity.Name, DateFrom, DateTo);
        }
        public DataTable Income_Report_SelfPurchase(string Reg_User_LoginName, string DateFrom, string DateTo)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@DateFrom", "@DateTo");
            return _de.ExecuteDataTable("Income_Report_SelfPurchaseAdmin", Reg_User_LoginName, DateFrom, DateTo);
        }
        #endregion

        #region Income_Report_Level
        public DataTable Income_Report_Level(string DateFrom, string DateTo)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@DateFrom", "@DateTo");
            return _de.ExecuteDataTable("Income_Report_Level", HttpContext.Current.User.Identity.Name, DateFrom, DateTo);
        }
        public DataTable Income_Report_Level(string Reg_User_LoginName, string DateFrom, string DateTo)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@DateFrom", "@DateTo");
            return _de.ExecuteDataTable("Income_Report_LevelAdmin", Reg_User_LoginName, DateFrom, DateTo);
        }
        #endregion

        #region Income_Report_CAB
        public DataTable Income_Report_CAB(string DateFrom, string DateTo)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@DateFrom", "@DateTo");
            return _de.ExecuteDataTable("Income_Report_CAB", HttpContext.Current.User.Identity.Name, DateFrom, DateTo);
        }
        public DataTable Income_Report_CAB(string Reg_User_LoginName, string DateFrom, string DateTo)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@DateFrom", "@DateTo");
            return _de.ExecuteDataTable("Income_Report_CABAdmin", Reg_User_LoginName, DateFrom, DateTo);
        }
        #endregion

        #region Income_Report_Trainer
        public DataTable Income_Report_Trainer(string DateFrom, string DateTo)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@DateFrom", "@DateTo");
            return _de.ExecuteDataTable("Income_Report_Trainer", HttpContext.Current.User.Identity.Name, DateFrom, DateTo);
        }
        public DataTable Income_Report_Trainer(string Reg_User_LoginName, string DateFrom, string DateTo)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@DateFrom", "@DateTo");
            return _de.ExecuteDataTable("Income_Report_TrainerAdmin", Reg_User_LoginName, DateFrom, DateTo);
        }
        #endregion
    }
}
