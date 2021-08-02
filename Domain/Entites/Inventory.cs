using Domain.Common;
using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Entites
{
    public class Inventory : StockEntity, IStockEntity
    {
        public decimal UnitsInInventory { get; set; }
        public decimal UnitsInOrder { get; set; }
        public decimal ReorderLevel { get; set; }
        public int? WarehouseId { get; set; }
    }
}