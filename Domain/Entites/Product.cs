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
        public decimal UnitsInStock { get; set; }
        public decimal ReorderLevel { get; set; }
        public int? WarehouseItemId { get; set; }
        public int? BatchId { get; set; }

        public int? ParentId { get; set; }
        public virtual Product Parent { get; set; }

        public int? ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<Batch> Batches { get; set; }

        public virtual ICollection<Product> Children { get; set; }
        public virtual ICollection<WarehouseItem> WarehouseItems { get; set; } = new HashSet<WarehouseItem>();
    }
}