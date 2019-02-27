namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using AllYouMedia.Models;

    public class PayoutDistributionService : IPayoutDistributionService
    {
        private IRepository<PayoutDistribution> entityRepository;
        private IRepository<Order> orderRepository;
        private IRepository<AspNetUser> aspNetUserRepository;
        private IRepository<AspNetUserHierarchy> userHierarchyRepository;
        private decimal TeamAmountPercentage = 19.69M;
        private decimal Level1PayoutPercentage = 5;
        private decimal Level2PayoutPercentage = 15;
        private decimal Level3PayoutPercentage = 25;
        private decimal Level99PayoutPercentage = 20;


        public PayoutDistributionService(IRepository<PayoutDistribution> entityRepository, IRepository<Order> orderRepository, IRepository<AspNetUser> aspNetUserRepository, IRepository<AspNetUserHierarchy> aspNetUserHierarchyRepository)
        {
            this.entityRepository = entityRepository;
            this.orderRepository = orderRepository;
            this.aspNetUserRepository = aspNetUserRepository;
            this.userHierarchyRepository = aspNetUserHierarchyRepository;
        }

        public List<PayoutDistribution> GetAll()
        {
            return this.entityRepository.GetAll().ToList();
        }

        public PayoutDistribution GetById(long id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.entityRepository.GetById(id);
        }

        public void Insert(PayoutDistribution model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Insert(model);
        }

        public void Update(PayoutDistribution model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Update(model);
        }

        public void Delete(PayoutDistribution model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Delete(model);
        }

        public Tuple<List<PayoutDistribution>, int> GetForDT(long AspNetUserID)
        {
            var queriable = this.entityRepository.GetByQuery(x => x.AspNetUserID == AspNetUserID);
            int totalRecord = queriable.Count();
            return new Tuple<List<PayoutDistribution>, int>(queriable.ToList(), totalRecord);
        }
        public void CreatePayoutForOrder(long OrderID)
        {
            var order = orderRepository.GetById(OrderID);
            var orderingUserID = order.AspNetUserID;
            decimal amount = ((TeamAmountPercentage * order.PayableAmount) / 100);
            CreatePayoutRecursivly(OrderID, orderingUserID, amount, 1);
            order.IsPayoutCreated = true;
            orderRepository.Update(order);

        }
        private void CreatePayoutRecursivly(long OrderID, long AspNetUserID, decimal Amount, int RecursionLevel)
        {
            AspNetUserHierarchy userHierarchy = userHierarchyRepository.GetByQuery(x => x.AspNetUserID == AspNetUserID).First();
            if (userHierarchy.ParentAspNetUserID != -1) ///// if user have parent then make payout
            {
                var parentAspNetUser = aspNetUserRepository.GetById(userHierarchy.ParentAspNetUserID);
                var payoutPercentage = RecursionLevel == 1 ?
                                            Level1PayoutPercentage : RecursionLevel == 2 ?
                                                                        Level2PayoutPercentage : RecursionLevel == 3 ?
                                                                                                    Level3PayoutPercentage : Level99PayoutPercentage;
                var payoutAmt = (Amount * payoutPercentage) / 100;
                entityRepository.Insert(new PayoutDistribution
                {
                    AspNetUserID = parentAspNetUser.Id,
                    OrderID = OrderID,
                    PayoutBaseAmount = Amount,
                    PayoutPercentage = payoutPercentage,
                    ReceivedAmount = payoutAmt,
                });
                parentAspNetUser.EarningAmount += payoutAmt;
                aspNetUserRepository.Update(parentAspNetUser);
                RecursionLevel++;
                var newAmount = Amount - payoutAmt;
                CreatePayoutRecursivly(OrderID, parentAspNetUser.Id, newAmount, RecursionLevel);
            }
            else //////// payout to company
            {
                var companyUser = aspNetUserRepository.GetById(1);
                entityRepository.Insert(new PayoutDistribution
                {
                    AspNetUserID = companyUser.Id,
                    OrderID = OrderID,
                    PayoutBaseAmount = Amount,
                    PayoutPercentage = -1,
                    ReceivedAmount = Amount,
                });
                companyUser.EarningAmount += Amount;
                aspNetUserRepository.Update(companyUser);
            }
        }
    }
}