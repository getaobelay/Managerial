// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entites
{
    public class Product : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public bool IsActive { get; set; }
        public int? StockId { get; set; }
        public int? InventoryId { get; set; }
        public int? WarehouseItemId { get; set; }
        public int? BatchId { get; set; }

        public int? ParentId { get; set; }
        public Product Parent { get; set; }

        public int? ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public ICollection<Product> Children { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual Stock Stock { get; set; }
        public virtual Inventory Inventory { get; set; }
        public virtual ICollection<WarehouseItem> WarehouseItems { get; set; } = new HashSet<WarehouseItem>();
        public virtual ICollection<Batch> Batches { get; set; } = new HashSet<Batch>();
    }
}