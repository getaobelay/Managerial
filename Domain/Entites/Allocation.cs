using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entites
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
}