using AllYouMedia.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BusinessEntity.ConcreateEntity;
using AllYouMedia.DataAccess.ServiceLayer;
using AllYouMedia.Code;

namespace AllYouMedia.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class SharedController : Controller
    {
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
                    str = userService.GetAspNetUserIDByUserName(User.Identity.Name);
                    Session["AspNetUserID"] = str;
                }
                return str;
            }
        }
        private IAspNetUserService userService;
        private IMessageService messageService;
        private ILogService userLogService;
        public SharedController(AspNetUserService userService, MessageService messageService, LogService userLogService)
        {
            this.userService = userService;
            this.messageService = messageService;
            this.userLogService = userLogService;
        }

        #region AdminHeader
        // GET: Shared
        [ChildActionOnly]
        public PartialViewResult AdminLeft()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult AdminHeader()
        {
            AdminHeaderModel model = new AdminHeaderModel();
            List<MessageModel> Message = new List<MessageModel>();
            var lstMessage = messageService.GetMessagesByUserID(AspNetUserID);
            ViewBag.MessageCount = lstMessage.Count;
            Message = lstMessage.Select(x =>
            {
                return new MessageModel()
                {
                    MailFrom = x.FromAspNetUser.Name + " <<" + x.FromAspNetUser.UserName + ">>",
                    Subject = x.Subject,
                    Message = x.Body,
                    SentDate = x.CreatedOn.ToDateTimeString(),
                };

            }).ToList();
            model.Message = Message;
            List<AdminActivityModel> Activity = null;
            //DataTable dt_Activity = new RoleDataEntity().Role_GetAdminActivityForHeader(AspNetUserID);
            var lstActivity = userLogService.GetUserLogForHeader(AspNetUserID);
            Activity = lstActivity.Select(x =>
            {
                return new AdminActivityModel()
                {
                    Reference = x.Description,
                    Type = x.ActivityType,
                    ActionDate = x.CreatedOn.ToDateTimeString(),
                };

            }).ToList();
            model.Activity = Activity;

            model.Image = "/Images/AdminImage.png";
            return PartialView(model);
        }
        #endregion
    }
}