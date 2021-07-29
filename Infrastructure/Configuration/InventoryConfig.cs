using Domain.Entites;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class InventoryConfig : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.BaseStockBuilder();

            builder.HasMany(d => d.Batches)
                .WithOne(p => p.Inventory)
                .HasForeignKey(d => d.InventoryId);

            builder.HasMany(d => d.Products)
                .WithOne(p => p.Inventory)
                .HasForeignKey(d => d.InventoryId);
        }
    }
}