namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class UserLevel : BaseEntity
    {
        public UserLevel()
        {
            this.AspNetUsers = new HashSet<AllYouMedia.Models.AspNetUser>();
        }

        [StringLength(200)]
        public string Name { get; set; }

        public virtual ICollection<AllYouMedia.Models.AspNetUser> AspNetUsers { get; set; }
    }
}
