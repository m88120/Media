namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System.Data.Entity;
    public class MessageService : IMessageService
    {
        private IRepository<Message> entityRepository;

        public MessageService(IRepository<Message> entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        public List<Message> GetAll()
        {
            return this.entityRepository.GetAll().ToList();
        }

        public Message GetById(long id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.entityRepository.GetById(id);
        }

        public void Insert(Message model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Insert(model);
        }

        public void Update(Message model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Update(model);
        }

        public void Delete(Message model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Delete(model);
        }

        public List<Message> GetMessagesByUserID(long UserID)
        {
            return this.entityRepository.GetByQuery(x => x.FromAspNetUserID == UserID || x.ToAspNetUserID == UserID).ToList();
        }
        public Tuple<dynamic, int> GetForDT(long AspNetUserID, string search, int start, int length)
        {
            var queriable = this.entityRepository.GetByAction(x => x.Include(y => y.FromAspNetUser).Include(y => y.ToAspNetUser)).Where(x => (x.FromAspNetUser.Name.Contains(search) || x.FromAspNetUser.UserName.Contains(search) || x.Subject.Contains(search)) && x.ID != -1 && x.ToAspNetUserID == AspNetUserID && x.IsDeleted == false);
            int totalRecord = queriable.Count();
            return new Tuple<dynamic, int>(queriable.ToList().Select(x => new
            {
                DT_RowId = x.ID,
                From = x.FromAspNetUser.Name + " (" + x.FromAspNetUser.Name + ")",
                x.Subject,
                x.Body,
                ReceivedOn = x.CreatedOn.ToString("dd-MM-yyyy hh:mm")
            }).ToList(), totalRecord);
        }
    }
}