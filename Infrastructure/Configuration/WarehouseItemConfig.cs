using Domain.Entites;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    internal class WarehouseItemConfig : IEntityTypeConfiguration<WarehouseItem>
    {
        public void Configure(EntityTypeBuilder<WarehouseItem> builder)
        {
            builder.BaseEntityBuilder();

            builder.HasOne(d => d.Product)
                 .WithMany(p => p.WarehouseItems)
                 .HasForeignKey(d => d.ProductID);

            builder.HasOne(d => d.Warehouse)
                 .WithMany(p => p.WarehouseItems)
                 .HasForeignKey(d => d.WarehouseId);

            builder.HasOne(d => d.Location)
                 .WithMany(p => p.WarehouseItems)
                 .HasForeignKey(d => d.LocationID);

            builder.HasOne(d => d.Batch)
              .WithMany(p => p.WarehouseItems)
              .HasForeignKey(d => d.BatchID);
        }
    }
}