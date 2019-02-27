namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;

    public class CartItemService : ICartItemService
    {
        private IRepository<CartItem> entityRepository;

        public CartItemService(IRepository<CartItem> entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        public List<CartItem> GetAll()
        {
            return this.entityRepository.GetAll().ToList();
        }

        public CartItem GetById(long id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.entityRepository.GetById(id);
        }

        public void Insert(CartItem model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Insert(model);
        }

        public void Update(CartItem model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Update(model);
        }

        public void Delete(CartItem model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Delete(model);
        }

        public List<CartItem> GetByCartID(long CartID)
        {
            return this.entityRepository.GetByQuery(x => x.CartID == CartID).ToList();
        }
    }
}