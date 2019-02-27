using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Threading;
using System.Web.Security;

namespace BusinessEntity.ConcreateEntity
{
    public class RoleDataEntity
    {
        private DataEntity _de;

        #region RoleDataEntity
        public RoleDataEntity()
        {
            _de = new DataEntity();
        }
        #endregion

        #region Role_GetUserName
        public string Role_GetUserName(string Reg_User_LoginName)
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteScalarFunction("SELECT dbo.GetUserName(@Reg_User_LoginName)", Reg_User_LoginName).ToString();
        }
        #endregion

        #region Role_GetUserName
        public long GetLoggedInUserID(string UserName)
        {
            _de.ParaNameArray("@UserName");
            return Convert.ToInt64(_de.ExecuteScalarFunction("SELECT [dbo].[GetUserIDByUserName](@UserName)", UserName));
        }
        #endregion

        #region Role_Insert_User_Customer()
        public DataTable Role_Insert_User_Customer(string Reg_UserAddress_Name, string Reg_UserAddress_Country, string Reg_UserAddress_State, string Reg_UserAddress_City, string Reg_UserAddress_Street, string Reg_UserAddress_ZipCode, string Reg_UserAddress_Address, string Reg_UserAddress_Mobile, string Reg_UserAddress_Email, string Reg_USER_LOGINPASSWORD, string Reg_USER_RefferedBy, out object message)
        {
            _de.ParaNameArray("@Reg_UserAddress_Name", "@Reg_UserAddress_Country", "@Reg_UserAddress_State", "@Reg_UserAddress_City", "@Reg_UserAddress_Street", "@Reg_UserAddress_ZipCode", "@Reg_UserAddress_Address", "@Reg_UserAddress_Mobile", "@Reg_UserAddress_Email", "@Reg_USER_LOGINPASSWORD", "@Reg_USER_RefferedBy", "@CurrentIP");
            return _de.ExecuteDataTable("Role_Insert_User_Customer", "@Message", out message, Reg_UserAddress_Name, Reg_UserAddress_Country, Reg_UserAddress_State, Reg_UserAddress_City, Reg_UserAddress_Street, Reg_UserAddress_ZipCode, Reg_UserAddress_Address, Reg_UserAddress_Mobile, Reg_UserAddress_Email, Reg_USER_LOGINPASSWORD, Reg_USER_RefferedBy, HttpContext.Current.Request.UserHostAddress);
        }
        #endregion

        #region Role_Insert_User_Member()
        public DataTable Role_Insert_User_Member(string Reg_UserAddress_Name, string Reg_UserAddress_Country, string Reg_UserAddress_State, string Reg_UserAddress_City, string Reg_UserAddress_Street, string Reg_UserAddress_ZipCode, string Reg_UserAddress_Mobile, string Reg_UserAddress_Phone1, string Reg_UserAddress_Phone2, string Reg_USER_LOGINNAME, string Reg_USER_LOGINPASSWORD, string Reg_UserTemp_UID, DataTable dtPMember, string Reg_UserAddress_ProductionOther, out object message)
        {
            DataSet ds = new DataSet();
            ds.Merge(dtPMember);
            _de.ParaNameArray("@Reg_UserAddress_Name", "@Reg_UserAddress_Country", "@Reg_UserAddress_State", "@Reg_UserAddress_City", "@Reg_UserAddress_Street", "@Reg_UserAddress_ZipCode", "@Reg_UserAddress_Mobile", "@Reg_UserAddress_Phone1", "@Reg_UserAddress_Phone2", "@Reg_USER_LOGINNAME", "@Reg_USER_LOGINPASSWORD", "@Reg_UserTemp_UID", "@XML", "@ProductionOther", "@CurrentIP");
            return _de.ExecuteDataTable("Role_Insert_User_Member", "@Message", out message, Reg_UserAddress_Name, Reg_UserAddress_Country, Reg_UserAddress_State, Reg_UserAddress_City, Reg_UserAddress_Street, Reg_UserAddress_ZipCode, Reg_UserAddress_Mobile, Reg_UserAddress_Phone1, Reg_UserAddress_Phone2, Reg_USER_LOGINNAME, Reg_USER_LOGINPASSWORD, Reg_UserTemp_UID, ds.GetXml(), Reg_UserAddress_ProductionOther, HttpContext.Current.Request.UserHostAddress);
        }

        #endregion

        #region Role_Insert_User_PromoterStep1()
        public int Role_Insert_User_PromoterStep1(string Reg_UserAddress_Name, string Reg_UserAddress_Country, string Reg_UserAddress_State, string Reg_UserAddress_City, string Reg_UserAddress_Street, string Reg_UserAddress_ZipCode, string Reg_UserAddress_Mobile, string Reg_USER_LOGINPASSWORD, string Reg_UserTemp_SpID, string Reg_UserTemp_UID, string Reg_UserTemp_Address, out object message)
        {
            _de.ParaNameArray("@Reg_UserAddress_Name", "@Reg_UserAddress_Country", "@Reg_UserAddress_State", "@Reg_UserAddress_City", "@Reg_UserAddress_Street", "@Reg_UserAddress_ZipCode", "@Reg_UserAddress_Mobile", "@Reg_USER_LOGINPASSWORD", "@Reg_UserTemp_SpID", "@Reg_UserTemp_UID", "@CurrentIP", "@Reg_UserTemp_Address");
            return _de.ExecuteNonQuery("Role_Insert_User_PromoterStep1", "@Message", out message, Reg_UserAddress_Name, Reg_UserAddress_Country, Reg_UserAddress_State, Reg_UserAddress_City, Reg_UserAddress_Street, Reg_UserAddress_ZipCode, Reg_UserAddress_Mobile, Reg_USER_LOGINPASSWORD, Reg_UserTemp_SpID, Reg_UserTemp_UID, HttpContext.Current.Request.UserHostAddress, Reg_UserTemp_Address);
        }

        public int Role_Insert_User_PromoterStepCombo1(string Reg_UserAddress_Name, string Reg_UserAddress_Country, string Reg_UserAddress_State, string Reg_UserAddress_City, string Reg_UserAddress_Street, string Reg_UserAddress_ZipCode, string Reg_UserAddress_Mobile, string Reg_UserAddress_Phone1, string Reg_UserAddress_Phone2, string Reg_USER_LOGINNAME, string Reg_USER_LOGINPASSWORD, DataTable dtPMember, string Reg_UserAddress_ProductionOther, string Reg_UserTemp_SpID, string Reg_UserTemp_UID, out object message)
        {
            DataSet ds = new DataSet();
            ds.Merge(dtPMember);
            _de.ParaNameArray("@Reg_UserAddress_Name", "@Reg_UserAddress_Country", "@Reg_UserAddress_State", "@Reg_UserAddress_City", "@Reg_UserAddress_Street", "@Reg_UserAddress_ZipCode", "@Reg_UserAddress_Mobile", "@Reg_UserAddress_Phone1", "@Reg_UserAddress_Phone2", "@Reg_USER_LOGINNAME", "@Reg_USER_LOGINPASSWORD", "@XML", "@ProductionOther", "@Reg_UserTemp_SpID", "@Reg_UserTemp_UID", "@CurrentIP");
            return _de.ExecuteNonQuery("Role_Insert_User_PromoterStepCombo1", "@Message", out message, Reg_UserAddress_Name, Reg_UserAddress_Country, Reg_UserAddress_State, Reg_UserAddress_City, Reg_UserAddress_Street, Reg_UserAddress_ZipCode, Reg_UserAddress_Mobile, Reg_UserAddress_Phone1, Reg_UserAddress_Phone2, Reg_USER_LOGINNAME, Reg_USER_LOGINPASSWORD, ds.GetXml(), Reg_UserAddress_ProductionOther, Reg_UserTemp_SpID, Reg_UserTemp_UID, HttpContext.Current.Request.UserHostAddress);
        }
        #endregion

        #region Role_Insert_User_PromoterStep2()
        public DataTable Role_Insert_User_PromoterStep2(string Reg_UserTemp_UID, string Shopping_PurchasePayment_Gateway, string Shopping_PurchasePayment_TransactionID, double Shopping_PurchasePayment_PaidAmount, string Shopping_PurchasePayment_Date, out object message)
        {
            _de.ParaNameArray("@Reg_UserTemp_UID", "@Shopping_PurchasePayment_Gateway", "@Shopping_PurchasePayment_TransactionID", "@Shopping_PurchasePayment_PaidAmount", "@Shopping_PurchasePayment_Date");
            return _de.ExecuteDataTable("Role_Insert_User_PromoterStep2", "@Message", out message, Reg_UserTemp_UID, Shopping_PurchasePayment_Gateway, Shopping_PurchasePayment_TransactionID, Shopping_PurchasePayment_PaidAmount, Shopping_PurchasePayment_Date);
        }

        public DataTable Role_Insert_User_PromoterStepCombo2(string Reg_UserTemp_UID, string Shopping_PurchasePayment_Gateway, string Shopping_PurchasePayment_TransactionID, double Shopping_PurchasePayment_PaidAmount, string Shopping_PurchasePayment_Date, out object message)
        {
            _de.ParaNameArray("@Reg_UserTemp_UID", "@Shopping_PurchasePayment_Gateway", "@Shopping_PurchasePayment_TransactionID", "@Shopping_PurchasePayment_PaidAmount", "@Shopping_PurchasePayment_Date");
            return _de.ExecuteDataTable("Role_Insert_User_PromoterStepCombo2", "@Message", out message, Reg_UserTemp_UID, Shopping_PurchasePayment_Gateway, Shopping_PurchasePayment_TransactionID, Shopping_PurchasePayment_PaidAmount, Shopping_PurchasePayment_Date);
        }
        #endregion

        #region Role_Insert_UserTemp()
        public DataTable Role_Insert_UserTemp(string Reg_UserTemp_Name, string Reg_UserTemp_Email, out object message)
        {
            _de.ParaNameArray("@Reg_UserTemp_Name", "@Reg_UserTemp_Email", "@CurrentIP");
            return _de.ExecuteDataTable("Role_Insert_UserTemp", "@Message", out message, Reg_UserTemp_Name, Reg_UserTemp_Email, HttpContext.Current.Request.UserHostAddress);
        }
        #endregion

        #region Role_Select_UserTemp()
        public DataTable Role_Select_UserTemp(string Reg_UserTemp_VarificationCode, out object message)
        {
            _de.ParaNameArray("@Reg_UserTemp_VarificationCode");
            return _de.ExecuteDataTable("Role_Select_UserTemp", "@Message", out message, Reg_UserTemp_VarificationCode);
        }
        #endregion

        #region Role_Update_UserTempShirtCharg()
        public int Role_Update_UserTempShirtCharg(string Reg_UserTemp_UID, bool WantShirt)
        {
            _de.ParaNameArray("@Reg_UserTemp_UID", "@WantShirt");
            return _de.ExecuteNonQuery("Role_Update_UserTempShirtCharg", Reg_UserTemp_UID, WantShirt);
        }
        #endregion

        #region Role_Select_UserDetailTemp()
        public DataTable Role_Select_UserDetailTemp(string Reg_UserTemp_UID)
        {
            _de.ParaNameArray("@Reg_UserTemp_UID");
            return _de.ExecuteDataTable("Role_Select_UserDetailTemp", Reg_UserTemp_UID);
        }
        #endregion

        #region Role_Report_UserDetail
        public DataTable Role_Report_UserDetail(long UserID, string Reg_UserAddress_DOJFrom, string Reg_UserAddress_DOJTo, string Reg_UserAddress_City, string Reg_UserAddress_Country, string Reg_UserAddress_College, string Reg_UserAddress_Major, string Reg_User_Status)
        {
            _de.ParaNameArray("@UserID", "@DOJFrom", "@DOJTo", "@City", "@Country", "@College", "@Major", "@Status");
            return _de.ExecuteDataTable("Role_Report_UserDetail", UserID, Reg_UserAddress_DOJFrom, Reg_UserAddress_DOJTo, Reg_UserAddress_City, Reg_UserAddress_Country, Reg_UserAddress_College, Reg_UserAddress_Major, Reg_User_Status);
        }
        #endregion

        #region Role_Select_AdminLogin()
        public int Role_Select_AdminLogin(string txtUserID, out object message)
        {
            _de.ParaNameArray("@UserName", "@CurrentIP");
            DataTable dt = _de.ExecuteDataTable("Role_Select_AdminLogin", "@Message", out message, txtUserID, HttpContext.Current.Request.UserHostAddress);

            if (dt.Rows.Count > 0)
                return 1;
            else
                return 0;
        }
        #endregion

        #region Role_Select_UserForgotPassword()
        public DataTable Role_Select_UserForgotPassword(string Reg_User_LoginName, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Role_Select_UserForgotPassword", "@Message", out message, Reg_User_LoginName);
        }
        #endregion

        #region Role_Select_UserLogin()
        public int Role_Select_UserLogin(string txtUserID, string txtPassword, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Reg_User_LoginPassword", "@CurrentIP");
            DataTable dt = _de.ExecuteDataTable("Role_Select_UserLogin", "@Message", out message, txtUserID, txtPassword, HttpContext.Current.Request.UserHostAddress);

            if (dt.Rows.Count > 0)
            {
                string userKID = dt.Rows[0][0].ToString();

                //foreach (DataRow dr in dt.Rows)
                //{
                //    if (!Roles.RoleExists(dr.ItemArray[2].ToString())) Roles.CreateRole(dr.ItemArray[2].ToString());
                //    if (!Roles.IsUserInRole(userKID, dr.ItemArray[2].ToString())) Roles.AddUserToRole(userKID, dr.ItemArray[2].ToString());

                //}
                message = userKID;

                return 1;
            }

            return 0;
        }
        #endregion

        #region Role_Select_UserProfileAdmin()
        public DataTable Role_Select_UserProfileAdmin(string Reg_User_LoginName, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Role_Select_UserProfileAdmin", "@Message", out message, Reg_User_LoginName);
        }
        #endregion


        #region Role_Update_AdminPassword()
        public int Role_Update_AdminPassword(string OldPassword, string NewPassword, out object message)
        {
            _de.ParaNameArray("@Reg_Admin_LoginID", "@OldPassword", "@NewPassword", "@CurrentIP");
            return _de.ExecuteNonQuery("Role_Update_AdminPassword", "@Message", out message, HttpContext.Current.User.Identity.Name, OldPassword, NewPassword, HttpContext.Current.Request.UserHostAddress);
        }
        #endregion

        #region Role_Update_Control_AdminPassword
        public int Role_Update_Control_AdminPassword(string OldPassword, string NewPassword, out object message)
        {
            _de.ParaNameArray("@OldPassword", "@NewPassword", "@CurrentIP");
            return _de.ExecuteNonQuery("Role_Update_Control_AdminPassword", "@Message", out message, OldPassword, NewPassword, HttpContext.Current.Request.UserHostAddress);
        }
        #endregion

        #region Role_Select_Control_AdminPassword
        public int Role_Select_Control_AdminPassword(string txtPassword, out object message)
        {
            _de.ParaNameArray("@Parameter_Control_Password");
            return Convert.ToInt32(_de.ExecuteScalar("Role_Select_Control_AdminPassword", "@message", out message, txtPassword));
        }
        #endregion

        #region Role_Update_UserPasswordAfterLogin
        public DataTable Role_Update_UserPasswordAfterLogin(string OldPassword, string NewPassword, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Reg_User_LoginPassword_Old", "@Reg_User_LoginPassword_New", "@CurrentIP");
            return _de.ExecuteDataTable("Role_Update_UserPasswordAfterLogin", "@Message", out message, HttpContext.Current.User.Identity.Name, OldPassword, NewPassword, HttpContext.Current.Request.UserHostAddress);
        }
        #endregion

        #region Role_Update_UserPassword()
        public DataTable Role_Update_UserPassword(string Reg_User_ResetCode, string NewPassword, out object message)
        {
            _de.ParaNameArray("@Reg_User_ResetCode", "@NewPassword", "@CurrentIP");
            return _de.ExecuteDataTable("Role_Update_UserPassword", "@Message", out message, Reg_User_ResetCode, NewPassword, HttpContext.Current.Request.UserHostAddress);
        }
        #endregion

        #region Role_Update_UserPasswordUser()
        public DataTable Role_Update_UserPasswordUser(string OldPassword, string NewPassword, out object message)
        {
            _de.ParaNameArray("@LoginName", "@OldPassword", "@NewPassword", "@CurrentIP");
            return _de.ExecuteDataTable("Role_Update_UserPasswordUser", "@Message", out message, HttpContext.Current.User.Identity.Name, OldPassword, NewPassword, HttpContext.Current.Request.UserHostAddress);
        }
        #endregion


        //today 25 nov 2014

        #region Role_Insert_UserCreativeTalentTemp
        public DataTable Role_Insert_UserCreativeTalentTemp(string Reg_UserCreativeTalentTemp_Name, string Reg_UserCreativeTalentTemp_Email, string Reg_UserCreativeTalentTemp_MemberID, string Reg_UserCreativeTalentTemp_Other, out object message)
        {
            _de.ParaNameArray("@Reg_UserCreativeTalentTemp_Name", "@Reg_UserCreativeTalentTemp_Email", "@Reg_UserCreativeTalentTemp_MemberID", "@Reg_UserCreativeTalentTemp_Other");
            return _de.ExecuteDataTable("Role_Insert_UserCreativeTalentTemp", "@Message", out message, Reg_UserCreativeTalentTemp_Name, Reg_UserCreativeTalentTemp_Email, Reg_UserCreativeTalentTemp_MemberID, Reg_UserCreativeTalentTemp_Other);
        }
        #endregion

        #region Role_Select_UserCreativeTalentTemp
        public DataTable Role_Select_UserCreativeTalentTemp(string Reg_UserCreativeTalentTemp_VarificationCode, out object message)
        {
            _de.ParaNameArray("@Reg_UserCreativeTalentTemp_VarificationCode");
            return _de.ExecuteDataTable("Role_Select_UserCreativeTalentTemp", "@Message", out message, Reg_UserCreativeTalentTemp_VarificationCode);
        }
        #endregion

        #region Role_Select_ProductionSubMemberName
        public DataTable Role_Select_ProductionSubMemberName(string Reg_ProductionSub_UID)
        {
            _de.ParaNameArray("@Reg_ProductionSub_UID");
            return _de.ExecuteDataTable("Role_Select_ProductionSubMemberName", Reg_ProductionSub_UID);
        }
        #endregion

        #region Role_Insert_UserCreativeTalent
        public DataTable Role_Insert_UserCreativeTalent(string Reg_UserCreativeTalent_UID, string Reg_UserCreativeTalent_Password, string Reg_UserCreativeTalent_MemberNameOther, DataTable dtMember, out object message)
        {
            DataSet ds = new DataSet();
            ds.Merge(dtMember);

            _de.ParaNameArray("@Reg_UserCreativeTalent_UID", "@Reg_UserCreativeTalent_Password", "@Reg_UserCreativeTalent_MemberNameOther", "@XML", "@CurrentIP");
            return _de.ExecuteDataTable("Role_Insert_UserCreativeTalent", "@Message", out message, Reg_UserCreativeTalent_UID, Reg_UserCreativeTalent_Password, Reg_UserCreativeTalent_MemberNameOther, ds.GetXml(), HttpContext.Current.Request.UserHostAddress);
        }
        #endregion

        //30-12-14

        #region Role_Set_UserProfileImage()
        public int Role_Set_UserProfileImage(byte[] picture_Image, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Reg_UserAddress_Image");
            return _de.ExecuteNonQuery("Role_Set_UserProfileImage", "@Message", out message, HttpContext.Current.User.Identity.Name, picture_Image);
        }
        #endregion

        #region Role_Get_UserProfileImage()
        public DataTable Role_Get_UserProfileImage()
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Role_Get_UserProfileImage", HttpContext.Current.User.Identity.Name);
        }
        #endregion

        #region Role_Get_UserProfileImageWithUID
        public object Role_Get_UserProfileImageWithUID(string Reg_User_LoginName)
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteScalar("Role_Get_UserProfileImage", Reg_User_LoginName);
        }
        #endregion

        #region Role_Update_UserDetail()
        public DataTable Role_Update_UserDetail(string Reg_UserAddress_Name, string Reg_UserAddress_Country, string Reg_UserAddress_State, string Reg_UserAddress_CITY, string Reg_UserAddress_Street, string Reg_UserAddress_Address, string Reg_UserAddress_ZipCode, string Reg_UserAddress_Mobile, string Reg_UserAddress_Email, string Reg_User_LoginName, string Reg_UserAddressBiography, out object message)
        {
            _de.ParaNameArray("@Reg_UserAddress_Name", "@Reg_UserAddress_Country", "@Reg_UserAddress_State", "@Reg_UserAddress_CITY", "@Reg_UserAddress_Street", "@Reg_UserAddress_Address", "@Reg_UserAddress_ZipCode", "@Reg_UserAddress_Mobile", "@Reg_UserAddress_Email", "@LoginName", "@CurrentIP", "@Reg_UserAddressBiography");
            return _de.ExecuteDataTable("Role_Update_UserDetail", "@Message", out message, Reg_UserAddress_Name, Reg_UserAddress_Country, Reg_UserAddress_State, Reg_UserAddress_CITY, Reg_UserAddress_Street, Reg_UserAddress_Address, Reg_UserAddress_ZipCode, Reg_UserAddress_Mobile, Reg_UserAddress_Email, Reg_User_LoginName, HttpContext.Current.Request.UserHostAddress, Reg_UserAddressBiography);
        }
        public int Role_Update_UserDetail(string Reg_UserAddress_Name, string Reg_UserAddress_Country, string Reg_UserAddress_State, string Reg_UserAddress_CITY, string Reg_UserAddress_Street, string Reg_UserAddress_ADDRESS, string Reg_UserAddress_ZipCode, string Reg_UserAddress_Mobile, string Reg_UserAddress_Email, string Reg_User_LoginName, string Reg_User_LoginPassword, string Reg_User_SponsorID, string Reg_User_SponsorIDOld, string Reg_UserAddressBiography, string Reg_User_Status, out object message)
        {
            _de.ParaNameArray("@Reg_UserAddress_Name", "@Reg_UserAddress_Country", "@Reg_UserAddress_State", "@Reg_UserAddress_CITY", "@Reg_UserAddress_Street", "@Reg_UserAddress_ADDRESS", "@Reg_UserAddress_ZipCode", "@Reg_UserAddress_Mobile", "@Reg_UserAddress_Email", "@Reg_User_LoginName", "@Reg_User_LoginPassword", "@Reg_User_SponsorID", "@Reg_User_SponsorIDOld", "@Reg_UserAddressBiography", "@Reg_User_Status");
            return _de.ExecuteNonQuery("Role_Update_UserDetailAdmin", "@Message", out message, Reg_UserAddress_Name, Reg_UserAddress_Country, Reg_UserAddress_State, Reg_UserAddress_CITY, Reg_UserAddress_Street, Reg_UserAddress_ADDRESS, Reg_UserAddress_ZipCode, Reg_UserAddress_Mobile, Reg_UserAddress_Email, Reg_User_LoginName, Reg_User_LoginPassword, Reg_User_SponsorID, Reg_User_SponsorIDOld, Reg_UserAddressBiography, Reg_User_Status);
        }
        #endregion

        #region Role_Select_UserDetail()
        public DataTable Role_Select_UserDetail(string Reg_User_LoginName)
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Role_Select_UserDetail", Reg_User_LoginName);
        }
        public DataTable Role_Select_UserDetail(string Reg_User_LoginName, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Role_Select_UserDetailAdmin", "@Message", out message, Reg_User_LoginName);
        }
        public DataTable Role_Select_TalentCategoryType()
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Role_Select_TalentCategoryType", HttpContext.Current.User.Identity.Name);
        }
        #endregion

        #region Role_Select_UserProfileBiography
        public DataSet Role_Select_UserProfileBiography(string Reg_User_LoginName)
        {
            _de.ParaNameArray("@Reg_UserLoginName");
            return _de.ExecuteDataSet("Role_Select_UserProfileBiography", Reg_User_LoginName);
        }
        public DataSet Role_Select_UserProfileBiography()
        {
            _de.ParaNameArray("@Reg_UserLoginName");
            return _de.ExecuteDataSet("Role_Select_UserProfileBiography", HttpContext.Current.User.Identity.Name);
        }
        #endregion

        public DataTable Role_Get_UserCreativeTalentSong_UID(int UID)
        {
            _de.ParaNameArray("@UID");
            return _de.ExecuteDataTable("Role_Get_UserCreativeTalentSong_UID", UID);
        }

        #region Role_Select_UserProfileDetail
        public DataTable Role_Select_UserProfileDetail()
        {
            _de.ParaNameArray("@Reg_UserLoginName");
            return _de.ExecuteDataTable("Role_Select_UserProfileDetail", HttpContext.Current.User.Identity.Name);
        }
        #endregion

        #region Role_Select_UserMedia
        public DataTable Role_Select_UserMedia()
        {
            _de.ParaNameArray("@Reg_UserLoginName");
            return _de.ExecuteDataTable("Role_Select_UserMedia", HttpContext.Current.User.Identity.Name);
        }
        public DataTable Role_Select_UserMedia(string Reg_User_LoginName)
        {
            _de.ParaNameArray("@Reg_UserLoginName");
            return _de.ExecuteDataTable("Role_Select_UserMedia", Reg_User_LoginName);
        }
        public DataTable Role_Select_AdminMedia()
        {
            return _de.ExecuteDataTable("Role_Select_AdminMedia");
        }
        #endregion

        #region Role_Select_Media
        public DataTable Role_Select_Media(string Master_Media_Type)
        {
            _de.ParaNameArray("@Master_Media_Type");
            return _de.ExecuteDataTable("Role_Select_AudioMedia", Master_Media_Type);
        }
        #endregion

        #region Role_Insert_Rating
        public int Role_Insert_Rating(string Master_Rating_CreativeTalent_UID, int Master_Rating_Rate, out object message)
        {
            _de.ParaNameArray("@Reg_UserLoginName", "@Master_Rating_CreativeTalent_UID", "@Master_Rating_Rate");
            return _de.ExecuteNonQuery("Role_Insert_Rating", "@Message", out message, HttpContext.Current.User.Identity.Name, Master_Rating_CreativeTalent_UID, Master_Rating_Rate);
        }
        #endregion

        #region Role_Select_UserRefferalReport
        public DataSet Role_Select_UserRefferalReport()
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataSet("Role_Select_UserRefferalReport", HttpContext.Current.User.Identity.Name);
        }
        #endregion

        #region GetPayableFee
        public object GetPayableFee()
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteScalarFunction("SELECT dbo.GetPayableFee(@Reg_User_LoginName)", HttpContext.Current.User.Identity.Name);
        }

        public DataTable Role_Insert_AllTalentTemp(string Reg_User_RoleName_Temp, string Master_Talent_Category_UID, string Master_Talent_SubCategory_UID, string Master_Talent_Sub_SubCategory_UID, string Reg_UserAddressBiography, string Reg_UserAddress_Name, string Reg_UserAddress_Country, string Reg_UserAddress_State, string Reg_UserAddress_City, string Reg_UserAddress_Address, string Reg_UserAddress_ZipCode, string Reg_UserAddress_Mobile, string Reg_UserAddress_Email, string Reg_User_LoginPassword, string Reg_UserLevel_SpId, string Reg_UserAddress_College, string Reg_UserAddress_Major, out object message)
        {
            _de.ParaNameArray("@Reg_User_RoleName_Temp", "@Master_Talent_Category_UID", "@Master_Talent_SubCategory_UID", "@Master_Talent_Sub_SubCategory_UID", "@Reg_UserAddressBiography", "@Reg_UserAddress_Name", "@Reg_UserAddress_Country", "@Reg_UserAddress_State", "@Reg_UserAddress_City", "@Reg_UserAddress_Address", "@Reg_UserAddress_ZipCode", "@Reg_UserAddress_Mobile", "@Reg_UserAddress_Email", "@Reg_User_LoginPassword", "@Reg_UserLevel_SpId", "@Reg_UserAddress_College", "@Reg_UserAddress_Major");
            return _de.ExecuteDataTable("Role_Insert_AllTalentTemp", "@Message", out message, Reg_User_RoleName_Temp, Master_Talent_Category_UID, Master_Talent_SubCategory_UID, Master_Talent_Sub_SubCategory_UID, Reg_UserAddressBiography, Reg_UserAddress_Name, Reg_UserAddress_Country, Reg_UserAddress_State, Reg_UserAddress_City, Reg_UserAddress_Address, Reg_UserAddress_ZipCode, Reg_UserAddress_Mobile, Reg_UserAddress_Email, Reg_User_LoginPassword, Reg_UserLevel_SpId, Reg_UserAddress_College, Reg_UserAddress_Major);
        }

        public DataTable Role_Insert_AllTalentUpgradeTemp(string Reg_User_RoleName_Temp, string Master_Talent_Category_UID, string Master_Talent_SubCategory_UID, string Master_Talent_Sub_SubCategory_UID, string Reg_UserAddressBiography, string Reg_UserAddress_Name, string Reg_UserAddress_Country, string Reg_UserAddress_State, string Reg_UserAddress_City, string Reg_UserAddress_Address, string Reg_UserAddress_ZipCode, string Reg_UserAddress_Mobile, string Reg_UserAddress_Email, string Reg_User_LoginPassword, string Reg_UserLevel_SpId, string Reg_UserAddress_College, string Reg_UserAddress_Major, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Reg_User_RoleName_Temp", "@Master_Talent_Category_UID", "@Master_Talent_SubCategory_UID", "@Master_Talent_Sub_SubCategory_UID", "@Reg_UserAddressBiography", "@Reg_UserAddress_Name", "@Reg_UserAddress_Country", "@Reg_UserAddress_State", "@Reg_UserAddress_City", "@Reg_UserAddress_Address", "@Reg_UserAddress_ZipCode", "@Reg_UserAddress_Mobile", "@Reg_UserAddress_Email", "@Reg_User_LoginPassword", "@Reg_UserLevel_SpId", "@Reg_UserAddress_College", "@Reg_UserAddress_Major");
            return _de.ExecuteDataTable("Role_Insert_AllTalentUpgradeTemp", "@Message", out message, HttpContext.Current.User.Identity.Name, Reg_User_RoleName_Temp, Master_Talent_Category_UID, Master_Talent_SubCategory_UID, Master_Talent_Sub_SubCategory_UID, Reg_UserAddressBiography, Reg_UserAddress_Name, Reg_UserAddress_Country, Reg_UserAddress_State, Reg_UserAddress_City, Reg_UserAddress_Address, Reg_UserAddress_ZipCode, Reg_UserAddress_Mobile, Reg_UserAddress_Email, Reg_User_LoginPassword, Reg_UserLevel_SpId, Reg_UserAddress_College, Reg_UserAddress_Major);
        }

        public DataTable Role_Insert_MediaPromoterTemp(string Reg_User_RoleName_Temp, string Reg_UserAddress_Name, string Reg_UserAddress_Country, string Reg_UserAddress_State, string Reg_UserAddress_City, string Reg_UserAddress_Address, string Reg_UserAddress_ZipCode, string Reg_UserAddress_Mobile, string Reg_UserAddress_Email, string Reg_User_LoginPassword, string Reg_UserLevel_SpId, string Reg_UserAddress_College, string Reg_UserAddress_Major, out object message)
        {
            _de.ParaNameArray("@Reg_User_RoleName_Temp", "@Reg_UserAddress_Name", "@Reg_UserAddress_Country", "@Reg_UserAddress_State", "@Reg_UserAddress_City", "@Reg_UserAddress_Address", "@Reg_UserAddress_ZipCode", "@Reg_UserAddress_Mobile", "@Reg_UserAddress_Email", "@Reg_User_LoginPassword", "@Reg_UserLevel_SpId", "@Reg_UserAddress_College", "@Reg_UserAddress_Major");
            return _de.ExecuteDataTable("Role_Insert_FanPromoterTemp", "@Message", out message, Reg_User_RoleName_Temp, Reg_UserAddress_Name, Reg_UserAddress_Country, Reg_UserAddress_State, Reg_UserAddress_City, Reg_UserAddress_Address, Reg_UserAddress_ZipCode, Reg_UserAddress_Mobile, Reg_UserAddress_Email, Reg_User_LoginPassword, Reg_UserLevel_SpId, Reg_UserAddress_College, Reg_UserAddress_Major);
        }
        public DataTable Role_Insert_MediaPromoterUpgradeTemp(string Reg_User_RoleName_Temp, string Reg_UserAddress_Name, string Reg_UserAddress_Country, string Reg_UserAddress_State, string Reg_UserAddress_City, string Reg_UserAddress_Address, string Reg_UserAddress_ZipCode, string Reg_UserAddress_Mobile, string Reg_UserAddress_Email, string Reg_User_LoginPassword, string Reg_UserLevel_SpId, string Reg_UserAddress_College, string Reg_UserAddress_Major, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Reg_User_RoleName_Temp", "@Reg_UserAddress_Name", "@Reg_UserAddress_Country", "@Reg_UserAddress_State", "@Reg_UserAddress_City", "@Reg_UserAddress_Address", "@Reg_UserAddress_ZipCode", "@Reg_UserAddress_Mobile", "@Reg_UserAddress_Email", "@Reg_User_LoginPassword", "@Reg_UserLevel_SpId", "@Reg_UserAddress_College", "@Reg_UserAddress_Major");
            return _de.ExecuteDataTable("Role_Insert_MediaPromoterUpgradeTemp", "@Message", out message, HttpContext.Current.User.Identity.Name, Reg_User_RoleName_Temp, Reg_UserAddress_Name, Reg_UserAddress_Country, Reg_UserAddress_State, Reg_UserAddress_City, Reg_UserAddress_Address, Reg_UserAddress_ZipCode, Reg_UserAddress_Mobile, Reg_UserAddress_Email, Reg_User_LoginPassword, Reg_UserLevel_SpId, Reg_UserAddress_College, Reg_UserAddress_Major);
        }
        #endregion

        #region Role_InsertUserWithPayment
        public DataTable Role_InsertNewUserWithPayment(string Reg_UserTemp_UID, string Shopping_PurchaseReg_Gateway, string Shopping_PurchaseReg_TransactionID, decimal Shopping_PurchaseReg_PaidAmount, string Shopping_PurchaseReg_Date, out object message)
        {
            _de.ParaNameArray("@Reg_UserTemp_UID", "@Shopping_PurchaseReg_Gateway", "@Shopping_PurchaseReg_TransactionID", "@Shopping_PurchaseReg_PaidAmount", "@Shopping_PurchaseReg_Date", "@CurrentIP");
            return _de.ExecuteDataTable("Role_InsertNewUserWithPayment", "@Message", out message, Reg_UserTemp_UID, Shopping_PurchaseReg_Gateway, Shopping_PurchaseReg_TransactionID, Shopping_PurchaseReg_PaidAmount, _de.ConvertedDate(Shopping_PurchaseReg_Date), HttpContext.Current.Request.UserHostAddress);
        }
        public DataTable Role_InsertUpgradeUserWithPayment(string Reg_UserTemp_UID, string Shopping_PurchaseReg_Gateway, string Shopping_PurchaseReg_TransactionID, decimal Shopping_PurchaseReg_PaidAmount, string Shopping_PurchaseReg_Date, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Reg_UserTemp_UID", "@Shopping_PurchaseReg_Gateway", "@Shopping_PurchaseReg_TransactionID", "@Shopping_PurchaseReg_PaidAmount", "@Shopping_PurchaseReg_Date", "@CurrentIP");
            return _de.ExecuteDataTable("Role_InsertUpgradeUserWithPayment", "@Message", out message, HttpContext.Current.User.Identity.Name, Reg_UserTemp_UID, Shopping_PurchaseReg_Gateway, Shopping_PurchaseReg_TransactionID, Shopping_PurchaseReg_PaidAmount, _de.ConvertedDate(Shopping_PurchaseReg_Date), HttpContext.Current.Request.UserHostAddress);
        }

        public DataTable Role_InsertNewMediaPromoterWithPayment(string Reg_UserTemp_UID, string Shopping_PurchaseReg_Gateway, string Shopping_PurchaseReg_TransactionID, decimal Shopping_PurchaseReg_PaidAmount, string Shopping_PurchaseReg_Date, out object message)
        {
            _de.ParaNameArray("@Reg_UserTemp_UID", "@Shopping_PurchaseReg_Gateway", "@Shopping_PurchaseReg_TransactionID", "@Shopping_PurchaseReg_PaidAmount", "@Shopping_PurchaseReg_Date", "@CurrentIP");
            return _de.ExecuteDataTable("Role_InsertNewFanPromoterWithPayment", "@Message", out message, Reg_UserTemp_UID, Shopping_PurchaseReg_Gateway, Shopping_PurchaseReg_TransactionID, Shopping_PurchaseReg_PaidAmount, _de.ConvertedDate(Shopping_PurchaseReg_Date), HttpContext.Current.Request.UserHostAddress);
        }
        public DataTable Role_InsertUpgradeMediaPromoterWithPayment(string Reg_UserTemp_UID, string Shopping_PurchaseReg_Gateway, string Shopping_PurchaseReg_TransactionID, decimal Shopping_PurchaseReg_PaidAmount, string Shopping_PurchaseReg_Date, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Reg_UserTemp_UID", "@Shopping_PurchaseReg_Gateway", "@Shopping_PurchaseReg_TransactionID", "@Shopping_PurchaseReg_PaidAmount", "@Shopping_PurchaseReg_Date", "@CurrentIP");
            return _de.ExecuteDataTable("Role_InsertUpgradeMediaPromoterWithPayment", "@Message", out message, HttpContext.Current.User.Identity.Name, Reg_UserTemp_UID, Shopping_PurchaseReg_Gateway, Shopping_PurchaseReg_TransactionID, Shopping_PurchaseReg_PaidAmount, _de.ConvertedDate(Shopping_PurchaseReg_Date), HttpContext.Current.Request.UserHostAddress);
        }
        #endregion

        #region GetAlreadyPaid
        public object GetAlreadyPaid()
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteScalarFunction("SELECT dbo.GetAlreadyPaid(@Reg_User_LoginName)", HttpContext.Current.User.Identity.Name);
        }
        #endregion

        #region Role_Get_TalentSearch
        public DataTable Role_Get_TalentSearch(string TalentLoginName, string Reg_UserAddress_Country, string Reg_UserAddress_State, string Master_Talent_Category_UID, string Master_Talent_SubCategory_UID)
        {
            _de.ParaNameArray("@TalentLoginName", "@Reg_User_LoginName", "@Reg_UserAddress_Country", "@Reg_UserAddress_State", "@Master_Talent_Category_UID", "@Master_Talent_SubCategory_UID");
            return _de.ExecuteDataTable("Role_Get_TalentSearch", TalentLoginName, HttpContext.Current.User.Identity.Name, Reg_UserAddress_Country, Reg_UserAddress_State, Master_Talent_Category_UID, Master_Talent_SubCategory_UID);
        }
        #endregion

        #region Role_Get_CreativeTalentNearByEvents
        public DataTable Role_Get_CreativeTalentNearByEvents()
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Role_Get_CreativeTalentNearByEvents", HttpContext.Current.User.Identity.Name);
        }

        public DataTable Role_Get_CreativeTalentNearByEventsSearch(string DateFrom, string DateTo, string Event_Country)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@DateFrom", "@DateTo", "@Event_Country");
            return _de.ExecuteDataTable("Role_Get_CreativeTalentNearByEventsSearch", HttpContext.Current.User.Identity.Name, DateFrom, DateTo, Event_Country);
        }
        #endregion

        #region Role_Get_MediaPromoterEarningDetail()
        public DataTable Role_Get_MediaPromoterEarningDetail()
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Role_Get_FanPromoterEarningDetail", HttpContext.Current.User.Identity.Name);
        }
        #endregion

        #region Role_DeleteRecordByTempUID
        public int Role_DeleteRecordByTempUID(string Reg_User_TempUID)
        {
            _de.ParaNameArray("@Reg_User_TempUID");
            return _de.ExecuteNonQuery("Role_DeleteRecordByTempUID", Reg_User_TempUID);
        }
        #endregion

        #region Role_Get_TalentBioImage()
        public DataTable Role_Get_TalentBioImage()
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Role_Get_TalentBioImage", HttpContext.Current.User.Identity.Name);
        }
        #endregion

        #region Role_Set_TalentBioImage()
        public int Role_Set_TalentBioImage(string Reg_User_BioImage_ImageUrl, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Reg_User_BioImage_ImageUrl");
            return _de.ExecuteNonQuery("Role_Set_TalentBioImage", "@Message", out message, HttpContext.Current.User.Identity.Name, Reg_User_BioImage_ImageUrl);
        }
        #endregion

        #region Role_Delete_TalentBioImage()
        public int Role_Delete_TalentBioImage(string Reg_User_BioImage_UID, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Reg_User_BioImage_UID");
            return _de.ExecuteNonQuery("Role_Delete_TalentBioImage", "@Message", out message, HttpContext.Current.User.Identity.Name, Reg_User_BioImage_UID);
        }

        public int Role_Change_TalentBioImage(string Reg_User_BioImage_UID, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Reg_User_BioImage_UID");
            return _de.ExecuteNonQuery("Role_Change_TalentBioImage", "@Message", out message, HttpContext.Current.User.Identity.Name, Reg_User_BioImage_UID);
        }
        #endregion

        #region Role_Set_BioGraphText()
        public int Role_Set_BioGraphText(string Reg_UserAddressBiography, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Reg_UserAddressBiography");
            return _de.ExecuteNonQuery("Role_Set_BioGraphText", "@Message", out message, HttpContext.Current.User.Identity.Name, Reg_UserAddressBiography);
        }
        #endregion

        #region Role_Check_TalentRating
        public int Role_Check_TalentRating(string Master_Rating_Talent_LoginName, out object message)
        {
            _de.ParaNameArray("@Reg_UserLoginName", "@Master_Rating_Talent_LoginName");
            return Convert.ToInt32(_de.ExecuteScalar("Role_Check_TalentRating", "@message", out message, HttpContext.Current.User.Identity.Name, Master_Rating_Talent_LoginName));
        }
        #endregion

        #region Role_Insert_TalentRating
        public int Role_Insert_TalentRating(string Master_Rating_Talent_LoginName, int Master_Rating_Rate, out object message)
        {
            _de.ParaNameArray("@Reg_UserLoginName", "@Master_Rating_Talent_LoginName", "@Master_Rating_Rate");
            return _de.ExecuteNonQuery("Role_Insert_TalentRating", "@Message", out message, HttpContext.Current.User.Identity.Name, Master_Rating_Talent_LoginName, Master_Rating_Rate);
        }
        #endregion

        #region Role_Get_TalentRole
        public object Role_Get_TalentRole()
        {
            _de.ParaNameArray("@Reg_UserLoginName");
            return _de.ExecuteScalarFunction("SELECT dbo.Role_Get_TalentRole(@Reg_UserLoginName)", HttpContext.Current.User.Identity.Name);
        }
        public object Role_Get_TalentRole1()
        {
            _de.ParaNameArray("@Reg_UserLoginName");
            return _de.ExecuteScalarFunction("SELECT dbo.Role_Get_TalentRole1(@Reg_UserLoginName)", HttpContext.Current.User.Identity.Name);
        }
        public object Role_Get_TalentRole2()
        {
            _de.ParaNameArray("@Reg_UserLoginName");
            return _de.ExecuteScalarFunction("SELECT dbo.Role_Get_TalentRole2(@Reg_UserLoginName)", HttpContext.Current.User.Identity.Name);
        }
        #endregion

        #region Role_Get_TalentRole
        public int Role_SetUserTalentSelection(DataTable dt, out object message)
        {
            dt.TableName = "Table";
            DataSet ds = new DataSet();
            ds.Merge(dt);

            _de.ParaNameArray("@Reg_UserLoginName", "@XML");
            return _de.ExecuteNonQuery("Role_SetUserTalentSelection", "@Message", out message, HttpContext.Current.User.Identity.Name, ds.GetXml());
        }
        #endregion

        #region Role_GetUserActivityLog
        /// <summary>
        /// Use for user recent activities log. Use it user panel.
        /// </summary>
        /// <returns></returns>
        public DataTable Role_GetUserActivityLog()
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Com_Role_GetUserActivityLog", HttpContext.Current.User.Identity.Name);
        }
        public DataTable Role_GetUserActivityLogReport(string Reg_UserActivityLog_DateFrom, string Reg_UserActivityLog_DateTo)
        {
            _de.ParaNameArray("@Reg_UserActivityLog_DateFrom", "@Reg_UserActivityLog_DateTo", "@Reg_User_LoginName");
            return _de.ExecuteDataTable("Com_Role_GetUserActivityLogReport", Reg_UserActivityLog_DateFrom, Reg_UserActivityLog_DateTo, HttpContext.Current.User.Identity.Name);
        }
        public DataTable Role_GetUserActivityForHeader()
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Com_Role_GetUserActivityForHeader", HttpContext.Current.User.Identity.Name);
        }
        public DataTable Role_GetAdminActivityForHeader(long UserID)
        {
            _de.ParaNameArray("@UserID");
            return _de.ExecuteDataTable("Role_GetAdminActivityForHeader", UserID);
        }
        /// <summary>
        /// Use for user recent activities log. Use it admin panel.
        /// </summary>
        /// <param name="Reg_User_LoginName"></param>
        /// <returns></returns>
        public DataTable Role_GetUserActivityLog(string Reg_User_LoginName)
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Com_Role_GetUserActivityLog", Reg_User_LoginName);
        }
        #endregion

        public DataTable Role_Insert_MediaPromoterAdmin(string Reg_User_RoleName, string Reg_UserAddress_Name, string Reg_UserAddress_Country, string Reg_UserAddress_State, string Reg_UserAddress_City, string Reg_UserAddress_Address, string Reg_UserAddress_ZipCode, string Reg_UserAddress_Mobile, string Reg_UserAddress_Email, string Reg_User_LoginPassword, string Reg_UserLevel_SpId, string Reg_UserAddress_College, string Reg_UserAddress_Major, string Reg_UserAddressBiography, string Master_Pin_Code, out object message)
        {
            _de.ParaNameArray("@Reg_User_RoleName", "@Reg_UserAddress_Name", "@Reg_UserAddress_Country", "@Reg_UserAddress_State", "@Reg_UserAddress_City", "@Reg_UserAddress_Address", "@Reg_UserAddress_ZipCode", "@Reg_UserAddress_Mobile", "@Reg_UserAddress_Email", "@Reg_User_LoginPassword", "@Reg_UserLevel_SpId", "@Reg_UserAddress_College", "@Reg_UserAddress_Major", "@Reg_UserAddressBiography", "@Master_Pin_Code");
            return _de.ExecuteDataTable("Role_Insert_MediaPromoterAdmin", "@Message", out message, Reg_User_RoleName, Reg_UserAddress_Name, Reg_UserAddress_Country, Reg_UserAddress_State, Reg_UserAddress_City, Reg_UserAddress_Address, Reg_UserAddress_ZipCode, Reg_UserAddress_Mobile, Reg_UserAddress_Email, Reg_User_LoginPassword, Reg_UserLevel_SpId, Reg_UserAddress_College, Reg_UserAddress_Major, Reg_UserAddressBiography, Master_Pin_Code);
        }

        #region Role_Get_UserTalentCategory/SubCategory
        public DataTable Role_Get_UserTalentCategory()
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Role_Get_UserTalentCategory", HttpContext.Current.User.Identity.Name);
        }
        public DataTable Role_Get_UserTalentSubCategory(string Category)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Category");
            return _de.ExecuteDataTable("Role_Get_UserTalentSubCategory", HttpContext.Current.User.Identity.Name, Category);
        }

        public DataTable Role_Get_UserPopup(string loginName, out object popupSub)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region Fan
        public int Role_Check_TalentFan(string Reg_User_Talent_LoginName)
        {
            _de.ParaNameArray("@Reg_UserLoginName", "@Reg_User_Talent_LoginName");
            return Convert.ToInt32(_de.ExecuteScalar("Role_Check_TalentFan", HttpContext.Current.User.Identity.Name, Reg_User_Talent_LoginName));
        }
        public int Role_Insert_TalentFan(string Reg_User_Talent_LoginName, out object message)
        {
            _de.ParaNameArray("@Reg_UserLoginName", "@Reg_User_Talent_LoginName");
            return _de.ExecuteNonQuery("Role_Insert_TalentFan", "@Message", out message, HttpContext.Current.User.Identity.Name, Reg_User_Talent_LoginName);
        }
        public int Role_Delete_TalentFan(string Reg_User_Talent_LoginName, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Reg_User_Talent_LoginName");
            return _de.ExecuteNonQuery("Role_Delete_TalentFan", "@Message", out message, HttpContext.Current.User.Identity.Name, Reg_User_Talent_LoginName);
        }
        public DataTable Role_Insert_TalentFanBaseShare(string Reg_User_Talent_LoginName, out object message)
        {
            _de.ParaNameArray("@Reg_UserLoginName", "@Reg_User_Talent_LoginName");
            return _de.ExecuteDataTable("Role_Insert_TalentFanBaseShare", "@Message", out message, HttpContext.Current.User.Identity.Name, Reg_User_Talent_LoginName);
        }
        public DataTable Role_GetUserFanBaseRequest()
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Role_GetUserFanBaseRequest", HttpContext.Current.User.Identity.Name);
        }
        public object GetTalentFanBaseRequestCount()
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteScalarFunction("SELECT dbo.GetTalentFanBaseRequestCount(@Reg_User_LoginName)", HttpContext.Current.User.Identity.Name);
        }
        public int Role_Reject_TalentFanBaseRequest(string Reg_UserFanBaseShare_UID, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Reg_UserFanBaseShare_UID");
            return _de.ExecuteNonQuery("Role_Reject_TalentFanBaseRequest", "@Message", out message, HttpContext.Current.User.Identity.Name, Reg_UserFanBaseShare_UID);
        }
        public DataTable Role_Accept_TalentFanBaseRequest(string Reg_UserFanBaseShare_UID, out object message)
        {
            _de.ParaNameArray("@Reg_UserLoginName", "@Reg_UserFanBaseShare_UID");
            return _de.ExecuteDataTable("Role_Accept_TalentFanBaseRequest", "@Message", out message, HttpContext.Current.User.Identity.Name, Reg_UserFanBaseShare_UID);
        }
        #endregion

        public DataTable Role_Select_GetAdminDashBoard()
        {
            return _de.ExecuteDataTable("Role_Select_GetAdminDashBoard");
        }

        public DataTable Role_Select_UserAlbumList()
        {
            _de.ParaNameArray("@Reg_UserLoginName");
            return _de.ExecuteDataTable("Role_Select_UserAlbumList", HttpContext.Current.User.Identity.Name);
        }

        #region Role_GetAlbum
        public DataTable Role_GetAlbumStatus(string Reg_User_Album_UID)
        {
            _de.ParaNameArray("@Reg_User_Album_UID");
            return _de.ExecuteDataTable("Role_GetAlbumStatus", Reg_User_Album_UID);
        }
        public int Role_UpdateAlbumStatus(string Reg_User_Album_UID, string Reg_User_Album_Active, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Reg_User_Album_UID", "@Reg_User_Album_Active");
            return _de.ExecuteNonQuery("Role_UpdateAlbumStatus", "@Message", out message, HttpContext.Current.User.Identity.Name, Reg_User_Album_UID, Reg_User_Album_Active);
        }
        public int Role_DeleteUserAlbum(string Reg_User_Album_UID, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Reg_User_Album_UID");
            return _de.ExecuteNonQuery("Role_DeleteUserAlbum", "@Message", out message, HttpContext.Current.User.Identity.Name, Reg_User_Album_UID);
        }
        #endregion
    }
}
