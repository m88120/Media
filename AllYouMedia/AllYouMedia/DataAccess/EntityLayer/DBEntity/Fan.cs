namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using AllYouMedia.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Fan : BaseEntity
    {
        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public bool IsEmailConfirmed { get; set; }
        public virtual ICollection<AllYouMedia.DataAccess.EntityLayer.DBEntity.FanUserMap> FanUserMaps { get; set; }
    }
}
