namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System.Data.Entity;

    public class CategoryTypeService : ICategoryTypeService
    {
        private IRepository<CategoryType> entityRepository;

        public CategoryTypeService(IRepository<CategoryType> entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        public List<CategoryType> GetAll()
        {
            return this.entityRepository.GetAll().ToList();
        }

        public CategoryType GetById(long id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.entityRepository.GetById(id);
        }

        public void Insert(CategoryType model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Insert(model);
        }

        public void Update(CategoryType model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Update(model);
        }

        public void Delete(CategoryType model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Delete(model);
        }

        public List<System.Web.Mvc.SelectListItem> CategoryTypeCbo()
        {
            return this.entityRepository.GetAll().Select(x => new System.Web.Mvc.SelectListItem { Text = x.Name, Value = x.ID.ToString() }).OrderBy(x => x.Text).ToList();
        }

        public List<CategoryType> GetAllActiveCatDataForMenu(int isProduction = -1)
        {
            return this.entityRepository.GetByAction((x) => { return x.Include(y => y.Categories).Include(y => y.Categories.Select(z => z.SubCategories)).Where(y => y.IsActive && y.ID != -1 && (isProduction == -1 || y.IsProduction)).OrderBy(y => y.Name); }).ToList();
        }
        public List<System.Web.Mvc.SelectListItem> CategoryTypeCboByMembershipType(int MembershipType)
        {
            if (MembershipType == 2)
                return this.entityRepository.GetByQuery(x => x.IsTalent == true).Select(x => new System.Web.Mvc.SelectListItem { Text = x.CBOExpression, Value = x.ID.ToString() }).OrderBy(x => x.Text).ToList();
            else
                return this.entityRepository.GetByQuery(x => x.IsProduction == true).Select(x => new System.Web.Mvc.SelectListItem { Text = x.Name, Value = x.ID.ToString() }).OrderBy(x => x.Text).ToList();
        }
        public List<System.Web.Mvc.SelectListItem> CategoryTypeCboByMembershipTypeOrderBy(int MembershipType)
        {
            if (MembershipType == 2)
                return this.entityRepository.GetByQuery(x => x.IsTalent == true && x.IsMembershipCategory == true).Select(x => new System.Web.Mvc.SelectListItem { Text = x.CBOExpression, Value = x.ID.ToString() }).OrderByDescending(x => x.Value).ToList();
            else
                return this.entityRepository.GetByQuery(x => x.IsProduction == true && x.IsMembershipCategory == true).Select(x => new System.Web.Mvc.SelectListItem { Text = x.Name, Value = x.ID.ToString() }).OrderByDescending(x => x.Text).ToList();
        }
    }
}