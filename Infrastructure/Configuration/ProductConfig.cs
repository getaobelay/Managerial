using Domain.Entites;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    internal class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            const string priceDecimalType = "decimal(18,2)";

            builder.BaseEntityBuilder();

            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.HasIndex(p => p.Name);
            builder.Property(p => p.Description).HasMaxLength(500);
            builder.ToTable($"Products");
            builder.HasOne(p => p.Parent).WithMany(p => p.Children).OnDelete(DeleteBehavior.Restrict);
            builder.Property(p => p.BuyingPrice).HasColumnType(priceDecimalType);
            builder.Property(p => p.SellingPrice).HasColumnType(priceDecimalType);

    
        }
    }
}