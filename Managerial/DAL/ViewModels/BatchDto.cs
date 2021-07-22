using DAL.Core.Helpers;
using DAL.Models;
using System;
using System.Collections.Generic;

namespace DAL.ViewModels
{
    public class BatchDto : BaseViewModel, IMapFrom<Batch>
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ProductViewModel Product { get; set; }
        public InventoryViewModel Inventory { get; set; }
        public StockViewModel Stock { get; set; }
        public List<WarehouseItemViewModel> WarehouseItems { get; set; }
    }
}