using AllYouMedia.DataAccess.EntityLayer;
using AllYouMedia.DataAccess.EntityLayer.DBEntity;
using AllYouMedia.DataAccess.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllYouMedia.DataAccess.ServiceLayer
{
    public class InstrumentSpecificationService: IInstrumentSpecificationService
    {

        private IRepository<InstrumentSpecificationCategory> entityRepository;

        public InstrumentSpecificationService(IRepository<InstrumentSpecificationCategory> entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        public List<InstrumentSpecificationCategory> GetAll()
        {
            return this.entityRepository.GetAll().ToList();
        }

        public InstrumentSpecificationCategory GetById(long id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.entityRepository.GetById(id);
        }

        public void Insert(InstrumentSpecificationCategory model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Insert(model);
        }

        public void Update(InstrumentSpecificationCategory model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Update(model);
        }

        public void Delete(InstrumentSpecificationCategory model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Delete(model);
        }

        public IEnumerable<System.Web.Mvc.SelectListItem> GenderCbo()
        {
            return this.entityRepository.GetAll().Select(x => new System.Web.Mvc.SelectListItem { Text = x.CBOExpression, Value = x.ID.ToString() }).OrderBy(x => x.Text);
        }
        public List<System.Web.Mvc.SelectListItem> InstrumentSpecificationCategory(int MembershipType, long InstrumentID)
        {
            if (InstrumentID > 0)
            {
                return this.entityRepository.GetByQuery(x => x.InstrumentCategoryId == InstrumentID).Select(x => new System.Web.Mvc.SelectListItem { Text = x.Name, Value = x.ID.ToString() }).OrderBy(x => x.Value).ToList();
               
            }
            else
                return null;
        }
    }
}