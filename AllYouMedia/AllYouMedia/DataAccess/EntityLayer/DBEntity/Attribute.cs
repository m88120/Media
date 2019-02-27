namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Attribute : BaseEntity
    {
        public Attribute()
        {
            this.ItemAttribute = new HashSet<ItemAttribute>();
        }

        [StringLength(200)]
        public string Name { get; set; }

        public long SubCategoryID { get; set; }
        public virtual SubCategory SubCategory { get; set; }

        public virtual ICollection<ItemAttribute> ItemAttribute { get; set; }
        public virtual ICollection<UserCategoryMap> TalentCategoryMaps { get; set; }
    }
}
