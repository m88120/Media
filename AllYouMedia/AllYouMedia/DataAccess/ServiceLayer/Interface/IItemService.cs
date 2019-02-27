namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System.Collections.Generic;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.Models;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System;

    public interface IItemService
    {
        List<Item> GetAll();
        Item GetById(long id);

        void Insert(Item model);

        void Update(Item model);

        void Delete(Item model);
        IEnumerable<System.Web.Mvc.SelectListItem> ItemCbo();

        Tuple<List<Item>, int> GetForDT(long AspNetUserID, string search, int start, int length);
        List<Item> GetBySubCategoryID(long SubCategoryID);

        List<Item> GetFeaturedItems();

        List<Item> GetPromotedItems();
        List<Item> GetSimilarItems(long ItemID);
    }
}