using DAL.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Warehouse : AuditableEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public int? WarehouseItemID { get; set; }
        public int? LocationID { get; set; }
        public int? ProductID { get; set; }
        public int? BatchID { get; set; }
        public ICollection<Allocation> Allocations { get; set; }
        public ICollection<WarehouseItem> WarehouseItems { get; set; }
        public ICollection<Location> Locations { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Batch> Batches { get; set; }
    }

    internal class WarehouseConfig : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.BaseEntityBuilder();

            builder.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(true)
                .ValueGeneratedOnAdd()
                .IsRequired();
        }
    }
}