namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using System;
    using System.Collections.Generic;

    public partial class ItemAttribute : BaseEntity
    {
        public long ItemID { get; set; }
        public long AttributeID { get; set; }

        public virtual Attribute Attribute { get; set; }
        public virtual Item Item { get; set; }
    }
}
