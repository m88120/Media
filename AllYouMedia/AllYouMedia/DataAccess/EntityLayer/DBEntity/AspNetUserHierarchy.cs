namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using AllYouMedia.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class AspNetUserHierarchy : BaseEntity
    {
        public long AspNetUserID { get; set; }
        [ForeignKey("AspNetUserID")]
        public virtual AspNetUser AspNetUser { get; set; }

        public long ParentAspNetUserID { get; set; }
        [ForeignKey("ParentAspNetUserID")]
        public virtual AspNetUser ParentAspNetUser { get; set; }

    }
}
