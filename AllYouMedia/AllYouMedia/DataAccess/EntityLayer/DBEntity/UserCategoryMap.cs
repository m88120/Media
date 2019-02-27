namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class UserCategoryMap : BaseEntity
    {
        public UserCategoryMap()
        {
        }
        public long AspNetUserRoleID { get; set; }
        public virtual AllYouMedia.Models.AspNetUserRole AspNetUserRole { get; set; }

        public long CategoryTypeID { get; set; }
        public virtual CategoryType CategoryType { get; set; }

        public long CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public long GenderID { get; set; }
        public virtual GenderSpecific Gender { get; set; }

        public long GenreID { get; set; }
        public virtual GenreCategory Genre { get; set; }

        public long InstrumentID { get; set; }
        public virtual InstrumentCategory Instrument { get; set; }
        public long InstrumentSpeciID { get; set; }
        public virtual InstrumentSpecificationCategory InstrumentSpecification { get; set; }
        public long SubCategoryID { get; set; }
        public virtual SubCategory SubCategory { get; set; }

        public long AttributeID { get; set; }
        public virtual Attribute Attribute { get; set; }
    }
}
