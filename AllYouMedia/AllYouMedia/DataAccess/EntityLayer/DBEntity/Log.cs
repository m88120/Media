namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using AllYouMedia.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Log : BaseEntity
    {
        public long AspNetUserID { get; set; }
        [StringLength(200)]
        public string ActivityType { get; set; }
        public string IPAddress { get; set; }
        public string Description { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
