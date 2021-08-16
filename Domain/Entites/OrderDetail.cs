using Domain.Common;

namespace Domain.Entites
{
    public class OrderDetail : AuditableEntity
    {
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }

        public int WarehouseItemId { get; set; }
        public WarehouseItem WarehouseItem { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}