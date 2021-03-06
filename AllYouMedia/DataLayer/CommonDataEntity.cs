using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Net;
using System.IO;

namespace BusinessEntity.ConcreateEntity
{
    public class CommonDataEntity
    {
        private DataEntity _de;

        #region CommonDataEntity
        public CommonDataEntity()
        {
            _de = new DataEntity();
        }
        #endregion                        

        #region GetCurrentDate()
        public string GetCurrentDate()
        {
            object name = _de.ExecuteScalarFunction("SELECT dbo.GetCurrentDate()");

            if (name != null && name.ToString() != "")
            {
                DateTime date = Convert.ToDateTime(name);

                string strDate = date.GetDateTimeFormats(new System.Globalization.CultureInfo("en-US"))[0].ToString();

                return strDate;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Common_Select_UserHome()
        public DataTable Common_Select_UserHome(string LoginName)
        {
            _de.ParaNameArray("@LoginName");
            return _de.ExecuteDataTable("Common_Select_UserHome", LoginName);
        }
        #endregion

        #region Select_PaidStautsList
        public DataTable Select_PaidStautsList()
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("Text", typeof(string));
            DataColumn dc2 = new DataColumn("Value", typeof(string));
            dt.Columns.Add(dc1); dt.Columns.Add(dc2);

            dt.Rows.Add("Paid", "1");
            dt.Rows.Add("Unpaid", "0");
            return dt;
        }
        #endregion

        #region Page_Select_Detail()
        public string Page_Select_Detail(string Page_Url)
        {
            _de.ParaNameArray("@Page_Url");
            object _html = _de.ExecuteScalar("Page_Select_Detail", Page_Url);

            return (_html != null) ? _html.ToString() : "";
        }
        #region Page_Get_Detail()
        public DataTable Page_Get_Detail(string Page_Url)
        {
            _de.ParaNameArray("@Page_Url");
            return _de.ExecuteDataTable("Page_Select_Detail", Page_Url);
        }
        #endregion
        #endregion

        #region Page_Update_Detail()
        public int Page_Update_Detail(string Page_Url, string Page_HTML, out object message)
        {
            _de.ParaNameArray("@Page_Url", "@Page_HTML");
            return _de.ExecuteNonQuery("Page_Update_Detail", "@Message", out message, Page_Url, Page_HTML);
        }
        #endregion

        #region Select_PageList
        public DataTable Select_PageList()
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("Text", typeof(string));
            DataColumn dc2 = new DataColumn("Value", typeof(string));
            dt.Columns.Add(dc1); dt.Columns.Add(dc2);

            //Starter
            dt.Rows.Add("ABOUT US", "aboutus");
            dt.Rows.Add("Contact US", "contact");
            dt.Rows.Add("MEMBERSHIP", "membership");
            dt.Rows.Add("PRIVACY POLICY", "privacypolicy");
            dt.Rows.Add("TERMS CONDITION", "termscondition");
            dt.Rows.Add("TALENT AGREEMENT", "talentagreement");
            dt.Rows.Add("MEDIA AGREEMENT", "mediaagreement");
            dt.Rows.Add("AREA PROMOTER AGREEMENT", "areapromoteragreement");
            dt.Rows.Add("CONTEST RULE AND QUALIFICATION", "contestruleagreement");
            dt.Rows.Add("AGREEMENT B/W MEMBERS", "User/contract");
            return dt;
        }
        #endregion

        #region Select_MajorDDL
        public DataTable Select_MajorDDL()
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("Text", typeof(string));
            DataColumn dc2 = new DataColumn("Value", typeof(string));
            dt.Columns.Add(dc1); dt.Columns.Add(dc2);

            dt.Rows.Add("Advertising/Marketing", "Advertising/Marketing");
            dt.Rows.Add("Business Management", "Business Management");
            dt.Rows.Add("Computer Networking", "Computer Networking");
            dt.Rows.Add("Human Resources", "Human Resources");
            dt.Rows.Add("Internet Coding", "Internet Coding");
            dt.Rows.Add("Internet Design", "Internet Design");
            dt.Rows.Add("Internet Security", "Internet Security");
            dt.Rows.Add("IT/Programming", "IT/Programming");
            dt.Rows.Add("Law Student", "Law Student");
            dt.Rows.Add("Public Relations", "Public Relations");
            dt.Rows.Add("Supply Chain Management", "Supply Chain Management");
            dt.Rows.Add("Other", "Other");
            return dt;
        }
        #endregion

        #region Select_StatusDDL
        public DataTable Select_StatusDDL()
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("Text", typeof(string));
            DataColumn dc2 = new DataColumn("Value", typeof(string));
            dt.Columns.Add(dc1); dt.Columns.Add(dc2);

            dt.Rows.Add("Active", "A");
            dt.Rows.Add("Inactive", "I");
            dt.Rows.Add("Deleted", "D");          
            return dt;
        }
        #endregion

        #region Select_AlbumStatusDDL
        public DataTable Select_AlbumStatusDDL()
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("Text", typeof(string));
            DataColumn dc2 = new DataColumn("Value", typeof(string));
            dt.Columns.Add(dc1); dt.Columns.Add(dc2);

            dt.Rows.Add("Active", "A");
            dt.Rows.Add("Inactive", "I");          
            return dt;
        }
        #endregion

        #region Select_TalentCategoryDDL
        public DataTable Select_TalentCategoryDDL()
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("Text", typeof(string));
            DataColumn dc2 = new DataColumn("Value", typeof(string));
            dt.Columns.Add(dc1); dt.Columns.Add(dc2);

            dt.Rows.Add("ALL TALENT", "All Talent");
            dt.Rows.Add("PRODUCTION", "Production");
            dt.Rows.Add("MEDIA PROMOTER", "Media Promoter");
            return dt;
        }
        public DataTable Select_TalentCategoryAdmin()
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("Text", typeof(string));
            DataColumn dc2 = new DataColumn("Value", typeof(string));
            dt.Columns.Add(dc1); dt.Columns.Add(dc2);

            dt.Rows.Add("ALL TALENT", "All Talent");
            dt.Rows.Add("PRODUCTION", "Production");
            dt.Rows.Add("MEDIA PROMOTER", "Media Promoter");
            dt.Rows.Add("AREA PROMOTER", "Area Promoter");
            return dt;
        }
        #endregion      

        #region Select_ContentPriceDDL
        public DataTable Select_ContentPriceDDL()
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("Text", typeof(string));
            DataColumn dc2 = new DataColumn("Value", typeof(string));
            dt.Columns.Add(dc1); dt.Columns.Add(dc2);

            dt.Rows.Add("$1.24", "1.24");
            dt.Rows.Add("$4.99", "4.99");
            dt.Rows.Add("$9.99", "9.99");
            dt.Rows.Add("$14.99", "14.99");
            dt.Rows.Add("$19.99", "19.99");
            return dt;
        }
        #endregion

        #region GetJoiningAmount
        public decimal GetJoiningAmount(string Role)
        {
            _de.ParaNameArray("@Role");
            object amount = _de.ExecuteScalarFunction("SELECT dbo.GetJoiningAmount(@Role)", Role);

            return Convert.ToDecimal(amount);
        }
        #endregion

        #region GetTreeLevel()
        public DataTable GetTreeLevel()
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("Text", typeof(int));
            DataColumn dc2 = new DataColumn("Value", typeof(int));
            dt.Columns.Add(dc1); dt.Columns.Add(dc2);

            dt.Rows.Add("1", 1);
            dt.Rows.Add("2", 2);
            dt.Rows.Add("3", 3);
            dt.Rows.Add("4", 4);
            dt.Rows.Add("5", 5);
            //dt.Rows.Add("6", 6);
            //dt.Rows.Add("7", 7);
            //dt.Rows.Add("8", 8);
            //dt.Rows.Add("9", 9);
            //dt.Rows.Add("10", 10);
            //dt.Rows.Add("11", 11);
            //dt.Rows.Add("12", 12);
            //dt.Rows.Add("13", 13);
            //dt.Rows.Add("14", 14);
            //dt.Rows.Add("15", 15);
            return dt;
        }
        #endregion

        #region Select_UserList
        public DataTable Select_UserList()
        {
            DataTable dt = _de.ExecuteDataTable("usp_GetUserCBO");
            return dt;
        }
        #endregion
    }
}