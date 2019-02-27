namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System.Collections.Generic;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.Models;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System;

    public interface IAttributeService
    {
        List<AllYouMedia.DataAccess.EntityLayer.DBEntity.Attribute> GetAll();
        AllYouMedia.DataAccess.EntityLayer.DBEntity.Attribute GetById(long id);

        void Insert(AllYouMedia.DataAccess.EntityLayer.DBEntity.Attribute model);

        void Update(AllYouMedia.DataAccess.EntityLayer.DBEntity.Attribute model);

        void Delete(AllYouMedia.DataAccess.EntityLayer.DBEntity.Attribute model);

        List<System.Web.Mvc.SelectListItem> AttributeCboBySubCategoryMembershipType(long SubCategoryID);
    }
}