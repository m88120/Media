using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AllYouMedia.DataAccess.EntityLayer
{
    public interface IBaseEntity
    {
        long ID { get; set; }
        bool IsActive { get; set; }
        string CBOExpression { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime? ModifiedOn { get; set; }
    }
}