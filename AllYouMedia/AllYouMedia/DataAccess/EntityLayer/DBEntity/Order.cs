namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Order : BaseEntity
    {
        public Order()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }

        public long AspNetUserID { get; set; }
        public virtual AllYouMedia.Models.AspNetUser AspNetUser { get; set; }

        [StringLength(200)]
        public string PaymentMode { get; set; }

        [StringLength(200)]
        public string PayerRefCode { get; set; }

        [StringLength(200)]
        public string PaymentRefCode { get; set; }

        public decimal NetAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal Shipping { get; set; }
        public decimal Tax { get; set; }
        public decimal PayableAmount { get; set; }
        public Nullable<System.DateTime> ShippingDate { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsPayoutCreated { get; set; }
        public long AspNetUserAddressID { get; set; }
        public virtual AspNetUserAddress AspNetUserAddress { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
