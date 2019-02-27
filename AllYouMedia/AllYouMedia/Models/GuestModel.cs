using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AllYouMedia.Models
{
    public class IndexModel
    {
        public List<MenuModel> Menu { get; set; }
        public List<SubMenuModel> SubMenu { get; set; }
        public List<ProductModel> Product { get; set; }
        public ProductDetailModel ProductDetail { get; set; }
        public List<CartModel> Cart { get; set; }
        public List<AlbumModel> AlbumList { get; set; }
        public List<AlbumModel> SongList { get; set; }
    }

    public class ProductModel
    {
        public string UID { get; set; }
        public string Code { get; set; }
        public string MediaURL { get; set; }
        public string Name { get; set; }
        public string RetailPrice { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string CategoryNAME { get; set; }
    }
    public class ProductDetailModel
    {
        public string UID { get; set; }
        public string Code { get; set; }
        public string MediaURL { get; set; }
        public string Name { get; set; }
        public string RetailPrice { get; set; }
        public string CategoryNAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string ShippingCharge { get; set; }
        public string Type { get; set; }

        [MaxLength(2)]
        [Required(ErrorMessage = "Quantity Required")]
        [RegularExpression("([1-9][1-9]*)", ErrorMessage = "Quantity must be a natural number")]
        public string Quantity { get; set; }
    }

    public class MenuModel
    {
        public string UID { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
    }
    public class SubMenuModel
    {
        public string UID { get; set; }
        public string Name { get; set; }
        public string MenuUID { get; set; }
        public string AlbumUID { get; set; }
        public string AlbumName { get; set; }
    }

    public class AlbumModel
    {
        public int Sno { get; set; }
        public string UID { get; set; }
        public string Name { get; set; }
        public string FileUrl { get; set; }
        public decimal Length { get; set; }                 
        public string Price { get; set; }       
        public string AlbumUID { get; set; }
    }

    public class CartModel
    {
        public int SrNo { get; set; }
        public string CartUID { get; set; }
        public string itemUID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string IMAGE { get; set; }
        public string ItemType1 { get; set; }
        public string ItemType2 { get; set; }
        public decimal Shipping { get; set; }
        public decimal Rate { get; set; }
        public string QTY { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public decimal Amount { get; set; }
        public decimal Tax { get; set; }
        public decimal NetAmount { get; set; }
    }
    public class EventModel
    {
        public string hdnReff { get; set; }
        public List<EventListModel> EventList { get; set; }
    }

    public class EventListModel
    {
        public string UID { get; set; }
        public string EventName { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string ImageURL { get; set; }
        public string Time { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Fee { get; set; }
        public string Remark { get; set; }
    }

    public class EventInvoiceModel
    {
        public string UID { get; set; }
        public string Name { get; set; }
        public string ADDRESS { get; set; }
        public string ADDRESS2 { get; set; }
        public string PinCode { get; set; }
        public string EMAIL { get; set; }
        public string InvoiceNo { get; set; }
        public string EventNAME { get; set; }
        public string EventADDRESS { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Rate { get; set; }
        public string Quantity { get; set; }
        public string Amount { get; set; }
        public string AmountWord { get; set; }
    }

    public class EventDetailModel
    {
        public string UID { get; set; }
        public string Host { get; set; }
        public string Venue { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string DateFrom { get; set; }
        public string Time { get; set; }
        public string MapUrl { get; set; }
        public string Remark { get; set; }
        public string EventImage { get; set; }
        public string Fee { get; set; }
        public string hdnReff { get; set; }

        public List<PerformerDetailModel> PerformerDetail { get; set; }
        public PerformerModel Performer { get; set; }
    }

    public class PerformerDetailModel
    {
        public string UID { get; set; }
        public string PerformerImage { get; set; }
        public string PerformarName { get; set; }
        public string PerformarCategory { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string PerformarDescription { get; set; }
    }

    public class PerformerModel
    {
        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "valid Email Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Required")]
        [Phone(ErrorMessage = "valid Phone Required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Category Required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "TimeFrom Required")]
        public string TimeFrom { get; set; }

        [Required(ErrorMessage = "TimeTo Required")]
        public string TimeTo { get; set; }

        [Required(ErrorMessage = "Message Required")]
        public string Message { get; set; }
    }

    public class EventCartModel
    {
        public int SrNo { get; set; }
        public string UID { get; set; }
        public string EventName { get; set; }
        public string Fee { get; set; }
        public string Amount { get; set; }
        public string Qty { get; set; }
        public string EventURL { get; set; }
        public string hdnReff { get; set; }
    }

    public class CheckoutModel
    {
        public string hdnReff { get; set; }
        public string hdnUID { get; set; }
        public string hdnPUID { get; set; }
        public string Fee { get; set; }
        public string hdnFee { get; set; }
        public string hdnAmount { get; set; }

        [Required(ErrorMessage = "Select Payment Option")]
        public string PaymentOption { get; set; }

        public bool User { get; set; }

        [Required(ErrorMessage = "Login Name Required")]
        [Display(Name = "* Login Name")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        [Display(Name = "* Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        [EmailAddress(ErrorMessage = "valid Email Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Enter State")]
        public string State { get; set; }

        [Required(ErrorMessage = "Enter Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Enter ZipCode")]
        public string Zip { get; set; }
    }

    public class PaymentProcessModel
    {
        public string hdnUID { get; set; }
        public string Fee { get; set; }
        public decimal hdnFee { get; set; }
       
        [Required(ErrorMessage = "Card Number Required")]
        [Display(Name = "* Card Number")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Card Holders Name Required")]      
        [Display(Name = "* Card Holders Name")]
        public string CardHoldersName { get; set; }

        [Required(ErrorMessage = "Expiry Date Required")]       
        [Display(Name = "* Expiry Date")]
        public string ExpiryDate { get; set; }

        [Required(ErrorMessage = "CVV Required")]
        [Display(Name = "* CVV")]
        public string CVV { get; set; }
    }

    public class ContactUsModel
    {
        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email Required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter correct email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Subject Required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message Required")]
        public string Message { get; set; }

        public string PageHtml { get; set; }
    }

    public class GuestPageModel
    {
        public string PageHtml { get; set; }
    }

    public class ShopInvoiceModel
    {
        public string UID { get; set; }
        public string Name { get; set; }
        public string ProductName { get; set; }
        public string ADDRESS { get; set; }
        public string ADDRESS2 { get; set; }
        public string PinCode { get; set; }
        public string EMAIL { get; set; }              
        public string Date { get; set; }        
        public string Rate { get; set; }
        public string Quantity { get; set; }
        public string Amount { get; set; }
        public string AmountWord { get; set; }
    }
}