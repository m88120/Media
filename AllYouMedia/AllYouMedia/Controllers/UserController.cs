using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AllYouMedia.Models;
using System.Data;
using System.IO;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Drawing.Drawing2D;
using System.Drawing;
using AllYouMedia.DataAccess.ServiceLayer;
using Microsoft.AspNet.Identity;
using AllYouMedia.Code;
using BusinessEntity.ConcreateEntity;

namespace AllYouMedia.Controllers
{
    [Authorize(Roles = "AllTalent,Customer,MediaPromoter,Production")]
    public class UserController : SharedAuthController
    {
        #region Private Properties
        private string _dateFrom;
        private string _dateTo;
        DataTable dt;
        List<TreeViewNodeVM> SubNode = new List<TreeViewNodeVM>();
        List<TreeViewNodeVM> SubSubNode = new List<TreeViewNodeVM>();
        TreeViewNodeVM model = new TreeViewNodeVM();
        List<BinaryTreeModel> Tree = new List<BinaryTreeModel>();

        private IAspNetUserService aspNetUserService;
        private IAspNetUserAddressService aspNetUserAddressService;
        private IOrderService orderService;
        private ISubCategoryService subCategoryService;
        private IAspNetUserConnectionService aspNetUserConnectionService;
        private IMessageService messageService;
        private IItemService itemService;
        private IPayoutDistributionService payoutDistributionService;
        private IUserCategoryMapService userCategoryMapService;
        private IFanService fanService;
        private IUserSpotlightService userSpotlightService;
        private ICategoryTypeService categoryTypeService;
        private ICategoryService categoryService;

        #endregion
        public UserController(AspNetUserService aspNetUserService, AspNetUserAddressService aspNetUserAddressService, OrderService orderService, CategoryService categoryService, CategoryTypeService categoryTypeService,SubCategoryService subCategoryService, AspNetUserConnectionService aspNetUserConnectionService, MessageService messageService, ItemService itemService, PayoutDistributionService payoutDistributionService, UserCategoryMapService userCategoryMapService, FanService fanService, UserSpotlightService userSpotlightService)
            : base(aspNetUserService, subCategoryService, itemService, orderService, payoutDistributionService)
        {
            this.aspNetUserService = aspNetUserService;
            this.aspNetUserAddressService = aspNetUserAddressService;
            this.orderService = orderService;
            this.subCategoryService = subCategoryService;
            this.aspNetUserConnectionService = aspNetUserConnectionService;
            this.messageService = messageService;
            this.itemService = itemService;
            this.payoutDistributionService = payoutDistributionService;
            this.userCategoryMapService = userCategoryMapService;
            this.fanService = fanService;
            this.categoryTypeService = categoryTypeService;
            this.categoryService = categoryService;
            this.userSpotlightService = userSpotlightService;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["ActiveMembership"] == null && filterContext.RouteData.Values["action"].ToString().ToLower() != "index")
                filterContext.Result = RedirectToAction("Index");
            base.OnActionExecuting(filterContext);
        }
        #region Index
        public ActionResult Index(int M = 0)
        {

            List<SelectListItem> _subList = new List<SelectListItem>();
            var aspNetUser = aspNetUserService.GetById(Convert.ToInt64(User.Identity.GetUserId()));
            ViewBag.UserAddresses = aspNetUserAddressService.AspNetUserAddressCbo(aspNetUser.Id);
            var userCategoryMaps = userCategoryMapService.GetByAspNetUserID(aspNetUser.Id);
            var allTalent = Convert.ToString(SharedLibrary.MemberTypeEnum.AllTalent);

           // var tt = categoryTypeService.CategoryTypeCboByMembershipType(M);
            //foreach (var item in categoryTypeService.CategoryTypeCboByMembershipType(M))
            //{
            //    _subList.Add(item);
            //}
            //foreach (var item in userCategoryMaps.Where(x => x.AspNetUserRole.RoleId == Convert.ToInt64(SharedLibrary.MemberTypeEnum.AllTalent)))
            //{ 
            //    var test = categoryService.CategoryCboByCategoryTypeMembershipType(M, item.CategoryType.ID);


            //    foreach ( var subItem in categoryService.CategoryCboByCategoryTypeMembershipType(M, item.CategoryTypeID))
            //    {
            //        _subList.Add(subItem);
            //    }
                
            //}
            //ViewBag.SubCategory = _subList;
            ViewBag.UserCategoryMaps = userCategoryMaps;
            ViewBag.RoleList = aspNetUserService.GetRolesDictionary();
            ViewBag.FanCount = fanService.GetFanCount(aspNetUser.Id);
            ViewBag.FanSharingRequestCount = fanService.GetFanSharingRequestCount(aspNetUser.Id);
            //if (aspNetUser.SubCategoryID != -1)
            //    ViewBag.SubCategoryName = subCategoryService.GetById(aspNetUser.SubCategoryID).CBOExpression;

            if (Session["ActiveMembership"] == null && M == 0) { Session["ActiveMembership"] = (SharedLibrary.MemberTypeEnum)aspNetUser.Roles.First().RoleId; }
            else if (M != 0) Session["ActiveMembership"] = (SharedLibrary.MemberTypeEnum)M;
            return View(aspNetUser);
        }
        #endregion

        #region Profile()
        public ActionResult UserProfile()
        {
            var model = aspNetUserService.GetById(Convert.ToInt64(User.Identity.GetUserId()));
            //ViewBag.SubCategoryCBO = subCategoryService.SubCategoryCbo();
            return View(model);
        }
        [HttpPost]
        public ActionResult UserProfile(AspNetUser model)
        {
            var aspNetUser = aspNetUserService.GetById(model.Id);
            aspNetUser.PhotoIMG = model.PhotoIMG;
            aspNetUser.Name = model.Name;
            aspNetUser.PhoneNumber = model.PhoneNumber;
            aspNetUser.BioDescription = model.BioDescription;
            //aspNetUser.SubCategoryID = model.SubCategoryID;
            aspNetUserService.Update(aspNetUser);
            TempData["Success"] = "Profile updated successfully.";
            return RedirectToAction("UserProfile", "User");
        }
        #endregion

        #region UserAddress()
        public ActionResult UserAddress()
        {
            return View();
        }
        public JsonResult UserAddressList(int draw, Dictionary<string, string> search, int start, int length)
        {
            var result = aspNetUserAddressService.GetForDT(Convert.ToInt64(User.Identity.GetUserId()), search["value"], start, length);
            var ds = result.Item1.Select(x =>
            {
                return new
                {
                    DT_RowId = x.ID,
                    x.AddressLine1,
                    x.AddressLine2,
                    x.AspNetUserID,
                    x.City,
                    x.Country,
                    x.Landmark,
                    x.PinCode,
                    x.Province,
                    IsActive = x.IsActive,
                };

            }).ToList();
            return Json(new
            {
                draw = draw,
                data = ds,
                recordsTotal = result.Item2,
                recordsFiltered = result.Item2,
            });
        }
        public JsonResult UserAddressCurd(string action, List<Dictionary<string, string>> data)
        {
            if (data == null || data.Count == 0) return Json(new { error = "Invalid operation" });
            AllYouMedia.DataAccess.EntityLayer.DBEntity.AspNetUserAddress dbAspNetUserAddress = null;
            if (action == "create")
            {
                dbAspNetUserAddress = new DataAccess.EntityLayer.DBEntity.AspNetUserAddress
                {
                    AspNetUserID = Convert.ToInt64(User.Identity.GetUserId()),
                    AddressLine1 = data[0]["AddressLine1"],
                    AddressLine2 = data[0]["AddressLine2"],
                    City = data[0]["City"],
                    Country = data[0]["Country"],
                    Landmark = data[0]["Landmark"],
                    PinCode = data[0]["PinCode"],
                    Province = data[0]["Province"],
                };
                if (string.IsNullOrEmpty(dbAspNetUserAddress.AddressLine1)) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "AddressLine1" }, { "status", "Address Line 1 is required." } } } });
                if (string.IsNullOrEmpty(dbAspNetUserAddress.City)) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "City" }, { "status", "City is required." } } } });
                if (string.IsNullOrEmpty(dbAspNetUserAddress.Country)) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "Country" }, { "status", "Country is required." } } } });
                if (string.IsNullOrEmpty(dbAspNetUserAddress.Province)) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "Province" }, { "status", "Province is required." } } } });
                if (string.IsNullOrEmpty(dbAspNetUserAddress.PinCode)) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "PinCode" }, { "status", "PinCode is required." } } } });
                aspNetUserAddressService.Insert(dbAspNetUserAddress);
                return Json(new { data = new List<AllYouMedia.DataAccess.EntityLayer.DBEntity.AspNetUserAddress> { dbAspNetUserAddress } });
            }
            else if (action == "edit")
            {
                dbAspNetUserAddress = aspNetUserAddressService.GetById(Convert.ToInt64(data[0]["ID"]));
                dbAspNetUserAddress.AspNetUserID = Convert.ToInt64(User.Identity.GetUserId());
                dbAspNetUserAddress.AddressLine1 = data[0]["AddressLine1"];
                dbAspNetUserAddress.AddressLine2 = data[0]["AddressLine2"];
                dbAspNetUserAddress.City = data[0]["City"];
                dbAspNetUserAddress.Country = data[0]["Country"];
                dbAspNetUserAddress.Landmark = data[0]["Landmark"];
                dbAspNetUserAddress.PinCode = data[0]["PinCode"];
                dbAspNetUserAddress.Province = data[0]["Province"];
                if (string.IsNullOrEmpty(dbAspNetUserAddress.AddressLine1)) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "AddressLine1" }, { "status", "Address Line 1 is required." } } } });
                if (string.IsNullOrEmpty(dbAspNetUserAddress.City)) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "City" }, { "status", "City is required." } } } });
                if (string.IsNullOrEmpty(dbAspNetUserAddress.Country)) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "Country" }, { "status", "Country is required." } } } });
                if (string.IsNullOrEmpty(dbAspNetUserAddress.Province)) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "Province" }, { "status", "Province is required." } } } });
                if (string.IsNullOrEmpty(dbAspNetUserAddress.PinCode)) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "PinCode" }, { "status", "PinCode is required." } } } });
                aspNetUserAddressService.Update(dbAspNetUserAddress);
                return Json(new
                {
                    data = new List<object> { new { DT_RowId = dbAspNetUserAddress.ID, dbAspNetUserAddress.AddressLine1, dbAspNetUserAddress.AddressLine2, dbAspNetUserAddress.AspNetUserID, dbAspNetUserAddress.City, dbAspNetUserAddress.Country, dbAspNetUserAddress.Landmark, dbAspNetUserAddress.PinCode, dbAspNetUserAddress.Province, dbAspNetUserAddress.IsActive, } }
                });
            }
            else if (action == "remove")
            {
                dbAspNetUserAddress = aspNetUserAddressService.GetById(Convert.ToInt64(data[0]["ID"]));
                try
                {
                    aspNetUserAddressService.Delete(dbAspNetUserAddress);
                    return Json(new { });
                }
                catch (Exception ex) { return Json(new { error = "Could not delete." }); }
            }
            return Json(new { error = "Invalid operation" });
        }

        #endregion

        #region OrderHistory()
        public ActionResult OrderHistory()
        {
            return View();
        }
        public JsonResult OrderHistoryList(int draw, Dictionary<string, string> search, int start, int length)
        {
            var result = orderService.GetForDT(Convert.ToInt64(User.Identity.GetUserId()), search["value"], start, length);
            var ds = result.Item1.Select(x =>
            {
                return new
                {
                    DT_RowId = x.ID,
                    x.DeliveryDate,
                    x.IsCompleted,
                    x.PayableAmount,
                    x.PayerRefCode,
                    x.PaymentMode,
                    x.PaymentRefCode,
                    x.ShippingDate,
                    x.CreatedOn,
                    Items = x.OrderItems.Select(y => new { y.Item.Name, y.Qty, y.ShippingDate, y.IsCompleted, y.DeliveryDate }),
                    x.AspNetUserAddress.AddressLine1,
                    x.AspNetUserAddress.AddressLine2,
                    x.AspNetUserAddress.City,
                    x.AspNetUserAddress.Country,
                    x.AspNetUserAddress.Province,
                    x.AspNetUserAddress.PinCode,
                    x.AspNetUserAddress.Landmark,
                    IsActive = x.IsActive,
                };

            }).ToList();
            return Json(new
            {
                draw = draw,
                data = ds,
                recordsTotal = result.Item2,
                recordsFiltered = result.Item2,
            });
        }

        #endregion

        #region TeamTree()
        public ActionResult TeamTree()
        {
            return View();
        }
        public JsonResult TeamTreeList()
        {
            var AspNetUserID = Convert.ToInt64(User.Identity.GetUserId());
            var userTree = aspNetUserService.GetUserTree(AspNetUserID);
            return Json(new { Result = "OK", UserTree = userTree });
        }

        #endregion

        #region TalentSearch()
        public ActionResult TalentSearch()
        {
            return View();
        }
        public JsonResult TalentSearchList(long CategoryTypeID, long CategoryID, long SubCategoryID, long AttributeID, string q, int draw, Dictionary<string, string> search, int start, int length)
        {
            var result = aspNetUserService.GetTalentSearchForDT(Convert.ToInt64(User.Identity.GetUserId()), CategoryTypeID, CategoryID, SubCategoryID, AttributeID, q, start, length);
            var ds = result.Item1.Select(x =>
            {
                return new
                {
                    DT_RowId = x.Id,
                    x.UserName,
                    x.Name,
                    //SubCategory = x.UserCategoryMaps.Select(y => y.SubCategory.Name),
                    Location = x.AspNetUserAddresses.Where(y => y.IsActive).Count() > 0 ? x.AspNetUserAddresses.Where(y => y.IsActive).Last().CBOExpression : "",
                };

            }).ToList();
            return Json(new
            {
                draw = draw,
                data = ds,
                recordsTotal = result.Item2,
                recordsFiltered = result.Item2,
            });
        }
        public JsonResult TalentSearchCurd(string action, List<Dictionary<string, string>> data)
        {
            if (data == null || data.Count == 0) return Json(new { error = "Invalid operation" });
            AllYouMedia.DataAccess.EntityLayer.DBEntity.AspNetUserConnection dbAspNetUserConnection = null;
            var AspNetUserID = Convert.ToInt64(User.Identity.GetUserId());
            if (action == "create")
            {
                if (aspNetUserConnectionService.GetConnectedUser(AspNetUserID, Convert.ToInt64(data[0]["ID"])) != null) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "ID" }, { "status", "This user is already connected with you." } } } });
                dbAspNetUserConnection = new DataAccess.EntityLayer.DBEntity.AspNetUserConnection
                {
                    AspNetUserID = AspNetUserID,
                    ConnectedAspNetUserID = Convert.ToInt64(data[0]["ID"]),
                };
                aspNetUserConnectionService.Insert(dbAspNetUserConnection);
                return Json(new { data = new List<AllYouMedia.DataAccess.EntityLayer.DBEntity.AspNetUserConnection> { dbAspNetUserConnection } });
            }
            else if (action == "remove")
            {
                dbAspNetUserConnection = aspNetUserConnectionService.GetConnectedUser(AspNetUserID, Convert.ToInt64(data[0]["ID"]));
                try
                {
                    aspNetUserConnectionService.Delete(dbAspNetUserConnection);
                    return Json(new { });
                }
                catch (Exception ex) { return Json(new { error = "Could not delete." }); }
            }
            return Json(new { error = "Invalid operation" });
        }

        #endregion

        [HttpPost]
        public JsonResult SendMessage(long ToID, string Subject, string Body)
        {
            messageService.Insert(new AllYouMedia.DataAccess.EntityLayer.DBEntity.Message
            {
                Body = Body,
                FromAspNetUserID = Convert.ToInt64(User.Identity.GetUserId()),
                Subject = Subject,
                ToAspNetUserID = ToID,
                Status = 1
            });
            return Json(new { Result = "OK" });
        }

        [AllowAnonymous]
        public ActionResult ProfileView(long ID)
        {
            if (ID == 1) throw new Exception("You are trying to request a invalid page");
            if (ID == -9999 && User.Identity.IsAuthenticated) ID = Convert.ToInt64(User.Identity.GetUserId());
            var model = aspNetUserService.GetById(ID);
            ViewBag.UserAddresses = aspNetUserAddressService.AspNetUserAddressCbo(model.Id);
            var userCategoryMaps = userCategoryMapService.GetByAspNetUserID(model.Id);
            ViewBag.UserCategoryMaps = userCategoryMaps;
            ViewBag.RoleList = aspNetUserService.GetRolesDictionary();
            ViewBag.UserRoleList = model.Roles.Select(x => x.RoleId).ToList();
            ViewBag.FanCount = fanService.GetFanCount(ID);
            ViewBag.FanRating = fanService.GetFanRating(ID);
            ViewBag.UserSpotlight = userSpotlightService.GetUserSpotlightForUser(ID);
            ViewBag.IsSelfProfile = User.Identity.IsAuthenticated && ID == Convert.ToInt64(User.Identity.GetUserId());
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult SaveFanInfo(long ID, string Name, string Email, int Rating)
        {

            if (User.Identity.IsAuthenticated) { Name = aspNetUserService.GetById(Convert.ToInt64(User.Identity.GetUserId())).Name; Email = User.Identity.Name; }
            var result = fanService.BecomeFan(ID, Name, Email, Rating);
            if (result.Item1)
                return Json(new { Result = "OK", Message = result.Item2 });
            else
                return Json(new { Result = "ERROR", Message = result.Item2 });
        }

        [HttpPost]
        public JsonResult SaveUserSpotlight(long ID, int Rating)
        {
            userSpotlightService.SaveSpotlight(ID, Rating, Convert.ToInt64(User.Identity.GetUserId()));
            return Json(new { Result = "OK", Message = "Updated successfully." });
        }

        public JsonResult RequestShareFanToUser(long ID)
        {
            var result = fanService.GenerateFanShareRequestToUser(Convert.ToInt64(User.Identity.GetUserId()), ID);
            if (result.Item1)
                return Json(new { Result = "OK", Message = result.Item2 });
            else
                return Json(new { Result = "ERROR", Message = result.Item2 });
        }

        #region FanSharingRequest
        public ActionResult FanSharingRequest()
        {
            return View();
        }
        [HttpPost]
        public JsonResult UpdateFanSharingUserRequest(long ID, bool IsAccepted)
        {
            fanService.UpdateFanSharingUserRequest(ID, IsAccepted);
            return Json(new { Result = "OK" });
        }
        public JsonResult FanSharingRequestList(int draw, Dictionary<string, string> search, int start, int length)
        {
            var result = fanService.GetFanSharingUserRequestForDT(Convert.ToInt64(User.Identity.GetUserId()), search["value"], start, length);
            var ds = result.Item1;
            return Json(new
            {
                draw = draw,
                data = ds,
                recordsTotal = result.Item2,
                recordsFiltered = result.Item2,
            });
        }
        #endregion

        public JsonResult GetUserInfo(long AspNetUserID)
        {
            var user = aspNetUserService.GetById(AspNetUserID);
            Dictionary<string, string> dicUserDetails = aspNetUserService.GetUserDetails(AspNetUserID);
            var data = new
            {
                LEVELNAME = dicUserDetails["LEVELNAME"],
                USERNAME = dicUserDetails["USERNAME"],
                RECRUITER = dicUserDetails["RECRUITER"],
                RECRUITERNAME = dicUserDetails["RECRUITERNAME"],
                TOTALONTEAM = dicUserDetails["TOTALONTEAM"],
                GROSSPROFITS = dicUserDetails["GROSSPROFITS"],
                NETPROFITTOYOU = dicUserDetails["NETPROFITTOYOU"]
            };
            return Json(new { Result = "OK", Data = data });
        }

        #region Mesage
        public ActionResult Message()
        {
            return View(model);
        }
        public JsonResult MessageList(int draw, Dictionary<string, string> search, int start, int length)
        {
            var result = messageService.GetForDT(Convert.ToInt64(User.Identity.GetUserId()), search["value"], start, length);
            var ds = result.Item1;
            return Json(new
            {
                draw = draw,
                data = ds,
                recordsTotal = result.Item2,
                recordsFiltered = result.Item2,
            });
        }
        #endregion

        public ActionResult ToolBox(int Type)
        {
            ViewBag.Type = Type;
            return View();
        }

        public ActionResult NewMembership()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMembership(int MembershipType, long CategoryTypeID = -1, long CategoryID = -1, long SubCategoryID = -1, long AttributeID = -1)
        {
            SharedLibrary.MemberTypeEnum userType = (SharedLibrary.MemberTypeEnum)MembershipType;
            var UserManager = new AspNetUserManager(new Microsoft.AspNet.Identity.EntityFramework.UserStore<AspNetUser, AspNetRole, long, AspNetUserLogin, AspNetUserRole, AspNetUserClaim>(new AllYouMedia.DataAccess.DataLayer.DataContext()));
            var user = UserManager.FindByName(User.Identity.Name);
            if (UserManager.IsInRole(user.Id, userType.ToString()))
            {
                TempData["Error"] = string.Format("You already have {0} membership.", userType.ToString());
                return RedirectToAction("NewMembership", "User");
            }
            var roleResult = UserManager.AddToRole(user.Id, userType.ToString());
            if (userType == SharedLibrary.MemberTypeEnum.AllTalent || userType == SharedLibrary.MemberTypeEnum.Production)
            {
                userCategoryMapService.Insert(new AllYouMedia.DataAccess.EntityLayer.DBEntity.UserCategoryMap
                {
                    AspNetUserRoleID = aspNetUserService.GetAspNetUserRoleIDByUserAndRole(user.Id, userType.ToString()),
                    AttributeID = AttributeID,
                    CategoryID = CategoryID,
                    CategoryTypeID = CategoryTypeID,
                    SubCategoryID = SubCategoryID
                });
            }
            TempData["Success"] = "Membership registered successfully.";
            return RedirectToAction("Index", "User");
        }

        public ActionResult VirtualAudition() { return View(); }

        [AllowAnonymous]
        public JsonResult ItemListForProfileViewPage(long ID, int draw, Dictionary<string, string> search, int start, int length)
        {
            if (ID == -99999 && User.Identity.IsAuthenticated) ID = Convert.ToInt64(User.Identity.GetUserId());
            var result = itemService.GetForDT(ID, search["value"], start, length);
            var ds = result.Item1.Select(x =>
            {
                return new
                {
                    DT_RowId = x.ID,
                    SubCategory = x.SubCategory.Name,
                    x.Name,
                    x.PhotoIMG,
                    x.SellPrice,
                    x.ShortDescription,
                    x.LongDescription,
                    x.IsPromoted,
                    x.IsFeatured
                };

            }).ToList();
            return Json(new
            {
                draw = draw,
                data = ds,
                recordsTotal = result.Item2,
                recordsFiltered = result.Item2,
            });
        }
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
            ddlTalentSubCategory.Add(new SelectListItem { Text = "<< Select SubCategory >>", Value = "" });
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


        //#region ChangePassword()
        //public ActionResult ChangePassword()
        //{
        //    if (Session["Role"] == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        Session["Role"] = Session["Role"].ToString();
        //    }
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult ChangePassword(UserPasswordModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        object message = string.Empty;
        //        DataTable dt = new RoleDataEntity().Role_Update_UserPasswordAfterLogin(model.OldPassword.Trim(), model.Password.Trim(), out message);

        //        if (dt.Rows.Count > 0)
        //        {
        //            _CodeClass.SendMail(dt.Rows[0]["MAIL"].ToString(), dt.Rows[0]["MAIL SUBJECT"].ToString(), dt.Rows[0]["MAIL BODY"].ToString());
        //            TempData["Message"] = message;
        //            ModelState.Clear();
        //        }
        //        else
        //        {
        //            TempData["Error"] = message;
        //        }
        //    }
        //    return View();
        //}
        //#endregion

        #region ChangeImage()
        public ActionResult ChangeImage()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            DataTable dt = new RoleDataEntity().Role_Get_TalentBioImage();
            List<ChangeImageModel> model = new List<ChangeImageModel>();
            foreach (DataRow dr in dt.Rows)
            {
                model.Add(new ChangeImageModel
                {
                    UID = dr["UID"].ToString(),
                    ImageUrl = dr["Image_Url"].ToString().Replace("~/", "/"),
                });
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult ChangeImage(HttpPostedFileBase file)
        {
            string _imageUrlBase = string.Empty; ;
            string _imageUrlBigBase = string.Empty;
            string _fileNameBase = string.Empty;
            string _fileNameBaseFile = string.Empty;
            string _FileUrl = string.Empty;

            if (file != null)
            {
                _imageUrlBase = ConfigurationManager.AppSettings["TalentImagePath"].ToString();

                if (file.FileName.Trim() != "" && file.ContentType.ToUpper().Contains("IMAGE"))
                {
                    Stream strm = file.InputStream;
                    using (var image = System.Drawing.Image.FromStream(strm))
                    {
                        int newWidth = 210;
                        int newHeight = 210;
                        var thumbImg = new Bitmap(newWidth, newHeight);
                        var thumbGraph = Graphics.FromImage(thumbImg);
                        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                        thumbGraph.DrawImage(image, imgRectangle);
                        _imageUrlBase = _imageUrlBase + file.FileName;
                        thumbImg.Save(Server.MapPath(_imageUrlBase), image.RawFormat);
                    }
                }
                else
                {
                    TempData["Error"] = "Please select only Image!";
                    return RedirectToAction("ChangeImage", "User");
                }

                RoleDataEntity rd = new RoleDataEntity();
                object message = string.Empty;

                if (rd.Role_Set_TalentBioImage(_imageUrlBase, out message) > 0)
                {
                    TempData["Message"] = message;
                }
                else
                {
                    TempData["Error"] = message;
                }
            }
            else
            {
                TempData["Error"] = "Please select a Image first!";
            }
            return RedirectToAction("ChangeImage", "User");
        }
        [HttpGet]
        public JsonResult DeleteBioImage(string UID, string MediaUrl)
        {
            object message = string.Empty;
            if (UID != "")
            {
                if (new RoleDataEntity().Role_Delete_TalentBioImage(UID, out message) > 0)
                {
                    if (System.IO.File.Exists(Server.MapPath(MediaUrl)) == true)
                    {
                        System.IO.File.Delete(Server.MapPath(MediaUrl));
                    }
                    TempData["Message"] = message;
                    message = "1";
                }
                else
                {
                    TempData["Error"] = message;
                    message = "0";
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult SetProfileImage(string UID)
        {
            object message = string.Empty;
            if (UID != "")
            {
                if (new RoleDataEntity().Role_Change_TalentBioImage(UID, out message) > 0)
                {
                    TempData["Message"] = message;
                    message = "1";
                }
                else
                {
                    TempData["Error"] = message;
                    message = "0";
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region BioGraphProfile()
        public ActionResult BioGraphProfile()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            BioUploadModel model = new BioUploadModel();
            DataSet dsDetails = new RoleDataEntity().Role_Select_UserProfileBiography();
            if (dsDetails.Tables[0].Rows.Count > 0)
            {
                model.UserName = dsDetails.Tables[0].Rows[0]["Name"].ToString();
                model.Address = dsDetails.Tables[0].Rows[0]["Address"].ToString();
                model.Biography = dsDetails.Tables[0].Rows[0]["Biography"].ToString();
                model.Role = dsDetails.Tables[0].Rows[0]["Role"].ToString();
                model.Rate = Convert.ToInt32(dsDetails.Tables[0].Rows[0]["Rate"].ToString());
                model.ImageUrl = (dsDetails.Tables[0].Rows[0]["IMAGE"].ToString()).Replace("~/", "/");
                model.Talent = dsDetails.Tables[0].Rows[0]["Talent"].ToString();
            }
            return View(model);
        }
        #endregion

        #region BioUploadContent()
        public ActionResult BioUploadContent()
        {
            //if (Session["Role"] == null)
            //{
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    Session["Role"] = Session["Role"].ToString();
            //}
            Session["Role"] = Convert.ToString(new RoleDataEntity().Role_Get_TalentRole());
            Session["MediaDocumentUID"] = "";
            Session["FileName"] = "";
            ViewBag.albumlist = false;
            BioUploadModel model = new BioUploadModel();
            DataTable dt_UserCategory = new RoleDataEntity().Role_Get_UserTalentCategory();
            List<ContentUploadModel> Upload = new List<ContentUploadModel>();
            foreach (DataRow dr in dt_UserCategory.Rows)
            {
                Upload.Add(new ContentUploadModel
                {
                    CategoryName = dr["CategoryName"].ToString(),
                });
            }
            for (var j = 0; j < Upload.Count; j++)
            {
                string CategoryName = (Upload[j].CategoryName).ToUpper();
                if (CategoryName == "MUSIC")
                {
                    Upload[j].CategoryName = "MUSIC";
                    Upload[j].CategoryID = "btnAudio";
                    Upload[j].CategoryText = "Click to Upload AUDIO";
                }
                else if (CategoryName == "FILM" || CategoryName == "FILM/MOVIE")
                {
                    Upload[j].CategoryName = "FILM/MOVIE";
                    Upload[j].CategoryID = "btnFilm";
                    Upload[j].CategoryText = "Click to Upload VIDEO";
                }
                else if (CategoryName == "BOOK WRITING" || CategoryName == "BOOKS")
                {
                    Upload[j].CategoryName = "BOOK";
                    Upload[j].CategoryID = "btnEbook";
                    Upload[j].CategoryText = "Click to Upload E-BOOK";
                }
                else
                {
                    Upload[j].CategoryName = "PHOTO's";
                    Upload[j].CategoryID = "btnImage";
                    Upload[j].CategoryText = "Click to Upload PICTURES";
                }
            }
            model.Upload = Upload.GroupBy(x => x.CategoryName, (key, group) => group.First()).ToList();
            DataSet dsDetails = new RoleDataEntity().Role_Select_UserProfileBiography();
            if (dsDetails.Tables[0].Rows.Count > 0)
            {
                model.UserName = dsDetails.Tables[0].Rows[0]["Name"].ToString();
                model.Address = dsDetails.Tables[0].Rows[0]["Address"].ToString();
                model.Biography = dsDetails.Tables[0].Rows[0]["Biography"].ToString();
                model.Category = dsDetails.Tables[0].Rows[0]["Talent"].ToString();
                model.Role = dsDetails.Tables[0].Rows[0]["Role"].ToString();
                model.Rate = Convert.ToInt32(dsDetails.Tables[0].Rows[0]["Rate"].ToString());
                model.ImageUrl = (dsDetails.Tables[0].Rows[0]["IMAGE"].ToString()).Replace("~/", "/");
                model.Talent = dsDetails.Tables[0].Rows[0]["Talent"].ToString();
            }

            DataTable dt_TalentBioImage = new RoleDataEntity().Role_Get_TalentBioImage();
            List<ChangeImageModel> TalentImage = new List<ChangeImageModel>();
            foreach (DataRow dr in dt_TalentBioImage.Rows)
            {
                TalentImage.Add(new ChangeImageModel
                {
                    UID = dr["UID"].ToString(),
                    ImageUrl = dr["Image_Url"].ToString().Replace("~/", "/"),
                });
            }
            model.TalentImage = TalentImage;
            model.UserAlbumList = PopulateUserAlbum();
            if (model.UserAlbumList.Count >= 2)
            {
                ViewBag.albumlist = true;
            }
            else
            {
                ViewBag.albumlist = false;
            }
            model.UserAlbumStatus = PopulateAlbumStatus();
            return View(model);
        }
        public JsonResult TalentMediaAngularJs()
        {
            List<TalentMediaModel> model = new List<TalentMediaModel>();
            int i = 1;
            DataTable dt = new RoleDataEntity().Role_Select_UserMedia();
            model = dt.AsEnumerable().Select(x =>
            {
                return new TalentMediaModel()
                {
                    Sno = i++,
                    UID = x["UID"].ToString(),
                    Name = x["Name"].ToString(),
                    FileUrl = x["FileUrl"].ToString().Replace("~/", "/"),
                    Type = x["Type"].ToString(),
                    TypeV = (x["VF"].ToString()),
                    TypeA = (x["AF"].ToString()),
                    TypeI = (x["IM"].ToString()),
                    TypeD = (x["DF"].ToString()),
                    Price = _CodeClass.GetCurrency(x["Price"].ToString()),
                    Description = x["Description"].ToString(),
                    Rate = Convert.ToInt32(x["Rate"]),
                    Category = x["Category"].ToString(),
                    Album = x["Album"].ToString(),
                };
            }).ToList();
            for (var j = 0; j < model.Count; j++)
            {
                string Category = model[j].Category;
                string Album = model[j].Album;

                if (Category == "MUSIC" && Album != "")
                {
                    model[j].Category = Category + " (" + Album + ")";
                }
                else if (Category == "MUSIC" && Album == "")
                {
                    model[j].Category = Category + " (Singles)";
                }
            }
            for (var j = 0; j < model.Count; j++)
            {
                string FileUrl = model[j].FileUrl;
                string ext = System.IO.Path.GetExtension(FileUrl).ToLower();
                string Type = model[j].Type;

                if ((ext == ".doc" || ext == ".docx") && Type == "Document")
                {
                    model[j].FileUrl = "/Images/Document/.doc.png";
                }
                else if ((ext == ".ppt" || ext == ".pptx") && Type == "Document")
                {
                    model[j].FileUrl = "/Images/Document/.ppt.png";
                }
                else if ((ext == ".xls" || ext == ".xlsx") && Type == "Document")
                {
                    model[j].FileUrl = "/Images/Document/.xls.png";
                }
                else if (ext == ".pdf" && Type == "Document")
                {
                    model[j].FileUrl = "/Images/Document/.pdf.png";
                }
                else if (Type == "Document")
                {
                    model[j].FileUrl = "/Images/document.png";
                }
            }
            //return Json(model.OrderBy(x => x.Category), JsonRequestBehavior.AllowGet);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteTalentMediaAngularJs(string UID = "", string MediaUrl = "")
        {
            if (UID != "")
            {
                object message = string.Empty;
                if (new MasterDataEntity().Master_Delete_Media(UID, out message) > 0)
                {
                    if (System.IO.File.Exists(Server.MapPath(MediaUrl)) == true)
                    {
                        System.IO.File.Delete(Server.MapPath(MediaUrl));
                    }
                    TempData["Message"] = message;
                }
                else
                {
                    TempData["Error"] = message;
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult BioUploadContent(string[] Name)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            ViewBag.albumlist = false;
            DataTable dtW = new DataTable("Table");
            dtW.Columns.Add(new DataColumn("W_UID", typeof(string)));
            foreach (string Names in Name)
            {
                dtW.Rows.Add(Names);
            }
            if (dtW.Rows.Count > 0)
            {
                object message = string.Empty;

                if (new RoleDataEntity().Role_SetUserTalentSelection(dtW, out message) > 0)
                {
                    TempData["Message"] = message;
                }
                else
                {
                    TempData["Error"] = message;
                }
            }
            else
            {
                TempData["Error"] = "Atleast one row must be selected!";
            }

            return RedirectToAction("BioUploadContent");
        }
        [HttpPost]
        public ActionResult UpdateBio(BioUploadModel model)
        {
            object message = string.Empty;
            ViewBag.albumlist = false;
            if ((new RoleDataEntity().Role_Set_BioGraphText(model.Biography, out message)) > 0)
            {
                TempData["Message"] = message.ToString();
            }
            else
            {
                TempData["Error"] = message.ToString();
            }
            return RedirectToAction("BioUploadContent");
        }
        public ActionResult GetAlbumStatus(string cid)
        {
            string Status = "";
            if (cid != "")
            {
                DataTable dt = new RoleDataEntity().Role_GetAlbumStatus(cid);
                if (dt.Rows.Count > 0)
                {
                    Status = dt.Rows[0]["Status"].ToString();
                }
            }
            return Json(new { success = true, Status = Status }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ChangeAlbumStatus(string Album = "", string Status = "")
        {
            object message = string.Empty;
            string flag = "";
            if ((new RoleDataEntity().Role_UpdateAlbumStatus(Album, Status, out message)) > 0)
            {
                flag = "1";
            }
            else
            {
                flag = "0";
            }
            return Json(new { flag = flag, msg = message }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteAlbum(string Album = "", string Status = "")
        {
            object message = string.Empty;
            string flag = "";
            if ((new RoleDataEntity().Role_DeleteUserAlbum(Album, out message)) > 0)
            {
                flag = "1";
            }
            else
            {
                flag = "0";
            }
            return Json(new { flag = flag, msg = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region BioPage()
        public ActionResult BioPage(string LoginName = "")
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            ViewBag.AlreadyRate = true;
            ViewBag.AlreadyFan = true;
            if (LoginName != "")
            {
                BioUploadModel model = new BioUploadModel();
                DataSet dsDetails = new RoleDataEntity().Role_Select_UserProfileBiography(LoginName);
                if (dsDetails.Tables[0].Rows.Count > 0)
                {
                    model.LoginName = dsDetails.Tables[0].Rows[0]["Login Name"].ToString();
                    model.UserName = dsDetails.Tables[0].Rows[0]["Name"].ToString();
                    model.Address = dsDetails.Tables[0].Rows[0]["Address"].ToString();
                    model.Biography = dsDetails.Tables[0].Rows[0]["Biography"].ToString();
                    model.Role = dsDetails.Tables[0].Rows[0]["Role"].ToString();
                    model.Rate = Convert.ToInt32(dsDetails.Tables[0].Rows[0]["Rate"].ToString());
                    model.ImageUrl = (dsDetails.Tables[0].Rows[0]["IMAGE"].ToString()).Replace("~/", "/");
                    model.Talent = dsDetails.Tables[0].Rows[0]["Talent"].ToString();
                    model.TotalFan = dsDetails.Tables[0].Rows[0]["TotalFan"].ToString();
                }
                if (model.ImageUrl == "")
                {
                    model.ImageUrl = "/Images/img_128x128.png";
                }
                object message = string.Empty;
                if (new RoleDataEntity().Role_Check_TalentRating(LoginName, out message) > 0)
                {
                    ViewBag.AlreadyRate = false;
                }
                if (new RoleDataEntity().Role_Check_TalentFan(LoginName) > 0)
                {
                    ViewBag.AlreadyFan = false;
                }
                TempData["UserLoginName"] = LoginName;
                return View(model);
            }
            else
            {
                return RedirectToAction("TalentSearch");
            }
        }
        public JsonResult UserTalentMediaAngularJs()
        {
            List<TalentMediaModel> model = new List<TalentMediaModel>();
            int i = 1;
            DataTable dt = new RoleDataEntity().Role_Select_UserMedia(TempData["UserLoginName"].ToString());
            model = dt.AsEnumerable().Select(x =>
            {
                return new TalentMediaModel()
                {
                    Sno = i++,
                    UID = x["UID"].ToString(),
                    Name = x["Name"].ToString(),
                    FileUrl = x["FileUrl"].ToString().Replace("~/", "/"),
                    Type = x["Type"].ToString(),
                    TypeV = (x["VF"].ToString()),
                    TypeA = (x["AF"].ToString()),
                    TypeI = (x["IM"].ToString()),
                    TypeD = (x["DF"].ToString()),
                    Price = x["Price"].ToString(),
                    Description = x["Description"].ToString(),
                    Rate = Convert.ToInt32(x["Rate"]),
                    Category = x["Category"].ToString(),
                };
            }).ToList();
            for (var j = 0; j < model.Count; j++)
            {
                string FileUrl = model[j].FileUrl;
                string ext = System.IO.Path.GetExtension(FileUrl).ToLower();
                string Type = model[j].Type;

                if ((ext == ".doc" || ext == ".docx") && Type == "Document")
                {
                    model[j].FileUrl = "/Images/Document/.doc.png";
                }
                else if ((ext == ".ppt" || ext == ".pptx") && Type == "Document")
                {
                    model[j].FileUrl = "/Images/Document/.ppt.png";
                }
                else if ((ext == ".xls" || ext == ".xlsx") && Type == "Document")
                {
                    model[j].FileUrl = "/Images/Document/.xls.png";
                }
                else if (ext == ".pdf" && Type == "Document")
                {
                    model[j].FileUrl = "/Images/Document/.pdf.png";
                }
                else if (Type == "Document")
                {
                    model[j].FileUrl = "/Images/document.png";
                }
            }
            return Json(model.OrderBy(x => x.Sno), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult BioPage(FormCollection formCollection)
        {
            ViewBag.AlreadyRate = false;
            ViewBag.AlreadyFan = false;
            object message = string.Empty;
            string name = Request.Form["Name"];
            string Rate = Request.Form["Rate"].Replace(",", "");
            if (new RoleDataEntity().Role_Insert_TalentRating(name, Convert.ToInt32(Rate), out message) > 0)
            {
                TempData["Message"] = message;
            }
            else
            {
                TempData["Error"] = message;
            }
            return RedirectToAction("TalentSearch", "User");
        }
        [HttpPost]
        public ActionResult BecomeFan(FormCollection formCollection)
        {
            ViewBag.AlreadyRate = false;
            ViewBag.AlreadyFan = false;
            object message = string.Empty;
            string Talentname = Request.Form["Name"];
            if (new RoleDataEntity().Role_Insert_TalentFan(Talentname, out message) > 0)
            {
                TempData["Message"] = message;
            }
            else
            {
                TempData["Error"] = message;
            }
            return RedirectToAction("TalentSearch", "User");
        }
        [HttpGet]
        public JsonResult UnfollowFan(string LoginName)
        {
            object message = string.Empty;
            string flag = "";
            if (LoginName != "")
            {
                if (new RoleDataEntity().Role_Delete_TalentFan(LoginName, out message) > 0)
                {
                    TempData["Message"] = message;
                    flag = "1";
                }
                else
                {
                    flag = "0";
                }
            }
            return Json(new { flag = flag, msg = message, newurl = Url.Action("TalentSearch") }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ShareFanBaseRequest(string LoginName)
        {
            object message = string.Empty;
            string flag = "";
            if (LoginName != "")
            {
                DataTable dt = new RoleDataEntity().Role_Insert_TalentFanBaseShare(LoginName, out message);
                if (dt.Rows.Count > 0)
                {
                    _CodeClass.SendMail(dt.Rows[0]["MAIL"].ToString(), dt.Rows[0]["MAIL SUBJECT"].ToString(), dt.Rows[0]["MAIL BODY"].ToString());
                    TempData["Message"] = message;
                    flag = "1";
                }
                else
                {
                    flag = "0";
                }
            }
            return Json(new { flag = flag, msg = message, newurl = Url.Action("TalentSearch") }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Populate()
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
        public List<SelectListItem> PopulateUserAlbum()
        {
            DataTable dtAlbum = new RoleDataEntity().Role_Select_UserAlbumList();
            List<SelectListItem> ddlAlbum = new List<SelectListItem>();
            ddlAlbum.Add(new SelectListItem { Text = "--Select Album--", Value = "" });
            foreach (DataRow dr in dtAlbum.Rows)
            {
                ddlAlbum.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                ViewData["AlbumList"] = ddlAlbum;
            }
            return ddlAlbum;
        }
        public List<SelectListItem> PopulateAlbumStatus()
        {
            DataTable dtAlbumStatus = new CommonDataEntity().Select_AlbumStatusDDL();
            List<SelectListItem> ddlAlbumStatus = new List<SelectListItem>();
            ddlAlbumStatus.Add(new SelectListItem { Text = "--Select --", Value = "" });
            foreach (DataRow dr in dtAlbumStatus.Rows)
            {
                ddlAlbumStatus.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                ViewData["UserAlbumStatus"] = ddlAlbumStatus;
            }
            return ddlAlbumStatus;
        }
        #endregion

        #region PopulateUserCategory()
        public List<SelectListItem> PopulateUserCategory()
        {
            DataTable dtUserCategory = new RoleDataEntity().Role_Get_UserTalentCategory();
            List<SelectListItem> ddlUserCategory = new List<SelectListItem>();
            ddlUserCategory.Add(new SelectListItem { Text = "--Select Category--", Value = "" });
            foreach (DataRow dr in dtUserCategory.Rows)
            {
                ddlUserCategory.Add(new SelectListItem { Text = dr["CategoryName"].ToString(), Value = dr["CategoryUID"].ToString() });
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
                DataTable dtUserSubCategory = new RoleDataEntity().Role_Get_UserTalentSubCategory(Category);
                if (dtUserSubCategory.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtUserSubCategory.Rows)
                    {
                        ddlUserSubCategory.Add(new SelectListItem { Text = dr["SubCategoryName"].ToString(), Value = dr["SubCategoryUID"].ToString() });
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
                DataTable dtUserSubCategory = new RoleDataEntity().Role_Get_UserTalentSubCategory(Category);
                if (dtUserSubCategory.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtUserSubCategory.Rows)
                    {
                        ddlUserSubCategory.Add(new SelectListItem { Text = dr["SubCategoryName"].ToString(), Value = dr["SubCategoryUID"].ToString() });
                    }
                }
            }
            return Json(new SelectList(ddlUserSubCategory.OrderBy(x => x.Text), "Value", "Text"), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PopulateTalentSubSubCategory()
        public List<SelectListItem> PopulateTalentSubSubCategory(string TalentCategory)
        {
            List<SelectListItem> ddlTalentSubSubCategory = new List<SelectListItem>();
            ddlTalentSubSubCategory.Add(new SelectListItem { Text = "<< Select >>", Value = "" });
            if (TalentCategory.ToString() != "")
            {
                DataTable dtTalentSubSubCategory = new MasterDataEntity().Master_Select_TalentSubSubCategoryDDL(TalentCategory);
                if (dtTalentSubSubCategory.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTalentSubSubCategory.Rows)
                    {
                        ddlTalentSubSubCategory.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                    }
                }
            }
            return ddlTalentSubSubCategory;
        }
        public JsonResult PopulateTalentSubSubCategoryAngularJs(string TalentCategory = "")
        {
            List<SelectListItem> ddlTalentSubSubCategory = new List<SelectListItem>();
            ddlTalentSubSubCategory.Add(new SelectListItem { Text = "<< Select >>", Value = "" });
            if (TalentCategory.ToString() != "")
            {
                DataTable dtTalentSubSubCategory = new MasterDataEntity().Master_Select_TalentSubSubCategoryDDL(TalentCategory);
                if (dtTalentSubSubCategory.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTalentSubSubCategory.Rows)
                    {
                        ddlTalentSubSubCategory.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                    }
                }
            }
            return Json(ddlTalentSubSubCategory.ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Upload
        #region UploadAudio()
        [ChildActionOnly]
        public PartialViewResult UploadAudio()
        {
            ViewBag.albumlist = false;
            UploadModel model = new UploadModel();
            List<SelectListItem> ddlPrice = new List<SelectListItem>();
            List<SelectListItem> ddlAlbumPrice = new List<SelectListItem>();
            ddlPrice.Add(new SelectListItem { Text = "$1.24", Value = "1.24" });
            ddlPrice.Add(new SelectListItem { Text = "$14.99", Value = "14.99" });
            model.Price = ddlPrice;
            ddlAlbumPrice.Add(new SelectListItem { Text = "$14.99", Value = "14.99" });
            model.Price = ddlPrice;
            model.AlbumPrice = ddlAlbumPrice;
            model.Category = PopulateUserCategory().Where(x => x.Text == "MUSIC").ToList();
            string category = "";
            if (model.Category.Count == 0) { category = ""; } else { category = model.Category[0].Value; }
            model.SubCategory = PopulateUserSubCategory(category);
            model.Genre = PopulateTalentSubSubCategory(category);
            model.AlbumList = PopulateUserAlbum();
            if (model.AlbumList.Count > 2)
            {
                ViewBag.albumlist = true;
            }
            else
            {
                ViewBag.albumlist = false;
            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult UploadAudio(UploadModel model, HttpPostedFileBase[] files, HttpPostedFileBase ClipFile, HttpPostedFileBase CoverImage, HttpPostedFileBase ImageUrl, FormCollection form)
        {
            string _AudioUrlBase = string.Empty;
            string _AudioClipUrlBase = string.Empty;
            string _CoverimageUrlBase = string.Empty;
            string _fileNameBase = string.Empty;
            string _ClipfileNameBase = string.Empty;
            float _fileLenghth;
            if (Session["MediaDocumentUID"].ToString() == "" || Session["FileName"].ToString() != model.FileName)
            {
                foreach (HttpPostedFileBase file in files)
                {
                    if (file != null && ClipFile != null)
                    {
                        string ext = System.IO.Path.GetExtension(file.FileName).ToLower();
                        string Clipext = System.IO.Path.GetExtension(ClipFile.FileName).ToLower();
                        if (ext == ".mp3" && Clipext == ".mp3")
                        {
                            _fileLenghth = file.ContentLength;
                            var fileLenghth = (_fileLenghth / 1048576);
                            if (_fileLenghth < 10485760)
                            {
                                _AudioUrlBase = ConfigurationManager.AppSettings["TalentMediaPath"].ToString();
                                _fileNameBase = file.FileName.ToString();
                                _AudioUrlBase = _AudioUrlBase + _fileNameBase;
                                file.SaveAs(Server.MapPath(_AudioUrlBase));

                                _AudioClipUrlBase = ConfigurationManager.AppSettings["TalentMediaPath"].ToString();
                                _ClipfileNameBase = ClipFile.FileName.ToString();
                                _AudioClipUrlBase = _AudioClipUrlBase + "Clip" + _ClipfileNameBase;
                                ClipFile.SaveAs(Server.MapPath(_AudioClipUrlBase));

                                if (CoverImage.FileName.Trim() != "" && CoverImage.ContentType.ToUpper().Contains("IMAGE"))
                                {
                                    _CoverimageUrlBase = ConfigurationManager.AppSettings["TalentMediaPath"].ToString();
                                    Stream strm = CoverImage.InputStream;
                                    using (var image = System.Drawing.Image.FromStream(strm))
                                    {
                                        int newWidth = 210;
                                        int newHeight = 210;
                                        var thumbImg = new Bitmap(newWidth, newHeight);
                                        var thumbGraph = Graphics.FromImage(thumbImg);
                                        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                                        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                                        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                        var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                                        thumbGraph.DrawImage(image, imgRectangle);
                                        _CoverimageUrlBase = _CoverimageUrlBase + "Cover" + CoverImage.FileName;
                                        thumbImg.Save(Server.MapPath(_CoverimageUrlBase), image.RawFormat);
                                    }
                                }
                                else
                                {
                                    TempData["Error"] = "Please Select Image File as Cover.";
                                    return RedirectToAction("BioUploadContent");
                                }

                                object message = string.Empty;
                                string Price = Request.Form["Price"];
                                string Category = Request.Form["Category"];
                                string SubCategory = Request.Form["SubCategory"];
                                string SubSubCategory = Request.Form["Genre"];
                                if (new MasterDataEntity().Master_Insert_ProductionMember_Content(model.FileName, model.Description, _AudioUrlBase, _AudioClipUrlBase, _CoverimageUrlBase, Convert.ToDecimal(fileLenghth), Convert.ToDecimal(Price), 'A', Category, SubCategory, SubSubCategory, out message) > 0)
                                {
                                    TempData["Message"] = message;
                                    ModelState.Clear();
                                    return RedirectToAction("BioUploadContent");
                                }
                                else
                                {
                                    TempData["Error"] = message;
                                }
                            }
                            else
                            {
                                TempData["Error"] = "File size exceeds maximum limit 10 MB.";
                                return RedirectToAction("BioUploadContent");
                            }
                        }
                        else
                        {
                            TempData["Error"] = "Please Select Audio File.";
                            return RedirectToAction("BioUploadContent");
                        }
                    }
                }
            }
            else if (Session["MediaDocumentUID"].ToString() != "" && Session["FileName"].ToString() == model.FileName)
            {
                if (CoverImage != null)
                {
                    if (CoverImage.FileName.Trim() != "" && CoverImage.ContentType.ToUpper().Contains("IMAGE"))
                    {
                        _CoverimageUrlBase = ConfigurationManager.AppSettings["TalentMediaPath"].ToString();
                        Stream strm = CoverImage.InputStream;
                        using (var image = System.Drawing.Image.FromStream(strm))
                        {
                            int newWidth = 210;
                            int newHeight = 210;
                            var thumbImg = new Bitmap(newWidth, newHeight);
                            var thumbGraph = Graphics.FromImage(thumbImg);
                            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                            thumbGraph.DrawImage(image, imgRectangle);
                            _CoverimageUrlBase = _CoverimageUrlBase + "Cover" + CoverImage.FileName;
                            thumbImg.Save(Server.MapPath(_CoverimageUrlBase), image.RawFormat);
                        }
                    }
                }
                if (ClipFile != null)
                {
                    string Clipext = System.IO.Path.GetExtension(ClipFile.FileName).ToLower();
                    if (Clipext == ".mp3")
                    {
                        _AudioClipUrlBase = ConfigurationManager.AppSettings["TalentMediaPath"].ToString();
                        _ClipfileNameBase = ClipFile.FileName.ToString();
                        _AudioClipUrlBase = _AudioClipUrlBase + "Clip" + _ClipfileNameBase;
                        ClipFile.SaveAs(Server.MapPath(_AudioClipUrlBase));
                    }
                    else
                    {
                        TempData["Error"] = "Please Select Audio File as clip file";
                        return RedirectToAction("BioUploadContent");
                    }
                }
                object message = string.Empty;
                string Price = Request.Form["Price"];
                string Category = Request.Form["Category"];
                string SubCategory = Request.Form["SubCategory"];
                string SubSubCategory = Request.Form["Genre"];
                if (_CoverimageUrlBase == string.Empty)
                {
                    _CoverimageUrlBase = Convert.ToString(ImageUrl);
                }
                if (new MasterDataEntity().Master_Update_ProductionMember_Content(Session["MediaDocumentUID"].ToString(), model.FileName, model.Description, _AudioClipUrlBase, _CoverimageUrlBase, Convert.ToDecimal(Price), Category, SubCategory, SubSubCategory, out message) > 0)
                {
                    TempData["Message"] = message;
                    ModelState.Clear();
                    return RedirectToAction("BioUploadContent");
                }
                else
                {
                    TempData["Error"] = message;
                }
            }
            Session["MediaDocumentUID"] = "";
            return RedirectToAction("BioUploadContent");
        }
        [HttpPost]
        public ActionResult UploadAlbumAudio(UploadModel model, HttpPostedFileBase[] files, HttpPostedFileBase ClipFile, HttpPostedFileBase CoverImage, HttpPostedFileBase AlbumCoverImage, HttpPostedFileBase ImageUrl, HttpPostedFileBase AlbumImageUrl, FormCollection form)
        {
            string _AudioUrlBase = string.Empty;
            string _AudioClipUrlBase = string.Empty;
            string _CoverimageUrlBase = string.Empty;
            string _AlbumCoverimageUrlBase = string.Empty;
            string _fileNameBase = string.Empty;
            string _ClipfileNameBase = string.Empty;
            float _fileLenghth;
            if (Session["MediaDocumentUID"].ToString() == "" || Session["FileName"].ToString() != model.FileName)
            {
                foreach (HttpPostedFileBase file in files)
                {
                    if (file.FileName.Trim() != "" && ClipFile != null)
                    {
                        string ext = System.IO.Path.GetExtension(file.FileName).ToLower();
                        string Clipext = System.IO.Path.GetExtension(ClipFile.FileName).ToLower();
                        if (ext == ".mp3" && Clipext == ".mp3")
                        {
                            _fileLenghth = file.ContentLength;
                            var fileLenghth = (_fileLenghth / 1048576);
                            if (_fileLenghth < 10485760)
                            {
                                _AudioUrlBase = ConfigurationManager.AppSettings["TalentMediaPath"].ToString();
                                _fileNameBase = file.FileName.ToString();
                                _AudioUrlBase = _AudioUrlBase + _fileNameBase;
                                file.SaveAs(Server.MapPath(_AudioUrlBase));


                                _AudioClipUrlBase = ConfigurationManager.AppSettings["TalentMediaPath"].ToString();
                                _ClipfileNameBase = ClipFile.FileName.ToString();
                                _AudioClipUrlBase = _AudioClipUrlBase + "Clip" + _ClipfileNameBase;
                                ClipFile.SaveAs(Server.MapPath(_AudioClipUrlBase));

                                if (CoverImage.FileName.Trim() != null && CoverImage.ContentType.ToUpper().Contains("IMAGE"))
                                {
                                    _CoverimageUrlBase = ConfigurationManager.AppSettings["TalentMediaPath"].ToString();
                                    Stream strm = CoverImage.InputStream;
                                    using (var image = System.Drawing.Image.FromStream(strm))
                                    {
                                        int newWidth = 210;
                                        int newHeight = 210;
                                        var thumbImg = new Bitmap(newWidth, newHeight);
                                        var thumbGraph = Graphics.FromImage(thumbImg);
                                        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                                        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                                        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                        var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                                        thumbGraph.DrawImage(image, imgRectangle);
                                        _CoverimageUrlBase = _CoverimageUrlBase + "Cover" + CoverImage.FileName;
                                        thumbImg.Save(Server.MapPath(_CoverimageUrlBase), image.RawFormat);
                                    }
                                }
                                else
                                {
                                    TempData["Error"] = "Please Select Image File for Cover";
                                }
                                if (AlbumCoverImage != null)
                                {
                                    if (AlbumCoverImage.ContentType.ToUpper().Contains("IMAGE"))
                                    {
                                        _AlbumCoverimageUrlBase = ConfigurationManager.AppSettings["TalentMediaPath"].ToString();
                                        Stream strm = AlbumCoverImage.InputStream;
                                        using (var image = System.Drawing.Image.FromStream(strm))
                                        {
                                            int newWidth = 210;
                                            int newHeight = 210;
                                            var thumbImg = new Bitmap(newWidth, newHeight);
                                            var thumbGraph = Graphics.FromImage(thumbImg);
                                            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                                            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                                            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                            var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                                            thumbGraph.DrawImage(image, imgRectangle);
                                            _AlbumCoverimageUrlBase = _AlbumCoverimageUrlBase + "AlbumCover" + AlbumCoverImage.FileName;
                                            thumbImg.Save(Server.MapPath(_AlbumCoverimageUrlBase), image.RawFormat);
                                        }
                                    }
                                    else
                                    {
                                        TempData["Error"] = "Please Select Image File for Album Cover";
                                    }
                                }

                                object message = string.Empty;
                                string Price = Request.Form["AlbumPrice"];
                                string AlbumNameExist = Request.Form["AlbumList"];
                                if (AlbumNameExist == null) { AlbumNameExist = ""; }
                                if (model.AlbumName == null) { model.AlbumName = ""; }
                                if (model.AlbumDescription == null) { model.AlbumDescription = ""; }
                                string Category = Request.Form["Category"];
                                string SubCategory = Request.Form["SubCategory"];
                                string SubSubCategory = Request.Form["Genre"];
                                if (new MasterDataEntity().Master_Insert_ProductionMember_AlbumContent(model.FileName, model.Description, _AudioUrlBase, _AudioClipUrlBase, _CoverimageUrlBase, Convert.ToDecimal(fileLenghth), Convert.ToDecimal(Price), 'A', Category, SubCategory, SubSubCategory, model.AlbumName, AlbumNameExist, model.AlbumDescription, _AlbumCoverimageUrlBase, out message) > 0)
                                {
                                    TempData["Message"] = message;
                                    ModelState.Clear();
                                }
                                else
                                {
                                    TempData["Error"] = message;
                                }
                            }
                            else
                            {
                                TempData["Error"] = "File size exceeds maximum limit 10 MB.";
                            }
                        }
                        else
                        {
                            TempData["Error"] = "Please Select Audio File.";
                        }
                    }
                }
            }
            else if (Session["MediaDocumentUID"].ToString() != "" && Session["FileName"].ToString() == model.FileName)
            {
                if (CoverImage != null)
                {
                    if (CoverImage.FileName.Trim() != null && CoverImage.ContentType.ToUpper().Contains("IMAGE"))
                    {
                        _CoverimageUrlBase = ConfigurationManager.AppSettings["TalentMediaPath"].ToString();
                        Stream strm = CoverImage.InputStream;
                        using (var image = System.Drawing.Image.FromStream(strm))
                        {
                            int newWidth = 210;
                            int newHeight = 210;
                            var thumbImg = new Bitmap(newWidth, newHeight);
                            var thumbGraph = Graphics.FromImage(thumbImg);
                            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                            thumbGraph.DrawImage(image, imgRectangle);
                            _CoverimageUrlBase = _CoverimageUrlBase + "Cover" + CoverImage.FileName;
                            thumbImg.Save(Server.MapPath(_CoverimageUrlBase), image.RawFormat);
                        }
                    }
                }
                if (AlbumCoverImage != null)
                {
                    if (AlbumCoverImage.FileName.Trim() != null && AlbumCoverImage.ContentType.ToUpper().Contains("IMAGE"))
                    {
                        _AlbumCoverimageUrlBase = ConfigurationManager.AppSettings["TalentMediaPath"].ToString();
                        Stream strm = CoverImage.InputStream;
                        using (var image = System.Drawing.Image.FromStream(strm))
                        {
                            int newWidth = 210;
                            int newHeight = 210;
                            var thumbImg = new Bitmap(newWidth, newHeight);
                            var thumbGraph = Graphics.FromImage(thumbImg);
                            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                            thumbGraph.DrawImage(image, imgRectangle);
                            _AlbumCoverimageUrlBase = _AlbumCoverimageUrlBase + "Cover" + AlbumCoverImage.FileName;
                            thumbImg.Save(Server.MapPath(_AlbumCoverimageUrlBase), image.RawFormat);
                        }
                    }
                }
                if (ClipFile != null)
                {
                    string Clipext = System.IO.Path.GetExtension(ClipFile.FileName).ToLower();
                    if (Clipext == ".mp3")
                    {
                        _AudioClipUrlBase = ConfigurationManager.AppSettings["TalentMediaPath"].ToString();
                        _ClipfileNameBase = ClipFile.FileName.ToString();
                        _AudioClipUrlBase = _AudioClipUrlBase + "Clip" + _ClipfileNameBase;
                        ClipFile.SaveAs(Server.MapPath(_AudioClipUrlBase));
                    }
                    else
                    {
                        TempData["Error"] = "Please Select Audio File as clip file";
                        return RedirectToAction("BioUploadContent");
                    }
                }
                object message = string.Empty;
                string AlbumNameExist = Request.Form["AlbumList"];
                if (AlbumNameExist == null) { AlbumNameExist = ""; }
                if (model.AlbumName == null) { model.AlbumName = ""; }
                if (model.AlbumDescription == null) { model.AlbumDescription = ""; }
                string Category = Request.Form["Category"];
                string SubCategory = Request.Form["SubCategory"];
                string SubSubCategory = Request.Form["Genre"];
                if (_CoverimageUrlBase == string.Empty)
                {
                    _CoverimageUrlBase = Convert.ToString(ImageUrl);
                }
                if (_AlbumCoverimageUrlBase == string.Empty)
                {
                    _AlbumCoverimageUrlBase = Convert.ToString(AlbumImageUrl);
                }
                if (new MasterDataEntity().Master_Update_ProductionMember_AlbumContent(Session["MediaDocumentUID"].ToString(), model.FileName, model.Description, _AudioClipUrlBase, _CoverimageUrlBase, Category, SubCategory, SubSubCategory, model.AlbumName, model.AlbumDescription, _AlbumCoverimageUrlBase, out message) > 0)
                {
                    TempData["Message"] = message;
                    ModelState.Clear();
                }
                else
                {
                    TempData["Error"] = message;
                }
            }
            Session["MediaDocumentUID"] = "";
            Session["FileName"] = "";
            return RedirectToAction("BioUploadContent");
        }
        #endregion

        #region UploadImage()
        [ChildActionOnly]
        public PartialViewResult UploadImage()
        {
            ViewBag.albumlist = false;
            UploadModel model = new UploadModel();
            model.ImageCategory = PopulateUserCategory().Where(x => x.Text != "BOOKS" && x.Text != "BOOK WRITING" && x.Text != "FILM" && x.Text != "MUSIC" && x.Text != "EVENT PROMOTIONS").ToList();
            string category = "";
            if (model.ImageCategory.Count == 0) { category = ""; } else { category = model.ImageCategory[0].Value; }
            model.ImageSubCategory = PopulateUserSubCategory("");
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult UploadImage(UploadModel model, HttpPostedFileBase[] files)
        {
            string _imageUrlBase = string.Empty; ;
            string _fileNameBase = string.Empty;
            string _fileNameBaseFile = string.Empty;
            float _fileLenghth;
            if (Session["MediaDocumentUID"].ToString() == "")
            {
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
                                var thumbImg = new Bitmap(newWidth, newHeight);
                                var thumbGraph = Graphics.FromImage(thumbImg);
                                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                                thumbGraph.DrawImage(image, imgRectangle);
                                _imageUrlBase = _imageUrlBase + file.FileName;
                                thumbImg.Save(Server.MapPath(_imageUrlBase), image.RawFormat);
                            }
                            object message = string.Empty;
                            string Price = model.PriceRate;
                            string ImageCategory = Request.Form["ImageCategory"];
                            string ImageSubCategory = Request.Form["ImageSubCategory"];
                            if (new MasterDataEntity().Master_Insert_ProductionMember_Content(model.FileName, model.Description, _imageUrlBase, "", "", Convert.ToDecimal(_fileLenghth), Convert.ToDecimal(Price), 'I', ImageCategory, ImageSubCategory, "", out message) > 0)
                            {
                                TempData["Message"] = message;
                                ModelState.Clear();
                                return RedirectToAction("BioUploadContent");
                            }
                            else
                            {
                                TempData["Error"] = message;
                                return RedirectToAction("BioUploadContent");
                            }
                        }
                        else
                        {
                            TempData["Error"] = "Please Select Image File.";
                            return RedirectToAction("BioUploadContent");
                        }
                    }
                    else
                    {
                        TempData["Error"] = "Please Select Image File.";
                    }
                }
            }
            else if (Session["MediaDocumentUID"].ToString() != "")
            {
                object message = string.Empty;
                string Price = Request.Form["Price"];
                string Category = Request.Form["Category"];
                string SubCategory = Request.Form["SubCategory"];
                string SubSubCategory = Request.Form["Genre"];
                if (new MasterDataEntity().Master_Update_ProductionMember_Content(Session["MediaDocumentUID"].ToString(), model.FileName, model.Description, "", "", Convert.ToDecimal(Price), Category, SubCategory, SubSubCategory, out message) > 0)
                {
                    TempData["Message"] = message;
                    ModelState.Clear();
                    return RedirectToAction("BioUploadContent");
                }
                else
                {
                    TempData["Error"] = message;
                }
            }
            return RedirectToAction("BioUploadContent");
        }
        #endregion

        #region UploadVideo()
        [ChildActionOnly]
        public PartialViewResult UploadVideo()
        {
            ViewBag.albumlist = false;
            UploadModel model = new UploadModel();
            List<SelectListItem> ddlPrice = new List<SelectListItem>();
            ddlPrice.Add(new SelectListItem { Text = "$17.99", Value = "17.99" });
            model.Price = ddlPrice;
            model.Category = PopulateUserCategory().Where(x => x.Text == "FILM").ToList();
            string category = "";
            if (model.Category.Count == 0) { category = ""; } else { category = model.Category[0].Value; }
            model.SubCategory = PopulateUserSubCategory(category);
            model.Genre = PopulateTalentSubSubCategory(category);
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult UploadVideo(UploadModel model, HttpPostedFileBase[] files, HttpPostedFileBase CoverImage, FormCollection form)
        {
            string _CoverimageUrlBase = string.Empty;
            string _VedioUrlBase = string.Empty;
            string _fileNameBase = string.Empty;
            float _fileLenghth;
            if (Session["MediaDocumentUID"].ToString() == "")
            {
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
                                if (CoverImage.FileName.Trim() != "" && CoverImage.ContentType.ToUpper().Contains("IMAGE"))
                                {
                                    _CoverimageUrlBase = ConfigurationManager.AppSettings["TalentMediaPath"].ToString();
                                    Stream strm = CoverImage.InputStream;
                                    using (var image = System.Drawing.Image.FromStream(strm))
                                    {
                                        int newWidth = 210;
                                        int newHeight = 210;
                                        var thumbImg = new Bitmap(newWidth, newHeight);
                                        var thumbGraph = Graphics.FromImage(thumbImg);
                                        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                                        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                                        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                        var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                                        thumbGraph.DrawImage(image, imgRectangle);
                                        _CoverimageUrlBase = _CoverimageUrlBase + "Cover" + CoverImage.FileName;
                                        thumbImg.Save(Server.MapPath(_CoverimageUrlBase), image.RawFormat);
                                    }
                                }
                                else
                                {
                                    TempData["Error"] = "Please Select Image File.";
                                    return RedirectToAction("BioUploadContent");
                                }
                                object message = string.Empty;
                                string Price = Request.Form["Price"];
                                string Category = Request.Form["Category"];
                                string SubCategory = Request.Form["SubCategory"];
                                string SubSubCategory = Request.Form["Genre"];
                                if (new MasterDataEntity().Master_Insert_ProductionMember_Content(model.FileName, model.Description, _VedioUrlBase, "", _CoverimageUrlBase, Convert.ToDecimal(fileLenghth), Convert.ToDecimal(Price), 'V', Category, SubCategory, SubSubCategory, out message) > 0)
                                {
                                    TempData["Message"] = message;
                                    ModelState.Clear();
                                    return RedirectToAction("BioUploadContent");
                                }
                                else
                                {
                                    TempData["Error"] = message;
                                }
                            }
                            else
                            {
                                TempData["Error"] = "File size exceeds maximum limit 20 MB.";
                                return RedirectToAction("BioUploadContent");
                            }
                        }
                        else
                        {
                            TempData["Error"] = "Please Select Video File.";
                            return RedirectToAction("BioUploadContent");
                        }
                    }
                }
            }
            else if (Session["MediaDocumentUID"].ToString() != "")
            {
                if (CoverImage != null)
                {
                    if (CoverImage.FileName.Trim() != "" && CoverImage.ContentType.ToUpper().Contains("IMAGE"))
                    {
                        _CoverimageUrlBase = ConfigurationManager.AppSettings["TalentMediaPath"].ToString();
                        Stream strm = CoverImage.InputStream;
                        using (var image = System.Drawing.Image.FromStream(strm))
                        {
                            int newWidth = 210;
                            int newHeight = 210;
                            var thumbImg = new Bitmap(newWidth, newHeight);
                            var thumbGraph = Graphics.FromImage(thumbImg);
                            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                            thumbGraph.DrawImage(image, imgRectangle);
                            _CoverimageUrlBase = _CoverimageUrlBase + "Cover" + CoverImage.FileName;
                            thumbImg.Save(Server.MapPath(_CoverimageUrlBase), image.RawFormat);
                        }
                    }
                }
                object message = string.Empty;
                string Price = Request.Form["Price"];
                string Category = Request.Form["Category"];
                string SubCategory = Request.Form["SubCategory"];
                string SubSubCategory = Request.Form["Genre"];
                if (_CoverimageUrlBase == string.Empty)
                {
                    _CoverimageUrlBase = Convert.ToString(Request.Form["ImgUrl"]);
                }
                if (new MasterDataEntity().Master_Update_ProductionMember_Content(Session["MediaDocumentUID"].ToString(), model.FileName, model.Description, "", _CoverimageUrlBase, Convert.ToDecimal(Price), Category, SubCategory, SubSubCategory, out message) > 0)
                {
                    TempData["Message"] = message;
                    ModelState.Clear();
                    return RedirectToAction("BioUploadContent");
                }
                else
                {
                    TempData["Error"] = message;
                }
            }
            return RedirectToAction("BioUploadContent");
        }
        #endregion

        #region UploadEbook()
        [ChildActionOnly]
        public PartialViewResult UploadEbook()
        {
            ViewBag.albumlist = false;
            UploadModel model = new UploadModel();
            model.Category = PopulateUserCategory().Where(x => x.Text == "BOOKS" || x.Text == "BOOK WRITING").ToList();
            string category = "";
            if (model.Category.Count == 0) { category = ""; } else { category = model.Category[0].Value; }
            model.SubCategory = PopulateUserSubCategory(category);
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult UploadEbook(UploadModel model, HttpPostedFileBase[] files, HttpPostedFileBase CoverImage, FormCollection form)
        {
            string _EbookUrlBase = string.Empty;
            string _CoverimageUrlBase = string.Empty;
            string _fileNameBase = string.Empty;
            float _fileLenghth;
            if (Session["MediaDocumentUID"].ToString() == "")
            {
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

                                if (CoverImage.FileName.Trim() != "" && CoverImage.ContentType.ToUpper().Contains("IMAGE"))
                                {
                                    _CoverimageUrlBase = ConfigurationManager.AppSettings["TalentMediaPath"].ToString();
                                    Stream strm = CoverImage.InputStream;
                                    using (var image = System.Drawing.Image.FromStream(strm))
                                    {
                                        int newWidth = 210;
                                        int newHeight = 210;
                                        var thumbImg = new Bitmap(newWidth, newHeight);
                                        var thumbGraph = Graphics.FromImage(thumbImg);
                                        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                                        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                                        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                        var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                                        thumbGraph.DrawImage(image, imgRectangle);
                                        _CoverimageUrlBase = _CoverimageUrlBase + "Cover" + CoverImage.FileName;
                                        thumbImg.Save(Server.MapPath(_CoverimageUrlBase), image.RawFormat);
                                    }
                                }
                                else
                                {
                                    TempData["Error"] = "Please Select Image File.";
                                    return RedirectToAction("BioUploadContent");
                                }
                                object message = string.Empty;
                                string Price = model.PriceRate;
                                string Category = Request.Form["Category"];
                                string SubCategory = Request.Form["SubCategory"];
                                if (new MasterDataEntity().Master_Insert_ProductionMember_Content(model.FileName, model.Description, _EbookUrlBase, "", _CoverimageUrlBase, Convert.ToDecimal(fileLenghth), Convert.ToDecimal(Price), 'D', Category, SubCategory, "", out message) > 0)
                                {
                                    TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                                    ModelState.Clear();
                                    return RedirectToAction("BioUploadContent");
                                }
                                else
                                {
                                    TempData["Error"] = message;
                                }
                            }
                            else
                            {
                                TempData["Error"] = "File size exceeds maximum limit 10 MB.";
                                return RedirectToAction("BioUploadContent");
                            }
                        }
                        else
                        {
                            TempData["Error"] = "Please Select Document File.";
                            return RedirectToAction("BioUploadContent");
                        }
                    }
                }
            }
            else if (Session["MediaDocumentUID"].ToString() != "")
            {
                if (CoverImage != null)
                {
                    if (CoverImage.FileName.Trim() != "" && CoverImage.ContentType.ToUpper().Contains("IMAGE"))
                    {
                        _CoverimageUrlBase = ConfigurationManager.AppSettings["TalentMediaPath"].ToString();
                        Stream strm = CoverImage.InputStream;
                        using (var image = System.Drawing.Image.FromStream(strm))
                        {
                            int newWidth = 210;
                            int newHeight = 210;
                            var thumbImg = new Bitmap(newWidth, newHeight);
                            var thumbGraph = Graphics.FromImage(thumbImg);
                            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                            thumbGraph.DrawImage(image, imgRectangle);
                            _CoverimageUrlBase = _CoverimageUrlBase + "Cover" + CoverImage.FileName;
                            thumbImg.Save(Server.MapPath(_CoverimageUrlBase), image.RawFormat);
                        }
                    }
                }
                object message = string.Empty;
                string Price = Request.Form["Price"];
                string Category = Request.Form["Category"];
                string SubCategory = Request.Form["SubCategory"];
                string SubSubCategory = Request.Form["Genre"];
                if (_CoverimageUrlBase == string.Empty)
                {
                    _CoverimageUrlBase = Convert.ToString(Request.Form["ImgUrl"]);
                }
                if (new MasterDataEntity().Master_Update_ProductionMember_Content(Session["MediaDocumentUID"].ToString(), model.FileName, model.Description, "", _CoverimageUrlBase, Convert.ToDecimal(Price), Category, SubCategory, SubSubCategory, out message) > 0)
                {
                    TempData["Message"] = message;
                    ModelState.Clear();
                    return RedirectToAction("BioUploadContent");
                }
                else
                {
                    TempData["Error"] = message;
                }
            }
            return RedirectToAction("BioUploadContent");
        }
        #endregion

        [HttpPost]
        public JsonResult EditTalentMediaAngularJs(string UID, string MediaUrl, string MediaType)
        {
            EditUploadModel model = new EditUploadModel();
            object message = string.Empty;
            DataTable dt = new MasterDataEntity().Master_Edit_Media(UID);
            if (dt.Rows.Count > 0)
            {
                model.UID = dt.Rows[0]["UID"].ToString();
                model.Category = dt.Rows[0]["Category"].ToString();
                model.SubCategory = dt.Rows[0]["SubCategory"].ToString();
                model.ImageCategory = dt.Rows[0]["Category"].ToString();
                model.ImageSubCategory = dt.Rows[0]["SubCategory"].ToString();
                model.FileName = dt.Rows[0]["FileName"].ToString();
                model.Description = dt.Rows[0]["Description"].ToString();
                model.Price = dt.Rows[0]["Price"].ToString();
                model.Genre = dt.Rows[0]["Genre"].ToString();
                model.PriceRate = dt.Rows[0]["Price"].ToString();
                model.MediaFileUrl = dt.Rows[0]["MediaFileUrl"].ToString();
                model.CoverImageUrl = dt.Rows[0]["CoverImageUrl"].ToString();
                model.AlbumUID = dt.Rows[0]["AlbumUID"].ToString();
                model.AlbumName = dt.Rows[0]["AlbumName"].ToString();
                model.AlbumDescription = dt.Rows[0]["AlbumDescription"].ToString();
                model.AlbumCoverImage = dt.Rows[0]["AlbumCoverImage"].ToString();
                Session["MediaDocumentUID"] = dt.Rows[0]["UID"].ToString();
                Session["FileName"] = dt.Rows[0]["FileName"].ToString();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CustomerHistory()
        public ActionResult CustomerHistory()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            return View();
        }
        public JsonResult CustomerHistoryAngularJs()
        {
            List<CusomerHistoryModel> model = new List<CusomerHistoryModel>();
            _CodeClass.GetStartAndEndDate("", "", out _dateFrom, out _dateTo);
            DataTable dt = new MasterDataEntity().Master_Get_TalentSales(_dateFrom, _dateTo);
            model = dt.AsEnumerable().Select(x =>
            {
                return new CusomerHistoryModel()
                {
                    UID = x["PurchaseUID"].ToString(),
                    UserName = x["UserName"].ToString(),
                    LoginName = x["LoginName"].ToString(),
                    ProductName = x["ProductName"].ToString(),
                    ProductType = x["ProductType"].ToString(),
                    Date = x["Date"].ToString(),
                    Amount = _CodeClass.GetCurrency(x["Amount"]),
                    ShippingDetail = x["ShippingDetail"].ToString(),
                    ShipCount = Convert.ToInt32(x["ShipCount"]),
                };
            }).ToList();
            return Json(model.OrderByDescending(x => x.Date), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Earning()
        public ActionResult Earning()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            return View();
        }
        public JsonResult EarningAngularJs()
        {
            List<EarningModel> model = new List<EarningModel>();
            int i = 1;
            DataTable dt = new RoleDataEntity().Role_Get_MediaPromoterEarningDetail();
            model = dt.AsEnumerable().Select(x =>
            {
                return new EarningModel()
                {
                    Sno = i++,
                    Today = (x["Today"].ToString()),
                    Week = (x["Week"].ToString()),
                    Month = (x["Month"].ToString()),
                    Year = (x["Year"].ToString()),
                    Total = (x["Total"].ToString()),
                };

            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Event()
        public ActionResult Event()
        {
            return View();
        }
        public JsonResult EventAngularJs(string DateFrom = "", string DateTo = "", string Country = "")
        {
            List<UserEventModel> model = new List<UserEventModel>();
            _CodeClass.GetStartAndEndDate(DateFrom, DateTo, out _dateFrom, out _dateTo);
            int i = 1;
            DataTable dt = new RoleDataEntity().Role_Get_CreativeTalentNearByEventsSearch(_dateFrom, _dateTo, Country);
            model = dt.AsEnumerable().Select(x =>
            {
                return new UserEventModel()
                {
                    Sno = i++,
                    UID = x["UID"].ToString(),
                    EventName = x["EventName"].ToString(),
                    Address = x["Country"].ToString(),
                    Fee = x["Fee"].ToString(),
                    Performance = x["Performance"].ToString(),
                    Host = x["Host"].ToString(),
                    Date = x["Date"].ToString(),
                };

            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Message()

        public JsonResult InboxAngularJs()
        {
            List<MessageModel> model = new List<MessageModel>();
            int i = 1;
            DataTable dt = new MessageDataEntity().Message_Select_Inbox(User.Identity.Name);
            model = dt.AsEnumerable().Select(x =>
            {
                return new MessageModel()
                {
                    Sno = i++,
                    UID = x["UID"].ToString(),
                    MailFrom = x["Mail From"].ToString(),
                    LoginName = x["LoginName"].ToString(),
                    Subject = x["Subject"].ToString(),
                    Message = x["Message"].ToString(),
                    SentDate = x["Sent Date"].ToString(),
                };
            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OutboxAngularJs()
        {
            List<MessageModel> model = new List<MessageModel>();
            int i = 1;
            DataTable dt = new MessageDataEntity().Message_Select_OutBox(User.Identity.Name);
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
                if (new MessageDataEntity().Message_Delete_User(UID) > 0)
                {
                    TempData["Message"] = "Message has been deleted successfully";
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Message(ComposeModel model, FormCollection form)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            object message = string.Empty;
            if (new MessageDataEntity().Message_Insert_TalentUser(User.Identity.Name, model.LoginName, model.Subject, model.Message, out message) > 0)
            {
                TempData["Message"] = message;
                ModelState.Clear();
            }
            else
            {
                TempData["Error"] = message;
            }
            return View();
        }

        public JsonResult MessageAngularJs(string LoginName = "", string Subject = "", string Message = "")
        {
            object message = string.Empty;
            string flag = "";
            if (new MessageDataEntity().Message_Insert_TalentUser(User.Identity.Name, LoginName, Subject, Message, out message) > 0)
            {
                TempData["Message"] = message;
                flag = "1";
            }
            else
            {
                TempData["Error"] = message;
                flag = "0";
            }
            return Json(new { flag = flag, msg = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult MessageReadInboxAngularJs(string UID = "")
        {
            MessageModel model = new MessageModel();
            dt = new MessageDataEntity().Message_GetMessageDetail(User.Identity.Name, UID);
            if (dt.Rows.Count > 0)
            {
                model.LoginName = dt.Rows[0]["LoginName"].ToString();
                model.SentDate = dt.Rows[0]["Sent Date"].ToString();
                model.Subject = dt.Rows[0]["Subject"].ToString();
                model.Message = dt.Rows[0]["Message"].ToString();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChatRoomAngularJs()
        {
            List<MessageModel> model = new List<MessageModel>();
            int i = 1;
            DataTable dt = new MessageDataEntity().Message_Select_Chatbox(User.Identity.Name);
            model = dt.AsEnumerable().Select(x =>
            {
                return new MessageModel()
                {
                    Sno = i++,
                    UID = x["UID"].ToString(),
                    MailFrom = x["Mail From"].ToString(),
                    MailTo = x["Mail To"].ToString(),
                    Subject = x["Subject"].ToString(),
                    Message = x["Message"].ToString(),
                    SentDate = x["Sent Date"].ToString(),
                    SentTime = x["Sent Time"].ToString(),
                    UserImage = x["IMAGE"].ToString().Replace("~/", "/"),
                };
            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Collaboration()
        public ActionResult Collaboration()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CollaborationList(int draw, Dictionary<string, string> search, int start, int length)
        {
            var result = aspNetUserConnectionService.GetForDT(Convert.ToInt64(User.Identity.GetUserId()), search["value"], start, length);
            var ds = result.Item1;
            return Json(new
            {
                draw = draw,
                data = ds,
                recordsTotal = result.Item2,
                recordsFiltered = result.Item2,
            });
        }
        #endregion

        #region PurchaseHistory()
        public ActionResult PurchaseHistory()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            return View();
        }
        public JsonResult PurchaseHistoryAngularJs()
        {
            List<PurchaseHistory> model = new List<PurchaseHistory>();
            int i = 1;
            DataTable dt = new ShoppingDataEntity().Shopping_Select_PurchaseHistory(User.Identity.Name);
            model = dt.AsEnumerable().Select(x =>
            {
                return new PurchaseHistory()
                {
                    Sno = i++,
                    Email = x["Email ID"].ToString(),
                    Name = x["Name"].ToString(),
                    Date = x["Date"].ToString(),
                    OrderNumber = x["Order Number"].ToString(),
                    PaidAmount = _CodeClass.GetCurrency(x["Paid Amount"].ToString()),
                };

            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region TicketHistory()
        public ActionResult TicketHistory()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            return View();
        }
        public JsonResult TicketHistoryAngularJs()
        {
            List<TicketHistory> model = new List<TicketHistory>();
            int i = 1;
            DataTable dt = new ShoppingDataEntity().Shopping_Select_EventTicketPurchaseHistory(User.Identity.Name);
            model = dt.AsEnumerable().Select(x =>
            {
                return new TicketHistory()
                {
                    Sno = i++,
                    Email = x["Email ID"].ToString(),
                    Name = x["Name"].ToString(),
                    Date = x["Date"].ToString(),
                    EventName = x["EventName"].ToString(),
                    PaidAmount = _CodeClass.GetCurrency(x["PaidAmount"].ToString()),

                };
            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ResourceCenter()
        public ActionResult ResourceCenter()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult Brochure()
        {
            PageModel model = new PageModel();
            model.PageHtml = new CommonDataEntity().Page_Select_Detail("Guest/Resources.aspx");
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult TermsCondition()
        {
            PageModel model = new PageModel();
            model.PageHtml = new CommonDataEntity().Page_Select_Detail("Guest/TermsCondition.aspx");
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult StartKit()
        {
            PageModel model = new PageModel();
            model.PageHtml = new CommonDataEntity().Page_Select_Detail("Guest/Starter.aspx");
            return PartialView(model);
        }
        #endregion

        #region ContactUs()
        public ActionResult ContactUs()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            return View();
        }
        [HttpPost]
        public ActionResult ContactUs(ContactModel model)
        {
            object message = string.Empty;
            string _emailaddress = System.Configuration.ConfigurationManager.AppSettings["mailaddress_admin"].ToString();
            string _body = "Dear " + _CodeClass.GetCompanyName() + ",<br/><br/>One person has invite you. His/her contact information is following..<br/><br/>";
            _body = _body + "Name: " + model.LoginName;
            _body = _body + "Email: " + model.Email + "<br/>";
            _body = _body + "Message: " + model.Message + "<br/>";
            _body = _body + "Sender ID: " + User.Identity.Name;

            if (_CodeClass.SendMail(_emailaddress, model.Subject, _body))
            {
                TempData["Message"] = "Your message has been sent to our customer care team.";
                ModelState.Clear();
            }
            else
            {
                TempData["Message"] = "Fatal error found. Please try again!";
            }
            return View();
        }
        #endregion

        #region MediaPromoterCustomer()
        public ActionResult MediaPromoterCustomer()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            return View();
        }
        public JsonResult MediaPromoterCustomerAngularJs()
        {
            List<MediaPromoterCustomerModel> model = new List<MediaPromoterCustomerModel>();
            int i = 1;
            DataTable dt = new ShoppingDataEntity().Shopping_Select_EventTicketRefferalReport();
            model = dt.AsEnumerable().Select(x =>
            {
                return new MediaPromoterCustomerModel()
                {
                    Sno = i++,
                    EmailID = x["EmailID"].ToString(),
                    Name = x["Name"].ToString(),
                    Date = x["Date"].ToString(),
                    EventName = x["EventName"].ToString(),
                    PaidAmount = _CodeClass.GetCurrency(x["PaidAmount"].ToString()),
                    SeatQTY = x["SeatQTY"].ToString(),
                };

            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ToolBox()
        public ActionResult OnlineProduction()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            OnlineProductionModel model = new OnlineProductionModel();
            DataTable dt = new RoleDataEntity().Role_Select_UserProfileDetail();
            if (dt.Rows.Count > 0)
            {
                model.Name = (dt.Rows[0]["Name"].ToString());
                model.Address = (dt.Rows[0]["Address"].ToString());
                model.ProductionSubName = (dt.Rows[0]["ProductionSub Name"].ToString());
            }
            DataTable dtM = new CommonDataEntity().Common_Select_UserHome(User.Identity.Name);
            if (dtM.Rows.Count > 0)
            {
                model.ImageUrl = (dtM.Rows[0]["IMAGE"].ToString()).Replace("~/", "/");
            }
            return View(model);
        }
        public ActionResult MarketingTools()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            return View();
        }
        public ActionResult MarketingLink()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            return View();
        }
        public ActionResult LearningCenter()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            return View();
        }
        public ActionResult LegalTools()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            return View();
        }
        public ActionResult ProductTools()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            return View();
        }
        public ActionResult AddShippingInfo(string UID = "")
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            AddShippingInfoModel model = new AddShippingInfoModel();
            if (UID != "")
            {
                _CodeClass.GetStartAndEndDate("", "", out _dateFrom, out _dateTo);
                DataTable dt = new MasterDataEntity().Master_Get_TalentSales(_dateFrom, _dateTo);
                dt = dt.AsEnumerable().Where(row => row.Field<Guid>("PurchaseUID") == Guid.Parse(UID)).CopyToDataTable(); if (dt.Rows.Count > 0)
                {
                    model.hdnUID = dt.Rows[0]["PurchaseUID"].ToString();
                    model.CustomerName = dt.Rows[0]["UserName"].ToString();
                    model.CustomerLoginName = dt.Rows[0]["LoginName"].ToString();
                    model.ProductName = dt.Rows[0]["ProductName"].ToString();
                    model.PurchaseDate = dt.Rows[0]["Date"].ToString();
                }
            }
            else
            {
                return RedirectToAction("ProductTools", "User");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddShippingInfo(AddShippingInfoModel model, FormCollection formCollection)
        {
            object message = string.Empty;
            DataTable dt = new ShoppingDataEntity().Shopping_Update_ShippingDetail(model.hdnUID, model.CourierName, model.TrackingNumber, Convert.ToDateTime(model.ShippingDate), Convert.ToDateTime(model.ExpectedDeliveryDate), model.CustomerLoginName, model.ProductName, out message);
            if (dt.Rows.Count > 0)
            {
                _CodeClass.SendMail(dt.Rows[0]["MAIL"].ToString(), dt.Rows[0]["MAIL SUBJECT"].ToString(), dt.Rows[0]["MAIL BODY"].ToString());
                TempData["Message"] = message;
            }
            else
            {
                TempData["Message"] = message;
            }
            return RedirectToAction("ProductTools");
        }
        [ChildActionOnly]
        public PartialViewResult Contract()
        {
            UserPageModel model = new UserPageModel();
            model.PageHtml = new CommonDataEntity().Page_Select_Detail("User/contract");
            return PartialView(model);
        }
        #endregion

        #region EventTicketReport()
        public ActionResult EventTicketReport()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            return View();
        }
        #endregion

        #region UserRefferalReport()
        public ActionResult UserRefferalReport()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            return View();
        }
        public JsonResult UserRefferalReportAngularJs()
        {
            List<UserRefferalReportModel> model = new List<UserRefferalReportModel>();
            int i = 1;
            DataSet ds = new RoleDataEntity().Role_Select_UserRefferalReport();
            model = ds.Tables[0].AsEnumerable().Select(x =>
            {
                return new UserRefferalReportModel()
                {
                    Sno = i++,
                    EmailID = x["EmailID"].ToString(),
                    Name = x["Name"].ToString(),
                    Date = x["Date"].ToString(),
                    Mobile = x["Mobile"].ToString(),
                    Country = x["Country"].ToString(),
                };
            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UserRefferalReportAllAngularJs()
        {
            List<UserRefferalReportModel> model = new List<UserRefferalReportModel>();
            int i = 1;
            DataSet ds = new RoleDataEntity().Role_Select_UserRefferalReport();
            model = ds.Tables[1].AsEnumerable().Select(x =>
            {
                return new UserRefferalReportModel()
                {
                    Sno = i++,
                    EmailID = x["EmailID"].ToString(),
                    Name = x["Name"].ToString(),
                    Date = x["Date"].ToString(),
                    Mobile = x["Mobile"].ToString(),
                    Country = x["Country"].ToString(),
                };
            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Info()
        public ActionResult Info()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult BrochureInfo()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public PartialViewResult Instructions()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public PartialViewResult Definitions()
        {
            return PartialView();
        }
        #endregion

        #region Populate()
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
            DataTable dtTalent = new CommonDataEntity().Select_TalentCategoryDDL();
            if (dtTalent.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTalent.Rows)
                {
                    ddlTalent.Add(new SelectListItem { Text = dr["Text"].ToString(), Value = dr["Value"].ToString() });
                }
            }
            return ddlTalent;
        }
        #endregion

        #region Upgrade()
        public ActionResult Upgrade()
        {
            Session["Role"] = Convert.ToString(new RoleDataEntity().Role_Get_TalentRole());
            UserUpgradeModel model = new UserUpgradeModel();
            model.TalentCategory = PopulateTalentCategory();
            model.ProductionCategory = PopulateProductionCategory();
            model.TalentSubCategory = PopulateTalentSubCategory("");
            model.ProductionSubCategory = PopulateProductionSubCategory("");
            model.Country = PopulateCountry();
            model.FCountry = PopulateCountry();
            DataTable dt = new RoleDataEntity().Role_Select_UserDetail(User.Identity.Name);
            if (dt.Rows.Count > 0)
            {
                model.UserName = dt.Rows[0]["Name"].ToString();
                model.Email = dt.Rows[0]["Email"].ToString();
                model.City = dt.Rows[0]["City"].ToString();
                model.Address = dt.Rows[0]["Address"].ToString();
                model.ZipCode = dt.Rows[0]["Zip Code"].ToString();
                model.Mobile = dt.Rows[0]["MOBILE"].ToString();
                model.SponsorID = dt.Rows[0]["SpID"].ToString();
                model.College = dt.Rows[0]["College"].ToString();
                model.BioDescription = dt.Rows[0]["Biography"].ToString();
                model.Major_Other = dt.Rows[0]["Major"].ToString();
                ViewData["Country"] = dt.Rows[0]["Country"].ToString();
                ViewData["State"] = dt.Rows[0]["State"].ToString();
                ViewData["FCountry"] = dt.Rows[0]["Country"].ToString();
                ViewData["FState"] = dt.Rows[0]["State"].ToString();
                ViewData["Major"] = dt.Rows[0]["Major"].ToString();
            }
            DataTable dt_JoiningAmount = new MasterDataEntity().Master_Select_JoiningAmount();
            if (dt_JoiningAmount.Rows.Count > 0)
            {
                model.AT_Fee = _CodeClass.GetCurrency(dt_JoiningAmount.Rows[0]["AT"].ToString());
                model.PM_Fee = _CodeClass.GetCurrency(dt_JoiningAmount.Rows[0]["PM"].ToString());
                model.MP_Fee = _CodeClass.GetCurrency(dt_JoiningAmount.Rows[0]["MP"].ToString());
            }
            model.State = PopulateState(ViewData["Country"].ToString());
            model.FState = PopulateState(ViewData["FCountry"].ToString());
            model.Major = PopulateMajor();
            model.Role1 = Convert.ToString(new RoleDataEntity().Role_Get_TalentRole());
            model.Role2 = Convert.ToString(new RoleDataEntity().Role_Get_TalentRole1());
            model.Role3 = Convert.ToString(new RoleDataEntity().Role_Get_TalentRole2());
            return View(model);
        }
        [HttpPost]
        public ActionResult Upgrade(UserUpgradeModel model, FormCollection formCollection)
        {
            var chckedValues = formCollection.GetValues("chkBoxT");
            if (chckedValues != null)
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
                string Category = "";
                string SubCategory = "";
                string major = Request.Form["Major"];
                string TalentCategory = Request.Form["TalentCategory"];
                string TalentSubCategory = Request.Form["TalentSubCategory"];
                string ProductionCategory = Request.Form["ProductionCategory"];
                string ProductionSubCategory = Request.Form["ProductionSubCategory"];
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
                if (model.SponsorID == null)
                {
                    model.SponsorID = "";
                }
                if (TalentCategory != "")
                {
                    Category = TalentCategory;
                }
                else
                {
                    Category = ProductionCategory;
                }
                if (TalentSubCategory != "")
                {
                    SubCategory = TalentSubCategory;
                }
                else
                {
                    SubCategory = ProductionSubCategory;
                }
                if (model.hdnRoleName == "All Talent")
                {
                    model.PayAmount = new CommonDataEntity().GetJoiningAmount("ALL TALENT");
                }
                else if (model.hdnRoleName == "Production")
                {
                    model.PayAmount = new CommonDataEntity().GetJoiningAmount("PRODUCTION");
                }
                else if (model.hdnRoleName == "Media Promoter")
                {
                    model.PayAmount = new CommonDataEntity().GetJoiningAmount("MEDIA PROMOTER");
                }
                object message = string.Empty;
                RoleDataEntity rd = new RoleDataEntity();
                if (model.hdnRoleName != "Media Promoter")
                {
                    DataTable dt = rd.Role_Insert_AllTalentUpgradeTemp(model.hdnRoleName, Category, SubCategory, "", model.BioDescription, model.UserName, ViewBag.Country, ViewBag.State, model.City, model.Address, model.ZipCode, model.Mobile, model.Email, "", model.SponsorID, model.College, major, out message);
                    if (dt.Rows.Count > 0)
                    {
                        string UID = dt.Rows[0]["UID"].ToString();
                        return RedirectToAction("PaymentProcessPZ", "Payment", new { U_uid = UID, fee = model.PayAmount });
                    }
                    else
                    {
                        TempData["Error"] = message;
                    }
                }
                else if (model.hdnRoleName == "Media Promoter")
                {
                    DataTable dt = rd.Role_Insert_MediaPromoterUpgradeTemp(model.hdnRoleName, model.UserName, ViewBag.Country, ViewBag.State, model.City, model.Address, model.ZipCode, model.Mobile, model.Email, "", model.SponsorID, model.College, major, out message);
                    if (dt.Rows.Count > 0)
                    {
                        string UID = dt.Rows[0]["UID"].ToString();
                        return RedirectToAction("PaymentProcessPZ", "Payment", new { U_fuid = UID, fee = model.PayAmount });
                    }
                    else
                    {
                        TempData["Error"] = message;
                    }
                }
            }
            else
            {
                TempData["Error"] = "Terms and conditions is required.";
            }
            return RedirectToAction("Upgrade");
        }
        #endregion

        #region Invoice()
        public ActionResult Invoice(string id = "")
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            InvoiceModel model = new InvoiceModel();
            DataTable dt = new ShoppingDataEntity().Shopping_Select_ProductInvoice(User.Identity.Name, id);
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
                model.Amount = _CodeClass.GetCurrency(dt.Rows[0]["Amount"].ToString());
                model.Tax = _CodeClass.GetCurrency(dt.Rows[0]["Tax"].ToString());
                model.NetAmount = _CodeClass.GetCurrency(dt.Rows[0]["Net Amount"].ToString());
                model.Product = dt.Rows[0]["Product"].ToString();
            }
            else
            {
                TempData["Error"] = "Invoice does not exists!";
                return RedirectToAction("PurchaseHistory");
            }
            return View(model);
        }
        #endregion

        #region Geneology()
        public ActionResult GeneologyLevel(string DateFrom = "", string DateTo = "", string LoginName = "")
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            _CodeClass.GetStartAndEndDate(DateFrom, DateTo, out _dateFrom, out _dateTo);
            List<GeneologyLevelModel> model = new List<GeneologyLevelModel>();
            if (LoginName == "")
            {
                LoginName = User.Identity.Name;
            }
            dt = new GenealogyDataEntity().Gen_Report_Level(_dateFrom, _dateTo, LoginName);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    model.Add(new GeneologyLevelModel
                    {
                        LoginName = dr["Login Name"].ToString(),
                        UserName = dr["User Name"].ToString(),
                        RepNo = dr["Rep No."].ToString(),
                        SponsorLoginName = dr["Sponsor Login Name"].ToString(),
                        Shopping = _CodeClass.GetCurrency(dr["Shopping"].ToString()),
                        EnrollmentDate = dr["Enrollment Date"].ToString(),
                        Level = Convert.ToInt32(dr["Level"].ToString()),
                    });
                }
            }
            return View(model.OrderBy(x => x.Level).ToList());
        }
        public ActionResult GeneologyTree(string LoginName = "")
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            TreeViewNodeVM model = new TreeViewNodeVM();
            GenealogyDataEntity gd = new GenealogyDataEntity();
            DataTable dt = gd.Gen_GetTreeReport(User.Identity.Name);
            foreach (DataRow dr in dt.Rows)
            {
                model.ChildNode.Add(new TreeViewNodeVM
                {
                    LoginName = dr["LoginName"].ToString(),
                    Name = dr["Name"].ToString(),
                    Count = Convert.ToInt32(dr["Count"].ToString()),
                });
                ViewData["LoginName"] = dr["LoginName"].ToString();
                this.GetSubNode();
            }
            model.SubNode = SubNode;
            model.SubSubNode = SubSubNode;
            model.LoginName = User.Identity.Name;
            if (LoginName == "")
            {
                LoginName = User.Identity.Name;
            }
            DataTable dt_Detail = gd.Gen_GetTree_UserDetail(LoginName);
            if (dt_Detail.Rows.Count > 0)
            {
                model.User_LoginName = dt_Detail.Rows[0]["LoginName"].ToString();
                model.SpID = dt_Detail.Rows[0]["SpID"].ToString();
                model.SponsorName = dt_Detail.Rows[0]["SponsorName"].ToString();
                model.Downline = dt_Detail.Rows[0]["Downline"].ToString();
                model.SelfInvestment = dt_Detail.Rows[0]["SelfInvestment"].ToString();
                model.GroupInvestment = dt_Detail.Rows[0]["GroupInvestment"].ToString();
                model.LevelCount = Convert.ToInt32(dt_Detail.Rows[0]["Level"]);
            }
            if (model.LevelCount >= 0 && model.LevelCount <= 24)
            {
                model.Level = "SCOUT";
            }
            else if (model.LevelCount >= 25 && model.LevelCount <= 124)
            {
                model.Level = "FAN";
            }
            else if (model.LevelCount >= 125 && model.LevelCount <= 624)
            {
                model.Level = "FANATIC";
            }
            else if (model.LevelCount >= 625 && model.LevelCount <= 3124)
            {
                model.Level = "MOGUL";
            }
            else if (model.LevelCount <= 3125)
            {
                model.Level = "TITAN";
            }
            return View(model);
        }
        public PartialViewResult _ChildNode()
        {
            return PartialView();
        }

        #region GetSubNode()
        public List<TreeViewNodeVM> GetSubNode()
        {
            GenealogyDataEntity gd = new GenealogyDataEntity();
            if (ViewData["LoginName"] != null)
            {
                DataTable dt = gd.Gen_GetTreeReport(ViewData["LoginName"].ToString());
                foreach (DataRow dr in dt.Rows)
                {
                    SubNode.Add(new TreeViewNodeVM
                    {
                        LoginName = dr["LoginName"].ToString(),
                        User_LoginName = ViewData["LoginName"].ToString(),
                        Name = dr["Name"].ToString(),
                    });
                    ViewData["Login_Name"] = dr["LoginName"].ToString();
                    this.GetSubSubNode();
                }
                ViewData["UID"] = null;
                model.SubNode = SubNode;
            }
            return SubNode;
        }
        #endregion

        #region GetSubSubNode()
        public List<TreeViewNodeVM> GetSubSubNode()
        {
            GenealogyDataEntity gd = new GenealogyDataEntity();
            if (ViewData["Login_Name"] != null)
            {
                DataTable dt = gd.Gen_GetTreeReport(ViewData["Login_Name"].ToString());
                foreach (DataRow dr in dt.Rows)
                {
                    SubSubNode.Add(new TreeViewNodeVM
                    {
                        LoginName = dr["LoginName"].ToString(),
                        User_LoginName = ViewData["Login_Name"].ToString(),
                        Name = dr["Name"].ToString(),
                    });
                }
                ViewData["UID"] = null;
                model.SubSubNode = SubSubNode;
            }
            return SubSubNode;
        }
        #endregion
        #endregion

        #region IncomeReferral
        public ActionResult IncomeReferral()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            return View();
        }
        public JsonResult IncomeReferralAngularJs(string DateFrom = "", string DateTo = "")
        {
            List<UserIncomeModel> model = new List<UserIncomeModel>();
            _CodeClass.GetStartAndEndDate(DateFrom, DateTo, out _dateFrom, out _dateTo);
            int i = 1;
            DataTable dt = new IncomeDataEntity().Income_Report_Referral(_dateFrom, _dateTo);
            model = dt.AsEnumerable().Select(x =>
            {
                return new UserIncomeModel()
                {
                    Sno = i++,
                    Date = x["Date"].ToString(),
                    Amount = _CodeClass.GetCurrency(x["Amount"].ToString()),
                    Deduction = _CodeClass.GetCurrency(x["Deduction"].ToString()),
                    NetAmount = _CodeClass.GetCurrency(x["Net Amount"].ToString()),
                };
            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region AreaPromoter
        public ActionResult AreaPromoter()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["Role"] = Session["Role"].ToString();
            }
            return View();
        }
        #endregion

        #region FanBaseRequest
        [HttpGet]
        public JsonResult RejectFanBaseRequest(string UID)
        {
            object message = string.Empty;
            string flag = "";
            if (UID != "")
            {
                if (new RoleDataEntity().Role_Reject_TalentFanBaseRequest(UID, out message) > 0)
                {
                    TempData["Message"] = message;
                    flag = "1";
                }
                else
                {
                    flag = "0";
                }
            }
            return Json(new { flag = flag, msg = message, newurl = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AcceptFanBaseRequest(string UID)
        {
            object message = string.Empty;
            string flag = "";
            if (UID != "")
            {
                DataTable dt = new RoleDataEntity().Role_Accept_TalentFanBaseRequest(UID, out message);
                if (dt.Rows.Count > 0)
                {
                    _CodeClass.SendMail(dt.Rows[0]["MAIL"].ToString(), dt.Rows[0]["MAIL SUBJECT"].ToString(), dt.Rows[0]["MAIL BODY"].ToString());
                    TempData["Message"] = message;
                    flag = "1";
                }
                else
                {
                    flag = "0";
                }
            }
            return Json(new { flag = flag, msg = message, newurl = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult GetCategoryTypeByMembershipType(int MembershipType)
        {
            return Json(new { Result = "OK", Data = categoryTypeService.CategoryTypeCboByMembershipTypeOrderBy(MembershipType) });
        }
        #endregion
    }

    public class BioController : Controller
    {
        public ActionResult Index(long ID)
        {
            return RedirectToAction("ProfileView", "User", new { ID = ID });
        }
    }
}
