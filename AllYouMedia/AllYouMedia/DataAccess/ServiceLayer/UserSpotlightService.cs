namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;

    public class UserSpotlightService : IUserSpotlightService
    {
        private IRepository<UserSpotlight> entityRepository;

        public UserSpotlightService(IRepository<UserSpotlight> entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        public List<UserSpotlight> GetAll()
        {
            return this.entityRepository.GetAll().ToList();
        }

        public UserSpotlight GetById(long id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.entityRepository.GetById(id);
        }

        public void Insert(UserSpotlight model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Insert(model);
        }

        public void Update(UserSpotlight model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Update(model);
        }

        public void Delete(UserSpotlight model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Delete(model);
        }

        public double GetUserSpotlightForUser(long AspNetUserID)
        {
            return this.entityRepository.GetByQuery(x => x.AspNetUserID == AspNetUserID && x.Spotlight > 0).Select(x => x.Spotlight).DefaultIfEmpty().Average();
        }
        public void SaveSpotlight(long RatedAspNetUserID, int Rating, long RaterAspNetUserID)
        {
            var spotlight = this.entityRepository.GetByQuery(x => x.AspNetUserID == RatedAspNetUserID && x.ReviewingAspNetUserID == RaterAspNetUserID).FirstOrDefault();
            if (spotlight == null)
            {
                spotlight = new UserSpotlight { AspNetUserID = RatedAspNetUserID, ReviewingAspNetUserID = RaterAspNetUserID, Spotlight = Rating };
                spotlight = this.entityRepository.Insert(spotlight);
            }
            else
            {
                spotlight.Spotlight = Rating;
                this.entityRepository.Update(spotlight);
            }
        }
    }
}