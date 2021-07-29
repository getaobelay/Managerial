using Domain.Entites;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
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