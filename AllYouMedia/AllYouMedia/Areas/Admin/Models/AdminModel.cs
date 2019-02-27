using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AllYouMedia.Areas.Admin.Models
{
    public class AdminHeaderModel
    {
        public string Image { get; set; }
        public List<MessageModel> Message { get; set; }
        public List<AdminActivityModel> Activity { get; set; }
    }

    public class AdminIndexModel
    {
        public string TotalTalent { get; set; }
        public string TotalProduction { get; set; }
        public string TotalMediaPromoter { get; set; }
        public string TotalCustomer { get; set; }

    }

    public class AdminActivityModel
    {
        public string UID { get; set; }
        public string Reference { get; set; }
        public string Type { get; set; }
        public string ActionDate { get; set; }
    }

    public class PasswordModel
    {
        [Required(ErrorMessage = "Enter Old Password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Enter New Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter Confirm Password")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password Must be Same")]
        public string ConfirmPassword { get; set; }
    }

    public class UserReportModel
    {
        public int Sno { get; set; }
        [Required(ErrorMessage = "LoginName Required")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "UserName Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Country Required")]
        public List<SelectListItem> CountryList { get; set; }

        [Required(ErrorMessage = "State Required")]
        public List<SelectListItem> StateList { get; set; }

        [Required(ErrorMessage = "Status Required")]
        public List<SelectListItem> StatusList { get; set; }

        public string Country { get; set; }
        public string State { get; set; }

        [Required(ErrorMessage = "City Required")]
        public string City { get; set; }
       
        public string Street { get; set; }

        [Required(ErrorMessage = "Address Required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "ZipCode Required")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "BioGraphy Required")]
        public string BioGraphy { get; set; }

        public string EnrollmentDate { get; set; }

        [Required(ErrorMessage = "SponsorID Required")]
        public string SponsorID { get; set; }

        [Required(ErrorMessage = "Mobile Required")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string College { get; set; }
        public string Major { get; set; }

        public string Role { get; set; }
        public string AddedBy { get; set; }
        public string Status { get; set; }
    }

    public class CategoryManagenentModel
    {
        public int Sno { get; set; }
        public long ID { get; set; }
        public long CategoryTypeID { get; set; }
        public string SubMenu { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Category Type Required")]
        public List<SelectListItem> CategoryTypeList { get; set; }

        [Required(ErrorMessage = "SubMenu Required")]
        public List<SelectListItem> SubMenuList { get; set; }
        [Required(ErrorMessage = "CategoryName Required")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "SubCategoryName Required")]
        public string SubCategoryName { get; set; }
    }

    public class PurchaseHistoryModel
    {
        public int Sno { get; set; }
        public string UID { get; set; }
        public string EmailID { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string OrderNumber { get; set; }
        public string PaidAmount { get; set; }
    }

    public class InvoiceModel
    {
        public string UID { get; set; }
        public string InvoiceNo { get; set; }
        public string NAME { get; set; }
        public string ADDRESS { get; set; }
        public string ADDRESS2 { get; set; }
        public string MOBILE { get; set; }
        public string EMAIL { get; set; }
        public string Code { get; set; }
        public string PaymentDate { get; set; }
        public string Rate { get; set; }
        public string Quantity { get; set; }
        public string Amount { get; set; }
        public string Tax { get; set; }
        public string NetAmount { get; set; }
        public string Product { get; set; }
    }

    public class IncomeModel
    {
        public int Sno { get; set; }
        public string UID { get; set; }
        public string LoginName { get; set; }
        public string Date { get; set; }
        public string Amount { get; set; }
        public string Deduction { get; set; }
        public string NetAmount { get; set; }
    }

    public class MessageModel
    {
        public int Sno { get; set; }
        public string UID { get; set; }
        public string LoginName { get; set; }
        public string MailFrom { get; set; }
        public string MailTo { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string SentDate { get; set; }

        public ComposeModel Compose { get; set; }
    }

    public class ComposeModel
    {
        public string ComposeType { get; set; }
        public string LoginName { get; set; }

        [Required(ErrorMessage = "Subject Required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message Required")]
        public string Message { get; set; }

    }

    public class ReadMessageModel
    {
        public string SentDate { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }

    public class ControlModel
    {
        [Required(ErrorMessage = "Control Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Amount Required")]
        public string ATAmount { get; set; }
        [Required(ErrorMessage = "Amount Required")]
        public string PMAmount { get; set; }
        [Required(ErrorMessage = "Amount Required")]
        public string MPAmount { get; set; }

        [Required(ErrorMessage = "Order Email Required")]
        [DataType(DataType.EmailAddress)]
        public string OrderEmail { get; set; }

        [Required(ErrorMessage = "Contact Email Required")]
        [DataType(DataType.EmailAddress)]
        public string ContactEmail { get; set; }

        public List<PvPlanModel> PvPlan { get; set; }
    }

    public class PvPlanModel
    {
        public int KID { get; set; }
        public string Code { get; set; }
        public double Commission { get; set; }
    }

    public class EventModel
    {
        public int Sno { get; set; }
        public string UID { get; set; }

        [Required(ErrorMessage = "EventName Required")]
        public string EventName { get; set; }

        [Required(ErrorMessage = "Country Required")]
        public List<SelectListItem> CountryList { get; set; }

        [Required(ErrorMessage = "State Required")]
        public List<SelectListItem> StateList { get; set; }
        public string Country { get; set; }
        public string State { get; set; }

        [Required(ErrorMessage = "City Required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Venue Required")]
        public string Venue { get; set; }

        [Required(ErrorMessage = "Host Required")]
        public string Host { get; set; }

        [Required(ErrorMessage = "Performance Required")]
        public string Performance { get; set; }

        [Required(ErrorMessage = "SeatCapacity Required")]
        public string SeatCapacity { get; set; }

        [Required(ErrorMessage = "Fee Required")]
        public string Fee { get; set; }

        [Required(ErrorMessage = "DateFrom Required")]
        [DataType(DataType.Date)]
        public string DateFrom { get; set; }

        [Required(ErrorMessage = "DateTo Required")]
        [DataType(DataType.Date)]
        public string DateTo { get; set; }

        [Required(ErrorMessage = "TimeFrom Required")]
        [DataType(DataType.Time)]
        public string TimeFrom { get; set; }

        [Required(ErrorMessage = "TimeTo Required")]
        [DataType(DataType.Time)]
        public string TimeTo { get; set; }

        [Required(ErrorMessage = "Remark Required")]
        public string Remark { get; set; }

        [Required(ErrorMessage = "MapUrl Required")]
        [DataType(DataType.Html)]
        public string MapUrl { get; set; }
        public string Status1 { get; set; }
        public bool Status { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase file { get; set; }
        public string ImageUrl { get; set; }
    }

    public class EventPerformerModel
    {
        public int Sno { get; set; }
        public string UID { get; set; }

        [Required(ErrorMessage = "EventName Required")]
        public List<SelectListItem> EventList { get; set; }

        public string EventName { get; set; }

        [Required(ErrorMessage = "PerformerName Required")]
        public string PerformerName { get; set; }

        [Required(ErrorMessage = "Category Required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Fee Required")]
        public string Fee { get; set; }

        [Required(ErrorMessage = "Description Required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "DateFrom Required")]
        [DataType(DataType.Date)]
        public string Date { get; set; }

        [Required(ErrorMessage = "TimeFrom Required")]
        [DataType(DataType.Time)]
        public string TimeFrom { get; set; }

        [Required(ErrorMessage = "TimeTo Required")]
        [DataType(DataType.Time)]
        public string TimeTo { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase file { get; set; }

        public string ImageUrl { get; set; }
    }

    public class GeneologyModel
    {
        public int Sno { get; set; }
        public string LoginName { get; set; }
        public string UserName { get; set; }
        public string RepNo { get; set; }
        public string SponsorLoginName { get; set; }
        public string EnrollmentDate { get; set; }
        public string Shopping { get; set; }
        public int Level { get; set; }
        public string Email { get; set; }
    }

    public class PageModel
    {
        [Required(ErrorMessage = "Page Name Required")]
        public List<SelectListItem> PageUrlList { get; set; }

        public int Sno { get; set; }
        public string PageTitle { get; set; }
        public string PageUrl { get; set; }
        public string PageHTML { get; set; }
    }

    public class AdminRegisterModel
    {
        [Required(ErrorMessage = "Bio Description Required")]
        public string BioDescription { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Name Required")]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [Required(ErrorMessage = "Email Required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter correct email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Country Required")]
        public List<SelectListItem> Country { get; set; }

        [Required(ErrorMessage = "State Required")]
        public List<SelectListItem> State { get; set; }

        [Required(ErrorMessage = "Mobile Required")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "City Required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Address Required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "ZipCode Required")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter Confirm Password")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password Must be Same")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Recruiter ID Required")]
        public string RecruiterID { get; set; }

        public string College { get; set; }

        public List<SelectListItem> Major { get; set; }

        public string Major_Other { get; set; }
    }

    public class GenerateCodeModel
    {
        public List<GenerateCodeModel> NewCode { get; set; }
        public List<GenerateCodeModel> SelectedCode { get; set; }
        public int Sno { get; set; }
        public string Code { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
    }

    public class AdminUploadModel
    {
        [Required(ErrorMessage = "Category Required")]
        public List<SelectListItem> Category { get; set; }

        [Required(ErrorMessage = "SubCategory Required")]
        public List<SelectListItem> SubCategory { get; set; }

        [Required(ErrorMessage = "Category Required")]
        public List<SelectListItem> ImageCategory { get; set; }

        [Required(ErrorMessage = "SubCategory Required")]
        public List<SelectListItem> ImageSubCategory { get; set; }

        [Required(ErrorMessage = "File Name Required")]
        public string FileName { get; set; }

        [Required(ErrorMessage = "Description Required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price Required")]
        public List<SelectListItem> Price { get; set; }

        [Required(ErrorMessage = "file Required")]
        public HttpPostedFileBase[] files { get; set; }
    }

    public class AdminMediaModel
    {
        public int Sno { get; set; }
        public string UID { get; set; }
        public string Name { get; set; }
        public string FileUrl { get; set; }
        public string Type { get; set; }
        public string TypeV { get; set; }
        public string TypeA { get; set; }
        public string TypeI { get; set; }
        public string TypeD { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public string Rating { get; set; }
        public string Category { get; set; }
    }
}