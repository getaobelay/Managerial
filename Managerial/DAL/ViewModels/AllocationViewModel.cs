using System;
using System.Collections.Generic;
using DAL.Core.Helpers;
using DAL.Core.Helpers.BaseDtos;
using DAL.Core.ViewModels;
using DAL.Models;

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