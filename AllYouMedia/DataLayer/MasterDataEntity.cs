using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;

namespace BusinessEntity.ConcreateEntity
{
    public class MasterDataEntity
    {
        private DataEntity _de;

        #region MasterDataEntity
        public MasterDataEntity()
        {
            _de = new DataEntity();
        }
        #endregion

        #region Master_Select_ProductType
        public DataTable Master_Select_ProductType()
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("Text", typeof(string));
            DataColumn dc2 = new DataColumn("Value", typeof(string));
            dt.Columns.Add(dc1); dt.Columns.Add(dc2);

            dt.Rows.Add("Other", "Other");
            dt.Rows.Add("Featured", "Featured");
            return dt;
        }
        #endregion

        #region Master_Select_SizeDDL
        public DataTable Master_Select_SizeDDL()
        {
            return _de.ExecuteDataTable("Master_Select_SizeDDL");
        }
        #endregion

        #region Master_Select_Gender
        public DataTable Master_Select_Gender()
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("Text", typeof(string));
            DataColumn dc2 = new DataColumn("Value", typeof(string));
            dt.Columns.Add(dc1); dt.Columns.Add(dc2);

            dt.Rows.Add("MENS", "MENS");
            dt.Rows.Add("WOMENS", "WOMENS");
            dt.Rows.Add("UNISEX", "UNISEX");
            return dt;
        }
        #endregion

        #region Master_Select_SupplierName
        public DataTable Master_Select_SupplierName()
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("Text", typeof(string));
            DataColumn dc2 = new DataColumn("Value", typeof(string));
            dt.Columns.Add(dc1); dt.Columns.Add(dc2);

            dt.Rows.Add("Light In The Box", "Light In The Box");
            dt.Rows.Add("Mini In The Box", "Mini In The Box");
            dt.Rows.Add("Creative talent", "Creative talent");

            return dt;
        }
        #endregion

        #region Select_MenuList
        public DataTable Select_MenuList()
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("Text", typeof(string));
            DataColumn dc2 = new DataColumn("Value", typeof(string));
            dt.Columns.Add(dc1); dt.Columns.Add(dc2);

            dt.Rows.Add("EVENTS & TICKETING", "EVENTS & TICKETING");
            dt.Rows.Add("CLOTHING APPAREL", "CLOTHING APPAREL");
            dt.Rows.Add("MUSIC", "MUSIC");
            dt.Rows.Add("FILM", "FILM");
            dt.Rows.Add("E BOOKS", "E BOOKS");
            dt.Rows.Add("ELECTRONICS", "ELECTRONICS");
            dt.Rows.Add("JEWELRY", "JEWELRY");
            dt.Rows.Add("HAND CRAFTED CREATIONS", "HAND CRAFTED CREATIONS");
            return dt;
        }
        #endregion

        #region Master_Select_CategoryList
        public DataTable Master_Select_CategoryList()
        {
            return _de.ExecuteDataTable("Master_Select_CategoryList");
        }
        public DataTable Master_Select_CategoryList(string Master_TalentCategory_UID)
        {
            _de.ParaNameArray("@Master_TalentCategory_UID");
            return _de.ExecuteDataTable("Master_Select_CategoryList", Master_TalentCategory_UID);
        }
        #endregion

        #region Master_Select_ProductionType
        public DataTable Master_Select_ProductionType()
        {
            return _de.ExecuteDataTable("Master_Select_ProductionType");
        }
        #endregion

        #region Master_Select_ProductionSubwithUID
        public DataTable Master_Select_ProductionSubwithUID(string Master_Production_UID)
        {
            _de.ParaNameArray("@Master_Production_UID");
            return _de.ExecuteDataTable("Master_Select_ProductionSubwithUID", Master_Production_UID);
        }
        #endregion

        #region Master_Select_CategoryDetail
        public DataTable Master_Select_CategoryDetail()
        {
            return _de.ExecuteDataTable("Master_Select_CategoryDetail");
        }
        public DataTable Master_Select_SuggestedCategoryDetail()
        {
            return _de.ExecuteDataTable("Master_Select_SuggestedCategoryDetail");
        }
        #endregion

        #region Master_Select_TalentCategoryDetail
        public DataTable Master_Select_TalentCategoryDetail()
        {
            return _de.ExecuteDataTable("Master_Select_TalentCategoryDetail");
        }
        #endregion

        #region Master_Select_CategoryDetailWithUID
        public DataTable Master_Select_CategoryDetailWithUID(string Master_Category_UID, out object message)
        {
            _de.ParaNameArray("@Master_Category_UID");
            return _de.ExecuteDataTable("Master_Select_CategoryDetailWithUID", "@Message", out message, Master_Category_UID);
        }
        public DataTable Master_Select_SuggestedCategoryDetailWithUID(string Master_Category_UID, out object message)
        {
            _de.ParaNameArray("@Master_Category_UID");
            return _de.ExecuteDataTable("Master_Select_SuggestedCategoryDetailWithUID", "@Message", out message, Master_Category_UID);
        }
        #endregion

        #region Master_Insert_Category
        public int Master_Insert_Category(string Master_TalentCategory_UID, string Master_Category_Name, out object message)
        {
            _de.ParaNameArray("@Master_TalentCategory_UID", "@Master_Category_Name");
            return _de.ExecuteNonQuery("Master_Insert_Category", "@Message", out message, Master_TalentCategory_UID, Master_Category_Name);
        }
        public int Master_Suggest_Category(string Master_TalentCategory_UID, string Master_Category_Name, out object message)
        {
            _de.ParaNameArray("@Master_TalentCategory_UID", "@Master_Category_Name");
            return _de.ExecuteNonQuery("Master_Suggest_Category", "@Message", out message, Master_TalentCategory_UID, Master_Category_Name);
        }
        #endregion

        #region Master_Update_Category
        public int Master_Update_Category(string Master_Category_UID, string Master_Category_Name, string Master_Category_Menu, out object message)
        {
            _de.ParaNameArray("@Master_Category_UID", "@Master_Category_Name", "@Master_Category_Menu");
            return _de.ExecuteNonQuery("Master_Update_Category", "@Message", out message, Master_Category_UID, Master_Category_Name, Master_Category_Menu);
        }
        public int Master_Update_SuggestedCategory(string Master_Category_UID, string Master_Category_Name, string Master_Category_Menu, out object message)
        {
            _de.ParaNameArray("@Master_Category_UID", "@Master_Category_Name", "@Master_Category_Menu");
            return _de.ExecuteNonQuery("Master_Update_SuggestedCategory", "@Message", out message, Master_Category_UID, Master_Category_Name, Master_Category_Menu);
        }
        #endregion

        #region Master_Select_Category_With_UID
        public DataTable Master_Select_Category_With_UID(string Master_Category_UID, out object message)
        {
            _de.ParaNameArray("@Master_Category_UID");
            return _de.ExecuteDataTable("Master_Select_Category_With_UID", "@Message", out message, Master_Category_UID);
        }
        #endregion

        #region Master_Delete_Category
        public int Master_Delete_Category(string Master_Category_UID, out object message)
        {
            _de.ParaNameArray("Master_Category_UID");
            return _de.ExecuteNonQuery("Master_Delete_Category", "@Message", out message, Master_Category_UID);
        }
        public int Master_Delete_SuggestedCategory(string Master_Category_UID, out object message)
        {
            _de.ParaNameArray("Master_Category_UID");
            return _de.ExecuteNonQuery("Master_Delete_SuggestedCategory", "@Message", out message, Master_Category_UID);
        }
        #endregion

        #region Master_Select_ItemWithCode
        public DataTable Master_Select_ItemWithCode(string Master_Item_Code)
        {
            _de.ParaNameArray("@Master_Item_Code");
            return _de.ExecuteDataTable("Master_Select_ItemWithCode", Master_Item_Code);
        }
        public DataTable Master_Select_ItemWithProductCode(string Master_Item_Code)
        {
            _de.ParaNameArray("@Master_Item_Code");
            return _de.ExecuteDataTable("Master_Select_ItemWithProductCode", Master_Item_Code);
        }
        public DataTable Master_Select_AlbumListWithUID(string Reg_User_Album_UID)
        {
            _de.ParaNameArray("@Reg_User_Album_UID");
            return _de.ExecuteDataTable("Master_Select_AlbumListWithUID", Reg_User_Album_UID);
        }
        public DataTable Master_Select_SongListWithUID(string Reg_User_Document_UID)
        {
            _de.ParaNameArray("@Reg_User_Document_UID");
            return _de.ExecuteDataTable("Master_Select_SongListWithUID", Reg_User_Document_UID);
        }
        #endregion

        #region Master_Select_SimilarPicture
        public DataTable Master_Select_SimilarPicture(string Master_Item_Code)
        {
            _de.ParaNameArray("@Master_Item_Code");
            return _de.ExecuteDataTable("Master_Select_SimilarPicture", Master_Item_Code);
        }
        #endregion

        #region Master_Select_Single_SimilarPicture
        public DataTable Master_Select_Single_SimilarPicture(string Master_Similar_Item_UID)
        {
            _de.ParaNameArray("@Master_Similar_Item_UID");
            return _de.ExecuteDataTable("Master_Select_Single_SimilarPicture", Master_Similar_Item_UID);
        }
        #endregion

        #region Master_Select_ItemWithUIDAdmin()
        public DataTable Master_Select_ItemWithUIDAdmin(string Master_Item_UID, out object message)
        {
            _de.ParaNameArray("@Master_Item_UID");
            return _de.ExecuteDataTable("Master_Select_ItemWithUIDAdmin", "@Message", out message, Master_Item_UID);
        }
        #endregion

        #region Master_Select_ItemWithUIDAdmin_ForSimilarPic()
        public DataTable Master_Select_ItemWithUIDAdmin_ForSimilarPic(string Master_Item_UID, out object message)
        {
            _de.ParaNameArray("@Master_Item_UID");
            return _de.ExecuteDataTable("Master_Select_ItemWithUIDAdmin_ForSimilarPic", "@Message", out message, Master_Item_UID);
        }
        #endregion

        #region Master_Select_ItemAdmin()
        public DataTable Master_Select_ItemAdmin(string Master_Category_UID, string Master_Item_Code, string Master_Item_SupplierProductNumbers)
        {
            _de.ParaNameArray("@Master_Category_UID", "@Master_Item_Code", "Master_Item_SupplierProductNumbers");
            return _de.ExecuteDataTable("Master_Select_ItemAdmin", Master_Category_UID, Master_Item_Code, Master_Item_SupplierProductNumbers);
        }
        #endregion

        #region Master_Delete_Item
        public int Master_Delete_Item(string Master_Item_UID, out object message)
        {
            _de.ParaNameArray("@Master_Item_UID");
            return _de.ExecuteNonQuery("Master_Delete_Item", "@Message", out message, Master_Item_UID);
        }
        #endregion

        #region Master_Insert_Item
        public DataTable Master_Insert_Item(string Master_Item_Category_UID, string Master_Item_CODE, string Master_Item_Name, decimal Master_Item_SalePrice, decimal Master_Item_Tax, string Master_Item_MediaUrl, string Master_Item_MediaFileName, string Master_Item_DESCRIPTION, bool Master_Item_Active, string Master_Item_SubCategory_UID, string Master_Item_Brand, decimal Master_Item_RetailPrice, string Master_Item_ProductType, decimal Master_Item_ManufacturesCost, decimal Master_Item_CompanyOprationCost, decimal Master_Item_CompanyTransactionCharge, decimal Master_Item_OnlineProcessingCharge, decimal Master_Item_PromotionCost, decimal Master_Item_StandardShippingCharge, decimal Master_Item_ShippingCharge, string Master_Item_SupplierName, Int64 Master_Item_SupplierProductNumber, string Master_Item_Gender, string Master_Item_CompanyNotes, out object message)
        {
            _de.ParaNameArray("@Master_Item_Category_UID", "@Master_Item_CODE", "@Master_Item_Name", "@Master_Item_SalePrice", "@Master_Item_Tax", "@Master_Item_MediaUrl", "@Master_Item_MediaFileName", "@Master_Item_DESCRIPTION", "@Master_Item_Active", "@Master_Item_SubCategory_UID", "@Master_Item_Brand", "@Master_Item_RetailPrice", "@Master_Item_ProductType", "@Master_Item_ManufacturesCost", "@Master_Item_CompanyOprationCost", "@Master_Item_CompanyTransactionCharge", "@Master_Item_OnlineProcessingCharge", "@Master_Item_PromotionCost", "@Master_Item_StandardShippingCharge", "@Master_Item_ShippingCharge", "@Master_Item_SupplierName", "@Master_Item_SupplierProductNumber", "@Master_Item_Gender", "@Master_Item_CompanyNotes");
            return _de.ExecuteDataTable("Master_Insert_Item", "@Message", out message, Master_Item_Category_UID, Master_Item_CODE, Master_Item_Name, Master_Item_SalePrice, Master_Item_Tax, Master_Item_MediaUrl, Master_Item_MediaFileName, Master_Item_DESCRIPTION, Master_Item_Active, Master_Item_SubCategory_UID, Master_Item_Brand, Master_Item_RetailPrice, Master_Item_ProductType, Master_Item_ManufacturesCost, Master_Item_CompanyOprationCost, Master_Item_CompanyTransactionCharge, Master_Item_OnlineProcessingCharge, Master_Item_PromotionCost, Master_Item_StandardShippingCharge, Master_Item_ShippingCharge, Master_Item_SupplierName, Master_Item_SupplierProductNumber, Master_Item_Gender, Master_Item_CompanyNotes);
        }
        #endregion

        #region Master_Update_Item
        public int Master_Update_Item(string Master_Item_UID, string Master_Item_Category_UID, string Master_Item_CODE, string Master_Item_Name, decimal Master_Item_SalePrice, decimal Master_Item_Tax, string Master_Item_MediaUrl, string Master_Item_MediaFileName, string Master_Item_DESCRIPTION, bool Master_Item_Active, string Master_Item_SubCategory_UID, string Master_Item_Brand, decimal Master_Item_RetailPrice, string Master_Item_ProductType, decimal Master_Item_ManufacturesCost, decimal Master_Item_CompanyOprationCost, decimal Master_Item_CompanyTransactionCharge, decimal Master_Item_OnlineProcessingCharge, decimal Master_Item_PromotionCost, decimal Master_Item_StandardShippingCharge, decimal Master_Item_ShippingCharge, string Master_Item_SupplierName, Int64 Master_Item_SupplierProductNumber, string Master_Item_Gender, string Master_Item_CompanyNotes, out object message)
        {
            _de.ParaNameArray("@Master_Item_UID", "@Master_Item_Category_UID", "@Master_Item_CODE", "@Master_Item_Name", "@Master_Item_SalePrice", "@Master_Item_Tax", "@Master_Item_MediaUrl", "@Master_Item_MediaFileName", "@Master_Item_DESCRIPTION", "@Master_Item_Active", "@Master_Item_SubCategory_UID", "@Master_Item_Brand", "@Master_Item_RetailPrice", "@Master_Item_ProductType", "@Master_Item_ManufacturesCost", "@Master_Item_CompanyOprationCost", "@Master_Item_CompanyTransactionCharge", "@Master_Item_OnlineProcessingCharge", "@Master_Item_PromotionCost", "@Master_Item_StandardShippingCharge", "@Master_Item_ShippingCharge", "@Master_Item_SupplierName", "@Master_Item_SupplierProductNumber", "@Master_Item_Gender", "@Master_Item_CompanyNotes");
            return _de.ExecuteNonQuery("Master_Update_Item", "@Message", out message, Master_Item_UID, Master_Item_Category_UID, Master_Item_CODE, Master_Item_Name, Master_Item_SalePrice, Master_Item_Tax, Master_Item_MediaUrl, Master_Item_MediaFileName, Master_Item_DESCRIPTION, Master_Item_Active, Master_Item_SubCategory_UID, Master_Item_Brand, Master_Item_RetailPrice, Master_Item_ProductType, Master_Item_ManufacturesCost, Master_Item_CompanyOprationCost, Master_Item_CompanyTransactionCharge, Master_Item_OnlineProcessingCharge, Master_Item_PromotionCost, Master_Item_StandardShippingCharge, Master_Item_ShippingCharge, Master_Item_SupplierName, Master_Item_SupplierProductNumber, Master_Item_Gender, Master_Item_CompanyNotes);
        }
        #endregion

        #region Master_Add_SimilarPic
        public int Master_Add_SimilarPic(string Master_Similar_Mastesr_Item_UID, string Master_Similar_ItemPic, string Master_Similar_ItemPicBig, string Master_Item_MediaUrl, string Master_Item_MediaFileName, out object message)
        {
            _de.ParaNameArray("@Master_Similar_Mastesr_Item_UID", "@Master_Similar_ItemPic", "@Master_Similar_ItemPicBig", "@Master_Item_MediaUrl", "@Master_Item_MediaFileName");
            return _de.ExecuteNonQuery("Master_Add_SimilarPic", "@Message", out message, Master_Similar_Mastesr_Item_UID, Master_Similar_ItemPic, Master_Similar_ItemPicBig, Master_Item_MediaUrl, Master_Item_MediaFileName);
        }
        #endregion

        #region Master_Delete_SimilarPic
        public int Master_Delete_SimilarPic(string Master_Similar_Item_UID, out object message)
        {
            _de.ParaNameArray("Master_Similar_Item_UID");
            return _de.ExecuteNonQuery("Master_Delete_SimilarPic", "@Message", out message, Master_Similar_Item_UID);
        }
        #endregion

        #region Master_Select_CountryDDL
        public DataTable Master_Select_CountryDDL()
        {
            return _de.ExecuteDataTable("Master_Select_CountryDDL");
        }
        #endregion

        #region Master_Select_StateDDL
        public DataTable Master_Select_StateDDL(string Master_Country_UID)
        {
            _de.ParaNameArray("@Master_COUNTRY_UID");
            return _de.ExecuteDataTable("Master_Select_StateDDL", Master_Country_UID);
        }
        #endregion

        #region Master_Select_StateByCountryName
        public DataTable Master_Select_StateByCountryName(string Master_Country_Name)
        {
            _de.ParaNameArray("@Master_Country_Name");
            return _de.ExecuteDataTable("Master_Select_StateByCountryName", Master_Country_Name);
        }
        #endregion

        #region Master_Select_StatesDDL
        public DataTable Master_Select_StatesDDL()
        {
            return _de.ExecuteDataTable("Master_Select_StatesDDL");
        }
        #endregion

        #region Master_Update_JoiningAmount
        public int Master_Update_JoiningAmount(decimal Parameter_Joining_Amount, string Parameter_Role, out object message)
        {
            _de.ParaNameArray("@Parameter_Joining_Amount", "@Parameter_Role");
            return _de.ExecuteNonQuery("Master_Update_JoiningAmount", "@Message", out message, Parameter_Joining_Amount, Parameter_Role);
        }
        #endregion

        #region Master_Select_JoiningAmount
        public DataTable Master_Select_JoiningAmount()
        {
            return _de.ExecuteDataTable("Master_Select_JoiningAmount");
        }
        #endregion

        #region Master_Select_Top6_Product
        public DataSet Master_Select_Top6_Product()
        {
            return _de.ExecuteDataSet("Master_Select_Top6_Product");
        }

        #endregion

        #region Master_Select_MainCategoryList
        public DataTable Master_Select_MainCategoryList()
        {
            return _de.ExecuteDataTable("Master_Select_MainCategoryList");
        }
        #endregion

        #region Master_Select_SubCategoryList
        public DataTable Master_Select_SubCategoryList(string Master_Category_UID)
        {
            _de.ParaNameArray("Master_Category_UID");
            return _de.ExecuteDataTable("Master_Select_SubCategoryList", Master_Category_UID);
        }
        #endregion

        #region Master_Select_SubCategoryDetail
        public DataTable Master_Select_SubCategoryDetail()
        {
            return _de.ExecuteDataTable("Master_Select_SubCategoryDetail");
        }
        #endregion

        #region Master_Delete_SubCategory
        public int Master_Delete_SubCategory(string Master_SubCategory_UID, out object message)
        {
            _de.ParaNameArray("@Master_SubCategory_UID");
            return _de.ExecuteNonQuery("Master_Delete_SubCategory", "@Message", out message, Master_SubCategory_UID);
        }
        #endregion

        #region Select_Master_CategoryList
        public DataTable Select_Master_CategoryList()
        {
            return _de.ExecuteDataTable("Select_Master_CategoryList");
        }
        #endregion

        #region Master_Insert_SubCategory
        public int Master_Insert_SubCategory(string Master_SubCategory_Category_UID, string Master_SubCategory_Name, out object message)
        {
            _de.ParaNameArray("@Master_SubCategory_Category_UID", "@Master_SubCategory_Name");
            return _de.ExecuteNonQuery("Master_Insert_SubCategory", "@Message", out message, Master_SubCategory_Category_UID, Master_SubCategory_Name);
        }
        #endregion

        #region Master_Update_SubCategory
        public int Master_Update_SubCategory(string Master_SubCategory_UID, string Master_SubCategory_Category_UID, string Master_SubCategory_Name, out object message)
        {
            _de.ParaNameArray("@Master_SubCategory_UID", "@Master_SubCategory_Category_UID", "@Master_SubCategory_Name");
            return _de.ExecuteNonQuery("Master_Update_SubCategory", "@Message", out message, Master_SubCategory_UID, Master_SubCategory_Category_UID, Master_SubCategory_Name);
        }
        #endregion

        #region Control_Select_PV
        public DataTable Control_Select_PV()
        {
            return _de.ExecuteDataTable("Control_Select_PV");
        }
        #endregion

        #region Control_Update_PV
        public int Control_Update_PV(DataTable dtPost, out object message)
        {
            DataSet ds = new DataSet();
            ds.Merge(dtPost);
            _de.ParaNameArray("@XML");
            return _de.ExecuteNonQuery("Control_Update_PV", "@Message", out message, ds.GetXml());
        }
        #endregion

        //TODAY..................
        #region Master_Select_MainCategoryListWithMenu
        public DataTable Master_Select_MainCategoryListWithMenu(string Master_Talent_Category_UID)
        {
            _de.ParaNameArray("@Master_Talent_Category_UID");
            return _de.ExecuteDataTable("Master_Select_MainCategoryListWithMenu", Master_Talent_Category_UID);

        }
        #endregion

        #region Master_Select_SubCategoryDetailWithUID
        public DataTable Master_Select_SubCategoryDetailWithUID(string Master_SubCategory_UID)
        {
            _de.ParaNameArray("@Master_SubCategory_UID");
            return _de.ExecuteDataTable("Master_Select_SubCategoryDetailWithUID", Master_SubCategory_UID);
        }
        #endregion

        #region Master_Select_SubCategoryDetailWithUIDMenu
        public DataTable Master_Select_SubCategoryDetailWithUIDMenu(string Master_SubCategory_Category_UID)
        {
            _de.ParaNameArray("@Master_SubCategory_Category_UID");
            return _de.ExecuteDataTable("Master_Select_SubCategoryDetailWithUIDMenu", Master_SubCategory_Category_UID);
        }
        #endregion

        #region Master_Select_Item()
        public DataTable Master_Select_Item(string Master_SubCategory_UID, int pageIndex, int pageSize, out object totalRow)
        {
            int pageStartIndex = (pageIndex - 1) * pageSize + 1;
            int pageEndIndex = pageIndex * pageSize;

            _de.ParaNameArray("@Master_SubCategory_UID", "@pageStartIndex", "@pageEndIndex");
            return _de.ExecuteDataTable("Master_Select_Item", "@totalRow", out totalRow, Master_SubCategory_UID, pageStartIndex, pageEndIndex);
        }
        #endregion

        //TODAY 29-9-14

        #region Master_Select_Item_SizeAdmin()
        public DataTable Master_Select_Item_SizeAdmin(string Master_Item_UID)
        {
            _de.ParaNameArray("@Master_Item_UID");
            return _de.ExecuteDataTable("Master_Select_Item_SizeAdmin", Master_Item_UID);
        }
        #endregion

        #region Master_Delete_Item_Size
        public int Master_Delete_Item_Size(string Master_Item_Size_UID, out object message)
        {
            _de.ParaNameArray("@Master_Item_Size_UID");
            return _de.ExecuteNonQuery("Master_Delete_Item_Size", "@Message", out message, Master_Item_Size_UID);

        }
        #endregion

        #region Master_Insert_Item_Size
        public int Master_Insert_Item_Size(string Master_Item_UID, DataTable dt)
        {
            DataSet ds = new DataSet();
            ds.Merge(dt);

            _de.ParaNameArray("@Master_Item_UID", "@XML");
            return _de.ExecuteNonQuery("Master_Insert_Item_Size", Master_Item_UID, ds.GetXml());
        }
        #endregion

        #region Master_Insert_Item_Color
        public int Master_Insert_Item_Color(string Master_Item_UID, string Master_Item_Color_Name, out object message)
        {
            _de.ParaNameArray("@Master_Item_UID", "@Master_Item_Color_Name");
            return _de.ExecuteNonQuery("Master_Insert_Item_Color", "@Message", out message, Master_Item_UID, Master_Item_Color_Name);
        }
        #endregion

        #region Master_Select_Item_ColorAdmin()
        public DataTable Master_Select_Item_ColorAdmin(string Master_Item_UID)
        {
            _de.ParaNameArray("@Master_Item_UID");
            return _de.ExecuteDataTable("Master_Select_Item_ColorAdmin", Master_Item_UID);
        }
        #endregion

        #region Master_Delete_Item_Color
        public int Master_Delete_Item_Color(string Master_Item_Color_UID, out object message)
        {
            _de.ParaNameArray("@Master_Item_Color_UID");
            return _de.ExecuteNonQuery("Master_Delete_Item_Color", "@Message", out message, Master_Item_Color_UID);

        }
        #endregion

        //7-Oct-2014

        #region Master_Select_ItemMainCategory
        public DataTable Master_Select_ItemMainCategory(string Master_MainCategory_UID, int pageIndex, int pageSize, out object totalRow)
        {

            int pageStartIndex = (pageIndex - 1) * pageSize + 1;
            int pageEndIndex = pageIndex * pageSize;

            _de.ParaNameArray("@Master_MainCategory_UID", "@pageStartIndex", "@pageEndIndex");
            return _de.ExecuteDataTable("Master_Select_ItemMainCategory", "@totalRow", out totalRow, Master_MainCategory_UID, pageStartIndex, pageEndIndex);

        }
        #endregion

        #region Master_Select_ItemSubCategory
        public DataTable Master_Select_ItemSubCategory(string Master_MainCategory_UID, int pageIndex, int pageSize, out object totalRow)
        {

            int pageStartIndex = (pageIndex - 1) * pageSize + 1;
            int pageEndIndex = pageIndex * pageSize;

            _de.ParaNameArray("@Master_MainCategory_UID", "@pageStartIndex", "@pageEndIndex");
            return _de.ExecuteDataTable("Master_Select_ItemSubCategory", "@totalRow", out totalRow, Master_MainCategory_UID, pageStartIndex, pageEndIndex);

        }
        #endregion

        #region Master_Select_ItemMainCat
        public DataTable Master_Select_ItemMainCat(string Master_MainCategory_UID, int pageIndex, int pageSize, out object totalRow)
        {
            int pageStartIndex = (pageIndex - 1) * pageSize + 1;
            int pageEndIndex = pageIndex * pageSize;

            _de.ParaNameArray("@Master_MainCategory_UID", "@pageStartIndex", "@pageEndIndex");
            return _de.ExecuteDataTable("Master_Select_ItemMainCat", "@totalRow", out totalRow, Master_MainCategory_UID, pageStartIndex, pageEndIndex);
        }
        #endregion

        #region Master_Select_MainCategory
        public DataTable Master_Select_MainCategory(string Master_MainCategory_UID)
        {
            _de.ParaNameArray("@Master_MainCategory_UID");
            return _de.ExecuteDataTable("Master_Select_MainCategory", Master_MainCategory_UID);
        }
        #endregion

        #region Master_Select_MainCategoryByUID
        public DataTable Master_Select_MainCategoryByUID(string Master_SubCategory_UID)
        {
            _de.ParaNameArray("@Master_SubCategory_UID");
            return _de.ExecuteDataTable("Master_Select_MainCategoryByUID", Master_SubCategory_UID);
        }
        #endregion

        //10-Oct-2014
        #region Master_Select_MainCategoryListWithMenuForSingle
        public DataTable Master_Select_MainCategoryListWithMenuForSingle(string Master_Category_Menu)
        {
            _de.ParaNameArray("@Master_Category_Menu");
            return _de.ExecuteDataTable("Master_Select_MainCategoryListWithMenuForSingle", Master_Category_Menu);

        }
        #endregion

        #region Master_Select_MainCategoryListWithMenuForSingle
        public DataTable Master_Select_MainCategoryListWithMenuForSingle_WithSubCategory(string Master_Category_Menu, string Master_SubCategory_UID)
        {
            _de.ParaNameArray("@Master_Category_Menu", "@Master_SubCategory_UID");
            return _de.ExecuteDataTable("Master_Select_MainCategoryListWithMenuForSingle_WithSubCategory", Master_Category_Menu, Master_SubCategory_UID);

        }
        #endregion

        #region Master_Select_MainCategoryListWithMenuForSingle_WithMainCategory
        public DataTable Master_Select_MainCategoryListWithMenuForSingle_WithMainCategory(string Master_Category_Menu, string Master_Category_UID)
        {
            _de.ParaNameArray("@Master_Category_Menu", "@Master_Category_UID");
            return _de.ExecuteDataTable("Master_Select_MainCategoryListWithMenuForSingle_WithMainCategory", Master_Category_Menu, Master_Category_UID);

        }
        #endregion

        #region Master_Select_CategoryDefaultCondition
        public DataTable Master_Select_CategoryDefaultCondition()
        {
            return _de.ExecuteDataTable("Master_Select_CategoryDefaultCondition");
        }
        #endregion

        // 11-10-14
        #region Master_Select_Email
        public DataTable Master_Select_Email()
        {
            return _de.ExecuteDataTable("Master_Select_Email");
        }
        #endregion

        #region Master_Update_Email
        public int Master_Update_Email(string Parameter_Order_Email, string Parameter_Contact_Email, out object message)
        {
            _de.ParaNameArray("@Parameter_Order_Email", "@Parameter_Contact_Email");
            return _de.ExecuteNonQuery("Master_Update_Email", "@Message", out message, Parameter_Order_Email, Parameter_Contact_Email);
        }
        #endregion

        #region Master_Select_Item_OnSelectedIndexChanged
        public DataTable Master_Select_Item_OnSelectedIndexChanged(string Master_MainCategory_UID, int productNo, int pageIndex, int pageSize, out object totalRow)
        {
            int pageStartIndex = (pageIndex - 1) * pageSize + 1;
            int pageEndIndex = pageIndex * pageSize;
            _de.ParaNameArray("@Master_MainCategory_UID", "@productNo", "@pageStartIndex", "@pageEndIndex");
            return _de.ExecuteDataTable("Master_Select_Item_OnSelectedIndexChanged", "@totalRow", out totalRow, Master_MainCategory_UID, productNo, pageIndex, pageSize);
        }
        #endregion


        //TODAY 25 NOV 14
        #region Master_Select_ProductionSubMember
        public DataTable Master_Select_ProductionSubMember()
        {
            return _de.ExecuteDataTable("Master_Select_ProductionSubMember");
        }
        #endregion

        #region Master_Select_CreativeTalentCategoryList
        public DataTable Master_Select_CreativeTalentCategoryList()
        {
            return _de.ExecuteDataTable("Master_Select_CreativeTalentCategoryList");
        }
        #endregion


        #region Master_Select_CreativeTalentItem
        public DataTable Master_Select_CreativeTalentItem(string Master_CreativeTalent_Item_LoginName, string Master_CreativeTalent_Item_Flag, string Master_CreativeTalent_Item_Code)
        {
            _de.ParaNameArray("@Master_CreativeTalent_Item_LoginName", "@Master_CreativeTalent_Item_Flag", "@Master_CreativeTalent_Item_Code");
            return _de.ExecuteDataTable("Master_Select_CreativeTalentItem", Master_CreativeTalent_Item_LoginName, Master_CreativeTalent_Item_Flag, Master_CreativeTalent_Item_Code);
        }
        #endregion

        #region Master_Select_CreativeTalentItemAdmin
        public DataTable Master_Select_CreativeTalentItemAdmin(string Master_CreativeTalent_Item_Category_UID, string Master_CreativeTalent_Item_Category_Code)
        {
            _de.ParaNameArray("@Master_CreativeTalent_Item_Category_UID", "@Master_CreativeTalent_Item_Code");
            return _de.ExecuteDataTable("Master_Select_CreativeTalentItemAdmin", Master_CreativeTalent_Item_Category_UID, Master_CreativeTalent_Item_Category_Code);
        }
        #endregion

        #region Master_Delete_CreativeTalentItem
        public int Master_Delete_CreativeTalentItem(string Master_CreativeTalent_Item_UID, out object message)
        {
            _de.ParaNameArray("@Master_CreativeTalent_Item_UID");
            return _de.ExecuteNonQuery("Master_Delete_CreativeTalentItem", "@Message", out message, Master_CreativeTalent_Item_UID);
        }
        #endregion

        #region Master_Select_CreativeTalentItemWithUIDAdmin
        public DataTable Master_Select_CreativeTalentItemWithUIDAdmin(string Master_CreativeTalent_Item_UID, out object message)
        {
            _de.ParaNameArray("@Master_CreativeTalent_Item_UID");
            return _de.ExecuteDataTable("Master_Select_CreativeTalentItemWithUIDAdmin", "@Message", out message, Master_CreativeTalent_Item_UID);
        }
        #endregion

        #region Master_Select_CreativeTalentItemAdminReport
        public DataTable Master_Select_CreativeTalentItemAdminReport(string Master_CreativeTalent_Item_Flag, string Master_CreativeTalent_Item_Category_Code)
        {
            _de.ParaNameArray("@Master_CreativeTalent_Item_Flag", "@Master_CreativeTalent_Item_Category_Code");
            return _de.ExecuteDataTable("Master_Select_CreativeTalentItemAdminReport", Master_CreativeTalent_Item_Flag, Master_CreativeTalent_Item_Category_Code);
        }
        #endregion


        //TODAY 26 NOV 14
        #region Master_Select_PagebodyWithUID
        public DataTable Master_Select_PagebodyWithUID(string ProductionSub_UID)
        {
            _de.ParaNameArray("@ProductionSub_UID");
            return _de.ExecuteDataTable("Master_Select_PagebodyWithUID", ProductionSub_UID);
        }
        #endregion

        #region Master_Update_PagebodyWithUID
        public int Master_Update_PagebodyWithUID(string ProductionSub_UID, string Page_HTML)
        {
            _de.ParaNameArray("@ProductionSub_UID", "@Page_HTML");
            return _de.ExecuteNonQuery("Master_Update_PagebodyWithUID", ProductionSub_UID, Page_HTML);
        }
        #endregion

        // ........2/Jam/14

        #region Master_Insert_CreativeTalentItem
        public int Master_Insert_CreativeTalentItem(string Master_CreativeTalent_Item_Category_UID, string Master_CreativeTalent_Item_Code, string Master_CreativeTalent_Item_Name, string Master_CreativeTalent_Item_MediaUrl, string Master_CreativeTalent_Item_MediaFileName, string Master_CreativeTalent_Item_Description, string Master_CreativeTalent_Item_SubCategory_UID, string Master_CreativeTalent_Item_Brand, decimal Master_CreativeTalent_Item_DesiredPrice, string Master_CreativeTalent_Item_Gender, string Master_CreativeTalent_Item_BUID, string Master_CreativeTalent_Item_CatType, float Master_CreativeTalent_Item_ContantLength, out object message)
        {
            _de.ParaNameArray("@Master_CreativeTalent_Item_Category_UID", "@Master_CreativeTalent_Item_Code", "@Master_CreativeTalent_Item_Name", "@Master_CreativeTalent_Item_MediaUrl", "@Master_CreativeTalent_Item_MediaFileName", "@Master_CreativeTalent_Item_Description", "@Master_CreativeTalent_Item_SubCategory_UID", "@Master_CreativeTalent_Item_Brand", "@Master_CreativeTalent_Item_DesiredPrice", "@Master_CreativeTalent_Item_Gender", "@Master_CreativeTalent_Item_BUID", "@Master_CreativeTalent_Item_CatType", "@Master_CreativeTalent_Item_ContantLength");
            return _de.ExecuteNonQuery("Master_Insert_CreativeTalentItem", "@Message", out message, Master_CreativeTalent_Item_Category_UID, Master_CreativeTalent_Item_Code, Master_CreativeTalent_Item_Name, Master_CreativeTalent_Item_MediaUrl, Master_CreativeTalent_Item_MediaFileName, Master_CreativeTalent_Item_Description, Master_CreativeTalent_Item_SubCategory_UID, Master_CreativeTalent_Item_Brand, Master_CreativeTalent_Item_DesiredPrice, Master_CreativeTalent_Item_Gender, Master_CreativeTalent_Item_BUID, Master_CreativeTalent_Item_CatType, Master_CreativeTalent_Item_ContantLength);
        }
        #endregion

        // 3-1-15
        #region Master_Insert_CreativeTalentItem
        public int Master_Insert_CreativeTalentItem(string Master_Item_UID, string Master_Item_Category_UID, string Master_Item_CODE, string Master_Item_Name, decimal Master_Item_SalePrice, decimal Master_Item_Tax, string Master_Item_MediaUrl, string Master_Item_MediaFileName, string Master_Item_DESCRIPTION, bool Master_Item_Active, string Master_Item_SubCategory_UID, string Master_Item_Brand, decimal Master_Item_RetailPrice, string Master_Item_ProductType, decimal Master_Item_ManufacturesCost, decimal Master_Item_CompanyOprationCost, decimal Master_Item_CompanyTransactionCharge, decimal Master_Item_OnlineProcessingCharge, decimal Master_Item_PromotionCost, decimal Master_Item_StandardShippingCharge, decimal Master_Item_ShippingCharge, string Master_Item_SupplierName, Int64 Master_Item_SupplierProductNumber, string Master_Item_Gender, string Master_Item_CompanyNotes, string Master_Item_CatType, out object message)
        {
            _de.ParaNameArray("@Master_Item_UID", "@Master_Item_Category_UID", "@Master_Item_CODE", "@Master_Item_Name", "@Master_Item_SalePrice", "@Master_Item_Tax", "@Master_Item_MediaUrl", "@Master_Item_MediaFileName", "@Master_Item_DESCRIPTION", "@Master_Item_Active", "@Master_Item_SubCategory_UID", "@Master_Item_Brand", "@Master_Item_RetailPrice", "@Master_Item_ProductType", "@Master_Item_ManufacturesCost", "@Master_Item_CompanyOprationCost", "@Master_Item_CompanyTransactionCharge", "@Master_Item_OnlineProcessingCharge", "@Master_Item_PromotionCost", "@Master_Item_StandardShippingCharge", "@Master_Item_ShippingCharge", "@Master_Item_SupplierName", "@Master_Item_SupplierProductNumber", "@Master_Item_Gender", "@Master_Item_CompanyNotes", "@Master_Item_CatType");
            return _de.ExecuteNonQuery("Master_Insert_CreativeTalentItemAdmin", "@Message", out message, Master_Item_UID, Master_Item_Category_UID, Master_Item_CODE, Master_Item_Name, Master_Item_SalePrice, Master_Item_Tax, Master_Item_MediaUrl, Master_Item_MediaFileName, Master_Item_DESCRIPTION, Master_Item_Active, Master_Item_SubCategory_UID, Master_Item_Brand, Master_Item_RetailPrice, Master_Item_ProductType, Master_Item_ManufacturesCost, Master_Item_CompanyOprationCost, Master_Item_CompanyTransactionCharge, Master_Item_OnlineProcessingCharge, Master_Item_PromotionCost, Master_Item_StandardShippingCharge, Master_Item_ShippingCharge, Master_Item_SupplierName, Master_Item_SupplierProductNumber, Master_Item_Gender, Master_Item_CompanyNotes, Master_Item_CatType);
        }
        #endregion

        #region Master_Insert_Event()
        public int Master_Insert_Event(string Event_Name, string Event_Country, string Event_State, string Event_City, string Event_Venue, string Event_Host, int Event_TotalPerformance, int Event_SeatCapacity, decimal Event_Fee, string Event_Remark, string Event_DateFrom, string Event_DateTo, string Event_TimeFrom, string Event_TimeTo, bool Event_Status, string Event_Image, string Event_MapUrl, out object message)
        {
            _de.ParaNameArray("@Event_Name", "@Event_Country", "@Event_State", "@Event_City", "@Event_Venue", "@Event_Host", "@Event_TotalPerformance", "@Event_SeatCapacity", "@Event_Fee", "@Event_Remark", "@Event_DateFrom", "@Event_DateTo", "@Event_TimeFrom", "@Event_TimeTo", "@Event_Status", "@Event_Image", "@Event_MapUrl");
            return _de.ExecuteNonQuery("Master_Insert_Event", "@Message", out message, Event_Name, Event_Country, Event_State, Event_City, Event_Venue, Event_Host, Event_TotalPerformance, Event_SeatCapacity, Event_Fee, Event_Remark, Event_DateFrom, Event_DateTo, Event_TimeFrom, Event_TimeTo, Event_Status, Event_Image, Event_MapUrl);
        }
        #endregion

        #region Master_Select_EventDetail
        public DataTable Master_Select_EventDetail()
        {
            return _de.ExecuteDataTable("Master_Select_EventDetail");
        }
        #endregion

        #region Master_Select_EventDetailWithUID
        public DataTable Master_Select_EventDetailWithUID(string Master_Event_UID, out object message)
        {
            _de.ParaNameArray("@Master_Event_UID");
            return _de.ExecuteDataTable("Master_Select_EventDetailWithUID", "@Message", out message, Master_Event_UID);
        }
        #endregion

        #region Master_Update_Event()
        public int Master_Update_Event(string Event_UID, string Event_Name, string Event_Country, string Event_State, string Event_City, string Event_Venue, string Event_Host, int Event_TotalPerformance, int Event_SeatCapacity, decimal Event_Fee, string Event_Remark, string Event_DateFrom, string Event_DateTo, string Event_TimeFrom, string Event_TimeTo, bool Event_Status, string Event_MapUrl, out object message)
        {
            _de.ParaNameArray("@Event_UID", "@Event_Name", "@Event_Country", "@Event_State", "@Event_City", "@Event_Venue", "@Event_Host", "@Event_TotalPerformance", "@Event_SeatCapacity", "@Event_Fee", "@Event_Remark", "@Event_DateFrom", "@Event_DateTo", "@Event_TimeFrom", "@Event_TimeTo", "@Event_Status", "@Event_MapUrl");
            return _de.ExecuteNonQuery("Master_Update_Event", "@Message", out message, Event_UID, Event_Name, Event_Country, Event_State, Event_City, Event_Venue, Event_Host, Event_TotalPerformance, Event_SeatCapacity, Event_Fee, Event_Remark, Event_DateFrom, Event_DateTo, Event_TimeFrom, Event_TimeTo, Event_Status, Event_MapUrl);
        }
        #endregion

        #region Master_Delete_Event
        public int Master_Delete_Event(string Event_UID, out object message)
        {
            _de.ParaNameArray("@Event_UID");
            return _de.ExecuteNonQuery("Master_Delete_Event", "@Message", out message, Event_UID);
        }
        #endregion

        #region Master_Insert_Performer()
        public int Master_Insert_Performer(string Event_Performer_BUID, string Event_Performer_Name, string Event_Performer_Category, string Event_Performer_Description, decimal Event_Performer_Fee, string Event_Performer_Date, string Event_Performer_TimeFrom, string Event_Performer_TimeTo, string Event_Performer_Image, out object message)
        {
            _de.ParaNameArray("@Event_Performer_BUID", "@Event_Performer_Name", "@Event_Performer_Category", "@Event_Performer_Description", "@Event_Performer_Fee", "@Event_Performer_Date", "@Event_Performer_TimeFrom", "@Event_Performer_TimeTo", "@Event_Performer_Image");
            return _de.ExecuteNonQuery("Master_Insert_Performer", "@Message", out message, Event_Performer_BUID, Event_Performer_Name, Event_Performer_Category, Event_Performer_Description, Event_Performer_Fee, Event_Performer_Date, Event_Performer_TimeFrom, Event_Performer_TimeTo, Event_Performer_Image);
        }
        #endregion

        #region Master_Select_PerformerDetail
        public DataTable Master_Select_PerformerDetail()
        {
            return _de.ExecuteDataTable("Master_Select_PerformerDetail");
        }
        #endregion

        #region Master_Select_PerformerDetailWithUID
        public DataTable Master_Select_PerformerDetailWithUID(string Event_Performer_UID, out object message)
        {
            _de.ParaNameArray("@Event_Performer_UID");
            return _de.ExecuteDataTable("Master_Select_PerformerDetailWithUID", "@Message", out message, Event_Performer_UID);
        }
        #endregion

        #region Master_Update_Performer()
        public int Master_Update_Performer(string Event_Performer_UID, string Event_Performer_Name, string Event_Performer_Category, string Event_Performer_Description, decimal Event_Performer_Fee, string Event_Performer_Date, out object message)
        {
            _de.ParaNameArray("@Event_Performer_UID", "@Event_Performer_Name", "@Event_Performer_Category", "@Event_Performer_Description", "@Event_Performer_Fee", "@Event_Performer_Date");
            return _de.ExecuteNonQuery("Master_Update_Performer", "@Message", out message, Event_Performer_UID, Event_Performer_Name, Event_Performer_Category, Event_Performer_Description, Event_Performer_Fee, Event_Performer_Date);
        }
        #endregion

        #region Master_Delete_Performer
        public int Master_Delete_Performer(string Event_Performer_UID, out object message)
        {
            _de.ParaNameArray("@Event_Performer_UID");
            return _de.ExecuteNonQuery("Master_Delete_Performer", "@Message", out message, Event_Performer_UID);
        }
        #endregion

        #region Master_Get_EventImage()
        public object Master_Get_EventImage(string Event_UID)
        {
            _de.ParaNameArray("@Event_UID");
            return _de.ExecuteScalar("Master_Get_EventImage", Event_UID);
        }
        #endregion

        #region Master_Insert_CreativeTalentItem_Audio
        public int Master_Insert_CreativeTalentItem_Audio(string Master_Media_MediaFileName, string Master_CreativeTalent_MediaUrl, decimal Master_Media_MediaLength, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Master_Media_MediaFileName", "@Master_CreativeTalent_MediaUrl", "@Master_Media_MediaLength");
            return _de.ExecuteNonQuery("Master_Insert_CreativeTalentItem_Audio", "@Message", out message, HttpContext.Current.User.Identity.Name, Master_Media_MediaFileName, Master_CreativeTalent_MediaUrl, Master_Media_MediaLength);
        }

        public int Master_Insert_CreativeTalentItem_Video(string Master_Media_MediaFileName, string Master_CreativeTalent_MediaUrl, decimal Master_Media_MediaLength, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Master_Media_MediaFileName", "@Master_CreativeTalent_MediaUrl", "@Master_Media_MediaLength");
            return _de.ExecuteNonQuery("Master_Insert_CreativeTalentItem_Video", "@Message", out message, HttpContext.Current.User.Identity.Name, Master_Media_MediaFileName, Master_CreativeTalent_MediaUrl, Master_Media_MediaLength);
        }
        #endregion

        #region Master_Delete_Media
        public int Master_Delete_Media(string Reg_User_Document_UID, out object message)
        {
            _de.ParaNameArray("@Reg_User_Document_UID");
            return _de.ExecuteNonQuery("Master_Delete_Media", "@Message", out message, Reg_User_Document_UID);
        }
        #endregion
        #region Master_Edit_Media
        public DataTable Master_Edit_Media(string Reg_User_Document_UID)
        {
            _de.ParaNameArray("@Reg_User_Document_UID");
            return _de.ExecuteDataTable("Master_Edit_Media" ,Reg_User_Document_UID);
        }
        #endregion

        #region Master_Insert_UserMediaDownload
        public int Master_Insert_UserMediaDownload(string Master_User_MediaDownload_MediaUID, string Master_User_MediaDownload_CreativeTalentUID, out object message)
        {
            _de.ParaNameArray("@Reg_UserLoginName", "@Master_User_MediaDownload_MediaUID", "@Master_User_MediaDownload_CreativeTalentUID");
            return _de.ExecuteNonQuery("Master_Insert_UserMediaDownload", "@Message", out message, HttpContext.Current.User.Identity.Name, Master_User_MediaDownload_MediaUID, Master_User_MediaDownload_CreativeTalentUID);
        }
        #endregion

        #region Master_Get_TalentSales
        public DataTable Master_Get_TalentSales(string DateFrom,  string DateTo)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@DateFrom", "@DateTo");
            return _de.ExecuteDataTable("Master_Get_TalentSales", HttpContext.Current.User.Identity.Name, DateFrom, DateTo);
        }
        #endregion

        #region GetTalentEventCount
        public object GetTalentEventCount()
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteScalarFunction("SELECT dbo.GetTalentEventCount(@Reg_User_LoginName)", HttpContext.Current.User.Identity.Name);
        }
        #endregion

        #region GetTalentUnreadMessage
        public object GetTalentUnreadMessage(long UserID)
        {
            _de.ParaNameArray("@UserID");
            return _de.ExecuteScalarFunction("SELECT dbo.GetTalentUnreadMessage(@UserID)", UserID);
        }
        #endregion

        #region Master_Select_TalentCategoryDDL
        public DataTable Master_Select_TalentCategoryDDL()
        {
            return _de.ExecuteDataTable("Master_Select_TalentCategoryDDL");
        }
        #endregion

        #region Master_Select_TalentSubCategoryDDL
        public DataTable Master_Select_TalentSubCategoryDDL(string Master_Talent_Category_UID)
        {
            _de.ParaNameArray("@Master_Talent_Category_UID");
            return _de.ExecuteDataTable("Master_Select_TalentSubCategoryDDL", Master_Talent_Category_UID);
        }
        #endregion

        #region Master_Select_TalentSubSubCategoryDDL
        public DataTable Master_Select_TalentSubSubCategoryDDL(string Master_Talent_Category_UID)
        {
            _de.ParaNameArray("@Master_Talent_Category_UID");
            return _de.ExecuteDataTable("Master_Select_TalentSubSubCategoryDDL", Master_Talent_Category_UID);
        }
        #endregion

        #region Master_Select_ProductionCategoryDDL
        public DataTable Master_Select_ProductionCategoryDDL()
        {
            return _de.ExecuteDataTable("Master_Select_ProductionCategoryDDL");
        }
        #endregion

        #region Master_Select_ProductionSubCategoryDDL
        public DataTable Master_Select_ProductionSubCategoryDDL(string Master_Production_Category_UID)
        {
            _de.ParaNameArray("@Master_Production_Category_UID");
            return _de.ExecuteDataTable("Master_Select_ProductionSubCategoryDDL", Master_Production_Category_UID);
        }
        #endregion

        #region Master_Insert_ProductionMember_Content
        public int Master_Insert_ProductionMember_Content(string Master_Media_MediaFileName, string Master_Media_MediaDescription, string Master_CreativeTalent_MediaUrl, string Master_CreativeTalent_ClipUrl, string Master_CreativeTalent_CoverUrl, decimal Master_Media_MediaLength, decimal Master_Media_Price, char Master_MediaType, string UserCategory_UID, string UserSubCategory_UID, string UserSubSubCategory_UID, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Master_Media_MediaFileName", "@Master_Media_MediaDescription", "@Master_CreativeTalent_MediaUrl", "@Master_CreativeTalent_ClipUrl", "@Master_CreativeTalent_CoverUrl", "@Master_Media_MediaLength", "@Master_Media_Price", "@Master_MediaType", "@UserCategory_UID", "@UserSubCategory_UID", "@UserSubSubCategory_UID");
            return _de.ExecuteNonQuery("Master_Insert_ProductionMember_Content", "@Message", out message, HttpContext.Current.User.Identity.Name, Master_Media_MediaFileName, Master_Media_MediaDescription, Master_CreativeTalent_MediaUrl, Master_CreativeTalent_ClipUrl, Master_CreativeTalent_CoverUrl, Master_Media_MediaLength, Master_Media_Price, Master_MediaType, UserCategory_UID, UserSubCategory_UID, UserSubSubCategory_UID);
        }
        public int Master_Insert_ProductionMember_AlbumContent(string Master_Media_MediaFileName, string Master_Media_MediaDescription, string Master_CreativeTalent_MediaUrl, string Master_CreativeTalent_ClipUrl, string Master_CreativeTalent_CoverUrl, decimal Master_Media_MediaLength, decimal Master_Media_Price, char Master_MediaType, string UserCategory_UID, string UserSubCategory_UID, string UserSubSubCategory_UID, string Reg_User_Album_Name, string Reg_User_Album_NameExist, string Reg_User_Album_Description, string Reg_User_Album_CoverImage, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Master_Media_MediaFileName", "@Master_Media_MediaDescription", "@Master_CreativeTalent_MediaUrl", "@Master_CreativeTalent_ClipUrl", "@Master_CreativeTalent_CoverUrl", "@Master_Media_MediaLength", "@Master_Media_Price", "@Master_MediaType", "@UserCategory_UID", "@UserSubCategory_UID", "@UserSubSubCategory_UID", "@Reg_User_Album_Name", "@Reg_User_Album_NameExist", "@Reg_User_Album_Description", "@Reg_User_Album_CoverImage");
            return _de.ExecuteNonQuery("Master_Insert_ProductionMember_AlbumContent", "@Message", out message, HttpContext.Current.User.Identity.Name, Master_Media_MediaFileName, Master_Media_MediaDescription, Master_CreativeTalent_MediaUrl, Master_CreativeTalent_ClipUrl, Master_CreativeTalent_CoverUrl, Master_Media_MediaLength, Master_Media_Price, Master_MediaType, UserCategory_UID, UserSubCategory_UID, UserSubSubCategory_UID, Reg_User_Album_Name, Reg_User_Album_NameExist, Reg_User_Album_Description, Reg_User_Album_CoverImage);
        }
        #endregion

        #region Master_Update_ProductionMember_Content
        public int Master_Update_ProductionMember_Content(string Reg_User_Document_UID ,string Master_Media_MediaFileName, string Master_Media_MediaDescription, string Master_CreativeTalent_ClipUrl, string Master_CreativeTalent_CoverUrl, decimal Master_Media_Price, string UserCategory_UID, string UserSubCategory_UID, string UserSubSubCategory_UID, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Reg_User_Document_UID", "@Master_Media_MediaFileName", "@Master_Media_MediaDescription", "@Master_CreativeTalent_ClipUrl", "@Master_CreativeTalent_CoverUrl", "@Master_Media_Price", "@UserCategory_UID", "@UserSubCategory_UID", "@UserSubSubCategory_UID");
            return _de.ExecuteNonQuery("Master_Update_ProductionMember_Content", "@Message", out message, HttpContext.Current.User.Identity.Name, Reg_User_Document_UID,Master_Media_MediaFileName, Master_Media_MediaDescription, Master_CreativeTalent_ClipUrl, Master_CreativeTalent_CoverUrl, Master_Media_Price,  UserCategory_UID, UserSubCategory_UID, UserSubSubCategory_UID);
        }
        public int Master_Update_ProductionMember_AlbumContent(string Reg_User_Document_UID, string Master_Media_MediaFileName, string Master_Media_MediaDescription, string Master_CreativeTalent_ClipUrl, string Master_CreativeTalent_CoverUrl, string UserCategory_UID, string UserSubCategory_UID, string UserSubSubCategory_UID, string Reg_User_Album_Name, string Reg_User_Album_Description, string Reg_User_Album_CoverImage, out object message)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Reg_User_Document_UID", "@Master_Media_MediaFileName", "@Master_Media_MediaDescription", "@Master_CreativeTalent_ClipUrl", "@Master_CreativeTalent_CoverUrl", "@UserCategory_UID", "@UserSubCategory_UID", "@UserSubSubCategory_UID", "@Reg_User_Album_Name", "@Reg_User_Album_Description", "@Reg_User_Album_CoverImage");
            return _de.ExecuteNonQuery("Master_Update_ProductionMember_AlbumContent", "@Message", out message, HttpContext.Current.User.Identity.Name, Reg_User_Document_UID, Master_Media_MediaFileName, Master_Media_MediaDescription, Master_CreativeTalent_ClipUrl, Master_CreativeTalent_CoverUrl, UserCategory_UID, UserSubCategory_UID, UserSubSubCategory_UID, Reg_User_Album_Name, Reg_User_Album_Description, Reg_User_Album_CoverImage);
        }
        #endregion

        #region Master_Insert_ProductAdmin
        public int Master_Insert_ProductAdmin(string Master_Media_MediaFileName, string Master_Media_MediaDescription, string Master_CreativeTalent_MediaUrl, decimal Master_Media_MediaLength, decimal Master_Media_Price, char Master_MediaType, string UserCategory_UID, string UserSubCategory_UID, out object message)
        {
            _de.ParaNameArray("@Reg_Admin_LoginName", "@Master_Media_MediaFileName", "@Master_Media_MediaDescription", "@Master_CreativeTalent_MediaUrl", "@Master_Media_MediaLength", "@Master_Media_Price", "@Master_MediaType", "@UserCategory_UID", "@UserSubCategory_UID");
            return _de.ExecuteNonQuery("Master_Insert_ProductAdmin", "@Message", out message, HttpContext.Current.User.Identity.Name, Master_Media_MediaFileName, Master_Media_MediaDescription, Master_CreativeTalent_MediaUrl, Master_Media_MediaLength, Master_Media_Price, Master_MediaType, UserCategory_UID, UserSubCategory_UID);
        }
        #endregion


        #region Master_Select_UserRoleDDL
        public DataTable Master_Select_UserRoleDDL()
        {
            return _de.ExecuteDataTable("Master_Select_UserRoleDDL");
        }
        #endregion


        #region Master_Select_Talent
        public DataTable Master_Select_MusicTalent()
        {
            _de.ParaNameArray("@Reg_UserLoginName");
            return _de.ExecuteDataTable("Master_Select_MusicTalent", HttpContext.Current.User.Identity.Name);
        }
        public DataTable Master_Select_FilmTalent()
        {
            _de.ParaNameArray("@Reg_UserLoginName");
            return _de.ExecuteDataTable("Master_Select_FilmTalent", HttpContext.Current.User.Identity.Name);
        }
        public DataTable Master_Select_WritersTalent()
        {
            _de.ParaNameArray("@Reg_UserLoginName");
            return _de.ExecuteDataTable("Master_Select_WritersTalent", HttpContext.Current.User.Identity.Name);
        }
        #endregion

        #region Master_Get_UserSelectedTalent
        public DataTable Master_Get_UserSelectedTalent()
        {
            _de.ParaNameArray("@Reg_UserLoginName");
            return _de.ExecuteDataTable("Master_Get_UserSelectedTalent", HttpContext.Current.User.Identity.Name);
        }
        #endregion

        public DataTable Master_GetCodeAdmin()
        {
            return _de.ExecuteDataTable("Master_GetCodeAdmin");
        }
        public DataTable Master_GetSelectedCodeAdmin()
        {
            return _de.ExecuteDataTable("Master_GetSelectedCodeAdmin");
        }
        public int Master_SetCodeAdmin(string Master_Pin_Code, out object message)
        {
            _de.ParaNameArray("@Master_Pin_Code");
            return _de.ExecuteNonQuery("Master_SetCodeAdmin", "@Message", out message, Master_Pin_Code);
        }
        public DataTable Master_CheckCodeExists(string Master_Pin_Code, out object message)
        {
            _de.ParaNameArray("@Master_Pin_Code");
            return _de.ExecuteDataTable("Master_CheckCodeExists", "@Message", out message, Master_Pin_Code);
        }       
    }
}
