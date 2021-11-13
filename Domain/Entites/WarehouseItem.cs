using Application.ViewModels;
using Domain.Common;

namespace Domain.Entites
{
    public class WarehouseItem : AuditableEntity
    {
        public string Location { get; set; }
        public int? ProductID { get; set; }
        public int? BatchID { get; set; }
        public int? WarehouseId { get; set; }
        public int? OrderDetailId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual Allocation Allocation { get; set; }
    }
}