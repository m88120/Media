using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using AllYouMedia.Models;
using BusinessEntity.ConcreateEntity;
using System.Web.Security;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using AllYouMedia.DataAccess.DataLayer;
using AllYouMedia.DataAccess.ServiceLayer;
using AllYouMedia.Code;
using AllYouMedia.Mailers;
using AllYouMedia.DataAccess.ServiceLayer.Interface;

namespace AllYouMedia.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IAspNetUserService userService;
        private ICartService cartService;
        private ICategoryTypeService categoryTypeService;
        private ICategoryService categoryService;
        private ISubCategoryService subCategoryService;
        private IAttributeService attributeService;
        private IUserCategoryMapService userCategoryMapService;
        private IGenderService genderService;
        private IInstrumentService instrumentService;
        private IInstrumentSpecificationService instrumentSpecification;
        private IGenreService genreService;
        //public AccountController()
        //    : this(new AspNetUserManager(new UserStore<AspNetUser, AspNetRole, long, AspNetUserLogin, AspNetUserRole, AspNetUserClaim>(new DataContext())))
        //{
        //}

        public AccountController(AspNetUserManager userManager, AspNetUserService _userService, CartService cartService, CategoryTypeService categoryTypeService, CategoryService categoryService, SubCategoryService subCategoryService, AttributeService attributeService, UserCategoryMapService userCategoryMapService, GenderService genderService, 
            InstrumentService instrumentService, InstrumentSpecificationService instrumentSpecification, GenreService genreService)
        {
            this.UserManager = userManager;
            this.userService = _userService;
            this.cartService = cartService;
            this.categoryTypeService = categoryTypeService;
            this.categoryService = categoryService;
            this.subCategoryService = subCategoryService;
            this.attributeService = attributeService;
            this.userCategoryMapService = userCategoryMapService;
            this.genderService = genderService;
            this.instrumentService = instrumentService;
            this.instrumentSpecification = instrumentSpecification;
            this.genreService = genreService;
        }

        public AspNetUserManager UserManager { get; private set; }
        #region Login()
        [AllowAnonymous]
        public ActionResult Login(string ReturnURL)
        {
            ViewBag.ReturnUrl = ReturnURL;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string ReturnURL)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    if (!user.EmailConfirmed) TempData["Error"] = "Please confirm your email to enable to access to your account.";
                    else if (!user.IsActive) TempData["Error"] = "Account deactivated. Please contact on support.";
                    else
                    {
                        await SignInAsync(user, model.RememberMe);
                        if (string.IsNullOrEmpty(ReturnURL)) return RedirectToAction("Index", "User");
                        else if (ReturnURL.Contains("Checkout")) SyncSessionCartToDbCart(user.Id);
                        return Redirect(ReturnURL);
                    }
                }
                else
                {
                    TempData["Error"] = "Invalid username or password.";
                }
            }
            ViewBag.ReturnURL = ReturnURL;
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Register()
        [AllowAnonymous]
        public ActionResult Register(string ReturnURL, int MembershipType = 1)
        {
            if (User.Identity.IsAuthenticated)
            {
                TempData["Alert"] = "You are already logged into a account. Kindly logout before registering for new membership.";
                return RedirectToAction("Index", "Home");
            }
            var model = new RegisterModel
            {
                ReturnURL = ReturnURL,
                MembershipType = MembershipType
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterModel model, string ReturnURL)
        {
            if (ModelState.IsValid)
            {
                //if (Session["Captcha"] == null || Session["Captcha"].ToString() != model.Captcha)
                //{
                //    ModelState.AddModelError("", "Please Enter Valid Captcha.");
                //}
                long parentUserID = -1;
                if (string.IsNullOrEmpty(model.RefferCode) == false)
                {
                    parentUserID = userService.GetAspNetUserIDByRefferCode(model.RefferCode);
                    if (parentUserID == -1) { TempData["Error"] = "Invalid reffer code. Please provide a valid reffer code if you are provided with any."; goto returnControle; }
                }
                SharedLibrary.MemberTypeEnum userType = (SharedLibrary.MemberTypeEnum)model.MembershipType;

                var result = UserManager.Create(new AspNetUser
                {
                    CurrentIP = Request.UserHostAddress,
                    CurrentLoginTime = DateTime.Now,
                    Email = model.UserName,
                    EmailConfirmed = false,
                    FailedPasswordAttemptCount = 0,
                    IsActive = true,
                    LastIP = Request.UserHostAddress,
                    LastLoginTime = DateTime.Now,
                    LockoutEnabled = false,
                    LoginCount = 1,
                    Name = model.Name,
                    PasswordOrignal = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberConfirmed = false,
                    RefferCode = Guid.NewGuid().ToString(),
                    Status = (int)_CodeClass.Status.Active,
                    UserName = model.UserName,
                    CreatedOn = DateTime.Now,
                    //SubCategoryID = -1
                    UserLevelID = userType == SharedLibrary.MemberTypeEnum.MediaPromoter ? -100 : -1
                }, model.Password);
                if (result.Succeeded)
                {
                    var user = UserManager.FindByName(model.UserName);
                    var roleResult = UserManager.AddToRole(user.Id, userType.ToString());
                    userService.AddToAspNetUserHierarchy(user.Id, parentUserID);
                    if (userType == SharedLibrary.MemberTypeEnum.AllTalent || userType == SharedLibrary.MemberTypeEnum.Production)
                    {
                        //userCategoryMapService.Insert(new AllYouMedia.DataAccess.EntityLayer.DBEntity.UserCategoryMap
                        //{
                        //    AspNetUserRoleID = userService.GetAspNetUserRoleIDByUserAndRole(user.Id, userType.ToString()),
                        //   // AttributeID = model.AttributeID,
                        //    CategoryID = model.CategoryID,
                        //    CategoryTypeID = model.CategoryTypeID,
                        //   // SubCategoryID = model.SubCategoryID
                        //});
                        AllYouMedia.DataAccess.EntityLayer.DBEntity.UserCategoryMap _model = new AllYouMedia.DataAccess.EntityLayer.DBEntity.UserCategoryMap();
                        _model.AspNetUserRoleID = userService.GetAspNetUserRoleIDByUserAndRole(user.Id, userType.ToString());
                       // AttributeID = model.AttributeID,
                        _model.CategoryTypeID = model.CategoryTypeID;
                        _model.CategoryID = model.CategoryID;
                        _model.GenderID = model.GenderID;
                        _model.GenreID = model.GenreId;
                        _model.InstrumentID = model.InstrumentId;
                        _model.InstrumentSpeciID = model.InstrumentSpecificationId;
                        _model.CreatedOn = DateTime.Now;
                        _model.ModifiedOn = DateTime.Now;
                        _model.IsActive = true;
                        userCategoryMapService.Insert(_model);
                    
                }
                    else
                    {
                        //model.CategoryID = -1;
                        //model.SubCategoryID = -1;
                        //model.CategoryTypeID = -1;
                        //model.AttributeID = -1;
                    }
                    //////////////// SEND User Confirmation email
                    AllYouMediaMailer mailer = new AllYouMediaMailer();
                    mailer.RegistrationSuccessfull(user.UserName, user.Name, userType.ToString(), Url.Action("ConfirmEmail", "Account", new { Token = user.Id, Email = user.Email }, Request.Url.Scheme)).SendAsync();

                    await SignInAsync(user, true);
                    if (string.IsNullOrEmpty(ReturnURL)) return RedirectToAction("Index", "Home");
                    else
                    {
                        if (ReturnURL.Contains("Checkout")) SyncSessionCartToDbCart(user.Id);
                        return Redirect(ReturnURL);
                    }
                }
                else
                    TempData["Error"] = string.Join("|", result.Errors);
            }
        returnControle: return View(model);
        }
        //[AllowAnonymous]
        //public JsonResult TestEmail()
        //{
        //    AllYouMediaMailer mailer = new AllYouMediaMailer();
        //    mailer.RegistrationSuccessfull("sharma_rudra@hotmail.com", "Rudra Sharma", "Customer", Url.Action("ConfirmEmail", "Account", new { Token = 2, Email = "sharma_rudra@hotmail.com" }, Request.Url.Scheme)).Send();
        //    return Json("OK");
        //}
        private void SyncSessionCartToDbCart(long AspNetUserID)
        {
            if (Session["SessionCart"] != null)
            {
                var sessionCart = (AllYouMedia.Controllers.HomeController.SessionCart)Session["SessionCart"];
                foreach (var cartItem in sessionCart.CartItems)
                {
                    cartService.AddToCart(AspNetUserID, cartItem.ItemID, cartItem.Qty);
                }
            }
        }
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string Token, string Email)
        {

            var user = UserManager.FindById(Convert.ToInt64(Token));
            if (user != null)
            {
                if (user.Email == Email)
                {
                    user.EmailConfirmed = true;
                    await UserManager.UpdateAsync(user);
                    TempData["Success"] = "Your email confirmed successfully.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Error"] = "Invalid confirmation url. Please contact support team.";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Error"] = "Invalid confirmation url. Please contact support team.";
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion

        [AllowAnonymous]
        public JsonResult GetCategoryTypeByMembershipType(int MembershipType)
        {
            return Json(new { Result = "OK", Data = categoryTypeService.CategoryTypeCboByMembershipType(MembershipType) });
        }

        [AllowAnonymous]
        public JsonResult GetCategoryByCategoryTypeMembershipType(int MembershipType, long CategoryTypeID)
        {
            return Json(new { Result = "OK", Data = categoryService.CategoryCboByCategoryTypeMembershipType(MembershipType, CategoryTypeID) });
        }
        [AllowAnonymous]
        public JsonResult GetGenderSpecificType(int MembershipType, long CategoryID)
        {
            return Json(new { Result = "OK", Data = categoryService.GenderSpecific(MembershipType, CategoryID) });
        }
        [AllowAnonymous]
        public JsonResult GetGenreType(int MembershipType, long CategoryID)
        {
            return Json(new { Result = "OK", Data = categoryService.GetGenreCategory(MembershipType, CategoryID) });
        }
        [AllowAnonymous]
        public JsonResult GetGenderSpecificGenreType(int MembershipType, long GenderTypeID)
        {
            return Json(new { Result = "OK", Data = genderService.GenderGenreCategory(MembershipType, GenderTypeID) });
        }
        [AllowAnonymous]
        public JsonResult GetinstrumentGenreType(int MembershipType, long GenreTypeID)
        {
            return Json(new { Result = "OK", Data = genreService.GenreInstrumentCategory(MembershipType, GenreTypeID) });
        }

        [AllowAnonymous]
        public JsonResult GetinstrumentType(int MembershipType, long InstrumentID)
        {
            return Json(new { Result = "OK", Data = instrumentSpecification.InstrumentSpecificationCategory(MembershipType, InstrumentID) });
        }
        [AllowAnonymous]
        public JsonResult GetSubCategoryByCategoryMembershipType(int MembershipType, long CategoryID)
        {
            return Json(new { Result = "OK", Data = subCategoryService.SubCategoryCboByCategoryTypeMembershipType(MembershipType, CategoryID) });
        }
        [AllowAnonymous]
        public JsonResult GetAttributeBySubCategory(long SubCategoryID)
        {
            return Json(new { Result = "OK", Data = attributeService.AttributeCboBySubCategoryMembershipType(SubCategoryID) });
        }

        [AllowAnonymous]
        public ActionResult BecomeAFan(long ID, int MembershipType = 1)
        {
            if (User.Identity.IsAuthenticated)
            {
                TempData["Alert"] = "You are already logged into a account. Kindly logout before registering for new membership.";
                return RedirectToAction("Index", "Home");
            }
            var model = new RegisterModel
            {
                ReturnURL = "",
                MembershipType = MembershipType
            };
            return View(model);
        }
        #region CaptchaImage(prefix, noisy)
        [AllowAnonymous]
        public ActionResult CaptchaImage(string prefix, bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            //generate new question 
            int a = rand.Next(10, 90);
            int b = rand.Next(1, 9);
            var captcha = string.Format("{0} + {1} = ?", a, b);

            //store answer 
            Session["Captcha" + prefix] = a + b;

            //image stream 
            FileContentResult img = null;

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(130, 30))
            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.LightGray, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise 
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.AliceBlue);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)));

                        r = rand.Next(0, (130 / 3));
                        x = rand.Next(0, 130);
                        y = rand.Next(0, 30);

                        gfx.DrawEllipse(pen, x - r, y - r, r, r);
                    }
                }

                //add question 
                gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Black, 4, 5);

                //render as Jpeg 
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpeg");
            }

            return img;
        }
        #endregion

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(AspNetUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        #region ForgotPaasWord()
        [AllowAnonymous]
        public ActionResult ForgotPaasWord()
        {
            ForgotPaasWordModel model = new ForgotPaasWordModel();
            string code = (Request.QueryString["code"] != null) ? Request.QueryString["code"].ToString().Trim() : "";
            if (code != "")
            {
                TempData["Code"] = code;
                ViewBag.code = true;
                ViewBag.forgot = false;
                model.Code = code;
            }
            else
            {
                ViewBag.code = false;
                ViewBag.forgot = true;
            }

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ForgotPaasWord(ForgotPaasWordModel model)
        {
            object message = string.Empty;
            DataTable dt = new RoleDataEntity().Role_Select_UserForgotPassword(model.LoginName, out message);

            if (dt.Rows.Count > 0)
            {
                _CodeClass.SendMail(dt.Rows[0]["MAIL"].ToString(), dt.Rows[0]["MAIL SUBJECT"].ToString(), dt.Rows[0]["MAIL BODY"].ToString());

                ModelState.Clear();
                TempData["Message"] = "<div class='alert alert-success' role='alert'>" + message.ToString() + "</div>";
            }
            else
            {
                TempData["Message"] = "<div class='alert alert-danger' role='alert'>" + message.ToString() + "</div>";
            }
            ViewBag.code = false;
            ViewBag.forgot = true;
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ResetPaasWord(ForgotPaasWordModel model)
        {
            object message = string.Empty;
            DataTable dt = new RoleDataEntity().Role_Update_UserPassword(model.Code, model.Password, out message);

            if (dt.Rows.Count > 0)
            {
                _CodeClass.SendMail(dt.Rows[0]["MAIL"].ToString(), dt.Rows[0]["MAIL SUBJECT"].ToString(), dt.Rows[0]["MAIL BODY"].ToString());
                ModelState.Clear();
                TempData["Message"] = "<div class='alert alert-success' role='alert'>" + message.ToString() + "</div>";
            }
            else
            {
                TempData["Message"] = "<div class='alert alert-danger' role='alert'>" + message.ToString() + "</div>";
            }
            return RedirectToAction("Login");
        }
        #endregion

        #region GetUserName
        public ActionResult GetUserName(string userid)
        {
            string lblclass = "";
            string Sponsorname = "";
            string name = new RoleDataEntity().Role_GetUserName(userid);
            if (name == "NO USER FOUND!")
            {
                lblclass = "validation";
            }
            else
            {
                lblclass = "text-success";
                Sponsorname = userid;
            }
            return Json(new { success = true, Sponsorname = Sponsorname, resultName = name, lblclass = lblclass }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}