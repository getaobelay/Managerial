using Domain.Common;
using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Entites
{
    public class Stock : AuditableEntity, IStockEntity
    {
        public string Name { get; set; }
        public bool IsQuanityAvailable { get; set; }
        public decimal TotalUnitsQuantity { get; set; }
        public decimal ProductQuantity { get; set; }
        public decimal BatchQuantity { get; set; }
        public int? ProductId { get; set; }
        public int? BatchId { get; set; }
        public decimal UnitsInStock { get; set; }
        public decimal ReorderLevel { get; set; }
        public int? WarehouseId { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
        public virtual ICollection<Batch> Batches { get; set; } = new HashSet<Batch>();
        public virtual ICollection<Warehouse> Warehouses { get; set; } = new HashSet<Warehouse>();
    }
}