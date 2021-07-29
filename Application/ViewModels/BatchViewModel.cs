using Application.Interfaces;
using Domain.Entites;
using System.Collections.Generic;

namespace Application.ViewModels
{
    public class BatchViewModel : BaseViewModel, IMapFrom<Batch>
    {
        public ProductViewModel Product { get; set; }
        public InventoryViewModel Inventory { get; set; }
        public StockViewModel Stock { get; set; }
        public List<WarehouseItemViewModel> WarehouseItems { get; set; }
    }
}