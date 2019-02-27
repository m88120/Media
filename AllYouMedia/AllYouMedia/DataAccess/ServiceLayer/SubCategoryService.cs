namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;

    public class SubCategoryService : ISubCategoryService
    {
        private IRepository<SubCategory> entityRepository;

        public SubCategoryService(IRepository<SubCategory> entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        public List<SubCategory> GetAll()
        {
            return this.entityRepository.GetAll().ToList();
        }

        public SubCategory GetById(long id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.entityRepository.GetById(id);
        }

        public void Insert(SubCategory model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Insert(model);
        }

        public void Update(SubCategory model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Update(model);
        }

        public void Delete(SubCategory model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Delete(model);
        }

        public IEnumerable<System.Web.Mvc.SelectListItem> SubCategoryCbo(bool IsProduction)
        {
            return this.entityRepository.GetByQuery(x => IsProduction == false || x.IsProduction).Select(x => new System.Web.Mvc.SelectListItem { Text = x.CBOExpression, Value = x.ID.ToString() }).OrderBy(x => x.Text);
        }

        public Tuple<List<SubCategory>, int> GetForDT(string search, int start, int length)
        {
            var queriable = this.entityRepository.GetByQuery(x => (x.Name.Contains(search) || x.Category.Name.Contains(search)) && x.ID != -1);
            int totalRecord = queriable.Count();
            return new Tuple<List<SubCategory>, int>(queriable.ToList(), totalRecord);
        }

        public List<SubCategory> GetSimilarSubcategoryBySubCategoryID(long SubCategoryID)
        {
            var categoryID = this.GetById(SubCategoryID).CategoryID;
            return this.entityRepository.GetByQuery(x => x.CategoryID == categoryID && x.IsActive).ToList();
        }
        public long GetFirstSubCategoryIDByCategoryTypeID(long CategoryTypeID)
        {
            long SubCategoryID = -1;
            var subCat = this.entityRepository.GetByQuery(x => x.IsActive && x.Category.CategoryTypeID == CategoryTypeID).FirstOrDefault();
            if (subCat != null)
                SubCategoryID = subCat.ID;
            return SubCategoryID;
        }
        public long GetFirstSubCategory()
        {
            long SubCategoryID = -1;
            var subCat = this.entityRepository.GetByQuery(x => x.IsActive && x.ID != -1).OrderBy(x => x.Category.CBOExpression).FirstOrDefault();
            if (subCat != null)
                SubCategoryID = subCat.ID;
            return SubCategoryID;
        }

        public List<System.Web.Mvc.SelectListItem> SubCategoryCboByCategoryTypeMembershipType(int MembershipType, long CategoryID)
        {
            if (MembershipType == 2)
                return this.entityRepository.GetByQuery(x => x.IsTalent == true && x.CategoryID == CategoryID).Select(x => new System.Web.Mvc.SelectListItem { Text = x.Name, Value = x.ID.ToString() }).OrderBy(x => x.Text).ToList();
            else
                return this.entityRepository.GetByQuery(x => x.IsProduction == true && x.CategoryID == CategoryID).Select(x => new System.Web.Mvc.SelectListItem { Text = x.Name, Value = x.ID.ToString() }).OrderBy(x => x.Text).ToList();
        }
    }
}