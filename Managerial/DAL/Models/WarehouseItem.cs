using DAL.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Models
{
    public class WarehouseItem : AuditableEntity
    {
        public int? ProductID { get; set; }
        public int? BatchID { get; set; }
        public int? LocationID { get; set; }
        public int? AllocationId { get; set; }
        public int? WarehouseId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual Allocation Allocation { get; set; }
        public virtual Location Location { get; set; }
        public virtual Batch Batch { get; set; }
    }

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