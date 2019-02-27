namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System.Collections.Generic;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.Models;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System;

    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetById(long id);

        void Insert(Category model);

        void Update(Category model);

        void Delete(Category model);
        IEnumerable<System.Web.Mvc.SelectListItem> CategoryCbo();

        Tuple<List<Category>, int> GetForDT(string search, int start, int length);

        List<System.Web.Mvc.SelectListItem> CategoryCboByCategoryTypeMembershipType(int MembershipType, long CategoryTypeID);
        List<System.Web.Mvc.SelectListItem> GenderSpecific(int MembershipType, long CategoryID);
        List<System.Web.Mvc.SelectListItem> GetGenreCategory(int MembershipType, long CategoryID);

    }
}