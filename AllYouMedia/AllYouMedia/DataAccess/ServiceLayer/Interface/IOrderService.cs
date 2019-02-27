namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System.Collections.Generic;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.Models;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System;

    public interface IOrderService
    {
        List<Order> GetAll();
        Order GetById(long id);

        void Insert(Order model);

        void Update(Order model);

        void Delete(Order model);
        Tuple<bool, string, long> ValidateAndPlaceOrder(AllYouMedia.Controllers.HomeController.SessionCart cart, long AspNetUserID, long AspNetUserAddressID);
        AspNetUserAddress GetShippingAddressByOrderID(long OrderID);
        Tuple<List<Order>, int> GetForDT(long AspNetUserID, string search, int start, int length);

        Tuple<List<OrderItem>, int> GetSoldItemForDT(long AspNetUserID, string search, int start, int length);
    }
}