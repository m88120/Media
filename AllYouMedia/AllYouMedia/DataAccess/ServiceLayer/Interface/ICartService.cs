namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System.Collections.Generic;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.Models;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System;

    public interface ICartService
    {
        List<Cart> GetAll();
        Cart GetById(long id);

        void Insert(Cart model);

        void Update(Cart model);

        void Delete(Cart model);
        void AddToCart(long AspNetUserID, long ItemID, int Qty);
        Cart GetCartByAspNetUserID(long AspNetUserID);
    }
}