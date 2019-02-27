using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    public class InstrumentSpecificationCategory : BaseEntity
    {
        public InstrumentSpecificationCategory()
        {

        }

        [StringLength(200)]
        public string Name { get; set; }
        public long InstrumentCategoryId { get; set; }
        public virtual InstrumentCategory InstrumentCategory { get; set; }
    }
}