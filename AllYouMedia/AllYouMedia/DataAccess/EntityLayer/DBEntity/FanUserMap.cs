namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using AllYouMedia.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class FanUserMap : BaseEntity
    {
        public long AspNetUserID { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public long FanID { get; set; }
        public virtual Fan Fan { get; set; }
        public int Rating { get; set; }
    }
}
