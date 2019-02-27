using Mvc.Mailer;

namespace AllYouMedia.Mailers
{
    public class AllYouMediaMailer : MailerBase, IAllYouMediaMailer
    {
        public AllYouMediaMailer()
        {
            MasterName = "_Layout";
        }

        public virtual MvcMailMessage RegistrationSuccessfull(string UserName, string Name, string MembershipType, string ConfirmationURL)
        {
            ViewBag.UserName = UserName;
            ViewBag.Name = Name;
            ViewBag.MembershipType = MembershipType;
            ViewBag.ConfirmationURL = ConfirmationURL;
            return Populate(x =>
            {
                x.Subject = "AllYouMedia Membership Registration Successful.";
                x.ViewName = "RegistrationSuccessfull";
                x.To.Add(UserName);
            });
        }
        public virtual MvcMailMessage OrderSuccessfull(string UserName, string Name, AllYouMedia.Controllers.HomeController.SessionCart cart, string DeliveryAddress, long OrderID)
        {
            ViewBag.UserName = UserName;
            ViewBag.Name = Name;
            ViewBag.SessionCart = cart;
            ViewBag.DeliveryAddress = DeliveryAddress;
            ViewBag.OrderID = OrderID;
            return Populate(x =>
            {
                x.Subject = "AllYouMedia new order placed.";
                x.ViewName = "OrderSuccessfull";
                x.To.Add(UserName);
            });
        }

        public virtual MvcMailMessage FanSharingRequestToUser(string ToEmail, string ToName, string FromEmail, string FromName)
        {
            ViewBag.ToName = ToName;
            ViewBag.FromEmail = FromEmail;
            ViewBag.FromName = FromName;            
            return Populate(x =>
            {
                x.Subject = "AllYouMedia new fan sharing request.";
                x.ViewName = "FanSharingRequestToUser";
                x.To.Add(ToEmail);
            });
        }
        public virtual MvcMailMessage FanSharingRequestToFan(string ToEmail, string ToName,string FromName,string ActionLink)
        {
            ViewBag.ToName = ToName;
            ViewBag.ToName = ToName;
            ViewBag.FromName = FromName;
            ViewBag.ActionLink = ActionLink;
            return Populate(x =>
            {
                x.Subject = "AllYouMedia become a fan request.";
                x.ViewName = "FanSharingRequestToFan";
                x.To.Add(ToEmail);
            });
        }
    }
}