namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using AllYouMedia.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class AspNetUserConnection : BaseEntity
    {
        public AspNetUserConnection()
        {
        }

        public long AspNetUserID { get; set; }
        [ForeignKey("AspNetUserID")]
        public virtual AspNetUser AspNetUser { get; set; }

        public long ConnectedAspNetUserID { get; set; }
        [ForeignKey("ConnectedAspNetUserID")]
        public virtual AspNetUser ConnectedAspNetUser { get; set; }
    }
}
