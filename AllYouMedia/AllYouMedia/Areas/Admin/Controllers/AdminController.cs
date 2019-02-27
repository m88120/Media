using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataLayer;
using System.Data;
using System.Net;
using System.Drawing.Drawing2D;
using SD = System.Drawing;
using System.IO;
using AllYouMedia.Areas.Admin.Models;
using BusinessEntity.ConcreateEntity;
using System.Configuration;
using AllYouMedia.DataAccess.ServiceLayer;
using AllYouMedia.DataAccess.EntityLayer.DBEntity;
using AllYouMedia.Controllers;

namespace AllYouMedia.Areas.Admin.Controllers
{
    [AdminAuthorize(Roles = "Admin")]
    public class AdminController : SharedAuthController
    {
        #region Private Properties
        private string _DateFrom = "";
        private string _DateTo = "";
        private IAdminService adminSerivce;
        private IAspNetUserService aspNetUserSerivce;
        private ICategoryService categoryService;
        private ICategoryTypeService categoryTypeService;
        private ISubCategoryService subCategoryService;
        private IItemService itemService;
        private IOrderService orderService;
        private IPayoutDistributionService payoutDistributionService;
        public long AspNetUserID
        {
            get
            {
                long str = -1;
                if (System.Web.HttpContext.Current.Session["AspNetUserID"] != null)
                {
                    str = Convert.ToInt64(System.Web.HttpContext.Current.Session["AspNetUserID"]);
                }
                else
                {
                    str = aspNetUserSerivce.GetAspNetUserIDByUserName(User.Identity.Name);
                    Session["AspNetUserID"] = str;
                }
                return str;
            }
        }
        #endregion

        public AdminController(AdminService adminSerivce, AspNetUserService aspNetUserSerivce, CategoryService categoryService, CategoryTypeService categoryTypeService,
            SubCategoryService subCategoryService, ItemService itemService, OrderService orderService, PayoutDistributionService payoutDistributionService)
            : base(aspNetUserSerivce, subCategoryService, itemService, orderService, payoutDistributionService)
        {
            this.adminSerivce = adminSerivce;
            this.aspNetUserSerivce = aspNetUserSerivce;
            this.categoryService = categoryService;
            this.categoryTypeService = categoryTypeService;
            this.subCategoryService = subCategoryService;
            this.itemService = itemService;
            this.orderService = orderService;
            this.payoutDistributionService = payoutDistributionService;
        }

        #region Index
        public ActionResult Index()
        {
            AdminIndexModel model = new AdminIndexModel();
            DataTable dt = adminSerivce.GetDashboard(AspNetUserID);
            if (dt.Rows.Count > 0)
            {
                model.TotalTalent = dt.Rows[0]["TotalAllTalent"].ToString();
                model.TotalProduction = dt.Rows[0]["TotalProduction"].ToString();
                model.TotalMediaPromoter = dt.Rows[0]["TotalMediaPromoter"].ToString();
                model.TotalCustomer = dt.Rows[0]["TotalCustomer"].ToString();
            }
            return View(model);
        }
        #endregion

        #region Logout
        public ActionResult Logout()
        {
            //Session.Clear();
            //Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Logon");
        }
        #endregion

        #region ChangePassword()
        [HttpPost]
        public ActionResult LoginPassword(PasswordModel model)
        {
            if (ModelState.IsValid)
            {
                object message = string.Empty;
                if (new RoleDataEntity().Role_Update_AdminPassword(model.OldPassword, model.Password, out message) > 0)
                {
                    TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                    ModelState.Clear();
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                }
            }
            return RedirectToAction("ChangePassword");
        }

        [HttpPost]
        public ActionResult ControlPassword(PasswordModel model)
        {
            if (ModelState.IsValid)
            {
                object message = string.Empty;
                if (new RoleDataEntity().Role_Update_Control_AdminPassword(model.OldPassword, model.Password, out message) > 0)
                {
                    TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                    ModelState.Clear();
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                }
            }
            return RedirectToAction("ChangePassword");
        }
        #endregion

        #region GetByJSON
        public JsonResult GetStates(Guid uid)
        {
            List<SelectListItem> ddlState = new List<SelectListItem>();
            if (uid.ToString() != "")
            {
                DataTable dtState = new MasterDataEntity().Master_Select_StateDDL(uid.ToString());
                ddlState.Add(new SelectListItem { Text = "<< Select State >>", Value = "" });
                if (dtState.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtState.Rows)
                    {
                        ddlState.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                    }
                }
            }
            else
            {
                ddlState.Add(new SelectListItem { Text = "<< Select State >>", Value = "" });
            }
            return Json(new SelectList(ddlState.OrderBy(x => x.Text), "Value", "Text"));
        }
        public JsonResult GetTalentSubCategory(string TalentCategory)
        {
            List<SelectListItem> ddlTalentSubCategory = new List<SelectListItem>();
            ddlTalentSubCategory.Add(new SelectListItem { Text = "<< Select >>", Value = "" });
            if (TalentCategory.ToString() != "")
            {
                DataTable dtTalentSubCategory = new MasterDataEntity().Master_Select_TalentSubCategoryDDL(TalentCategory);
                if (dtTalentSubCategory.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTalentSubCategory.Rows)
                    {
                        ddlTalentSubCategory.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                    }
                }
            }
            return Json(new SelectList(ddlTalentSubCategory.OrderBy(x => x.Text), "Value", "Text"));
        }
        public JsonResult GetProductionSubCategory(string ProductionCategory)
        {
            List<SelectListItem> ddlProductionSubCategory = new List<SelectListItem>();
            ddlProductionSubCategory.Add(new SelectListItem { Text = "<< Select SubCategory >>", Value = "" });
            if (ProductionCategory.ToString() != "")
            {
                DataTable dtProductionSubCategory = new MasterDataEntity().Master_Select_ProductionSubCategoryDDL(ProductionCategory);
                if (dtProductionSubCategory.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProductionSubCategory.Rows)
                    {
                        ddlProductionSubCategory.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                    }
                }
            }
            return Json(new SelectList(ddlProductionSubCategory.OrderBy(x => x.Text), "Value", "Text"));
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

        public JsonResult PopulateCountryAngularJs()
        {
            DataTable dtCountry = new MasterDataEntity().Master_Select_CountryDDL();
            List<SelectListItem> ddlCountry = new List<SelectListItem>();
            ddlCountry.Add(new SelectListItem { Text = "--Select Country--", Value = "" });
            foreach (DataRow dr in dtCountry.Rows)
            {
                ddlCountry.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
            }
            return Json(ddlCountry.ToList(), JsonRequestBehavior.AllowGet);
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
        public JsonResult PopulateStateAngularJs(string Country_UID = "")
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
            return Json(ddlState.ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PopulateTalentCategory()
        public List<SelectListItem> PopulateTalentCategory()
        {
            List<SelectListItem> ddlTalentCategory = new List<SelectListItem>();
            ddlTalentCategory.Add(new SelectListItem { Text = "<< Select >>", Value = "" });
            DataTable dtTalentCategory = new MasterDataEntity().Master_Select_TalentCategoryDDL();
            if (dtTalentCategory.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTalentCategory.Rows)
                {
                    ddlTalentCategory.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                }
            }
            return ddlTalentCategory;
        }
        public JsonResult PopulateTalentCategoryAngularJs()
        {
            List<SelectListItem> ddlTalentCategory = new List<SelectListItem>();
            ddlTalentCategory.Add(new SelectListItem { Text = "<< Select >>", Value = "" });
            DataTable dtTalentCategory = new MasterDataEntity().Master_Select_TalentCategoryDDL();
            if (dtTalentCategory.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTalentCategory.Rows)
                {
                    ddlTalentCategory.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                }
            }
            return Json(ddlTalentCategory.ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PopulateTalentSubCategory()
        public List<SelectListItem> PopulateTalentSubCategory(string TalentCategory)
        {
            List<SelectListItem> ddlTalentSubCategory = new List<SelectListItem>();
            ddlTalentSubCategory.Add(new SelectListItem { Text = "<< Select >>", Value = "" });
            if (TalentCategory.ToString() != "")
            {
                DataTable dtTalentSubCategory = new MasterDataEntity().Master_Select_TalentSubCategoryDDL(TalentCategory);
                if (dtTalentSubCategory.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTalentSubCategory.Rows)
                    {
                        ddlTalentSubCategory.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                    }
                }
            }
            return ddlTalentSubCategory;
        }

        public JsonResult PopulateTalentSubCategoryAngularJs(string TalentCategory = "")
        {
            List<SelectListItem> ddlTalentSubCategory = new List<SelectListItem>();
            ddlTalentSubCategory.Add(new SelectListItem { Text = "<< Select >>", Value = "" });
            if (TalentCategory.ToString() != "")
            {
                DataTable dtTalentSubCategory = new MasterDataEntity().Master_Select_TalentSubCategoryDDL(TalentCategory);
                if (dtTalentSubCategory.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTalentSubCategory.Rows)
                    {
                        ddlTalentSubCategory.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                    }
                }
            }
            return Json(ddlTalentSubCategory.ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PopulateProductionCategory()
        public List<SelectListItem> PopulateProductionCategory()
        {
            List<SelectListItem> ddlProductionCategory = new List<SelectListItem>();
            ddlProductionCategory.Add(new SelectListItem { Text = "<< Select >>", Value = "" });
            DataTable dtProductionCategory = new MasterDataEntity().Master_Select_ProductionCategoryDDL();
            if (dtProductionCategory.Rows.Count > 0)
            {
                foreach (DataRow dr in dtProductionCategory.Rows)
                {
                    ddlProductionCategory.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                }
            }
            return ddlProductionCategory;
        }
        public JsonResult PopulateProductionCategoryAngularJs()
        {
            List<SelectListItem> ddlProductionCategory = new List<SelectListItem>();
            ddlProductionCategory.Add(new SelectListItem { Text = "<< Select >>", Value = "" });
            DataTable dtProductionCategory = new MasterDataEntity().Master_Select_ProductionCategoryDDL();
            if (dtProductionCategory.Rows.Count > 0)
            {
                foreach (DataRow dr in dtProductionCategory.Rows)
                {
                    ddlProductionCategory.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                }
            }
            return Json(ddlProductionCategory.ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PopulateTalentSubCategory()
        public List<SelectListItem> PopulateProductionSubCategory(string ProductionCategory)
        {
            List<SelectListItem> ddlProductionSubCategory = new List<SelectListItem>();
            ddlProductionSubCategory.Add(new SelectListItem { Text = "<< Select >>", Value = "" });
            if (ProductionCategory.ToString() != "")
            {
                DataTable dtProductionSubCategory = new MasterDataEntity().Master_Select_ProductionSubCategoryDDL(ProductionCategory);
                if (dtProductionSubCategory.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProductionSubCategory.Rows)
                    {
                        ddlProductionSubCategory.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                    }
                }
            }
            return ddlProductionSubCategory;
        }

        public JsonResult PopulateProductionSubCategoryAngularJs(string ProductionCategory = "")
        {
            List<SelectListItem> ddlProductionSubCategory = new List<SelectListItem>();
            ddlProductionSubCategory.Add(new SelectListItem { Text = "<< Select >>", Value = "" });
            if (ProductionCategory.ToString() != "")
            {
                DataTable dtProductionSubCategory = new MasterDataEntity().Master_Select_TalentSubCategoryDDL(ProductionCategory);
                if (dtProductionSubCategory.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProductionSubCategory.Rows)
                    {
                        ddlProductionSubCategory.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                    }
                }
            }
            return Json(ddlProductionSubCategory.ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Populate()
        [HttpPost]
        public JsonResult PopulateMajorAngularJs()
        {
            List<SelectListItem> ddlMajor = new List<SelectListItem>();
            ddlMajor.Add(new SelectListItem { Text = "-- Select Major --", Value = "" });
            DataTable dtMajor = new CommonDataEntity().Select_MajorDDL();
            if (dtMajor.Rows.Count > 0)
            {
                foreach (DataRow dr in dtMajor.Rows)
                {
                    ddlMajor.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                }
            }
            return Json(ddlMajor.ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult PopulateUserAngularJs()
        {
            List<SelectListItem> ddlUser = new List<SelectListItem>();
            DataTable dtUser = new CommonDataEntity().Select_UserList();
            if (dtUser.Rows.Count > 0)
            {
                foreach (DataRow dr in dtUser.Rows)
                {
                    ddlUser.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                }
            }
            return Json(ddlUser.ToList(), JsonRequestBehavior.AllowGet);
        }

        public List<SelectListItem> PopulateMajor()
        {
            List<SelectListItem> ddlMajor = new List<SelectListItem>();
            ddlMajor.Add(new SelectListItem { Text = "-- Select Major --", Value = "" });
            DataTable dtState = new CommonDataEntity().Select_MajorDDL();
            if (dtState.Rows.Count > 0)
            {
                foreach (DataRow dr in dtState.Rows)
                {
                    ddlMajor.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                }
            }
            return ddlMajor;
        }

        public List<SelectListItem> PopulateTalent()
        {
            List<SelectListItem> ddlTalent = new List<SelectListItem>();
            ddlTalent.Add(new SelectListItem { Text = "-- Select Membership --", Value = "" });
            DataTable dtTalent = new CommonDataEntity().Select_TalentCategoryAdmin();
            if (dtTalent.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTalent.Rows)
                {
                    ddlTalent.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                }
            }
            return ddlTalent;
        }
        [HttpPost]
        public JsonResult PopulateTalentAngularJs()
        {
            List<SelectListItem> ddlTalent = new List<SelectListItem>();
            ddlTalent.Add(new SelectListItem { Text = "-- Select Membership --", Value = "" });
            DataTable dtTalent = new CommonDataEntity().Select_TalentCategoryAdmin();
            if (dtTalent.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTalent.Rows)
                {
                    ddlTalent.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                }
            }
            return Json(ddlTalent.ToList(), JsonRequestBehavior.AllowGet);
        }

        public List<SelectListItem> PopulateStatus()
        {
            DataTable dtStatus = new CommonDataEntity().Select_StatusDDL();
            List<SelectListItem> ddlStatus = new List<SelectListItem>();
            ddlStatus.Add(new SelectListItem { Text = "--Select Status--", Value = "" });
            foreach (DataRow dr in dtStatus.Rows)
            {
                ddlStatus.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                ViewData["StatusList"] = ddlStatus;
            }
            return ddlStatus;
        }
        [HttpPost]
        public JsonResult PopulateStatusAngularJs()
        {
            List<SelectListItem> ddlStatus = new List<SelectListItem>();
            ddlStatus.Add(new SelectListItem { Text = "-- Select Status --", Value = "" });
            DataTable dtStatus = new CommonDataEntity().Select_StatusDDL();
            if (dtStatus.Rows.Count > 0)
            {
                foreach (DataRow dr in dtStatus.Rows)
                {
                    ddlStatus.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                }
            }
            return Json(ddlStatus.ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Distributor
        public ActionResult Distributor()
        {
            return View();
        }
        public JsonResult DistributorReportAngularJs(long UserID, string DateFrom = "", string DateTo = "", string City = "", string Major = "", string Country = "", string College = "", string Status = "")
        {
            List<UserReportModel> model = new List<UserReportModel>();
            _CodeClass.GetStartAndEndDate(DateFrom, DateTo, out _DateFrom, out _DateTo);
            int i = 1;
            DataTable dt = new RoleDataEntity().Role_Report_UserDetail(UserID, _DateFrom, _DateTo, City, Country, College, Major, Status);
            model = dt.AsEnumerable().Select(x =>
            {
                return new UserReportModel()
                {
                    Sno = i++,
                    Country = x["Country"].ToString(),
                    LoginName = x["Login Name"].ToString(),
                    EnrollmentDate = x["Enrollment Date"].ToString(),
                    UserName = x["User Name"].ToString(),
                    College = x["College"].ToString(),
                    Major = x["Major"].ToString(),
                    Role = x["Role"].ToString(),
                    AddedBy = x["AddedBy"].ToString(),
                    Status = x["Status"].ToString(),
                };

            }).ToList();

            for (var j = 0; j < model.Count; j++)
            {
                string UserStatus = (model[j].Status).ToLower();
                if (UserStatus == "active")
                {
                    model[j].Status = "<span class='label label-success'>Active</span>";
                }
                else if (UserStatus == "inactive")
                {
                    model[j].Status = "<div class='label label-warning'> Inactive </div>";
                }
                else if (UserStatus == "deleted")
                {
                    model[j].Status = "<div class='label label-danger'> Deleted </div>";
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Membership
        public ActionResult Membership()
        {
            return View();
        }
        public JsonResult MembershipReportAngularJs(long UserID = -1, string DateFrom = "", string DateTo = "", string Talent = "")
        {
            List<UserReportModel> model = new List<UserReportModel>();
            _CodeClass.GetStartAndEndDate(DateFrom, DateTo, out _DateFrom, out _DateTo);
            int i = 1;
            DataTable dt = new RoleDataEntity().Role_Report_UserDetail(UserID, _DateFrom, _DateTo, "", "", "", "", "");
            if (Talent != "")
            {
                dt = dt.AsEnumerable().Where(row => row.Field<String>("Role") == Talent).CopyToDataTable();
            }
            model = dt.AsEnumerable().Select(x =>
            {
                return new UserReportModel()
                {
                    Sno = i++,
                    Country = x["Country"].ToString(),
                    LoginName = x["Login Name"].ToString(),
                    EnrollmentDate = x["Enrollment Date"].ToString(),
                    UserName = x["User Name"].ToString(),
                    Role = x["Role"].ToString(),
                    Status = x["Status"].ToString(),
                };

            }).ToList();
            for (var j = 0; j < model.Count; j++)
            {
                string UserStatus = (model[j].Status).ToLower();
                if (UserStatus == "active")
                {
                    model[j].Status = "<span class='label label-success'>Active</span>";
                }
                else if (UserStatus == "inactive")
                {
                    model[j].Status = "<div class='label label-warning'> Inactive </div>";
                }
                else if (UserStatus == "deleted")
                {
                    model[j].Status = "<div class='label label-danger'> Deleted </div>";
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region AddMediaPromoter
        public ActionResult AddMediaPromoter()
        {
            AdminRegisterModel model = new AdminRegisterModel();
            model.Country = PopulateCountry();
            model.State = PopulateState("");
            model.Major = PopulateMajor();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddMediaPromoter(AdminRegisterModel model)
        {
            string CountryID = Request.Form["Country"];
            string StateID = Request.Form["State"];
            model.Country = PopulateCountry();
            model.State = PopulateState(CountryID);
            var Country = model.Country.Find(p => p.Value == CountryID);
            var State = model.State.Find(p => p.Value == StateID);
            if (Country != null)
            {
                ViewBag.Country = Country.Text;
            }
            if (State != null)
            {
                ViewBag.State = State.Text;
            }
            string major = Request.Form["Major"];
            if (major == "Other")
            {
                major = model.Major_Other;
            }
            else if (major == null)
            {
                major = "";
            }
            if (model.College == null)
            {
                model.College = "";
            }
            object message = string.Empty;
            RoleDataEntity rd = new RoleDataEntity();
            DataTable dt = rd.Role_Insert_MediaPromoterAdmin("Media Promoter", model.UserName, ViewBag.Country, ViewBag.State, model.City, model.Address, model.ZipCode, model.Mobile, model.Email, model.Password, model.RecruiterID, model.College, major, model.BioDescription, "", out message);
            if (dt.Rows.Count > 0)
            {
                TempData["Message"] = "<div class='alert alert-success'>" + message + "</div>";
                return RedirectToAction("Distributor", "Admin");
            }
            else
            {
                TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
            }
            return View(model);
        }
        #endregion

        #region AreaPromoterDetail
        public ActionResult AreaPromoterDetail()
        {
            return View();
        }
        #endregion

        #region UserEdit
        public ActionResult UserEdit(string LoginName = "")
        {
            UserReportModel model = new UserReportModel();
            model.CountryList = PopulateCountry();
            model.StatusList = PopulateStatus();
            object message = string.Empty;
            DataTable dt = new RoleDataEntity().Role_Select_UserDetail(LoginName, out message);
            if (dt.Rows.Count > 0)
            {
                model.LoginName = dt.Rows[0]["Login Name"].ToString();
                model.UserName = dt.Rows[0]["Name"].ToString();
                model.Email = dt.Rows[0]["Email"].ToString();
                model.City = dt.Rows[0]["City"].ToString();
                model.Street = dt.Rows[0]["Street"].ToString();
                model.Address = dt.Rows[0]["Address"].ToString();
                model.ZipCode = dt.Rows[0]["Zip Code"].ToString();
                model.Mobile = dt.Rows[0]["MOBILE"].ToString();
                model.BioGraphy = dt.Rows[0]["Biography"].ToString();
                model.College = dt.Rows[0]["College"].ToString();
                model.Major = dt.Rows[0]["Major"].ToString();
                Session["Password"] = dt.Rows[0]["Password"].ToString();
                model.SponsorID = dt.Rows[0]["Sponser ID"].ToString();
                Session["SponsorID"] = dt.Rows[0]["Sponser ID"].ToString();
                ViewData["CountryList"] = dt.Rows[0]["Country"].ToString();
                ViewData["StateList"] = dt.Rows[0]["State"].ToString();
                ViewData["StatusList"] = dt.Rows[0]["Status"].ToString();
            }
            model.StateList = PopulateState(ViewData["CountryList"].ToString());
            return View(model);
        }

        [HttpPost]
        public ActionResult UserEdit(UserReportModel model, FormCollection formCollection)
        {
            string CountryID = Request.Form["CountryList"];
            string StateID = Request.Form["StateList"];
            string Status = Request.Form["StatusList"];
            model.CountryList = PopulateCountry();
            model.StateList = PopulateState(CountryID);
            var Country = model.CountryList.Find(p => p.Value == CountryID);
            var State = model.StateList.Find(p => p.Value == StateID);
            if (Country != null)
            {
                ViewBag.Country = Country.Text;
            }
            if (State != null)
            {
                ViewBag.State = State.Text;
            }
            RoleDataEntity rd = new RoleDataEntity();
            object message = string.Empty;
            if (new RoleDataEntity().Role_Update_UserDetail(model.UserName, ViewBag.Country, ViewBag.State, model.City, "", model.Address, model.ZipCode, model.Mobile, model.Email, model.LoginName, Session["Password"].ToString(), model.SponsorID, Session["SponsorID"].ToString(), model.BioGraphy, Status, out message) > 0)
            {
                TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
            }
            else
            {
                TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
            }
            return RedirectToAction("Distributor");
        }
        #endregion

        #region CategoryManagement

        public ActionResult CategoryManagement()
        {
            return View();
        }
        public JsonResult CategoryList(int draw, Dictionary<string, string> search, int start, int length)
        {
            var result = categoryService.GetForDT(search["value"], start, length);
            var ds = result.Item1.Select(x =>
            {
                return new
                {
                    DT_RowId = x.ID,
                    CategoryType = x.CategoryType.Name,
                    x.CategoryTypeID,
                    Name = x.Name,
                    IsActive = x.IsActive,
                };

            }).ToList();
            return Json(new
            {
                draw = draw,
                data = ds,
                options = new
                {
                    CategoryTypeID = categoryTypeService.CategoryTypeCbo()
                },
                recordsTotal = result.Item2,
                recordsFiltered = result.Item2,
            });
        }
        public JsonResult CategoryCurd(string action, List<Dictionary<string, string>> data)
        {
            if (data == null || data.Count == 0) return Json(new { error = "Invalid operation" });
            AllYouMedia.DataAccess.EntityLayer.DBEntity.Category dbCategory = null;
            if (action == "create")
            {
                dbCategory = new DataAccess.EntityLayer.DBEntity.Category
                {
                    CategoryTypeID = Convert.ToInt64(data[0]["CategoryTypeID"]),
                    Name = data[0]["Name"],
                };
                if (string.IsNullOrEmpty(dbCategory.Name)) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "Name" }, { "status", "Category Name is required." } } } });
                if (dbCategory.CategoryTypeID == -1) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "CategoryTypeID" }, { "status", "Category Type is required." } } } });
                categoryService.Insert(dbCategory);
                return Json(new { data = new List<AllYouMedia.DataAccess.EntityLayer.DBEntity.Category> { dbCategory } });
            }
            else if (action == "edit")
            {
                dbCategory = categoryService.GetById(Convert.ToInt64(data[0]["ID"]));
                dbCategory.CategoryTypeID = Convert.ToInt64(data[0]["CategoryTypeID"]);
                dbCategory.Name = data[0]["Name"];
                if (string.IsNullOrEmpty(dbCategory.Name)) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "Name" }, { "status", "Category Name is required." } } } });
                if (dbCategory.CategoryTypeID == -1) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "CategoryTypeID" }, { "status", "Category Type is required." } } } });
                categoryService.Update(dbCategory);
                return Json(new { data = new List<object> { new { DT_RowId = dbCategory.ID, CategoryType = dbCategory.CategoryType.Name, dbCategory.CategoryTypeID, Name = dbCategory.Name, IsActive = dbCategory.IsActive, } } });
            }
            else if (action == "remove")
            {
                dbCategory = categoryService.GetById(Convert.ToInt64(data[0]["ID"]));
                try
                {
                    categoryService.Delete(dbCategory);
                    return Json(new { });
                }
                catch (Exception ex) { return Json(new { error = "Could not delete." }); }
            }
            return Json(new { error = "Invalid operation" });
        }
        #endregion

        #region SubCategoryManagement

        public ActionResult SubCategoryManagement()
        {
            return View();
        }
        public JsonResult SubCategoryList(int draw, Dictionary<string, string> search, int start, int length)
        {
            var result = subCategoryService.GetForDT(search["value"], start, length);
            var ds = result.Item1.Select(x =>
            {
                return new
                {
                    DT_RowId = x.ID,
                    Category = x.Category.Name,
                    x.CategoryID,
                    Name = x.Name,
                    IsActive = x.IsActive,
                };

            }).ToList();
            return Json(new
            {
                draw = draw,
                data = ds,
                options = new
                {
                    CategoryID = categoryService.CategoryCbo()
                },
                recordsTotal = result.Item2,
                recordsFiltered = result.Item2,
            });
        }
        public JsonResult SubCategoryCurd(string action, List<Dictionary<string, string>> data)
        {
            if (data == null || data.Count == 0) return Json(new { error = "Invalid operation" });
            AllYouMedia.DataAccess.EntityLayer.DBEntity.SubCategory dbSubCategory = null;
            if (action == "create")
            {
                dbSubCategory = new DataAccess.EntityLayer.DBEntity.SubCategory
                {
                    CategoryID = Convert.ToInt64(data[0]["CategoryID"]),
                    Name = data[0]["Name"],
                };
                if (string.IsNullOrEmpty(dbSubCategory.Name)) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "Name" }, { "status", "SubCategory Name is required." } } } });
                if (dbSubCategory.CategoryID == -1) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "CategoryID" }, { "status", "SubCategory Type is required." } } } });
                subCategoryService.Insert(dbSubCategory);
                return Json(new { data = new List<AllYouMedia.DataAccess.EntityLayer.DBEntity.SubCategory> { dbSubCategory } });
            }
            else if (action == "edit")
            {
                dbSubCategory = subCategoryService.GetById(Convert.ToInt64(data[0]["ID"]));
                dbSubCategory.CategoryID = Convert.ToInt64(data[0]["CategoryID"]);
                dbSubCategory.Name = data[0]["Name"];
                if (string.IsNullOrEmpty(dbSubCategory.Name)) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "Name" }, { "status", "SubCategory Name is required." } } } });
                if (dbSubCategory.CategoryID == -1) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "CategoryID" }, { "status", "SubCategory Type is required." } } } });
                subCategoryService.Update(dbSubCategory);
                return Json(new { data = new List<object> { new { DT_RowId = dbSubCategory.ID, Category = dbSubCategory.Category.Name, dbSubCategory.CategoryID, Name = dbSubCategory.Name, IsActive = dbSubCategory.IsActive, } } });
            }
            else if (action == "remove")
            {
                dbSubCategory = subCategoryService.GetById(Convert.ToInt64(data[0]["ID"]));
                try
                {
                    subCategoryService.Delete(dbSubCategory);
                    return Json(new { });
                }
                catch (Exception ex) { return Json(new { error = "Could not delete." }); }
            }
            return Json(new { error = "Invalid operation" });
        }
        #endregion

        #region SuugestCategory
        public ActionResult SuggestCategory()
        {
            Session["UID"] = "";
            CategoryManagenentModel model = new CategoryManagenentModel();
            model.CategoryTypeList = PopulateTalentCategory();
            return View(model);
        }

        [HttpPost]
        public JsonResult SuggestCategoryEdit(string UID)
        {
            CategoryManagenentModel model = new CategoryManagenentModel();
            object message = string.Empty;
            model.CategoryTypeList = PopulateTalentCategory();
            DataTable dt = new MasterDataEntity().Master_Select_SuggestedCategoryDetailWithUID(UID, out message);
            if (dt.Rows.Count > 0)
            {
                model.CategoryTypeID = Convert.ToInt64(dt.Rows[0]["Talent UID"]);
                ViewData["Menu"] = dt.Rows[0]["Menu"].ToString();
                model.CategoryName = dt.Rows[0]["Name"].ToString();
                Session["UID"] = UID;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SuggestCategoryDelete(string UID)
        {
            object message = string.Empty;

            if (UID != "")
            {
                if (new MasterDataEntity().Master_Delete_SuggestedCategory(UID, out message) > 0)
                {
                    TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + "Category has been deleted successfully" + "</div>";
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SuggestCategoryReportByAngularJS()
        {
            List<CategoryManagenentModel> model = new List<CategoryManagenentModel>();
            int i = 1;
            DataTable dt = new MasterDataEntity().Master_Select_SuggestedCategoryDetail();
            model = dt.AsEnumerable().Select(x =>
            {
                return new CategoryManagenentModel()
                {
                    Sno = i++,
                    ID = Convert.ToInt64(dt.Rows[0]["ID"]),
                    CategoryTypeID = Convert.ToInt64(dt.Rows[0]["Talent UID"]),
                    Name = x["Name"].ToString(),
                };

            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SuggestCategory(CategoryManagenentModel model, FormCollection formCollection)
        {
            string CategoryID = Request.Form["MenuList"];
            MasterDataEntity md = new MasterDataEntity();
            object message = string.Empty;

            if (md.Master_Insert_Category(CategoryID, model.CategoryName, out message) > 0)
            {
                ModelState.Clear();
                TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                return RedirectToAction("CategoryManagement");
            }
            else
            {
                TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
            }
            model.CategoryTypeList = PopulateTalentCategory();
            return View(model);
        }
        #endregion

        #region PurchaseHistory
        public ActionResult PurchaseHistory()
        {
            return View();
        }
        public JsonResult PurchaseHistoryAngularJs(string DateFrom = "", string DateTo = "", string LoginNameSearch = "")
        {
            List<PurchaseHistoryModel> model = new List<PurchaseHistoryModel>();
            _CodeClass.GetStartAndEndDate(DateFrom, DateTo, out _DateFrom, out _DateTo);
            int i = 1;
            DataTable dt = new ShoppingDataEntity().Shopping_Select_PurchaseHistory_Admin(_DateFrom, _DateTo, LoginNameSearch);
            model = dt.AsEnumerable().Select(x =>
            {
                return new PurchaseHistoryModel()
                {
                    Sno = i++,
                    EmailID = x["Email ID"].ToString(),
                    Name = x["Name"].ToString(),
                    Date = x["Date"].ToString(),
                    OrderNumber = x["Order Number"].ToString(),
                    PaidAmount = x["Paid Amount"].ToString(),
                };

            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Invoice()
        public ActionResult Invoice(string id = "")
        {
            InvoiceModel model = new InvoiceModel();
            DataTable dt = new ShoppingDataEntity().Shopping_Select_ProductInvoiceAdmin(id);
            if (dt.Rows.Count > 0)
            {
                model.InvoiceNo = dt.Rows[0]["invoice No"].ToString();
                model.NAME = dt.Rows[0]["NAME"].ToString();
                model.ADDRESS = dt.Rows[0]["ADDRESS"].ToString();
                model.ADDRESS2 = dt.Rows[0]["ADDRESS 2"].ToString();
                model.EMAIL = dt.Rows[0]["EMAIL"].ToString();
                model.MOBILE = dt.Rows[0]["MOBILE"].ToString();
                model.PaymentDate = dt.Rows[0]["PaymentDate"].ToString();
                model.Rate = dt.Rows[0]["Rate"].ToString();
                model.Quantity = dt.Rows[0]["Quantity"].ToString();
                model.Amount = dt.Rows[0]["Amount"].ToString();
                model.Tax = dt.Rows[0]["Tax"].ToString();
                model.NetAmount = dt.Rows[0]["Net Amount"].ToString();
                model.Product = dt.Rows[0]["Product"].ToString();
            }
            else
            {
                TempData["Message"] = "<div class='alert alert-danger'>" + "Invoice does not exists!" + " </div>";
                return RedirectToAction("PurchaseHistory");
            }
            return View(model);
        }
        #endregion

        #region IncomeReferral
        public ActionResult IncomeReferral()
        {
            return View();
        }

        public JsonResult IncomeReferralAngularJs(string DateFrom = "", string DateTo = "", string LoginNameSearch = "")
        {
            List<IncomeModel> model = new List<IncomeModel>();
            _CodeClass.GetStartAndEndDate(DateFrom, DateTo, out _DateFrom, out _DateTo);
            int i = 1;
            DataTable dt = new IncomeDataEntity().Income_Report_ReferralAdmin(LoginNameSearch, _DateFrom, _DateTo);
            model = dt.AsEnumerable().Select(x =>
            {
                return new IncomeModel()
                {
                    Sno = i++,
                    LoginName = x["Login Name"].ToString(),
                    Date = x["Date"].ToString(),
                    Amount = x["Amount"].ToString(),
                    Deduction = x["Deduction"].ToString(),
                    NetAmount = x["Net Amount"].ToString(),
                };

            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Message()
        public ActionResult Message()
        {
            return View();
        }

        public JsonResult InboxAngularJs(string LoginNameSearch = "")
        {
            List<MessageModel> model = new List<MessageModel>();
            int i = 1;
            DataTable dt = new MessageDataEntity().Message_Select_InboxAdmin(AspNetUserID);
            model = dt.AsEnumerable().Select(x =>
            {
                return new MessageModel()
                {
                    Sno = i++,
                    UID = x["UID"].ToString(),
                    MailFrom = x["Mail From"].ToString(),
                    Subject = x["Subject"].ToString(),
                    Message = x["Message"].ToString(),
                    SentDate = x["Sent Date"].ToString(),
                };

            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OutboxAngularJs(string LoginNameSearch = "")
        {
            List<MessageModel> model = new List<MessageModel>();
            int i = 1;
            DataTable dt = new MessageDataEntity().Message_Select_OutBoxAdmin(LoginNameSearch);
            model = dt.AsEnumerable().Select(x =>
            {
                return new MessageModel()
                {
                    Sno = i++,
                    UID = x["UID"].ToString(),
                    MailTo = x["Mail To"].ToString(),
                    Subject = x["Subject"].ToString(),
                    Message = x["Message"].ToString(),
                    SentDate = x["Sent Date"].ToString(),
                };

            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteOutboxAngularJs(string UID = "")
        {
            if (UID != "")
            {
                if (new MessageDataEntity().Message_Delete_Admin(UID) > 0)
                {
                    TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + "Message has been deleted successfully" + "</div>";
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Message(ComposeModel model, FormCollection form)
        {
            object message = string.Empty;
            if (model.ComposeType == "N" && (model.LoginName == null || model.LoginName == ""))
            {
                TempData["Message"] = "<div class='alert alert-danger'>" + "Please enter atleast one LoginName to send Message!" + "</div>";
                return View();
            }
            else if (model.ComposeType == "A")
            {
                model.LoginName = "";
            }
            if (new MessageDataEntity().Message_Insert_Admin(model.LoginName, model.Subject, model.Message, model.ComposeType, "", out message) > 0)
            {
                TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                ModelState.Clear();
            }
            else
            {
                TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
            }
            return View();
        }

        [HttpPost]
        public JsonResult MessageReadInboxAngularJs(string UID = "")
        {
            MessageModel model = new MessageModel();
            DataTable dt = new MessageDataEntity().Message_GetMessageDetail(User.Identity.Name, UID);
            if (dt.Rows.Count > 0)
            {
                model.SentDate = dt.Rows[0]["Sent Date"].ToString();
                model.Subject = dt.Rows[0]["Subject"].ToString();
                model.Message = dt.Rows[0]["Message"].ToString();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Control()
        public ActionResult Control()
        {
            ControlModel model = new ControlModel();
            List<PvPlanModel> PvPlan = new List<PvPlanModel>();
            DataTable dt_PvPlan = new MasterDataEntity().Control_Select_PV();
            PvPlan = dt_PvPlan.AsEnumerable().Select(x =>
            {
                return new PvPlanModel()
                {
                    KID = Convert.ToInt32(x["KID"].ToString()),
                    Code = x["Code"].ToString(),
                    Commission = Convert.ToDouble(x["Commission"].ToString()),
                };

            }).ToList();
            model.PvPlan = PvPlan;

            DataTable dt_Email = new MasterDataEntity().Master_Select_Email();
            if (dt_Email.Rows.Count > 0)
            {
                model.OrderEmail = dt_Email.Rows[0]["ORDER EMAIL"].ToString();
                model.ContactEmail = dt_Email.Rows[0]["CONTACT EMAIL"].ToString();
            }
            DataTable dt_JoiningAmount = new MasterDataEntity().Master_Select_JoiningAmount();
            if (dt_JoiningAmount.Rows.Count > 0)
            {
                model.ATAmount = dt_JoiningAmount.Rows[0]["AT"].ToString();
                model.PMAmount = dt_JoiningAmount.Rows[0]["PM"].ToString();
                model.MPAmount = dt_JoiningAmount.Rows[0]["MP"].ToString();
            }
            ViewBag.Password = true;
            ViewBag.View = false;
            return View(model);
        }
        [HttpPost]
        public ActionResult Control(ControlModel model)
        {
            object message = string.Empty;
            if (model.Password != null)
            {
                if (new RoleDataEntity().Role_Select_Control_AdminPassword(model.Password, out message) > 0)
                {
                    ControlModel model_new = new ControlModel();
                    List<PvPlanModel> PvPlan = new List<PvPlanModel>();
                    DataTable dt_PvPlan = new MasterDataEntity().Control_Select_PV();
                    PvPlan = dt_PvPlan.AsEnumerable().Select(x =>
                    {
                        return new PvPlanModel()
                        {
                            KID = Convert.ToInt32(x["KID"].ToString()),
                            Code = x["Code"].ToString(),
                            Commission = Convert.ToDouble(x["Commission"].ToString()),
                        };
                    }).ToList();
                    model_new.PvPlan = PvPlan;

                    DataTable dt_Email = new MasterDataEntity().Master_Select_Email();
                    if (dt_Email.Rows.Count > 0)
                    {
                        model_new.OrderEmail = dt_Email.Rows[0]["ORDER EMAIL"].ToString();
                        model_new.ContactEmail = dt_Email.Rows[0]["CONTACT EMAIL"].ToString();
                    }
                    DataTable dt_JoiningAmount = new MasterDataEntity().Master_Select_JoiningAmount();
                    if (dt_JoiningAmount.Rows.Count > 0)
                    {
                        model_new.ATAmount = dt_JoiningAmount.Rows[0]["AT"].ToString();
                        model_new.PMAmount = dt_JoiningAmount.Rows[0]["PM"].ToString();
                        model_new.MPAmount = dt_JoiningAmount.Rows[0]["MP"].ToString();
                    }
                    ViewBag.Password = false;
                    ViewBag.View = true;
                    return View(model_new);
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                    return RedirectToAction("Control");
                }
            }
            else
            {
                TempData["Message"] = "<div class='alert alert-danger'>" + "Please enter Password first!" + "</div>";
                return RedirectToAction("Control");
            }
        }
        [HttpPost]
        public ActionResult ATAmount(ControlModel model)
        {
            object message = string.Empty;
            if (model.ATAmount != null)
            {
                if (new MasterDataEntity().Master_Update_JoiningAmount(Convert.ToDecimal(model.ATAmount), "ALL TALENT", out message) > 0)
                {
                    TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                    return RedirectToAction("Control", "Admin");
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                    return RedirectToAction("Control");
                }
            }
            else
            {
                TempData["Message"] = "<div class='alert alert-danger'>" + "Please enter Joining Amount first!" + "</div>";
                return RedirectToAction("Control");
            }
        }
        [HttpPost]
        public ActionResult PMAmount(ControlModel model)
        {
            object message = string.Empty;
            if (model.PMAmount != null)
            {
                if (new MasterDataEntity().Master_Update_JoiningAmount(Convert.ToDecimal(model.PMAmount), "PRODUCTION", out message) > 0)
                {
                    TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                    return RedirectToAction("Control", "Admin");
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                    return RedirectToAction("Control");
                }
            }
            else
            {
                TempData["Message"] = "<div class='alert alert-danger'>" + "Please enter Joining Amount first!" + "</div>";
                return RedirectToAction("Control");
            }
        }
        [HttpPost]
        public ActionResult MPAmount(ControlModel model)
        {
            object message = string.Empty;
            if (model.MPAmount != null)
            {
                if (new MasterDataEntity().Master_Update_JoiningAmount(Convert.ToDecimal(model.MPAmount), "MEDIA PROMOTER", out message) > 0)
                {
                    TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                    return RedirectToAction("Control", "Admin");
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                    return RedirectToAction("Control");
                }
            }
            else
            {
                TempData["Message"] = "<div class='alert alert-danger'>" + "Please enter Joining Amount first!" + "</div>";
                return RedirectToAction("Control");
            }
        }
        [HttpPost]
        public ActionResult Email(ControlModel model)
        {
            object message = string.Empty;
            if (model.OrderEmail != null || model.ContactEmail != null)
            {
                if (model.OrderEmail == null) { model.OrderEmail = ""; }
                if (model.ContactEmail == null) { model.ContactEmail = ""; }
                if (new MasterDataEntity().Master_Update_Email(model.OrderEmail, model.ContactEmail, out message) > 0)
                {
                    TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                    return RedirectToAction("Control", "Admin");
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                    return RedirectToAction("Control");
                }
            }
            else
            {
                TempData["Message"] = "<div class='alert alert-danger'>" + "Please enter at least one Email first!" + "</div>";
                return RedirectToAction("Control");
            }
        }
        [HttpPost]
        public ActionResult PvPlan(ControlModel model, FormCollection form)
        {
            object message = string.Empty;
            DataTable dt = new DataTable("Post");
            dt.Columns.Add(new DataColumn("KID", typeof(int)));
            dt.Columns.Add(new DataColumn("Commission", typeof(double)));

            var KidValues = form.GetValues("KID");
            var CommissionValues = form.GetValues("Commission");
            for (int i = 0; i < KidValues.Length; i++)
            {
                var newRow = dt.NewRow();
                newRow["KID"] = KidValues[i];
                newRow["Commission"] = CommissionValues[i];
                dt.Rows.Add(newRow);
            }

            if (new MasterDataEntity().Control_Update_PV(dt, out message) > 0)
            {
                TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                return RedirectToAction("Control", "Admin");
            }
            else
            {
                TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                return RedirectToAction("Control");
            }
        }
        #endregion

        #region Event()
        public ActionResult Event()
        {
            Session["UID"] = "";
            EventModel model = new EventModel();
            model.CountryList = PopulateCountry();
            model.StateList = PopulateState("");
            return View(model);
        }
        public JsonResult EventReportByAngularJS()
        {
            List<EventModel> model = new List<EventModel>();
            int i = 1;
            DataTable dt = new MasterDataEntity().Master_Select_EventDetail();
            model = dt.AsEnumerable().Select(x =>
            {
                return new EventModel()
                {
                    Sno = i++,
                    UID = x["UID"].ToString(),
                    EventName = x["Name"].ToString(),
                    Country = x["Country"].ToString(),
                    City = x["City"].ToString(),
                    Host = x["Host"].ToString(),
                    DateFrom = x["DateFrom"].ToString(),
                    Status1 = x["Status"].ToString(),
                };

            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EventEdit(string UID)
        {
            EventModel model = new EventModel();
            object message = string.Empty;
            model.CountryList = PopulateCountry();
            DataTable dt = new MasterDataEntity().Master_Select_EventDetailWithUID(UID, out message);
            if (dt.Rows.Count > 0)
            {
                model.UID = dt.Rows[0]["UID"].ToString();
                model.EventName = dt.Rows[0]["Name"].ToString();
                model.Country = dt.Rows[0]["Country"].ToString();
                ViewData["CountryList"] = dt.Rows[0]["Country"].ToString();
                model.State = dt.Rows[0]["State"].ToString();
                ViewData["StateList"] = dt.Rows[0]["State"].ToString();
                model.City = dt.Rows[0]["City"].ToString();
                model.Venue = dt.Rows[0]["Venue"].ToString();
                model.Host = dt.Rows[0]["Host"].ToString();
                model.SeatCapacity = dt.Rows[0]["SeatCapacity"].ToString();
                model.Fee = dt.Rows[0]["Fee"].ToString();
                model.Remark = dt.Rows[0]["Remark"].ToString();
                model.Performance = dt.Rows[0]["TotalPerformance"].ToString();
                model.DateFrom = dt.Rows[0]["DateFrom"].ToString();
                model.DateTo = dt.Rows[0]["DateTo"].ToString();
                model.TimeFrom = dt.Rows[0]["TimeFrom"].ToString();
                model.TimeTo = dt.Rows[0]["TimeTo"].ToString();
                model.Status = Convert.ToBoolean(dt.Rows[0]["Active"].ToString());
                model.ImageUrl = dt.Rows[0]["Image"].ToString().Replace("~/", "/");
                model.MapUrl = dt.Rows[0]["MapUrl"].ToString();
                Session["UID"] = dt.Rows[0]["UID"].ToString();
            }
            model.StateList = PopulateState(ViewData["CountryList"].ToString());
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Event(EventModel model, HttpPostedFileBase file, FormCollection formCollection)
        {
            string CountryID = Request.Form["CountryList"];
            string StateID = Request.Form["StateList"];
            string _imageUrl = string.Empty;
            string _fileName = string.Empty;
            if (Session["UID"].ToString() == "")
            {
                if (file.FileName.Trim() != "" && file.ContentType.ToUpper().Contains("IMAGE"))
                {
                    _imageUrl = ConfigurationManager.AppSettings["EventImagePath"].ToString();

                    _fileName = model.EventName + file.FileName.Substring(file.FileName.LastIndexOf("."));

                    _imageUrl = _imageUrl + _fileName;
                    byte[] array;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        array = ms.GetBuffer();
                    }
                    _CodeClass.SaveCroppedImage(new MemoryStream(array), Server.MapPath(_imageUrl), 400);
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger'>" + "Please select only Image!" + "</div>";
                    return RedirectToAction("Event", "Admin");
                }
            }
            else if (Session["UID"].ToString() != "")
            {
                if (Request.Files.Count > 0)
                {
                    if (file.FileName.Trim() != "" && file.ContentType.ToUpper().Contains("IMAGE"))
                    {
                        _imageUrl = ConfigurationManager.AppSettings["EventImagePath"].ToString();

                        _fileName = model.EventName + file.FileName.Substring(file.FileName.LastIndexOf("."));

                        _imageUrl = _imageUrl + _fileName;
                        byte[] array;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            file.InputStream.CopyTo(ms);
                            array = ms.GetBuffer();
                        }
                        _CodeClass.SaveCroppedImage(new MemoryStream(array), Server.MapPath(_imageUrl), 400);
                    }
                }
            }
            object message = string.Empty;
            if (Session["UID"].ToString() == "")
            {
                if ((new MasterDataEntity().Master_Insert_Event(model.EventName, CountryID, StateID, model.City, model.Venue, model.Host, Convert.ToInt32(model.Performance), Convert.ToInt32(model.SeatCapacity), Convert.ToDecimal(model.Fee), model.Remark, model.DateFrom, model.DateTo, model.TimeFrom, model.TimeTo, model.Status, _imageUrl, model.MapUrl, out message)) > 0)
                {
                    ModelState.Clear();
                    TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                    return RedirectToAction("Event");
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                    return RedirectToAction("Event");
                }
            }
            else if (Session["UID"].ToString() != "")
            {
                if ((new MasterDataEntity().Master_Update_Event(Session["UID"].ToString(), model.EventName, CountryID, StateID, model.City, model.Venue, model.Host, Convert.ToInt32(model.Performance), Convert.ToInt32(model.SeatCapacity), Convert.ToDecimal(model.Fee), model.Remark, model.DateFrom, model.DateTo, model.TimeFrom, model.TimeTo, model.Status, model.MapUrl, out message)) > 0)
                {
                    ModelState.Clear();
                    Session["UID"] = "";
                    TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                    return RedirectToAction("Event");
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                    return RedirectToAction("Event");
                }
            }
            return RedirectToAction("Event");
        }

        public JsonResult DeleteEventAngularJs(string UID = "")
        {
            if (UID != "")
            {
                object message = string.Empty;
                if (new MasterDataEntity().Master_Delete_Event(UID, out message) > 0)
                {
                    TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PopulateEvent()
        public List<SelectListItem> PopulateEvent()
        {
            DataTable dtEvent = new EventDataEntity().Event_Select_EventDDL();
            List<SelectListItem> ddlEvent = new List<SelectListItem>();
            ddlEvent.Add(new SelectListItem { Text = "--Select Event--", Value = "" });
            foreach (DataRow dr in dtEvent.Rows)
            {
                ddlEvent.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
            }
            return ddlEvent;
        }
        #endregion

        #region EventPerformer()
        public ActionResult EventPerformer()
        {
            Session["UID"] = "";
            EventPerformerModel model = new EventPerformerModel();
            model.EventList = PopulateEvent();
            return View(model);
        }
        public JsonResult EventPerformerReportByAngularJS()
        {
            List<EventPerformerModel> model = new List<EventPerformerModel>();
            DataTable dt = new MasterDataEntity().Master_Select_PerformerDetail();
            model = dt.AsEnumerable().Select(x =>
            {
                return new EventPerformerModel()
                {
                    UID = x["UID"].ToString(),
                    EventName = x["EventName"].ToString(),
                    PerformerName = x["Name"].ToString(),
                    Category = x["Category"].ToString(),
                    Fee = x["Fee"].ToString(),
                };

            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EventPerformerEdit(string UID)
        {
            EventPerformerModel model = new EventPerformerModel();
            object message = string.Empty;
            model.EventList = PopulateEvent();
            DataTable dt = new MasterDataEntity().Master_Select_PerformerDetailWithUID(UID, out message);
            if (dt.Rows.Count > 0)
            {
                model.UID = dt.Rows[0]["UID"].ToString();
                model.EventName = dt.Rows[0]["EventName"].ToString();
                ViewData["EventList"] = dt.Rows[0]["EventName"].ToString();
                model.PerformerName = dt.Rows[0]["Name"].ToString();
                model.Category = dt.Rows[0]["Category"].ToString();
                model.Fee = dt.Rows[0]["Fee"].ToString();
                model.Description = dt.Rows[0]["Description"].ToString();
                model.Date = dt.Rows[0]["Date"].ToString();
                model.TimeFrom = dt.Rows[0]["TimeFrom"].ToString();
                model.TimeTo = dt.Rows[0]["TimeTo"].ToString();
                model.ImageUrl = dt.Rows[0]["Image"].ToString().Replace("~/", "/");
                Session["UID"] = dt.Rows[0]["UID"].ToString();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EventPerformer(EventPerformerModel model, HttpPostedFileBase file, FormCollection formCollection)
        {
            string EventID = Request.Form["EventList"];
            string _imageUrl = string.Empty;
            string _fileName = string.Empty;
            if (Session["UID"].ToString() == "")
            {
                if (Request.Files.Count > 0)
                {
                    if (file.FileName.Trim() != "" && file.ContentType.ToUpper().Contains("IMAGE"))
                    {
                        _imageUrl = ConfigurationManager.AppSettings["EventPerformerImagePath"].ToString();

                        _fileName = model.PerformerName + file.FileName.Substring(file.FileName.LastIndexOf("."));

                        _imageUrl = _imageUrl + _fileName;
                        byte[] array;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            file.InputStream.CopyTo(ms);
                            array = ms.GetBuffer();
                        }
                        _CodeClass.SaveCroppedImage(new MemoryStream(array), Server.MapPath(_imageUrl), 400);
                    }
                    else
                    {
                        TempData["Message"] = "<div class='alert alert-danger'>" + "Please select only Image!" + "</div>";
                        return RedirectToAction("EventPerformer", "Admin");
                    }
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger'>" + "Please select file first!" + "</div>";
                    return RedirectToAction("EventPerformer", "Admin");
                }
            }
            else if (Session["UID"].ToString() != "")
            {
                if (Request.Files.Count > 1)
                {
                    if (file.FileName.Trim() != "" && file.ContentType.ToUpper().Contains("IMAGE"))
                    {
                        _imageUrl = ConfigurationManager.AppSettings["EventPerformerImagePath"].ToString();

                        _fileName = model.PerformerName + file.FileName.Substring(file.FileName.LastIndexOf("."));

                        _imageUrl = _imageUrl + _fileName;
                        byte[] array;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            file.InputStream.CopyTo(ms);
                            array = ms.GetBuffer();
                        }
                        _CodeClass.SaveCroppedImage(new MemoryStream(array), Server.MapPath(_imageUrl), 400);
                    }
                }
            }
            object message = string.Empty;
            if (Session["UID"].ToString() == "")
            {
                if ((new MasterDataEntity().Master_Insert_Performer(EventID, model.PerformerName, model.Category, model.Description, Convert.ToDecimal(model.Fee), model.Date, model.TimeFrom, model.TimeTo, _imageUrl, out message)) > 0)
                {
                    ModelState.Clear();
                    TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                    return RedirectToAction("EventPerformer");
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                    return RedirectToAction("EventPerformer");
                }
            }
            else if (Session["UID"].ToString() != "")
            {
                if ((new MasterDataEntity().Master_Update_Performer(Session["UID"].ToString(), model.PerformerName, model.Category, model.Description, Convert.ToDecimal(model.Fee), model.Date, out message)) > 0)
                {
                    ModelState.Clear();
                    Session["UID"] = "";
                    TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                    return RedirectToAction("EventPerformer");
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                    return RedirectToAction("EventPerformer");
                }
            }
            return RedirectToAction("EventPerformer");
        }

        public JsonResult DeleteEventPerformerAngularJs(string UID = "")
        {
            if (UID != "")
            {
                object message = string.Empty;
                if (new MasterDataEntity().Master_Delete_Performer(UID, out message) > 0)
                {
                    TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Geneology()
        public ActionResult GeneologyLevel(string DateFrom = "", string DateTo = "", string LoginName = "")
        {
            _CodeClass.GetStartAndEndDate(DateFrom, DateTo, out _DateFrom, out _DateTo);
            List<GeneologyModel> model = new List<GeneologyModel>();
            DataTable dt = new GenealogyDataEntity().Gen_Report_LevelAdmin(LoginName, _DateFrom, _DateTo);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    model.Add(new GeneologyModel
                    {
                        LoginName = dr["Login Name"].ToString(),
                        UserName = dr["User Name"].ToString(),
                        RepNo = dr["Rep No."].ToString(),
                        SponsorLoginName = dr["Sponsor Login Name"].ToString(),
                        Shopping = dr["Shopping"].ToString(),
                        EnrollmentDate = dr["Enrollment Date"].ToString(),
                        Level = Convert.ToInt32(dr["Level"].ToString()),
                    });
                }
            }
            return View(model.OrderBy(x => x.Level).ToList());
        }
        public ActionResult GeneologyDownline(string DateFrom = "", string DateTo = "", string LoginName = "")
        {
            _CodeClass.GetStartAndEndDate(DateFrom, DateTo, out _DateFrom, out _DateTo);
            List<GeneologyModel> model = new List<GeneologyModel>();
            DataTable dt = new GenealogyDataEntity().Gen_Report_DownlineAdmin(LoginName, _DateFrom, _DateTo);
            int i = 1;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    model.Add(new GeneologyModel
                    {
                        Sno = i++,
                        LoginName = dr["Login Name"].ToString(),
                        UserName = dr["User Name"].ToString(),
                        RepNo = dr["Rep No."].ToString(),
                        SponsorLoginName = dr["Sponsor Login Name"].ToString(),
                        EnrollmentDate = dr["Enrollment Date"].ToString(),
                        Email = dr["Email"].ToString(),
                    });
                }
            }
            return View(model.ToList());
        }
        #endregion

        #region PageManagement()
        public ActionResult PageManagement()
        {
            List<PageModel> PM = new List<PageModel>();
            DataTable dt = new CommonDataEntity().Select_PageList();
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                PM.Add(new PageModel
                {
                    Sno = ++i,
                    PageUrl = dr["Value"].ToString(),
                    PageTitle = dr["Text"].ToString(),
                });
            }
            return View(PM);
        }

        #region GetPage(PageUrl)
        public List<PageModel> GetPage(string PageUrl)
        {
            List<PageModel> PM = new List<PageModel>();
            DataTable dt = new CommonDataEntity().Page_Get_Detail(PageUrl);
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                PM.Add(new PageModel
                {
                    Sno = ++i,
                    PageUrl = PageUrl,
                    PageHTML = dr["HTML"].ToString(),
                });
            }
            return PM;
        }
        #endregion

        #region PageSearch(PageUrl)
        [HttpPost]
        public PartialViewResult PageSearch(string Url)
        {
            List<PageModel> PM = new List<PageModel>();
            DataTable dt = new CommonDataEntity().Select_PageList();
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                PM.Add(new PageModel
                {
                    Sno = ++i,
                    PageUrl = dr["Value"].ToString(),
                    PageTitle = dr["Text"].ToString(),
                });
            }
            PM = PM.Where(x => x.PageUrl.ToUpperInvariant().Contains(Url.ToUpperInvariant())).ToList();
            return PartialView("_PageList", PM);
        }
        #endregion

        #region PageEdit(PageUrl)
        [HttpGet, ValidateInput(false)]
        public PartialViewResult PageEdit(string PageUrl)
        {
            List<PageModel> PM = new List<PageModel>();
            PM = GetPage(PageUrl);
            return PartialView("_PageEdit", PM);
        }
        #endregion

        #region PageView(PageUrl, Html)
        [HttpPost, ValidateInput(false)]
        public PartialViewResult PageView(string Url, string Html)
        {
            List<PageModel> PM = new List<PageModel>();
            PM = GetPage(Url);
            var st = PM.Find(c => c.PageUrl == Url);
            PM.Remove(st);
            PM.Add(new PageModel
            {
                PageUrl = Url,
                PageHTML = Html,
            });
            return PartialView("_PageView", PM);
        }
        #endregion

        #region PageUpdate(Url, Html)
        [HttpPost, ValidateInput(false)]
        public ActionResult PageUpdate(string Url, string Html)
        {
            object message = string.Empty;
            string flag = "";
            if ((new CommonDataEntity().Page_Update_Detail(Url, Html, out message)) > 0)
            {
                TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                flag = "1";
            }
            else
            {
                TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                flag = "0";
            }
            return Json(new { flag = flag, msg = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion

        #region GenerateCode()
        public ActionResult GenerateCode()
        {
            GenerateCodeModel model = new GenerateCodeModel();
            List<GenerateCodeModel> NCode = new List<GenerateCodeModel>();
            List<GenerateCodeModel> SCode = new List<GenerateCodeModel>();
            DataTable dt = new MasterDataEntity().Master_GetCodeAdmin();
            foreach (DataRow dr in dt.Rows)
            {
                NCode.Add(new GenerateCodeModel
                {
                    Sno = Convert.ToInt32(dr["SrNo"]),
                    Code = dr["Code"].ToString(),
                });
            }
            model.NewCode = NCode;
            int i = 0;
            DataTable dt2 = new MasterDataEntity().Master_GetSelectedCodeAdmin();
            foreach (DataRow dr in dt2.Rows)
            {
                SCode.Add(new GenerateCodeModel
                {
                    Sno = ++i,
                    Code = dr["Code"].ToString(),
                    Date = dr["Date"].ToString(),
                    Status = dr["Status"].ToString(),
                });
            }
            model.SelectedCode = SCode;
            return View(model);
        }
        [HttpPost]
        public ActionResult GenerateCode(string Code)
        {
            object message = string.Empty;
            string flag = "";
            if ((new MasterDataEntity().Master_SetCodeAdmin(Code, out message)) > 0)
            {
                TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                flag = "1";
            }
            else
            {
                TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                flag = "0";
            }
            return Json(new { flag = flag, msg = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion



        #region PopulateContentPrice()
        public List<SelectListItem> PopulateContentPrice()
        {
            DataTable dtPrice = new CommonDataEntity().Select_ContentPriceDDL();
            List<SelectListItem> ddlPrice = new List<SelectListItem>();
            ddlPrice.Add(new SelectListItem { Text = "--Select Price--", Value = "" });
            foreach (DataRow dr in dtPrice.Rows)
            {
                ddlPrice.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                ViewData["Price"] = ddlPrice;
            }
            return ddlPrice;
        }
        #endregion

        #region PopulateUserCategory()
        public List<SelectListItem> PopulateUserCategory()
        {
            DataTable dtUserCategory = new MasterDataEntity().Master_Select_TalentCategoryDDL();
            List<SelectListItem> ddlUserCategory = new List<SelectListItem>();
            ddlUserCategory.Add(new SelectListItem { Text = "--Select Category--", Value = "" });
            foreach (DataRow dr in dtUserCategory.Rows)
            {
                ddlUserCategory.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                ViewData["Category"] = ddlUserCategory;
            }
            return ddlUserCategory;
        }
        public List<SelectListItem> PopulateUserSubCategory(string Category)
        {
            List<SelectListItem> ddlUserSubCategory = new List<SelectListItem>();
            ddlUserSubCategory.Add(new SelectListItem { Text = "<< Select SubCategory >>", Value = "" });
            if (Category.ToString() != "")
            {
                DataTable dtUserSubCategory = new MasterDataEntity().Master_Select_TalentSubCategoryDDL(Category);
                if (dtUserSubCategory.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtUserSubCategory.Rows)
                    {
                        ddlUserSubCategory.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                    }
                }
            }
            return ddlUserSubCategory;
        }
        public JsonResult GetUserSubCategory(string Category)
        {
            List<SelectListItem> ddlUserSubCategory = new List<SelectListItem>();
            ddlUserSubCategory.Add(new SelectListItem { Text = "<< Select SubCategory >>", Value = "" });
            if (Category.ToString() != "")
            {
                DataTable dtUserSubCategory = new MasterDataEntity().Master_Select_TalentSubCategoryDDL(Category);
                if (dtUserSubCategory.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtUserSubCategory.Rows)
                    {
                        ddlUserSubCategory.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                    }
                }
            }
            return Json(new SelectList(ddlUserSubCategory.OrderBy(x => x.Text), "Value", "Text"), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region UploadAudio()
        [ChildActionOnly]
        public PartialViewResult UploadAudio()
        {
            AdminUploadModel model = new AdminUploadModel();
            List<SelectListItem> ddlPrice = new List<SelectListItem>();
            ddlPrice.Add(new SelectListItem { Text = "--Select Price--", Value = "" });
            ddlPrice.Add(new SelectListItem { Text = "$1.24", Value = "1.24" });
            ddlPrice.Add(new SelectListItem { Text = "$14.99", Value = "14.99" });
            model.Price = ddlPrice;
            model.Category = PopulateUserCategory().Where(x => x.Text == "MUSIC").ToList();
            string category = "";
            if (model.Category.Count == 0) { category = ""; } else { category = model.Category[0].Value; }
            model.SubCategory = PopulateUserSubCategory(category);
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult UploadAudio(AdminUploadModel model, HttpPostedFileBase[] files, FormCollection form)
        {
            string _AudioUrlBase = string.Empty; ;
            string _fileNameBase = string.Empty;
            float _fileLenghth;
            foreach (HttpPostedFileBase file in files)
            {
                if (file.FileName.Trim() != "")
                {
                    string ext = System.IO.Path.GetExtension(file.FileName).ToLower();
                    if (ext == ".mp3")
                    {
                        _fileLenghth = file.ContentLength;
                        var fileLenghth = (_fileLenghth / 1048576);
                        if (_fileLenghth < 10485760)
                        {
                            _AudioUrlBase = ConfigurationManager.AppSettings["TalentMediaPath"].ToString();

                            _fileNameBase = file.FileName.ToString();

                            _AudioUrlBase = _AudioUrlBase + _fileNameBase;
                            file.SaveAs(Server.MapPath(_AudioUrlBase));
                            object message = string.Empty;
                            string Price = Request.Form["Price"];
                            string Category = Request.Form["Category"];
                            string SubCategory = Request.Form["SubCategory"];
                            if (new MasterDataEntity().Master_Insert_ProductAdmin(model.FileName, model.Description, _AudioUrlBase, Convert.ToDecimal(fileLenghth), Convert.ToDecimal(Price), 'A', Category, SubCategory, out message) > 0)
                            {
                                TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                                ModelState.Clear();
                                return RedirectToAction("AddProduct");
                            }
                            else
                            {
                                TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                            }
                        }
                        else
                        {
                            TempData["Message"] = "<div class='alert alert-danger'>" + "File size exceeds maximum limit 10 MB." + "</div>";
                            return RedirectToAction("AddProduct");
                        }
                    }
                    else
                    {
                        TempData["Message"] = "<div class='alert alert-danger'>" + "Please Select Audio File." + "</div>";
                        return RedirectToAction("AddProduct");
                    }
                }
            }
            return RedirectToAction("AddProduct");
        }
        #endregion

        #region UploadImage()
        [ChildActionOnly]
        public PartialViewResult UploadImage()
        {
            AdminUploadModel model = new AdminUploadModel();
            List<SelectListItem> ddlPrice = new List<SelectListItem>();
            ddlPrice.Add(new SelectListItem { Text = "$3.00", Value = "3.00" });
            model.Price = ddlPrice;
            model.ImageCategory = PopulateUserCategory().Where(x => x.Text != "DIGITAL E-BOOKS" && x.Text != "BOOK WRITING" && x.Text != "FILM" && x.Text != "MUSIC" && x.Text != "BOOKS" && x.Text != "FILM/MOVIE" && x.Text != "EVENT").ToList();
            string category = "";
            if (model.ImageCategory.Count == 0) { category = ""; } else { category = model.ImageCategory[0].Value; }
            model.ImageSubCategory = PopulateUserSubCategory("");
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult UploadImage(AdminUploadModel model, HttpPostedFileBase[] files)
        {
            string _imageUrlBase = string.Empty; ;
            string _fileNameBase = string.Empty;
            string _fileNameBaseFile = string.Empty;
            float _fileLenghth;
            foreach (HttpPostedFileBase file in files)
            {
                if (file.FileName.Trim() != "")
                {
                    _fileLenghth = file.ContentLength;
                    _fileLenghth = (_fileLenghth / 1048576);
                    if (file.FileName.Trim() != "" && file.ContentType.ToUpper().Contains("IMAGE"))
                    {
                        _imageUrlBase = ConfigurationManager.AppSettings["TalentMediaPath"].ToString();
                        Stream strm = file.InputStream;
                        using (var image = System.Drawing.Image.FromStream(strm))
                        {
                            int newWidth = 210;
                            int newHeight = 210;
                            var thumbImg = new SD.Bitmap(newWidth, newHeight);
                            var thumbGraph = SD.Graphics.FromImage(thumbImg);
                            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            var imgRectangle = new SD.Rectangle(0, 0, newWidth, newHeight);
                            thumbGraph.DrawImage(image, imgRectangle);
                            _imageUrlBase = _imageUrlBase + file.FileName;
                            thumbImg.Save(Server.MapPath(_imageUrlBase), image.RawFormat);
                        }
                        object message = string.Empty;
                        string Price = Request.Form["Price"];
                        string ImageCategory = Request.Form["ImageCategory"];
                        string ImageSubCategory = Request.Form["ImageSubCategory"];
                        if (new MasterDataEntity().Master_Insert_ProductAdmin(model.FileName, model.Description, _imageUrlBase, Convert.ToDecimal(_fileLenghth), Convert.ToDecimal(Price), 'I', ImageCategory, ImageSubCategory, out message) > 0)
                        {
                            TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                            ModelState.Clear();
                            return RedirectToAction("AddProduct");
                        }
                        else
                        {
                            TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                            return RedirectToAction("AddProduct");
                        }
                    }
                    else
                    {
                        TempData["Message"] = "<div class='alert alert-danger'>" + "Please Select Image File." + "</div>";
                        return RedirectToAction("AddProduct");
                    }
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger'>" + "Please Select Image File." + "</div>";
                }
            }
            return RedirectToAction("AddProduct");
        }
        #endregion

        #region UploadVideo()
        [ChildActionOnly]
        public PartialViewResult UploadVideo()
        {
            AdminUploadModel model = new AdminUploadModel();
            List<SelectListItem> ddlPrice = new List<SelectListItem>();
            ddlPrice.Add(new SelectListItem { Text = "$17.99", Value = "17.99" });
            model.Price = ddlPrice;
            model.Category = PopulateUserCategory().Where(x => x.Text == "FILM/MOVIE" || x.Text == "FILM").ToList();
            string category = "";
            if (model.Category.Count == 0) { category = ""; } else { category = model.Category[0].Value; }
            model.SubCategory = PopulateUserSubCategory(category);
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult UploadVideo(AdminUploadModel model, HttpPostedFileBase[] files, FormCollection form)
        {
            string _VedioUrlBase = string.Empty; ;
            string _fileNameBase = string.Empty;
            float _fileLenghth;

            foreach (HttpPostedFileBase file in files)
            {
                if (file.FileName.Trim() != "")
                {
                    string ext = System.IO.Path.GetExtension(file.FileName).ToLower();
                    if (ext == ".mp4" || ext == ".avi" || ext == ".wmv")
                    {
                        _fileLenghth = file.ContentLength;
                        var fileLenghth = (_fileLenghth / 1048576);
                        if (_fileLenghth < 104857600)
                        {
                            _VedioUrlBase = ConfigurationManager.AppSettings["TalentMediaPath"].ToString();

                            _fileNameBase = file.FileName.ToString();

                            _VedioUrlBase = _VedioUrlBase + _fileNameBase;

                            file.SaveAs(Server.MapPath(_VedioUrlBase));
                            object message = string.Empty;
                            string Price = Request.Form["Price"];
                            string Category = Request.Form["Category"];
                            string SubCategory = Request.Form["SubCategory"];
                            if (new MasterDataEntity().Master_Insert_ProductAdmin(model.FileName, model.Description, _VedioUrlBase, Convert.ToDecimal(fileLenghth), Convert.ToDecimal(Price), 'V', Category, SubCategory, out message) > 0)
                            {
                                TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                                ModelState.Clear();
                                return RedirectToAction("AddProduct");
                            }
                            else
                            {
                                TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                            }
                        }
                        else
                        {
                            TempData["Message"] = "<div class='alert alert-danger'>" + "File size exceeds maximum limit 20 MB." + "</div>";
                            return RedirectToAction("AddProduct");
                        }
                    }
                    else
                    {
                        TempData["Message"] = "<div class='alert alert-danger'>" + "Please Select Video File." + "</div>";
                        return RedirectToAction("AddProduct");
                    }
                }
            }
            return RedirectToAction("AddProduct");
        }
        #endregion

        #region UploadEbook()
        [ChildActionOnly]
        public PartialViewResult UploadEbook()
        {
            AdminUploadModel model = new AdminUploadModel();
            List<SelectListItem> ddlPrice = new List<SelectListItem>();
            ddlPrice.Add(new SelectListItem { Text = "$3.00", Value = "3.00" });
            model.Price = ddlPrice;
            model.Category = PopulateUserCategory().Where(x => x.Text == "DIGITAL E-BOOKS" || x.Text == "BOOK WRITING" || x.Text == "BOOKS").ToList();
            string category = "";
            if (model.Category.Count == 0) { category = ""; } else { category = model.Category[0].Value; }
            model.SubCategory = PopulateUserSubCategory(category);
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult UploadEbook(AdminUploadModel model, HttpPostedFileBase[] files, FormCollection form)
        {
            string _EbookUrlBase = string.Empty; ;
            string _fileNameBase = string.Empty;
            float _fileLenghth;

            foreach (HttpPostedFileBase file in files)
            {
                if (file.FileName.Trim() != "")
                {
                    string ext = System.IO.Path.GetExtension(file.FileName).ToLower();
                    if (ext == ".txt" || ext == ".doc" || ext == ".ppt" || ext == ".xls" || ext == ".docx" || ext == ".pptx" || ext == ".xlsx" || ext == ".pdf")
                    {
                        _fileLenghth = file.ContentLength;
                        var fileLenghth = (_fileLenghth / 1048576);
                        if (_fileLenghth < 10485760)
                        {
                            _EbookUrlBase = ConfigurationManager.AppSettings["TalentMediaPath"].ToString();

                            _fileNameBase = file.FileName.ToString();
                            _EbookUrlBase = _EbookUrlBase + _fileNameBase;

                            file.SaveAs(Server.MapPath(_EbookUrlBase));
                            object message = string.Empty;
                            string Price = Request.Form["Price"];
                            string Category = Request.Form["Category"];
                            string SubCategory = Request.Form["SubCategory"];
                            if (new MasterDataEntity().Master_Insert_ProductAdmin(model.FileName, model.Description, _EbookUrlBase, Convert.ToDecimal(fileLenghth), Convert.ToDecimal(Price), 'D', Category, SubCategory, out message) > 0)
                            {
                                TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                                ModelState.Clear();
                                return RedirectToAction("AddProduct");
                            }
                            else
                            {
                                TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                            }
                        }
                        else
                        {
                            TempData["Message"] = "<div class='alert alert-danger'>" + "File size exceeds maximum limit 10 MB." + "</div>";
                            return RedirectToAction("AddProduct");
                        }
                    }
                    else
                    {
                        TempData["Message"] = "<div class='alert alert-danger'>" + "Please Select Document File." + "</div>";
                        return RedirectToAction("AddProduct");
                    }
                }
            }
            return RedirectToAction("AddProduct");
        }
        #endregion
    }

    public class AdminAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //string controller = filterContext.RouteData.Values["controller"].ToString();
            filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { area = "Admin", controller = "Logon", action = "Index" }));
        }
    }
}