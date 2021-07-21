using System;
using System.Collections.Generic;
using DAL.Models;
using DAL.Core.Helpers.BaseDtos;
using DAL.ViewModels;
using DAL.Core.Helpers.InventoryViewModels;
using Managerial.ViewModels;

namespace DAL.Core.Helpers.ProductDtos
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