using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entites
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
}