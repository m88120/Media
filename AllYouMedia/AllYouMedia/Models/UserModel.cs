using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AllYouMedia.Models
{
    public class UserModel
    {
        public string LoginName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string ImageUrl { get; set; }
        public string Role { get; set; }
        public string Role_1 { get; set; }
        public string Role_2 { get; set; }
        public string Role_3 { get; set; }
        public string Role_11 { get; set; }
        public string Role_22 { get; set; }
        public string Role_33 { get; set; }
        public string Category { get; set; }
        public string Category_1 { get; set; }
        public string Category_2 { get; set; }
        public string Address { get; set; }
        public int Rate { get; set; }
        public string Biography { get; set; }
        public string Class1 { get; set; }
        public string Class2 { get; set; }
        public string Class3 { get; set; }

        public string SponsorLoginName { get; set; }
        public string SponsorUserName { get; set; }
        public string NetProfit { get; set; }
        public string TotalProfitSales { get; set; }
        public string TotalMediaPromoters { get; set; }
        public string RecruitmentBonuses { get; set; }
        public string Prizes { get; set; }
        public string txtBio { get; set; }
        public string TotalCustomer { get; set; }
        public string EventEarning { get; set; }
        public string AppearenceEarning { get; set; }
        public string Partnerships { get; set; }
        public int LevelCount { get; set; }
        public string StageName { get; set; }


        public List<UserCategoryModel> UserCategory { get; set; }
        public List<UserSubCategoryModel> UserSubCategory { get; set; }
    }

    public class UserCategoryModel
    {
        public string CategoryUID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryClass { get; set; }
        public string CategoryTooltip { get; set; }
    }

    public class UserSubCategoryModel
    {
        public string SubCategoryUID { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryClass { get; set; }
        public string SubCategoryTooltip { get; set; }
    }

    public class UserProfileModel
    {
        [MaxLength(50)]
        [Required(ErrorMessage = "First Name Required")]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [Required(ErrorMessage = "Email Required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter correct email!")]
        public string Email { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Mobile Required")]
        public string Mobile { get; set; }
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Country Required")]
        public List<SelectListItem> Country { get; set; }

        [Required(ErrorMessage = "State Required")]
        public List<SelectListItem> State { get; set; }

        [Required(ErrorMessage = "City Required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Street Required")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Address Required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "ZipCode Required")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Biography Required")]
        public string Biography { get; set; }
    }

    public class ChangeImageModel
    {
        public string UID { get; set; }
        public string ImageUrl { get; set; }
    }

    public class UserPasswordModel
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

    public class BioUploadModel
    {
        public string LoginName { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public string Role { get; set; }
        public string Category { get; set; }
        public string Address { get; set; }
        public int Rate { get; set; }
        public string Biography { get; set; }
        public string Talent { get; set; }
        public string TalentCategory { get; set; }
        public string TotalFan { get; set; }

        public bool Terms { get; set; }
        public List<ChangeImageModel> TalentImage { get; set; }
        public List<SelectedTalentModel> SelectedTalent { get; set; }
        public List<MusicModel> Music { get; set; }
        public List<FilmModel> Film { get; set; }
        public List<WriterModel> Writer { get; set; }
        public List<ContentUploadModel> Upload { get; set; }
        public EditAlbumModel EditAlbum { get; set; }
            
        public List<SelectListItem> UserAlbumList { get; set; }
        public List<SelectListItem> UserAlbumStatus { get; set; }
    }

    public class ContentUploadModel
    {
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryText { get; set; }
    }

    public class TalentMediaModel
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
        public string Album { get; set; }
        public string AlbumUID { get; set; }
    }

    public class SelectedTalentModel
    {
        public int Sno { get; set; }
        public string SelectedTalentName { get; set; }
        public string CategoryName { get; set; }
    }

    public class MusicModel
    {
        public bool Selected { get; set; }
        public string MusicTalentUID { get; set; }
        public string MusicTalentName { get; set; }
    }

    public class FilmModel
    {
        public bool Selected { get; set; }
        public string FilmTalentUID { get; set; }
        public string FilmTalentName { get; set; }
    }

    public class WriterModel
    {
        public bool Selected { get; set; }
        public string WritersTalentUID { get; set; }
        public string WritersTalentName { get; set; }
    }

    public class UploadModel
    {
        public string UID { get; set; }

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

        [Required(ErrorMessage = "Album Name Required")]
        public List<SelectListItem> AlbumList { get; set; }

        [Required(ErrorMessage = "Album Name Required")]
        public string AlbumName { get; set; }

        [Required(ErrorMessage = "Album Description Required")]
        public string AlbumDescription { get; set; }

        public HttpPostedFileBase AlbumCoverImage { get; set; }

        [Required(ErrorMessage = "Description Required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price Required")]
        public List<SelectListItem> Price { get; set; }

        [Required(ErrorMessage = "Price Required")]
        public List<SelectListItem> AlbumPrice { get; set; }

        [Required(ErrorMessage = "Genre Required")]
        public List<SelectListItem> Genre { get; set; }

        [Required(ErrorMessage = "Price Required")]
        [RegularExpression("^([3-9]|[1-9][0-9]|[1-9][0-9][0-9])$", ErrorMessage = "price should be number and above than 3")]
        public string PriceRate { get; set; }
       
        public HttpPostedFileBase[] files { get; set; }

       
        public HttpPostedFileBase ClipFile { get; set; }

        public HttpPostedFileBase CoverImage { get; set; }

        public string FilesToBeUploaded { get; set; }
    }

    public class EditUploadModel
    {
        public string UID { get; set; }       
        public string Category { get; set; }       
        public string SubCategory { get; set; }
        public string ImageCategory { get; set; }
        public string ImageSubCategory { get; set; }
        public string FileName { get; set; }       
        public string Description { get; set; }
        public string Price { get; set; }
        public string Genre { get; set; }      
        public string PriceRate { get; set; }
        public string MediaFileUrl { get; set; }
        public string CoverImageUrl { get; set; }
        public string AlbumUID { get; set; }
        public string AlbumName { get; set; }
        public string AlbumDescription { get; set; }       
        public string AlbumCoverImage { get; set; }
    }

    public class EditAlbumModel
    {
        public string UID { get; set; }    
        public string AlbumName { get; set; }
        public string AlbumDescription { get; set; }
        public string Status { get; set; }
        public string CoverImageUrl { get; set; }        
        public HttpPostedFileBase CoverImage { get; set; }
    }

    public class CusomerHistoryModel
    {
        public int Sno { get; set; }
        public string UID { get; set; }
        public string LoginName { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public string Date { get; set; }
        public string Amount { get; set; }
        public string ShippingDetail { get; set; }
        public int ShipCount { get; set; }
    }

    public class ContactModel
    {
        [Required(ErrorMessage = "Login Name Required")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "Email Required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter correct email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Subject Required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message Required")]
        public string Message { get; set; }
    }

    public class EarningModel
    {
        public int Sno { get; set; }
        public string Today { get; set; }
        public string Week { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Total { get; set; }
    }

    public class CountryModel
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    public class UserEventModel
    {
        public int Sno { get; set; }
        public string UID { get; set; }
        public string EventName { get; set; }
        public string Address { get; set; }
        public string Host { get; set; }
        public string Fee { get; set; }
        public string Performance { get; set; }
        public string Date { get; set; }
    }

    public class TalentSearchModel
    {
        public int Sno { get; set; }
        public string LoginName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ProductionSubName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public int Rate { get; set; }
        public string Fan { get; set; }

        public ContactModel Contact { get; set; }
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
        public string SentTime { get; set; }
        public string UserImage { get; set; }

        public ComposeModel Compose { get; set; }
    }

    public class ChatRoomModel
    {      
        public string AdminMsg { get; set; }
        public string Time { get; set; }
        public string UserName { get; set; }
        public string UserMsg { get; set; }
        public string UserImage { get; set; }          
    }

    public class ComposeModel
    {
        [Required(ErrorMessage = "LoginName Required")]
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

    public class PurchaseHistory
    {
        public int Sno { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string OrderNumber { get; set; }
        public string PaidAmount { get; set; }
    }

    public class TicketHistory
    {
        public int Sno { get; set; }
        public string UID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string EventName { get; set; }
        public string PaidAmount { get; set; }
    }

    public class PageModel
    {
        public string PageHtml { get; set; }
    }

    public class MediaPromoterCustomerModel
    {
        public int Sno { get; set; }
        public string EmailID { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string EventName { get; set; }
        public string PaidAmount { get; set; }
        public string SeatQTY { get; set; }
    }

    public class OnlineProductionModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ProductionSubName { get; set; }
        public string ImageUrl { get; set; }
    }

    public class UserRefferalReportModel
    {
        public int Sno { get; set; }
        public string EmailID { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Mobile { get; set; }
        public string Country { get; set; }
    }

    public class UserHeaderModel
    {
        public string Image { get; set; }
        public List<MessageModel> Message { get; set; }
        public List<UserActivityModel> Activity { get; set; }
        public List<UserFanBaseModel> FanBase { get; set; }
    }

    public class UserActivityModel
    {
        public string UID { get; set; }
        public string Reference { get; set; }
        public string Type { get; set; }
        public string ActionDate { get; set; }
    }

    public class UserFanBaseModel
    {
        public string UID { get; set; }
        public string TalentBUID { get; set; }
        public string FanBUID { get; set; }
        public string ImageUrl { get; set; }
        public string FanLoginName { get; set; }
    }

    public class UserUpgradeModel
    {
        public string Role1 { get; set; }
        public string Role2 { get; set; }
        public string Role3 { get; set; }

        public string UID { get; set; }
        public string FUID { get; set; }
        public string hdnRoleName { get; set; }

        public string AT_Fee { get; set; }
        public string PM_Fee { get; set; }
        public string MP_Fee { get; set; }

        [Required(ErrorMessage = "Category Required")]
        public List<SelectListItem> TalentCategory { get; set; }

        [Required(ErrorMessage = "Category Required")]
        public List<SelectListItem> ProductionCategory { get; set; }

        [Required(ErrorMessage = "SubCategory Required")]
        public List<SelectListItem> TalentSubCategory { get; set; }

        [Required(ErrorMessage = "SubCategory Required")]
        public List<SelectListItem> ProductionSubCategory { get; set; }

        [Required(ErrorMessage = "BioDescription Required")]
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

        [Required(ErrorMessage = "Country Required")]
        public List<SelectListItem> FCountry { get; set; }

        [Required(ErrorMessage = "State Required")]
        public List<SelectListItem> FState { get; set; }

        [Required(ErrorMessage = "City Required")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "City Required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Address Required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "ZipCode Required")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "SponsorID Required")]
        public string SponsorID { get; set; }

        public string RecruiterID { get; set; }

        public string College { get; set; }

        public List<SelectListItem> Major { get; set; }

        public string Major_Other { get; set; }

        [Required(ErrorMessage = "TermsCoditions Required")]
        public bool TermsCoditions { get; set; }

        public decimal PayAmount { get; set; }
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
        public string Product { get; set; }
        public string Rate { get; set; }
        public string Quantity { get; set; }
        public string Amount { get; set; }
        public string Tax { get; set; }
        public string NetAmount { get; set; }
    }

    public class GeneologyLevelModel
    {
        public int Sno { get; set; }
        public string LoginName { get; set; }
        public string UserName { get; set; }
        public string RepNo { get; set; }
        public string SponsorLoginName { get; set; }
        public string EnrollmentDate { get; set; }
        public string Shopping { get; set; }
        public int Level { get; set; }
    }

    public class TreeViewNodeVM
    {
        public TreeViewNodeVM()
        {
            ChildNode = new List<TreeViewNodeVM>();
            SubNode = new List<TreeViewNodeVM>();
            SubSubNode = new List<TreeViewNodeVM>();
        }
        public string LoginName { get; set; }
        public string Name { get; set; }
        public IList<TreeViewNodeVM> ChildNode { get; set; }
        public IList<TreeViewNodeVM> SubNode { get; set; }
        public IList<TreeViewNodeVM> SubSubNode { get; set; }
        public int Count { get; set; }
        public string User_LoginName { get; set; }
        public string SpID { get; set; }
        public string SponsorName { get; set; }
        public string Downline { get; set; }
        public string SelfInvestment { get; set; }
        public string GroupInvestment { get; set; }
        public string Level { get; set; }
        public int LevelCount { get; set; }
    }

    public class UserIncomeModel
    {
        public int Sno { get; set; }
        public string UID { get; set; }
        public string LoginName { get; set; }
        public string Date { get; set; }
        public string Amount { get; set; }
        public string Deduction { get; set; }
        public string NetAmount { get; set; }
    }

    public class UserPageModel
    {
        public string PageHtml { get; set; }
    }

    public class AddShippingInfoModel
    {        
        public string hdnUID { get; set; }        
        public string CustomerName { get; set; }
        public string CustomerLoginName { get; set; }
        public string ProductName { get; set; }
        public string PurchaseDate { get; set; }

        [Required(ErrorMessage = "Courier Name Required")]
        public string CourierName { get; set; }

        [Required(ErrorMessage = "Tracking Number Required")]
        public string TrackingNumber { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Only Date Required")]
        [Required(ErrorMessage = "Shipping Date Required")]        
        public string ShippingDate { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Only Date Required")]
        [Required(ErrorMessage = "Expected Delivery Date Required")]
        public string ExpectedDeliveryDate { get; set; }       
    }
    public class BinaryTreeModel
    {
        public List<BinaryTreeModel> BinaryTree { get; set; }
        public string LoginName { get; set; }
        public string CssClass { get; set; }
        public string PackageNo { get; set; }
        public string PopUp { get; set; }
    }
}

