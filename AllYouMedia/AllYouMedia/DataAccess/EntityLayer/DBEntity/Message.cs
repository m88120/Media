namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using AllYouMedia.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Message : BaseEntity
    {
        public long FromAspNetUserID { get; set; }
        public long ToAspNetUserID { get; set; }

        [StringLength(200)]
        public string Subject { get; set; }
        public string Body { get; set; }
        public int Status { get; set; }
        public bool IsDeleted { get; set; }

        public virtual AspNetUser FromAspNetUser { get; set; }
        public virtual AspNetUser ToAspNetUser { get; set; }
    }
}
