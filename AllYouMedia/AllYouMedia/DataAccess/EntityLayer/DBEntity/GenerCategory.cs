using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    public class GenreCategory : BaseEntity
    {
        public GenreCategory()
        {
            this.Categories = new HashSet<Category>();
            this.genderSpecific = new HashSet<GenderSpecific>();
            this.InstrumentCategory = new HashSet<InstrumentCategory>();


        }
     
        [StringLength(200)]
        public string Name { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<GenderSpecific> genderSpecific { get; set; }
        public virtual ICollection<InstrumentCategory> InstrumentCategory { get; set; }

    }
}