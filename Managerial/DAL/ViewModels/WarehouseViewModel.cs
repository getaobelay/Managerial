using DAL.Core.Helpers;
using DAL.Models;
using System;
using System.Collections.Generic;

namespace DAL.ViewModels
{
    public class WarehouseViewModel : BaseViewModel, IMapFrom<Warehouse>
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseItemID { get; set; }

        public IEnumerable<WarehouseItemViewModel> WarehouseItems { get; set; }
        public IEnumerable<LocationViewModel> Locations { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
        public IEnumerable<BatchDto> Batches { get; set; }
    }
}