using AllYouMedia.DataAccess.EntityLayer;
using AllYouMedia.DataAccess.EntityLayer.DBEntity;
using AllYouMedia.DataAccess.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AllYouMedia.DataAccess.ServiceLayer
{
    public class InstrumentService: IInstrumentService
    {
        private IRepository<InstrumentCategory> entityRepository;

        public InstrumentService(IRepository<InstrumentCategory> entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        public List<InstrumentCategory> GetAll()
        {
            return this.entityRepository.GetAll().ToList();
        }

        public InstrumentCategory GetById(long id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.entityRepository.GetById(id);
        }

        public void Insert(InstrumentCategory model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Insert(model);
        }

        public void Update(InstrumentCategory model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Update(model);
        }

        public void Delete(InstrumentCategory model)
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
        public List<System.Web.Mvc.SelectListItem> GenreInstrumentCategory(int MembershipType, long GenreID)
        {
            if (GenreID > 0)
            {
                var data = this.entityRepository.GetByQuery(x => x.ID == GenreID).Select(x => x.GenreCategory).FirstOrDefault();
                if (data != null )
                return data.Where(x=>x.IsActive==true).Select(x => new System.Web.Mvc.SelectListItem { Text = x.Name, Value = x.ID.ToString() }).OrderBy(x => x.Value).ToList();
                else
                    return null;
            }
            else
                return null;
        }
    }
}