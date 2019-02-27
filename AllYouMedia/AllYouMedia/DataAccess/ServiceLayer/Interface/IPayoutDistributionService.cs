namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System.Collections.Generic;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.Models;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System;

    public interface IPayoutDistributionService
    {
        List<PayoutDistribution> GetAll();
        PayoutDistribution GetById(long id);

        void Insert(PayoutDistribution model);

        void Update(PayoutDistribution model);

        void Delete(PayoutDistribution model);
        Tuple<List<PayoutDistribution>, int> GetForDT(long AspNetUserID);
        void CreatePayoutForOrder(long OrderID);
    }
}