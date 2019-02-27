namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using System;
    using System.Collections.Generic;

    public partial class CartItem : BaseEntity
    {
        public long CartID { get; set; }
        public long ItemID { get; set; }
        public int Qty { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual Item Item { get; set; }
    }
}
