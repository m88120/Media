namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using AllYouMedia.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class UserSpotlight : BaseEntity
    {
        public UserSpotlight()
        {
        }

        public long AspNetUserID { get; set; }
        [ForeignKey("AspNetUserID")]
        public virtual AspNetUser AspNetUser { get; set; }

        public long ReviewingAspNetUserID { get; set; }
        [ForeignKey("ReviewingAspNetUserID")]
        public virtual AspNetUser ReviewingAspNetUser { get; set; }

        public int Spotlight { get; set; }
    }
}
