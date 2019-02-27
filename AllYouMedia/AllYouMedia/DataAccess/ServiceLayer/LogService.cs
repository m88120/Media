namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;

    public class LogService : ILogService
    {
        private IRepository<Log> entityRepository;

        public LogService(IRepository<Log> entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        public List<Log> GetAll()
        {
            return this.entityRepository.GetAll().ToList();
        }

        public Log GetById(long id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.entityRepository.GetById(id);
        }

        public void Insert(Log model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Insert(model);
        }

        public void Update(Log model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Update(model);
        }

        public void Delete(Log model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Delete(model);
        }

        public List<Log> GetUserLogForHeader(long AspNetUserID)
        {

            return this.entityRepository.GetByQuery(x => x.AspNetUserID == AspNetUserID && x.ActivityType == "UserActivicty").OrderByDescending(x => x.CreatedOn).Take(10).ToList();
        }
    }
}