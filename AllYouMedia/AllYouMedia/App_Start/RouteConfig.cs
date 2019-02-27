using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AllYouMedia
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(name: "Bio", url: "bio/{id}", defaults: new { controller = "Bio", action = "Index", ID = UrlParameter.Optional });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "AllYouMedia.Controllers" }
            );

            //routes.MapRoute(name: "Login", url: "login", defaults: new { controller = "Account", action = "Login" });
            //routes.MapRoute(name: "ForgotPaasWord", url: "forgotpassword", defaults: new { controller = "Account", action = "ForgotPaasWord" });
            //routes.MapRoute(name: "AppLogin", url: "AppLogin", defaults: new { controller = "Account", action = "AppLogin" });           

            //routes.MapRoute(name: "GuestEvent", url: "event", defaults: new { controller = "Event", action = "Index" });
            //routes.MapRoute(name: "EventDetail", url: "eventdetail", defaults: new { controller = "Event", action = "EventDetail" });
            //routes.MapRoute(name: "EventCart", url: "eventcart", defaults: new { controller = "Event", action = "EventCart" });
            //routes.MapRoute(name: "EventTicketInvoice", url: "eventticketinvoice", defaults: new { controller = "Account", action = "EventTicketInvoice" });

            //routes.MapRoute(name: "User", url: "user", defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional });
            //routes.MapRoute(name: "UserLogout", url: "user/logout", defaults: new { controller = "User", action = "Logout" });
            //routes.MapRoute(name: "ChangePassword", url: "user/changepassword", defaults: new { controller = "User", action = "ChangePassword" });
            //routes.MapRoute(name: "BioGraphProfile", url: "user/biographprofile", defaults: new { controller = "User", action = "BioGraphProfile" });
            //routes.MapRoute(name: "BioUploadContent", url: "user/biouploadcontent", defaults: new { controller = "User", action = "BioUploadContent" });
            //routes.MapRoute(name: "ChangeImage", url: "user/changeimage", defaults: new { controller = "User", action = "ChangeImage" });
            //routes.MapRoute(name: "Collaboration", url: "user/collaboration", defaults: new { controller = "User", action = "Collaboration" });
            //routes.MapRoute(name: "MyProfile", url: "user/myprofile", defaults: new { controller = "User", action = "MyProfile" });
            //routes.MapRoute(name: "CustomerHistory", url: "user/customerhistory", defaults: new { controller = "User", action = "CustomerHistory" });
            //routes.MapRoute(name: "Earning", url: "user/earning", defaults: new { controller = "User", action = "Earning" });
            //routes.MapRoute(name: "UserMessage", url: "user/message", defaults: new { controller = "User", action = "Message" });
            //routes.MapRoute(name: "UserEvent", url: "user/userevent", defaults: new { controller = "User", action = "Event" });
            //routes.MapRoute(name: "TalentSearch", url: "user/talentsearch", defaults: new { controller = "User", action = "TalentSearch" });
            //routes.MapRoute(name: "UserPurchaseHistory", url: "user/purchasehistory", defaults: new { controller = "User", action = "PurchaseHistory" });
            //routes.MapRoute(name: "TicketHistory", url: "user/tickethistory", defaults: new { controller = "User", action = "TicketHistory" });
            //routes.MapRoute(name: "ResourceCenter", url: "user/resourcecenter", defaults: new { controller = "User", action = "ResourceCenter" });
            //routes.MapRoute(name: "ContactUs", url: "user/contactus", defaults: new { controller = "User", action = "ContactUs" });
            //routes.MapRoute(name: "MediaPromoterCustomer", url: "user/Mediapromotercustomer", defaults: new { controller = "User", action = "MediaPromoterCustomer" });
            //routes.MapRoute(name: "ToolBox", url: "user/toolbox", defaults: new { controller = "User", action = "ToolBox" });
            //routes.MapRoute(name: "EventTicketReport", url: "user/eventticketreport", defaults: new { controller = "User", action = "EventTicketReport" });
            //routes.MapRoute(name: "UserRefferalReport", url: "user/userrefferalreport", defaults: new { controller = "User", action = "UserRefferalReport" });
            //routes.MapRoute(name: "Info", url: "user/info", defaults: new { controller = "User", action = "Info" });
            //routes.MapRoute(name: "Upgrade", url: "user/Upgrade", defaults: new { controller = "User", action = "Upgrade" });
            //routes.MapRoute(name: "UserGeneologyLevel", url: "user/geneologylevel", defaults: new { controller = "User", action = "GeneologyLevel" });
            //routes.MapRoute(name: "GeneologyTree", url: "user/geneologytree", defaults: new { controller = "User", action = "GeneologyTree" });
            //routes.MapRoute(name: "UserAreaPromoter", url: "user/areapromoter", defaults: new { controller = "User", action = "AreaPromoter" });

            //routes.MapRoute(name: "UserIncomeReferral", url: "user/incomereferral", defaults: new { controller = "User", action = "IncomeReferral" });

            //routes.MapRoute(name: "OnlineProduction", url: "user/onlineproduction", defaults: new { controller = "User", action = "OnlineProduction" });
            //routes.MapRoute(name: "MarketingTools", url: "user/marketingtools", defaults: new { controller = "User", action = "MarketingTools" });
            //routes.MapRoute(name: "LegalTools", url: "user/legaltools", defaults: new { controller = "User", action = "LegalTools" });
            //routes.MapRoute(name: "LearningCenter", url: "user/learningcenter", defaults: new { controller = "User", action = "LearningCenter" });
            //routes.MapRoute(name: "ProductTools", url: "user/producttools", defaults: new { controller = "User", action = "ProductTools" });
            //routes.MapRoute(name: "MarketingLink", url: "user/marketinglink", defaults: new { controller = "User", action = "MarketingLink" });
            //routes.MapRoute(name: "AddShippingInfo", url: "user/addshippinginfo", defaults: new { controller = "User", action = "AddShippingInfo" });

            //routes.MapRoute(name: "Product", url: "product", defaults: new { controller = "Product", action = "Index" });
            //routes.MapRoute(name: "ProductDetail", url: "productdetail", defaults: new { controller = "Product", action = "ProductDetail" });
            //routes.MapRoute(name: "Cart", url: "cart", defaults: new { controller = "Product", action = "Cart" });
            //routes.MapRoute(name: "ShoppingInvoice", url: "shoppinginvoice", defaults: new { controller = "Product", action = "ShoppingInvoice" });            

            //routes.MapRoute(name: "AboutUs", url: "aboutus", defaults: new { controller = "Home", action = "About" });
            //routes.MapRoute(name: "Contact", url: "contact", defaults: new { controller = "Home", action = "Contact" });
            //routes.MapRoute(name: "Membership", url: "membership", defaults: new { controller = "Home", action = "Membership" });
            //routes.MapRoute(name: "PrivacyPolicy", url: "privacypolicy", defaults: new { controller = "Home", action = "PrivacyPolicy" });
            //routes.MapRoute(name: "TermsCondition", url: "termscondition", defaults: new { controller = "Home", action = "TermsCondition" });
            //routes.MapRoute(name: "TalentAgreement", url: "talentagreement", defaults: new { controller = "Home", action = "TalentAgreement" });
            //routes.MapRoute(name: "MediaAgreement", url: "mediaagreement", defaults: new { controller = "Home", action = "MediaAgreement" });
            //routes.MapRoute(name: "AreaPromoterAgreement", url: "areapromoteragreement", defaults: new { controller = "Home", action = "AreaPromoterAgreement" });
            //routes.MapRoute(name: "ContestRule", url: "contestruleagreement", defaults: new { controller = "Home", action = "ContestRule" });

            //routes.MapRoute(name: "Register", url: "register", defaults: new { controller = "Account", action = "Register" });
            //routes.MapRoute(name: "AreaPromoter", url: "areapromoter", defaults: new { controller = "Account", action = "AreaPromoter" });

            //#region
            //routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new[] { "AllYouMedia.Controllers" });
            //#endregion
        }
    }
}
