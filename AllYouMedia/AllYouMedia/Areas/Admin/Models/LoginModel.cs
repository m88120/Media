using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AllYouMedia.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter login id!")]
        public string LoginID { get; set; }

        [Required(ErrorMessage = "Enter Password!")]
        public string LoginPassword { get; set; }

        public bool RememberMe { get; set; }
    }

    
}