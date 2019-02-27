using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessEntity.ConcreateEntity
{
    public class GenealogyDataEntity
    {
        private DataEntity _de;

        #region GenealogyDataEntity
        public GenealogyDataEntity()
        {
            _de = new DataEntity();
        }
        #endregion


        #region Gen_Report_Downline
        public DataTable Gen_Report_Downline(string Reg_User_LoginName, string DateFrom, string DateTo, string Reg_UserAddress_State)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@DateFrom", "@DateTo", "@Reg_UserAddress_State");
            return _de.ExecuteDataTable("Gen_Report_Downline", Reg_User_LoginName, DateFrom, DateTo, Reg_UserAddress_State);
        }
        #endregion

        #region Gen_Report_DownlineAdmin
        public DataTable Gen_Report_DownlineAdmin(string Reg_User_LoginName, string DateFrom, string DateTo)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@DateFrom", "@DateTo");
            return _de.ExecuteDataTable("Gen_Report_DownlineAdmin", Reg_User_LoginName, DateFrom, DateTo);
        }
        #endregion

        #region Gen_Report_Level
        public DataTable Gen_Report_Level(string DateFrom, string DateTo, string Reg_User_LoginName_S)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@DateFrom", "@DateTo", "@Reg_User_LoginName_S");
            return _de.ExecuteDataTable("Gen_Report_Level", HttpContext.Current.User.Identity.Name, DateFrom, DateTo, Reg_User_LoginName_S);
        }
        #endregion

        #region Gen_Report_LevelAdmin
        public DataTable Gen_Report_LevelAdmin(string Reg_User_LoginName, string DateFrom, string DateTo)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@DateFrom", "@DateTo");
            return _de.ExecuteDataTable("Gen_Report_LevelAdmin", Reg_User_LoginName, DateFrom, DateTo);
        }
        #endregion

        #region Gen_GetUserTree()
        public DataTable Gen_GetUserTree(string Reg_User_LoginName_Search)
        {
            _de.ParaNameArray("@Reg_User_LoginName_Search", "@Reg_User_LoginName");
            return _de.ExecuteDataTable("Gen_GetUserTree", Reg_User_LoginName_Search, HttpContext.Current.User.Identity.Name);
        }
        public DataTable Gen_GetUserTree(string Reg_User_LoginName_Search, string Reg_User_LoginName)
        {
            _de.ParaNameArray("@Reg_User_LoginName_Search", "@Reg_User_LoginName");
            return _de.ExecuteDataTable("Gen_GetUserTree", Reg_User_LoginName_Search, Reg_User_LoginName);
        }
        #endregion

        #region Gen_GetVerticalTree_User
        public DataTable Gen_GetVerticalTree_User(string Reg_User_LoginName_Search, bool isBaseID)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Reg_User_LoginName_Search", "@isBaseID");
            return _de.ExecuteDataTable("Gen_GetVerticalTree_User", HttpContext.Current.User.Identity.Name, Reg_User_LoginName_Search, isBaseID);
        }
        #endregion

        #region Gen_GetTree_UserDetail
        public DataTable Gen_GetTree_UserDetail(string Reg_User_LoginName_Search)
        {
            _de.ParaNameArray("@Reg_User_LoginName","@Reg_User_LoginName_Search");
            return _de.ExecuteDataTable("Gen_GetTree_UserDetail", HttpContext.Current.User.Identity.Name, Reg_User_LoginName_Search);
        }
        #endregion

        #region Gen_GetTreeReport()
        public DataTable Gen_GetTreeReport(string Reg_User_LoginName)
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Gen_GetTreeReport", Reg_User_LoginName);
        }
        #endregion
    }
}
