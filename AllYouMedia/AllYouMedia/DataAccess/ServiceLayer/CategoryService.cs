namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using AllYouMedia.Models;

    public class CategoryService : ICategoryService
    {
        private IRepository<Category> entityRepository;

        public CategoryService(IRepository<Category> entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        public List<Category> GetAll()
        {
            return this.entityRepository.GetAll().ToList();
        }

        public Category GetById(long id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.entityRepository.GetById(id);
        }

        public void Insert(Category model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Insert(model);
        }

        public void Update(Category model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Update(model);
        }

        public void Delete(Category model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Delete(model);
        }

        public IEnumerable<System.Web.Mvc.SelectListItem> CategoryCbo()
        {
            return this.entityRepository.GetAll().Select(x => new System.Web.Mvc.SelectListItem { Text = x.CBOExpression, Value = x.ID.ToString() }).OrderBy(x => x.Text);
        }

        public Tuple<List<Category>, int> GetForDT(string search, int start, int length)
        {
            var queriable = this.entityRepository.GetByQuery(x => (x.Name.Contains(search) || x.CategoryType.Name.Contains(search)) && x.ID != -1);
            int totalRecord = queriable.Count();
            return new Tuple<List<Category>, int>(queriable.ToList(), totalRecord);
        }
        public List<System.Web.Mvc.SelectListItem> CategoryCboByCategoryTypeMembershipType(int MembershipType, long CategoryTypeID)
        {
            if (MembershipType == 2)
            {

                return this.entityRepository.GetByQuery(x => x.IsTalent == true && x.CategoryTypeID == CategoryTypeID).Select(x => new System.Web.Mvc.SelectListItem { Text = x.Name, Value = x.ID.ToString() }).OrderBy(x => x.Text).ToList();
            }
            else
                return this.entityRepository.GetByQuery(x => x.IsProduction == true && x.CategoryTypeID == CategoryTypeID).Select(x => new System.Web.Mvc.SelectListItem { Text = x.Name, Value = x.ID.ToString() }).OrderBy(x => x.Text).ToList();
        }
        public List<System.Web.Mvc.SelectListItem> GenderSpecific(int MembershipType, long CategoryID)
        {
            // if (MembershipType == 2)
            // {

            var data = this.entityRepository.GetByQuery(x => x.IsTalent == true && x.ID == CategoryID).Select(x => x.GenderSpecific).FirstOrDefault();
            if (data != null)
                return data.Select(x => new System.Web.Mvc.SelectListItem { Text = x.Name, Value = x.ID.ToString() }).OrderBy(x => x.Value).ToList();
            else
                return null;
            //this.entityRepository.GetByQuery(x => x.IsTalent == true && x.CategoryTypeID == CategoryID).Select(x => new System.Web.Mvc.SelectListItem { Text = x.GenderSpecific., Value = x.ID.ToString() }).OrderBy(x => x.Text).ToList();
            //}
            // else
            //   return this.entityRepository.GetByQuery(x => x.IsProduction == true && x.CategoryTypeID == CategoryID).Select(x => new System.Web.Mvc.SelectListItem { Text = x.Name, Value = x.ID.ToString() }).OrderBy(x => x.Text).ToList();
        }
        public List<System.Web.Mvc.SelectListItem> GetGenreCategory(int MembershipType, long CategoryID)
        {
            // if (MembershipType == 2)
            // {

            var data = this.entityRepository.GetByQuery(x => x.IsTalent == true && x.ID == CategoryID).Select(x => x.GenreCategory).FirstOrDefault();

            return data.OrderBy(x => x.ID).Select(x => new System.Web.Mvc.SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
            //this.entityRepository.GetByQuery(x => x.IsTalent == true && x.CategoryTypeID == CategoryID).Select(x => new System.Web.Mvc.SelectListItem { Text = x.GenderSpecific., Value = x.ID.ToString() }).OrderBy(x => x.Text).ToList();
            //}
            // else
            //   return this.entityRepository.GetByQuery(x => x.IsProduction == true && x.CategoryTypeID == CategoryID).Select(x => new System.Web.Mvc.SelectListItem { Text = x.Name, Value = x.ID.ToString() }).OrderBy(x => x.Text).ToList();
        }

        public List<DropdownModel> CategoryCboByCategoryTypeMembershipTypeWithExtendedProp(int MembershipType, long CategoryTypeID)
        {
            if (MembershipType == 2)
            {

                return this.entityRepository.GetByQuery(x => x.IsTalent == true && x.CategoryTypeID == CategoryTypeID).Select(x => new DropdownModel { Text = x.Name, Value = x.ID.ToString(), IsExtended = x.IsExtended == false ? "No" : "Yes" }).OrderBy(x => x.Text).ToList();
            }
            else
                return this.entityRepository.GetByQuery(x => x.IsProduction == true && x.CategoryTypeID == CategoryTypeID).Select(x => new DropdownModel { Text = x.Name, Value = x.ID.ToString(), IsExtended = x.IsExtended == false ? "No" : "Yes" }).OrderBy(x => x.Text).ToList();
        }
    }
}