namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System.Collections.Generic;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.Models;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System;

    public interface IUserSpotlightService
    {
        List<UserSpotlight> GetAll();

        UserSpotlight GetById(long id);

        void Insert(UserSpotlight model);

        void Update(UserSpotlight model);

        void Delete(UserSpotlight model);
        double GetUserSpotlightForUser(long AspNetUserID);

        void SaveSpotlight(long RatedAspNetUserID, int Rating, long RaterAspNetUserID);
    }
}