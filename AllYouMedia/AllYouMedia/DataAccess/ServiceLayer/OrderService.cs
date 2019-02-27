namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System.Data.Entity;
    public class OrderService : IOrderService
    {
        private IRepository<Order> entityRepository;
        private IRepository<OrderItem> orderItemRepository;
        private IRepository<Item> itemRepository;
        private IRepository<Cart> cartRepository;
        public OrderService(IRepository<Order> entityRepository, IRepository<OrderItem> OrderItemRepository, IRepository<Item> itemRepository, IRepository<Cart> cartRepository)
        {
            this.entityRepository = entityRepository;
            this.orderItemRepository = OrderItemRepository;
            this.itemRepository = itemRepository;
            this.cartRepository = cartRepository;
        }

        public List<Order> GetAll()
        {
            return this.entityRepository.GetAll().ToList();
        }

        public Order GetById(long id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.entityRepository.GetById(id);
        }

        public void Insert(Order model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Insert(model);
        }

        public void Update(Order model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Update(model);
        }

        public void Delete(Order model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Delete(model);
        }
        public Tuple<bool, string, long> ValidateAndPlaceOrder(AllYouMedia.Controllers.HomeController.SessionCart cart, long AspNetUserID, long AspNetUserAddressID)
        {
            ///// Validating
            var itemIds = cart.CartItems.Select(x => x.ItemID);
            var items = itemRepository.GetByQuery(x => itemIds.Contains(x.ID)).ToList();
            foreach (var item in items)
            {
                var cartItem = cart.CartItems.First(x => x.ItemID == item.ID);
                if (item.IsStockApplicable && item.StockQty < cartItem.Qty)
                    return new Tuple<bool, string, long>(false, string.Format("Item {0} in your cart does not have sufficient quantity in stock (Available Qty: {1}).", item.Name, item.StockQty), -1);
                if (item.SellPrice != cartItem.SellPrice)
                    return new Tuple<bool, string, long>(false, string.Format("Price for Item {0} in your cart has changed. Please remove item from cart and add it back.", item.Name), -1);
            }
            var order = new Order()
            {
                AspNetUserAddressID = AspNetUserAddressID,
                AspNetUserID = AspNetUserID,
                NetAmount = cart.NetAmount,
                PayableAmount = cart.PayableAmount,
                PaymentMode = null,
                PaymentRefCode = null,
                Shipping = cart.Shipping,
                Tax = cart.Tax,
                TotalDiscount = cart.TotalDiscount,
            };
            foreach (var cartItem in cart.CartItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    BasePrice = cartItem.BasePrice,
                    ItemID = cartItem.ItemID,
                    LineAmount = cartItem.LineAmount,
                    LineDiscount = cartItem.LineDiscount,
                    Qty = cartItem.Qty,
                    SellPrice = cartItem.SellPrice,
                    Tax = cartItem.Tax
                });
            }
            order = this.entityRepository.Insert(order);
            var dbCart = cartRepository.GetByQuery(x => x.AspNetUserID == AspNetUserID).First();
            var sql = new System.Text.StringBuilder();
            sql.Append("DELETE FROM CartItem WHERE ID IN(");
            foreach (var dbcartItem in dbCart.CartItems)
            {
                sql.AppendFormat("{0},", dbcartItem.ID);
            }
            sql.Remove(sql.Length - 1, 1);
            sql.AppendLine(");");
            sql.AppendLine(string.Format("DELETE FROM Cart WHERE ID={0};", dbCart.ID));
            cartRepository.ExecSql(sql.ToString(), true);
            return new Tuple<bool, string, long>(true, "Order placed successfully.", order.ID);
        }

        public AspNetUserAddress GetShippingAddressByOrderID(long OrderID)
        {
            return entityRepository.GetByAction(ac => ac.Include(x => x.AspNetUserAddress)).First(x => x.ID == OrderID).AspNetUserAddress;
        }
        public Tuple<List<Order>, int> GetForDT(long AspNetUserID, string search, int start, int length)
        {
            var queriable = this.entityRepository.GetByAction(ac => ac.Include(x => x.AspNetUserAddress).Include(x => x.OrderItems).Include(x => x.OrderItems.Select(y => y.Item))).Where(x => x.AspNetUserID == AspNetUserID).OrderByDescending(x => x.CreatedOn);
            int totalRecord = queriable.Count();
            return new Tuple<List<Order>, int>(queriable.ToList(), totalRecord);
        }
        public Tuple<List<OrderItem>, int> GetSoldItemForDT(long AspNetUserID, string search, int start, int length)
        {
            var queriable = this.orderItemRepository.GetByAction(ac => ac.Include(x => x.Order).Include(x => x.Order.AspNetUser).Include(x => x.Order.AspNetUserAddress).Include(x => x.Item)).Where(x => x.Item.AspNetUserID == AspNetUserID).OrderByDescending(x => x.CreatedOn);
            int totalRecord = queriable.Count();
            return new Tuple<List<OrderItem>, int>(queriable.ToList(), totalRecord);
        }
    }
}