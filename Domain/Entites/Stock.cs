using Domain.Common;
using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Entites
{
    public class Stock : StockEntity, IStockEntity
    {
        public decimal UnitsInStock { get; set; }
        public decimal ReorderLevel { get; set; }
        public int? WarehouseId { get; set; }
        public virtual ICollection<Warehouse> Warehouses { get; set; } = new HashSet<Warehouse>();
    }
}