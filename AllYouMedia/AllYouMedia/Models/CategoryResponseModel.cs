using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AllYouMedia.Models
{
    public class CategoryResponseModel : System.Web.Mvc.SelectListItem
    {
        public List<System.Web.Mvc.SelectListItem> genderSpecfic {get; set;}
    }
}