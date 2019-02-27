namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System.Collections.Generic;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.Models;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System;

    public interface IUserCategoryMapService
    {
        List<UserCategoryMap> GetAll();
        UserCategoryMap GetById(long id);

        void Insert(UserCategoryMap model);

        void Update(UserCategoryMap model);

        void Delete(UserCategoryMap model);

        List<UserCategoryMap> GetByAspNetUserID(long AspNetUserID);
    }
}