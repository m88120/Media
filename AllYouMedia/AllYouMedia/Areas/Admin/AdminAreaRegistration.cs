using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AllYouMedia.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(name: "AdminLogin", url: "adminlogin", defaults: new { controller = "Logon", action = "Index" });
            context.MapRoute(name: "Admin", url: "Admin", defaults: new { controller = "Admin", action = "Index" });
            context.MapRoute(name: "AdminLogout", url: "logout", defaults: new { controller = "Admin", action = "Logout" });
            context.MapRoute(name: "AdminChangePassword", url: "admin/changepassword", defaults: new { controller = "Admin", action = "ChangePassword" });
            context.MapRoute(name: "PayoutHistory", url: "admin/PayoutHistory", defaults: new { controller = "Admin", action = "PayoutHistory" });
            context.MapRoute(name: "PayoutHistoryList", url: "admin/PayoutHistoryList", defaults: new { controller = "Admin", action = "PayoutHistoryList" });
            context.MapRoute(name: "UserEdit", url: "admin/useredit", defaults: new { controller = "Admin", action = "UserEdit", id = UrlParameter.Optional });
            context.MapRoute(name: "Distributor", url: "admin/distributor", defaults: new { controller = "Admin", action = "Distributor" });
            context.MapRoute(name: "MembershipReport", url: "admin/membership", defaults: new { controller = "Admin", action = "Membership" });
            context.MapRoute(name: "IncomeReferral", url: "admin/income/referral", defaults: new { controller = "Admin", action = "IncomeReferral" });
            context.MapRoute(name: "Event", url: "admin/event", defaults: new { controller = "Admin", action = "Event" });
            context.MapRoute(name: "EventPerformer", url: "admin/performer", defaults: new { controller = "Admin", action = "EventPerformer" });
            context.MapRoute(name: "GeneologyLevel", url: "admin/geneology/level", defaults: new { controller = "Admin", action = "GeneologyLevel" });
            context.MapRoute(name: "GeneologyDownline", url: "admin/geneology/downline", defaults: new { controller = "Admin", action = "GeneologyDownline" });
            context.MapRoute(name: "Message", url: "admin/message", defaults: new { controller = "Admin", action = "Message" });
            context.MapRoute(name: "PurchaseHistory", url: "admin/purchasehistory", defaults: new { controller = "Admin", action = "PurchaseHistory" });
            context.MapRoute(name: "CategoryManagement", url: "admin/category", defaults: new { controller = "Admin", action = "CategoryManagement" });
            context.MapRoute(name: "SubCategoryManagement", url: "admin/subcategory", defaults: new { controller = "Admin", action = "SubCategoryManagement" });
            context.MapRoute(name: "Control", url: "admin/control", defaults: new { controller = "Admin", action = "Control" });
            context.MapRoute(name: "PageManagement", url: "admin/pagemanagement", defaults: new { controller = "Admin", action = "PageManagement" });
            context.MapRoute(name: "AddMediaPromoter", url: "admin/addareapromoter", defaults: new { controller = "Admin", action = "AddMediaPromoter" });
            context.MapRoute(name: "GenerateCode", url: "admin/generatecode", defaults: new { controller = "Admin", action = "GenerateCode" });
            context.MapRoute(name: "SuggestCategory", url: "admin/suggestcategory", defaults: new { controller = "Admin", action = "SuggestCategory" });
            context.MapRoute(name: "AddProduct", url: "admin/addproduct", defaults: new { controller = "Admin", action = "AddProduct" });

            context.MapRoute(
            "Admin_default",
            "Admin/{controller}/{action}/{id}",
            new { controller = "Admin", action = "Index", id = UrlParameter.Optional },
            new[] { "AllYouMedia.Areas.Admin.Controllers" }
        );
        }
    }
}