namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;

    public class ItemService : IItemService
    {
        private IRepository<Item> entityRepository;

        public ItemService(IRepository<Item> entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        public List<Item> GetAll()
        {
            return this.entityRepository.GetAll().ToList();
        }

        public Item GetById(long id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.entityRepository.GetById(id);
        }

        public void Insert(Item model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Insert(model);
        }

        public void Update(Item model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Update(model);
        }

        public void Delete(Item model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Delete(model);
        }

        public IEnumerable<System.Web.Mvc.SelectListItem> ItemCbo()
        {
            return this.entityRepository.GetAll().Select(x => new System.Web.Mvc.SelectListItem { Text = x.Name, Value = x.ID.ToString() }).OrderBy(x => x.Text);
        }

        public Tuple<List<Item>, int> GetForDT(long AspNetUserID, string search, int start, int length)
        {
            var queriable = this.entityRepository.GetByQuery(x => (x.Name.Contains(search) || x.SubCategory.Name.Contains(search)) && x.ID != -1 && x.AspNetUserID == AspNetUserID);
            int totalRecord = queriable.Count();
            return new Tuple<List<Item>, int>(queriable.ToList(), totalRecord);
        }

        public List<Item> GetBySubCategoryID(long SubCategoryID)
        {
            return this.entityRepository.GetByQuery(x => x.SubCategoryID == SubCategoryID && x.IsActive).ToList();
        }
        public List<Item> GetFeaturedItems()
        {
            return this.entityRepository.GetByQuery(x => x.IsFeatured && x.IsActive).ToList();
        }
        public List<Item> GetPromotedItems()
        {
            return this.entityRepository.GetByQuery(x => x.IsPromoted && x.IsActive).ToList();
        }
        public List<Item> GetSimilarItems(long ItemID)
        {
            var categoryID = this.entityRepository.GetById(ItemID).SubCategory.CategoryID;
            var guid = Guid.NewGuid().ToString();
            return this.entityRepository.GetByQuery(x => x.IsActive && x.SubCategory.CategoryID == categoryID && x.ID != ItemID).OrderBy(x => guid).Take(4).ToList();
        }
    }
}