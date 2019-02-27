using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AllYouMedia.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Email"), Required, DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Display(Name = "Password"), Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        public string ReturnURL { get; set; }
        public int MembershipType { get; set; }

        [Display(Name = "Email"), Required, DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Display(Name = "Password"), Required]
        public string Password { get; set; }

        [Display(Name = "Confirm Password"), Required, System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Full Name"), Required]
        public string Name { get; set; }

        [Display(Name = "Mobile"), Required, DataType(DataType.EmailAddress)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Special/ Discount Code")]
        public string RefferCode { get; set; }

        public long CategoryTypeID { get; set; }
        public long CategoryID { get; set; }
        public long SubCategoryID { get; set; }
        public long AttributeID { get; set; }
    }

    public class ForgotPaasWordModel
    {
        [Required(ErrorMessage = "Login Name Required")]
        public string LoginName { get; set; }

        public string Code { get; set; }

        [Required(ErrorMessage = "New Password Required")]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Required")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "New password and confirmation does not match.")]
        public string ConfirmPassword { get; set; }
    }
}
