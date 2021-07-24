using DAL.Core.Helpers;
using DAL.Models;
using System;

namespace DAL.ViewModels
{
    public class WarehouseItemViewModel : BaseViewModel, IMapFrom<WarehouseItem>
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
        public BatchViewModel Batch { get; set; }
    }
}