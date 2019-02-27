namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using AllYouMedia.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SubCategory : BaseEntity
    {
        public SubCategory()
        {
            this.Items = new HashSet<Item>();
        }

        public long CategoryID { get; set; }
        public virtual Category Category { get; set; }

        [StringLength(200)]
        public string Name { get; set; }
        public bool IsTalent { get; set; }
        public bool IsProduction { get; set; }

        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Attribute> Attributes { get; set; }
        public virtual ICollection<UserCategoryMap> TalentCategoryMaps { get; set; }
    }
}
