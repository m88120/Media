namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System.Collections.Generic;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.Models;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System;

    public interface ICategoryTypeService
    {
        List<CategoryType> GetAll();
        CategoryType GetById(long id);

        void Insert(CategoryType model);

        void Update(CategoryType model);

        void Delete(CategoryType model);
        List<System.Web.Mvc.SelectListItem> CategoryTypeCbo();
        List<CategoryType> GetAllActiveCatDataForMenu(int isProduction);
        List<System.Web.Mvc.SelectListItem> CategoryTypeCboByMembershipType(int MembershipType);
        List<System.Web.Mvc.SelectListItem> CategoryTypeCboByMembershipTypeOrderBy(int MembershipType);
    }
}