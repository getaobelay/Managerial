// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DAL.Models
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

    internal class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.BaseEntityBuilder();

            const string priceDecimalType = "decimal(18,2)";
            builder.Property(o => o.Comments).HasMaxLength(500);
            builder.ToTable("Orders");
            builder.Property(p => p.Discount).HasColumnType(priceDecimalType);
        }
    }
}