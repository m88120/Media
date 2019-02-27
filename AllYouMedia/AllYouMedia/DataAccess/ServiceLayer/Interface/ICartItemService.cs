namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System.Collections.Generic;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.Models;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System;

    public interface ICartItemService
    {
        List<CartItem> GetAll();
        CartItem GetById(long id);

        void Insert(CartItem model);

        void Update(CartItem model);

        void Delete(CartItem model);
        List<CartItem> GetByCartID(long CartID);
    }
}