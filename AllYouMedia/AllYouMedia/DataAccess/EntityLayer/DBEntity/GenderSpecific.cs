using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    public class GenderSpecific : BaseEntity
    {
        public GenderSpecific()
        {
            this.Categories = new HashSet<Category>();
            this.GenreCategory = new HashSet<GenreCategory>();
        }

        [StringLength(200)]
        public string Name { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<GenreCategory> GenreCategory { get; set; }

    }
}