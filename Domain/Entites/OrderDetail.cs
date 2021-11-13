using Application.ViewModels;
using Domain.Common;

namespace Domain.Entites
{
    public class OrderDetail : AuditableEntity
    {
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public string Comments { get; set; }
        public int WarehouseItemId { get; set; }
        public WarehouseItem WarehouseItem { get; set; }

        public int AllocationId { get; set; }
        public Allocation Allocation { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}