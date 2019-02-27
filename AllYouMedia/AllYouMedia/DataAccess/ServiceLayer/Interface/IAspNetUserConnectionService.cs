namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System.Collections.Generic;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.Models;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System;

    public interface IAspNetUserConnectionService
    {
        List<AspNetUserConnection> GetAll();
        AspNetUserConnection GetById(long id);

        void Insert(AspNetUserConnection model);

        void Update(AspNetUserConnection model);

        void Delete(AspNetUserConnection model);
        AspNetUserConnection GetConnectedUser(long AspNetUserID, long ConnectedAspNetUserID);

        Tuple<dynamic, int> GetForDT(long AspNetUserID, string search, int start, int length);
    }
}