namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System.Collections.Generic;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.Models;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System;

    public interface ISubCategoryService
    {
        List<SubCategory> GetAll();
        SubCategory GetById(long id);

        void Insert(SubCategory model);

        void Update(SubCategory model);

        void Delete(SubCategory model);
        IEnumerable<System.Web.Mvc.SelectListItem> SubCategoryCbo(bool IsProduction);

        Tuple<List<SubCategory>, int> GetForDT(string search, int start, int length);

        List<SubCategory> GetSimilarSubcategoryBySubCategoryID(long SubCategoryID);

        long GetFirstSubCategoryIDByCategoryTypeID(long CT);

        long GetFirstSubCategory();

        List<System.Web.Mvc.SelectListItem> SubCategoryCboByCategoryTypeMembershipType(int MembershipType, long CategoryID);
    }
}