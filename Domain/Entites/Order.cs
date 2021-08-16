using Domain.Common;
using Domain.Entites.Identity;
using System.Collections.Generic;

namespace Domain.Entites
{
    public class Order : AuditableEntity
    {
        public decimal Discount { get; set; }
        public string Comments { get; set; }

        public string CashierId { get; set; }
        public virtual ApplicationUser Cashier { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}