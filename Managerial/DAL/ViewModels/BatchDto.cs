using DAL.Models;
using DAL.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using DAL.Core.Helpers;

namespace DAL.ViewModels
{
    public class BatchDto : IBaseViewModel, IMapFrom<Batch>
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