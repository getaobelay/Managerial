using DAL.Core.Helpers;
using DAL.Models;
using DAL.ViewModels.Interfaces;
using System;
using System.Collections.Generic;

namespace DAL.ViewModels
{
    public class AllocationViewModel : IBaseViewModel, IMapFrom<Allocation>
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsCompleted { get; set; }
        public IEnumerable<WarehouseItemViewModel> WarehouseItems { get; set; }
        public OrderViewModel Order { get; set; }
    }
}