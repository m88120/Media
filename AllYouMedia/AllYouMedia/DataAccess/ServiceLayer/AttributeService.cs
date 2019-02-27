namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;

    public class AttributeService : IAttributeService
    {
        private IRepository<AllYouMedia.DataAccess.EntityLayer.DBEntity.Attribute> entityRepository;

        public AttributeService(IRepository<AllYouMedia.DataAccess.EntityLayer.DBEntity.Attribute> entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        public List<AllYouMedia.DataAccess.EntityLayer.DBEntity.Attribute> GetAll()
        {
            return this.entityRepository.GetAll().ToList();
        }

        public AllYouMedia.DataAccess.EntityLayer.DBEntity.Attribute GetById(long id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.entityRepository.GetById(id);
        }

        public void Insert(AllYouMedia.DataAccess.EntityLayer.DBEntity.Attribute model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Insert(model);
        }

        public void Update(AllYouMedia.DataAccess.EntityLayer.DBEntity.Attribute model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Update(model);
        }

        public void Delete(AllYouMedia.DataAccess.EntityLayer.DBEntity.Attribute model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Delete(model);
        }

        public List<System.Web.Mvc.SelectListItem> AttributeCboBySubCategoryMembershipType(long SubCategoryID)
        {
            return this.entityRepository.GetByQuery(x => x.SubCategoryID == SubCategoryID && SubCategoryID != -1).Select(x => new System.Web.Mvc.SelectListItem { Text = x.Name, Value = x.ID.ToString() }).OrderBy(x => x.Text).ToList();
        }
    }
}