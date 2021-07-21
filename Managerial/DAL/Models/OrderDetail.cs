// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace DAL.Models
{
    public class OrderDetail : AuditableEntity
    {
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }


        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }

    internal class OrderDetailConfig : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {

            builder.BaseEntityBuilder();

            const string priceDecimalType = "decimal(18,2)";
            builder.ToTable("OrderDetails");
            builder.Property(p => p.UnitPrice).HasColumnType(priceDecimalType);
            builder.Property(p => p.Discount).HasColumnType(priceDecimalType);

        }
    }

}
