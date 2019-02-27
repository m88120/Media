namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System.Collections.Generic;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.Models;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System;

    public interface IAspNetUserAddressService
    {
        List<AspNetUserAddress> GetAll();
        AspNetUserAddress GetById(long id);

        void Insert(AspNetUserAddress model);

        void Update(AspNetUserAddress model);

        void Delete(AspNetUserAddress model);
        IEnumerable<System.Web.Mvc.SelectListItem> AspNetUserAddressCbo(long AspNetUserID);
        Tuple<List<AspNetUserAddress>, int> GetForDT(long AspNetUserID, string search, int start, int length);
    }
}