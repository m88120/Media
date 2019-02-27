namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;

    public class AspNetUserAddressService : IAspNetUserAddressService
    {
        private IRepository<AspNetUserAddress> entityRepository;

        public AspNetUserAddressService(IRepository<AspNetUserAddress> entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        public List<AspNetUserAddress> GetAll()
        {
            return this.entityRepository.GetAll().ToList();
        }

        public AspNetUserAddress GetById(long id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.entityRepository.GetById(id);
        }

        public void Insert(AspNetUserAddress model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Insert(model);
        }

        public void Update(AspNetUserAddress model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Update(model);
        }

        public void Delete(AspNetUserAddress model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Delete(model);
        }

        public IEnumerable<System.Web.Mvc.SelectListItem> AspNetUserAddressCbo(long AspNetUserID)
        {
            return this.entityRepository.GetByQuery(x => x.AspNetUserID == AspNetUserID && x.IsActive).Select(x => new System.Web.Mvc.SelectListItem { Text = x.CBOExpression, Value = x.ID.ToString() }).OrderBy(x => x.Text);
        }

        public Tuple<List<AspNetUserAddress>, int> GetForDT(long AspNetUserID, string search, int start, int length)
        {
            var queriable = this.entityRepository.GetByQuery(x => (x.CBOExpression.Contains(search)) && x.ID != -1 && x.AspNetUserID == AspNetUserID);
            int totalRecord = queriable.Count();
            return new Tuple<List<AspNetUserAddress>, int>(queriable.ToList(), totalRecord);
        }
    }
}