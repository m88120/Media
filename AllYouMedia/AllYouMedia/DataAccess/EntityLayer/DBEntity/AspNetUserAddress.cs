namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using AllYouMedia.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class AspNetUserAddress : BaseEntity
    {
        public AspNetUserAddress()
        {
            this.Orders = new HashSet<Order>();
        }

        public long AspNetUserID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        [StringLength(200)]
        public string City { get; set; }

        [StringLength(200)]
        public string Province { get; set; }

        [StringLength(200)]
        public string Country { get; set; }

        [StringLength(20)]
        public string PinCode { get; set; }

        public string Landmark { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
