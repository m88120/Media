namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using System;
    using System.Collections.Generic;

    public partial class OrderItem : BaseEntity
    {
        public long OrderID { get; set; }
        public long ItemID { get; set; }
        public int Qty { get; set; }
        public decimal BasePrice { get; set; }
        public decimal SellPrice { get; set; }
        public decimal LineAmount { get; set; }
        public decimal LineDiscount { get; set; }
        public decimal Tax { get; set; }
        public Nullable<System.DateTime> ShippingDate { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public bool IsCompleted { get; set; }
        public virtual Item Item { get; set; }
        public virtual Order Order { get; set; }
    }
}
