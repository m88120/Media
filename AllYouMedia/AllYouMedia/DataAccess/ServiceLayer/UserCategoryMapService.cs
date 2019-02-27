namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System.Data.Entity;

    public class UserCategoryMapService : IUserCategoryMapService
    {
        private IRepository<UserCategoryMap> entityRepository;

        public UserCategoryMapService(IRepository<UserCategoryMap> entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        public List<UserCategoryMap> GetAll()
        {
            return this.entityRepository.GetAll().ToList();
        }

        public UserCategoryMap GetById(long id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.entityRepository.GetById(id);
        }

        public void Insert(UserCategoryMap model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Insert(model);
        }

        public void Update(UserCategoryMap model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Update(model);
        }

        public void Delete(UserCategoryMap model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Delete(model);
        }
        public List<UserCategoryMap> GetByAspNetUserID(long AspNetUserID) {
            return this.entityRepository.GetByAction(x => x.Include(y => y.AspNetUserRole).Include(y => y.Attribute).Include(y => y.Category).Include(y => y.CategoryType).Include(y => y.SubCategory))
                .Where(x => x.AspNetUserRole.UserId == AspNetUserID).ToList();
        }

       
    }
}