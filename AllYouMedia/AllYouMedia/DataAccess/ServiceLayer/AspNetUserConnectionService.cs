namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System.Data.Entity;
    public class AspNetUserConnectionService : IAspNetUserConnectionService
    {
        private IRepository<AspNetUserConnection> entityRepository;

        public AspNetUserConnectionService(IRepository<AspNetUserConnection> entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        public List<AspNetUserConnection> GetAll()
        {
            return this.entityRepository.GetAll().ToList();
        }

        public AspNetUserConnection GetById(long id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.entityRepository.GetById(id);
        }

        public void Insert(AspNetUserConnection model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Insert(model);
        }

        public void Update(AspNetUserConnection model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Update(model);
        }

        public void Delete(AspNetUserConnection model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Delete(model);
        }

        public AspNetUserConnection GetConnectedUser(long AspNetUserID, long ConnectedAspNetUserID)
        {
            return this.entityRepository.GetByAction(x => x.Where(y => y.AspNetUserID == AspNetUserID && y.ConnectedAspNetUserID == ConnectedAspNetUserID)).FirstOrDefault();
        }

        public Tuple<dynamic, int> GetForDT(long AspNetUserID, string search, int start, int length)
        {
            var queriable = this.entityRepository.GetByAction(x => x.Include(y => y.AspNetUser).Include(y => y.ConnectedAspNetUser)).Where(x => (x.AspNetUser.Name.Contains(search) || x.ConnectedAspNetUser.UserName.Contains(search)) && x.ID != -1 && (x.AspNetUserID == AspNetUserID || x.ConnectedAspNetUserID == AspNetUserID));
            int totalRecord = queriable.Count();
            return new Tuple<dynamic, int>(queriable.ToList().Select(x => new
            {
                DT_RowId = x.ID,
                ConnectedTo = x.ConnectedAspNetUserID == AspNetUserID ? x.AspNetUser.Name : x.ConnectedAspNetUser.Name,
                Category = x.ConnectedAspNetUserID == AspNetUserID ? "" : "",
                ConnectedOn = x.CreatedOn.ToString("dd-MM-yyyy hh:mm")
            }).ToList(), totalRecord);
        }
    }
}