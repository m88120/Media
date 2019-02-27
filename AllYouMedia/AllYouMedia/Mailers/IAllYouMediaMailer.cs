using Mvc.Mailer;

namespace AllYouMedia.Mailers
{ 
    public interface IAllYouMediaMailer
    {
        MvcMailMessage RegistrationSuccessfull(string UserName, string Name, string MembershipType, string ConfirmationURL);
        MvcMailMessage OrderSuccessfull(string UserName, string Name, AllYouMedia.Controllers.HomeController.SessionCart cart, string DeliveryAddress,long OrderID);
	}
}