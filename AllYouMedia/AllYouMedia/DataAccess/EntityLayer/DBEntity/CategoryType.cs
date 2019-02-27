namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class CategoryType : BaseEntity
    {
        public CategoryType()
        {
            this.Categories = new HashSet<Category>();
        }

        [StringLength(200)]
        public string Name { get; set; }

        public bool IsTalent { get; set; }
        public bool IsProduction { get; set; }
        public bool IsMembershipCategory { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<UserCategoryMap> TalentCategoryMaps { get; set; }
    }
}
