namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using AllYouMedia.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class FanSharingFanRequest : BaseEntity  //////Step 2
    {
        public FanSharingFanRequest()
        {
        }
        /// <summary>
        /// ID of Requesting Member. For example, if user A wants to get fans of user B, then this id is the ID of user A
        /// </summary>
        public long AspNetUserID { get; set; } 
        public virtual AspNetUser AspNetUser { get; set; }
        /// <summary>
        /// ID of Fan, for whome request is sent to. For example, if user A wants to get fans of user B, then this id is the one of ID of Fan of User B
        /// </summary>
        public long FanID { get; set; }
        public virtual Fan Fan { get; set; }

        public bool IsGranted { get; set; }
        public Nullable<DateTime> GrantedOn { get; set; }
    }
}
