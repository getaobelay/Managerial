using Domain.Common;

namespace Domain.Entites
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
}