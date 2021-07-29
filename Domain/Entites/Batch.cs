using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entites
{
    public class Batch : AuditableEntity
    {
        public int? ProductId { get; set; }
        public int? InventoryId { get; set; }
        public int? StockId { get; set; }
        public int? WarehouseItemId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Inventory Inventory { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual ICollection<WarehouseItem> WarehouseItems { get; set; } = new HashSet<WarehouseItem>();
    }
}