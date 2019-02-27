namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System.Data.Entity;
    public class CartService : ICartService
    {
        private IRepository<Cart> entityRepository;
        private IRepository<CartItem> cartItemRepository;

        public CartService(IRepository<Cart> entityRepository, IRepository<CartItem> cartItemRepository)
        {
            this.entityRepository = entityRepository;
            this.cartItemRepository = cartItemRepository;
        }

        public List<Cart> GetAll()
        {
            return this.entityRepository.GetAll().ToList();
        }

        public Cart GetById(long id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.entityRepository.GetById(id);
        }

        public void Insert(Cart model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Insert(model);
        }

        public void Update(Cart model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Update(model);
        }

        public void Delete(Cart model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Delete(model);
        }
        public void AddToCart(long AspNetUserID, long ItemID, int Qty)
        {
            var cart = this.entityRepository.GetByQuery(x => x.AspNetUserID == AspNetUserID).FirstOrDefault();
            if (cart != null)
            {
                var cartItem = this.cartItemRepository.GetByQuery(x => x.CartID == cart.ID && x.ItemID == ItemID).FirstOrDefault();
                if (cartItem != null)
                {
                    if (Qty == 0)
                        this.cartItemRepository.Delete(cartItem);
                    else
                        cartItem.Qty = Qty; this.cartItemRepository.Update(cartItem);
                }
                else
                {
                    cartItem = new CartItem
                    {
                        CartID = cart.ID,
                        ItemID = ItemID,
                        Qty = Qty
                    };
                    this.cartItemRepository.Insert(cartItem);
                }
            }
            else
            {
                cart = new Cart { AspNetUserID = AspNetUserID };
                cart = this.entityRepository.Insert(cart);
                this.cartItemRepository.Insert(new CartItem
                {
                    CartID = cart.ID,
                    ItemID = ItemID,
                    Qty = Qty
                });

            }
        }

        public Cart GetCartByAspNetUserID(long AspNetUserID)
        {
            var cart = this.entityRepository.GetByAction((x) => { return x.Include(y => y.CartItems.Select(z => z.Item)).Where(y => y.AspNetUserID == AspNetUserID); }).FirstOrDefault();
            if (cart == null) cart = new Cart();
            return cart;
        }
    }
}