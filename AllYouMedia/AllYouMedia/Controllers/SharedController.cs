using AllYouMedia.DataAccess.ServiceLayer;
using AllYouMedia.Models;
using BusinessEntity.ConcreateEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace AllYouMedia.Controllers
{
    [AllowAnonymous]
    public class SharedController : Controller
    {
        List<SubMenuModel> SubMenu = new List<SubMenuModel>();
        public long UserID
        {
            get
            {
                long str = -1;
                if (System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    str = Convert.ToInt64(System.Web.HttpContext.Current.Session["UserID"]);
                }
                else
                {
                    str = new RoleDataEntity().GetLoggedInUserID(User.Identity.Name);
                    Session["UserID"] = str;
                }
                return str;
            }
        }

        // GET: Shared      
        [ChildActionOnly]
        [Authorize]
        public PartialViewResult UserLeft()
        {
            return PartialView();
        }

        [ChildActionOnly]
        [Authorize]
        public PartialViewResult UserHeader()
        {
            UserHeaderModel model = new UserHeaderModel();
            List<MessageModel> Message = new List<MessageModel>();
            DataTable dt = new MessageDataEntity().Message_Select_InboxForHeader(User.Identity.Name);
            if (dt.Rows.Count > 0)
            {
                ViewBag.MessageCount = dt.Rows[0]["MessageCount"].ToString();
            }
            else
            {
                ViewBag.MessageCount = "0";
            }
            Message = dt.AsEnumerable().Select(x =>
            {
                return new MessageModel()
                {
                    MailFrom = x["Mail From"].ToString(),
                    Subject = x["Subject"].ToString(),
                    Message = x["Message"].ToString(),
                    SentDate = x["Sent Date"].ToString(),
                };

            }).ToList();
            model.Message = Message;

            List<UserActivityModel> Activity = new List<UserActivityModel>();
            DataTable dt_Activity = new RoleDataEntity().Role_GetUserActivityForHeader();
            Activity = dt_Activity.AsEnumerable().Select(x =>
            {
                return new UserActivityModel()
                {
                    Reference = x["Reference"].ToString(),
                    Type = x["Type"].ToString(),
                    ActionDate = x["Action Date"].ToString(),
                };

            }).ToList();
            model.Activity = Activity;

            DataTable dt_image = new RoleDataEntity().Role_Get_UserProfileImage();
            if (dt_image.Rows.Count > 0)
            {
                model.Image = dt_image.Rows[0]["Profile Image"].ToString().Replace("~/", "/");
            }
            else
            {
                model.Image = "/Images/img_128x128.png";
            }

            List<UserFanBaseModel> FanBase = new List<UserFanBaseModel>();
            DataTable dt_FanBase = new RoleDataEntity().Role_GetUserFanBaseRequest();
            FanBase = dt_FanBase.AsEnumerable().Select(x =>
        {
            return new UserFanBaseModel()
            {
                UID = x["UID"].ToString(),
                TalentBUID = x["TalentBUID"].ToString(),
                FanBUID = x["FanBUID"].ToString(),
                ImageUrl = x["FanImage"].ToString().Replace("~/", "/"),
                FanLoginName = x["FanLoginName"].ToString(),
            };

        }).ToList();
            for (var j = 0; j < FanBase.Count; j++)
            {
                string ImageUrl = FanBase[j].ImageUrl.ToString();
                if (ImageUrl == "")
                {
                    FanBase[j].ImageUrl = "/Images/img_128x128.png";
                }
            }
            model.FanBase = FanBase;
            if (dt_FanBase.Rows.Count > 0)
            {
                ViewBag.fanBase = true;
            }
            else
            {
                ViewBag.fanBase = false;
            }

            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult GuestMenu()
        {
            IndexModel model = new IndexModel();
            List<MenuModel> Menu = new List<MenuModel>();
            DataTable dt_Menu = new DataTable();
            dt_Menu = new MasterDataEntity().Master_Select_CategoryDefaultCondition();
            foreach (DataRow dr in dt_Menu.Rows)
            {
                Menu.Add(new MenuModel
                {
                    UID = dr["UID"].ToString(),
                    Text = dr["Text"].ToString(),
                    Name = dr["Name"].ToString(),
                });
                ViewData["UID"] = dr["UID"].ToString();
                this.GetSubMenu();
            }
            Menu = Menu.OrderBy(x => x.Text).ToList();
            model.Menu = Menu;
            model.SubMenu = SubMenu;
            return PartialView(model);
        }

        public List<SubMenuModel> GetSubMenu()
        {
            if (ViewData["UID"] != null)
            {
                DataTable dt = new DataTable();
                dt = new MasterDataEntity().Master_Select_MainCategoryListWithMenu(ViewData["UID"].ToString());
                foreach (DataRow dr in dt.Rows)
                {
                    SubMenu.Add(new SubMenuModel
                    {
                        UID = dr["UID"].ToString(),
                        Name = dr["Name"].ToString(),
                        MenuUID = dr["MenuUID"].ToString(),
                        AlbumUID = dr["AlbumUID"].ToString(),
                        AlbumName = dr["AlbumName"].ToString(),
                    });
                }
                ViewData["UID"] = null;
                SubMenu = SubMenu.OrderBy(x => x.Name).ToList();
            }
            return SubMenu;
        }

        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            IndexModel model = new IndexModel();
            List<CartModel> Cart = new List<CartModel>();
            DataTable dt_Cart = new ShoppingDataEntity().Shopping_Select_Cart(_CodeClass.GetProductCart(), User.Identity.Name);
            if (dt_Cart.Rows.Count > 0)
            {
                _CodeClass.SetProductCart(dt_Cart);
                foreach (DataRow dr in dt_Cart.Rows)
                {
                    Cart.Add(new CartModel
                    {
                        itemUID = dr["UID"].ToString(),
                        Name = dr["Name"].ToString(),
                        QTY = dr["QTY"].ToString(),
                        IMAGE = dr["Product URL"].ToString().Replace("~/", "/"),
                        Rate = Convert.ToDecimal(dr["Rate"].ToString()),
                    });
                    ViewBag.GrossAmount = _CodeClass.GetCurrency(Convert.ToDecimal(dt_Cart.Rows[0]["Expedited Shipping"]) + Convert.ToDecimal(dt_Cart.Compute("SUM([Net Amount])", "")));
                }
                for (var i = 0; i < Cart.Count; i++)
                {
                    string extFile = System.IO.Path.GetExtension(Cart[i].IMAGE).ToLower();
                    if (extFile == ".mp4" || extFile == ".avi" || extFile == ".wmv")
                    {
                        Cart[i].IMAGE = "/Images/document/mp4.jpg";
                    }
                    else if (extFile == ".pdf")
                    {
                        Cart[i].IMAGE = "/Images/document/.pdf.png";
                    }
                    else if (extFile == ".ppt" || extFile == ".pptx")
                    {
                        Cart[i].IMAGE = "/Images/document/.ppt.png";
                    }
                    else if (extFile == ".doc" || extFile == ".docx")
                    {
                        Cart[i].IMAGE = "/Images/document/.doc.PNG";
                    }
                    else if (extFile == ".xls" || extFile == ".xlsx")
                    {
                        Cart[i].IMAGE = "/Images/document/.xls.PNG";
                    }
                    else if (extFile == ".mp3")
                    {
                        Cart[i].IMAGE = "/Images/document/mp3.jpg";
                    }
                }
            }
            else
            {
                ViewBag.GrossAmount = "$ 0.00";
            }
            model.Cart = Cart;
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult Product()
        {
            IndexModel model = new IndexModel();
            List<MenuModel> Menu = new List<MenuModel>();
            DataTable dt_Menu = new DataTable();
            dt_Menu = new MasterDataEntity().Master_Select_CategoryDefaultCondition();
            foreach (DataRow dr in dt_Menu.Rows)
            {
                Menu.Add(new MenuModel
                {
                    UID = dr["UID"].ToString(),
                    Text = dr["Text"].ToString(),
                    Name = dr["Name"].ToString(),
                });
            }
            Menu = Menu.OrderBy(x => x.Text).ToList();
            model.Menu = Menu;
            return PartialView(model);
        }
    }

    [Authorize]
    public class SharedAuthController : Controller
    {
        private IAspNetUserService aspNetUserSerivce;
        private ISubCategoryService subCategoryService;
        private IItemService itemService;
        private IOrderService orderService;
        private IPayoutDistributionService payoutDistributionService;

        public SharedAuthController(AspNetUserService aspNetUserSerivce, SubCategoryService subCategoryService, ItemService itemService, OrderService orderService, PayoutDistributionService payoutDistributionService)
        {
            this.aspNetUserSerivce = aspNetUserSerivce;
            this.subCategoryService = subCategoryService;
            this.itemService = itemService;
            this.orderService = orderService;
            this.payoutDistributionService = payoutDistributionService;
        }
        #region Item()
        [Authorize(Roles = "Admin,Production")]
        public ActionResult Item()
        {
            return View("~/Views/Shared/Item.cshtml");
        }
        [Authorize(Roles = "Admin,Production")]
        public JsonResult ItemList(int draw, Dictionary<string, string> search, int start, int length)
        {
            var result = itemService.GetForDT(Convert.ToInt64(User.Identity.GetUserId()), search["value"], start, length);
            var ds = result.Item1.Select(x =>
            {
                return new
                {
                    DT_RowId = x.ID,
                    x.SubCategoryID,
                    SubCategory = x.SubCategory.Name,
                    x.Name,
                    x.PhotoIMG,
                    x.BasePrice,
                    x.SellPrice,
                    x.ContentURL,
                    x.IsStockApplicable,
                    x.StockQty,
                    x.MinPurchaseQty,
                    x.MaxPurchaseQty,
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
                options = new
                {
                    SubCategoryID = subCategoryService.SubCategoryCbo(true)
                },
                recordsTotal = result.Item2,
                recordsFiltered = result.Item2,
            });
        }
        [Authorize(Roles = "Admin,Production")]
        public JsonResult ItemCurd(string action, List<Dictionary<string, string>> data)
        {
            if (data == null || data.Count == 0) return Json(new { error = "Invalid operation" });
            AllYouMedia.DataAccess.EntityLayer.DBEntity.Item dbItem = null;
            if (action == "create")
            {
                dbItem = new DataAccess.EntityLayer.DBEntity.Item
                {
                    AspNetUserID = Convert.ToInt64(User.Identity.GetUserId()),
                    SubCategoryID = Convert.ToInt64(data[0]["SubCategoryID"]),
                    Name = data[0]["Name"],
                    BasePrice = Convert.ToDecimal(data[0]["BasePrice"]),
                    SellPrice = Convert.ToDecimal(data[0]["SellPrice"]),
                    IsStockApplicable = Convert.ToBoolean(data[0]["IsStockApplicable"]),
                    StockQty = Convert.ToInt32(data[0]["StockQty"]),
                    MinPurchaseQty = Convert.ToInt32(data[0]["MinPurchaseQty"]),
                    MaxPurchaseQty = Convert.ToInt32(data[0]["MaxPurchaseQty"]),
                    ShortDescription = data[0]["ShortDescription"],
                    LongDescription = data[0]["LongDescription"],
                    IsPromoted = Convert.ToBoolean(data[0]["IsPromoted"]),
                    IsFeatured = Convert.ToBoolean(data[0]["IsFeatured"]),
                };
                if (string.IsNullOrEmpty(dbItem.Name)) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "Name" }, { "status", "Item Name is required." } } } });
                if (dbItem.SubCategoryID == -1) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "SubCategoryID" }, { "status", "Sub Category is required." } } } });
                itemService.Insert(dbItem);
                return Json(new { data = new List<AllYouMedia.DataAccess.EntityLayer.DBEntity.Item> { dbItem } });
            }
            else if (action == "edit")
            {
                dbItem = itemService.GetById(Convert.ToInt64(data[0]["ID"]));
                dbItem.SubCategoryID = Convert.ToInt64(data[0]["SubCategoryID"]);
                dbItem.Name = data[0]["Name"];
                dbItem.BasePrice = Convert.ToDecimal(data[0]["BasePrice"]);
                dbItem.SellPrice = Convert.ToDecimal(data[0]["SellPrice"]);
                dbItem.IsStockApplicable = Convert.ToBoolean(data[0]["IsStockApplicable"]);
                dbItem.StockQty = Convert.ToInt32(data[0]["StockQty"]);
                dbItem.MinPurchaseQty = Convert.ToInt32(data[0]["MinPurchaseQty"]);
                dbItem.MaxPurchaseQty = Convert.ToInt32(data[0]["MaxPurchaseQty"]);
                dbItem.ShortDescription = data[0]["ShortDescription"];
                dbItem.LongDescription = data[0]["LongDescription"];
                dbItem.IsPromoted = Convert.ToBoolean(data[0]["IsPromoted"]);
                dbItem.IsFeatured = Convert.ToBoolean(data[0]["IsFeatured"]);

                if (string.IsNullOrEmpty(dbItem.Name)) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "Name" }, { "status", "Item Name is required." } } } });
                if (dbItem.SubCategoryID == -1) return Json(new { fieldErrors = new List<Dictionary<string, string>> { new Dictionary<string, string>() { { "name", "SubCategoryID" }, { "status", "Sub Category is required." } } } });
                itemService.Update(dbItem);
                return Json(new
                {
                    data = new List<object> { new {  DT_RowId = dbItem.ID,
                    dbItem.SubCategoryID,
                    SubCategory = dbItem.SubCategory.Name,
                    dbItem.Name,
                    dbItem.PhotoIMG,
                    dbItem.BasePrice,
                    dbItem.SellPrice,
                    dbItem.ContentURL,
                    dbItem.IsStockApplicable,
                    dbItem.StockQty,
                    dbItem.MinPurchaseQty,
                    dbItem.MaxPurchaseQty,
                    dbItem.ShortDescription,
                    dbItem.LongDescription,dbItem.IsPromoted,dbItem.IsFeatured } }
                });
            }
            else if (action == "remove")
            {
                dbItem = itemService.GetById(Convert.ToInt64(data[0]["ID"]));
                try
                {
                    itemService.Delete(dbItem);
                    return Json(new { });
                }
                catch (Exception ex) { return Json(new { error = "Could not delete." }); }
            }
            return Json(new { error = "Invalid operation" });
        }
        [Authorize(Roles = "Admin,Production")]
        public JsonResult UpdateItemImgContent(long ID, string URL, bool IsContentURL)
        {
            var item = itemService.GetById(ID);
            if (IsContentURL)
                item.ContentURL = URL;
            else
                item.PhotoIMG = URL;
            itemService.Update(item);
            return Json(new { Result = "OK" });
        }
        #endregion

        #region SoldItems
        [Authorize(Roles = "Admin,Production")]
        public ActionResult SoldItem()
        {
            return View("~/Views/Shared/SoldItem.cshtml");
        }
        [Authorize(Roles = "Admin,Production")]
        public JsonResult SoldItemList(int draw, Dictionary<string, string> search, int start, int length)
        {
            var result = orderService.GetSoldItemForDT(Convert.ToInt64(User.Identity.GetUserId()), search["value"], start, length);
            var ds = result.Item1.Select(x =>
            {
                return new
                {
                    DT_RowId = x.ID,
                    OrderNo = x.Order.ID,
                    ItemName = x.Item.Name,
                    x.Item.SellPrice,
                    x.Item.PhotoIMG,
                    x.Qty,
                    x.LineAmount,
                    x.ShippingDate,
                    x.DeliveryDate,
                    x.IsCompleted,
                    CustomerName = x.Order.AspNetUser.Name,
                    Address = x.Order.AspNetUserAddress.CBOExpression
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

        #region ChangePassword
        [Authorize()]
        public ActionResult ChangePassword()
        {
            return View("~/Views/Shared/ChangePassword.cshtml");
        }

        [Authorize()]
        [HttpPost]
        public ActionResult ChangePassword(string CurrentPassword, string NewPassword, string ConfirmNewPassword)
        {
            var userManager = new AspNetUserManager(new Microsoft.AspNet.Identity.EntityFramework.UserStore<AspNetUser, AspNetRole, long, AspNetUserLogin, AspNetUserRole, AspNetUserClaim>(new AllYouMedia.DataAccess.DataLayer.DataContext()));
            var identityResult = userManager.ChangePassword(Convert.ToInt64(User.Identity.GetUserId()), CurrentPassword, NewPassword);
            if (identityResult.Succeeded)
            {
                if (aspNetUserSerivce == null) aspNetUserSerivce = DependencyResolver.Current.GetService<IAspNetUserService>();

                var aspnetUser = aspNetUserSerivce.GetById(Convert.ToInt64(User.Identity.GetUserId()));
                aspnetUser.PasswordOrignal = NewPassword;
                aspNetUserSerivce.Update(aspnetUser);
                TempData["Success"] = "Password updated successfully.";
            }
            else
                TempData["Error"] = string.Join(". ", identityResult.Errors);
            return View("~/Views/Shared/ChangePassword.cshtml");
        }
        #endregion

        #region PayoutHistory
        public ActionResult PayoutHistory()
        {
            return View("~/Views/Shared/PayoutHistory.cshtml");
        }
        public JsonResult PayoutHistoryList(int draw, Dictionary<string, string> search, int start, int length)
        {
            if (payoutDistributionService == null) payoutDistributionService = DependencyResolver.Current.GetService<IPayoutDistributionService>();
            var result = payoutDistributionService.GetForDT(Convert.ToInt64(User.Identity.GetUserId()));
            var ds = result.Item1.Select(x =>
            {
                return new
                {
                    DT_RowId = x.ID,
                    x.OrderID,
                    x.PayoutPercentage,
                    x.ReceivedAmount,
                    x.IsAmountReleased,
                    CreatedOn = x.CreatedOn.ToString("dd-MM-yyyy hh:mm")
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
    }
}