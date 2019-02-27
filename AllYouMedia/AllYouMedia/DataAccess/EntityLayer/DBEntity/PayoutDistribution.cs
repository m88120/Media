namespace AllYouMedia.DataAccess.EntityLayer.DBEntity
{
    using AllYouMedia.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PayoutDistribution : BaseEntity
    {
        public PayoutDistribution()
        {

        }

        public long OrderID { get; set; }
        public virtual Order Order { get; set; }

        public long AspNetUserID { get; set; }
        public AspNetUser AspNetUser { get; set; }

        public decimal PayoutBaseAmount { get; set; }

        public decimal PayoutPercentage { get; set; }

        public decimal ReceivedAmount { get; set; }

        public bool IsAmountReleased { get; set; }
    }
}
