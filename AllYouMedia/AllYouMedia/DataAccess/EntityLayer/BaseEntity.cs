using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AllYouMedia.DataAccess.EntityLayer
{
    public class BaseEntity : IBaseEntity
    {
        public long ID { get; set; }
        public bool IsActive { get; set; }

        [StringLength(500), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string CBOExpression { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}