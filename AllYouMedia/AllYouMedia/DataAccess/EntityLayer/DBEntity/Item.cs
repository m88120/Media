namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using AllYouMedia.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Item : BaseEntity
    {
        public Item()
        {
            this.CartItem = new HashSet<CartItem>();
            this.ItemAttribute = new HashSet<ItemAttribute>();
            this.OrderItem = new HashSet<OrderItem>();
        }

        public long AspNetUserID { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public long SubCategoryID { get; set; }
        public virtual SubCategory SubCategory { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(2000)]
        public string PhotoIMG { get; set; }

        public decimal BasePrice { get; set; }
        public decimal SellPrice { get; set; }
        public string ContentURL { get; set; }
        public bool IsStockApplicable { get; set; }
        public int StockQty { get; set; }
        public int MinPurchaseQty { get; set; }
        public int MaxPurchaseQty { get; set; }

        [StringLength(200)]
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }

        public bool IsPromoted { get; set; }
        public bool IsFeatured { get; set; }
        public virtual ICollection<CartItem> CartItem { get; set; }
        public virtual ICollection<ItemAttribute> ItemAttribute { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
