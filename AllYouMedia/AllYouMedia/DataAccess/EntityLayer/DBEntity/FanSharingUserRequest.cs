namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using AllYouMedia.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class FanSharingUserRequest : BaseEntity  //////Step 1
    {
        public FanSharingUserRequest()
        {
        }
        /// <summary>
        /// ID of user who already have Fans. For example, if user A wants to get fans of user B, then this id is the ID of user B
        /// </summary>
        public long AspNetUserID { get; set; }
        [ForeignKey("AspNetUserID")]
        public virtual AspNetUser AspNetUser { get; set; }

        /// <summary>
        /// ID of user who want to get Fans. For example, if user A wants to get fans of user B, then this id is the ID of user A
        /// </summary>
        public long RequestingAspNetUserID { get; set; }
        [ForeignKey("RequestingAspNetUserID")]
        public virtual AspNetUser RequestingAspNetUser { get; set; }

        public bool IsGranted { get; set; }
        public Nullable<DateTime> GrantedOn { get; set; }
    }
}
