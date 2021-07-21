using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using DAL.Core.Helpers;

namespace DAL.Models
{
    public class Allocation : AuditableEntity
    {
        public bool IsAvailable { get; set; }
        public bool IsCompleted { get; set; }
        public int? WarehouseItemID { get; set; }
        public int? WarehouseId { get; set; }
        public virtual ICollection<WarehouseItem> WarehouseItems { get; set; } = new HashSet<WarehouseItem>();
        public virtual Warehouse Warehouse { get; set; }
        public int OrderID { get; set; }
        public virtual Order Order { get; set; }
    }


    public class AllocationConfig : IEntityTypeConfiguration<Allocation>
    {
        public void Configure(EntityTypeBuilder<Allocation> builder)
        {
            builder.BaseEntityBuilder();

            builder.HasOne(o => o.Order)
              .WithMany(w => w.Allocations)
              .HasForeignKey(o => o.OrderID);

            builder.HasMany(d => d.WarehouseItems)
                 .WithOne(p => p.Allocation)
                 .HasForeignKey(d => d.AllocationId);
        }
    }
}