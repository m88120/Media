using System;
using System.Collections.Generic;
using System.Linq;
using AllYouMedia.DataAccess.EntityLayer;
using AllYouMedia.DataAccess.EntityLayer.DBEntity;
using AllYouMedia.DataAccess.ServiceLayer.Interface;
using AllYouMedia.Models;

namespace AllYouMedia.DataAccess.ServiceLayer
{
    public class GenderService: IGenderService
    {
        private IRepository<GenderSpecific> entityRepository;

        public GenderService(IRepository<GenderSpecific> entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        public List<GenderSpecific> GetAll()
        {
            return this.entityRepository.GetAll().ToList();
        }

        public GenderSpecific GetById(long id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.entityRepository.GetById(id);
        }

        public void Insert(GenderSpecific model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Insert(model);
        }

        public void Update(GenderSpecific model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Update(model);
        }

        public void Delete(GenderSpecific model)
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
        public List<System.Web.Mvc.SelectListItem> GenderGenreCategory(int MembershipType, long GenderTypeID)
        {
            if (GenderTypeID > 0)
            {
                var data = this.entityRepository.GetByQuery(x => x.ID == GenderTypeID).Select(x => x.GenreCategory).FirstOrDefault();
                if (data != null)
                    return data.OrderBy(x => x.ID).Select(x => new System.Web.Mvc.SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
                else
                    return null;
            }
            else
                return null;
        }


    }
}