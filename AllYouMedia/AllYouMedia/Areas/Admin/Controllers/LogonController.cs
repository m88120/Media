using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AllYouMedia.Areas.Admin.Models;
using DataLayer;
using System.Web.Security;
using BusinessEntity.ConcreateEntity;
using Microsoft.AspNet.Identity;
using AllYouMedia.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using AllYouMedia.DataAccess.DataLayer;
using AllYouMedia.DataAccess.ServiceLayer;
using AllYouMedia.DataAccess.EntityLayer.DBEntity;

namespace AllYouMedia.Areas.Admin.Controllers
{
    public class LogonController : Controller
    {
        private IAspNetUserService userService;
        //public LogonController()
        //    : this(new AspNetUserManager(new UserStore<AspNetUser, AspNetRole, long, AspNetUserLogin, AspNetUserRole, AspNetUserClaim>(new DataContext())))
        //{
        //    userService = _userService;
        //}

        public LogonController(AspNetUserManager userManager, AspNetUserService _userService)
        {
            UserManager = userManager;
            userService = _userService;
        }

        public AspNetUserManager UserManager { get; private set; }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public JsonResult RegisterAdmin()
        {
            var userName="admin@allyoumedia.com";
            var result = UserManager.Create(new AspNetUser
            {
                PasswordOrignal = "12345678",
                UserName = userName,
                CreatedOn = DateTime.Now,
                Email = userName,
                EmailConfirmed = true,
                IsActive = true,
                Name = "Admin",
            }, "12345678");
            if (result.Succeeded)
            {
                var user=UserManager.FindByName(userName);
                UserManager.AddToRole(user.Id, "Admin");
                return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { Result = "ERROR", Message = string.Join("|", result.Errors) }, JsonRequestBehavior.AllowGet);
        }
        // GET: Admin/AdminLogin
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Index(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.LoginID, model.LoginPassword);
                if (user != null)
                {
                    if (UserManager.IsInRole(user.Id, "Admin"))
                    {
                        var userInternalTupple = userService.UserLoginChecknUpdate(model.LoginID, Request.UserHostAddress);
                        if (userInternalTupple.Item1 == null)
                        {
                            await SignInAsync(user, model.RememberMe);
                            return RedirectToLocal(returnUrl);
                        }
                        else ModelState.AddModelError("", userInternalTupple.Item1);
                    }
                    else
                        ModelState.AddModelError("", "Unauthorized! Your are not a admin.");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View(model);
        }
        private async Task SignInAsync(AspNetUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }
    }
}