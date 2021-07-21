// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Product : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string Measurement { get; set; }
        public decimal QuantityPerUnit { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public bool IsActive { get; set; }
        public int? StockId { get; set; }
        public int? InventoryId { get; set; }
        public int? WarehouseItemId { get; set; }
        public int? BatchId { get; set; }
        public int? ProductCategoryId { get; set; }

        public ProductCategory ProductCategory { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual Stock Stock { get; set; }
        public virtual Inventory Inventory { get; set; }
        public virtual ICollection<WarehouseItem> WarehouseItems { get; set; } = new HashSet<WarehouseItem>();
        public virtual ICollection<Batch> Batches { get; set; } = new HashSet<Batch>();

    }

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
            builder.Property(p => p.BuyingPrice).HasColumnType(priceDecimalType);
            builder.Property(p => p.SellingPrice).HasColumnType(priceDecimalType);

            builder.HasMany(d => d.Batches)
                  .WithOne(p => p.Product)
                  .HasForeignKey(d => d.ProductId);
        }
    }
}
