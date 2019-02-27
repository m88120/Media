namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using AllYouMedia.Models;
    using System;
    using System.Collections.Generic;

    public partial class Cart : BaseEntity
    {
        public Cart()
        {
            this.CartItems = new HashSet<CartItem>();
        }

        public long AspNetUserID { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
