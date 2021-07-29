// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

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
        public ApplicationUser Cashier { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int? AllocationId { get; set; }
        public virtual ICollection<Allocation> Allocations { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}