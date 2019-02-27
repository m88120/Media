namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Category : BaseEntity
    {
        public Category()
        {
            this.SubCategories = new HashSet<SubCategory>();
            this.GenderSpecific = new HashSet<GenderSpecific>();
            this.GenreCategory = new HashSet<GenreCategory>();
        }
        public long CategoryTypeID { get; set; }
        public virtual CategoryType CategoryType { get; set; }
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(2000)]
        public string PhotoIMG { get; set; }
        public bool IsTalent { get; set; }
        public bool IsProduction { get; set; }
        public virtual ICollection<SubCategory> SubCategories { get; set; }
        public virtual ICollection<UserCategoryMap> TalentCategoryMaps { get; set; }
        public virtual ICollection<GenderSpecific> GenderSpecific { get; set; }
        public virtual ICollection<GenreCategory> GenreCategory { get; set; }

    }
}
