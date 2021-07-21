using System;
using DAL.Core.Helpers.ProductDtos;
using DAL.Core.Helpers.BaseDtos;
using DAL.Models;
using DAL.Core.Helpers;
using Managerial.ViewModels;

namespace DAL.ViewModels
{
    public class WarehouseItemViewModel: IBaseViewModel, IMapFrom<WarehouseItem>
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ProductViewModel Product { get; set; }
        public WarehouseViewModel Warehouse { get; set; }
        public AllocationViewModel Allocation { get; set; }
        public LocationViewModel Location { get; set; }
        public BatchDto Batch { get; set; }
    }
}