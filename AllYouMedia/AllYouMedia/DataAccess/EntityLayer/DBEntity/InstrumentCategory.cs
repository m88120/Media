using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    public class InstrumentCategory : BaseEntity
    {
        public InstrumentCategory()
        {
            this.GenreCategory = new HashSet<GenreCategory>();
          

        }

        [StringLength(200)]
        public string Name { get; set; }
        public virtual ICollection<GenreCategory> GenreCategory { get; set; }
      

    }
}