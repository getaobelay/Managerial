using Domain.Entites;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    internal class OrderDetailConfig : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.BaseEntityBuilder();

            const string priceDecimalType = "decimal(18,2)";
            builder.ToTable("OrderDetails");
            builder.Property(p => p.Discount).HasColumnType(priceDecimalType);
        }
    }
}