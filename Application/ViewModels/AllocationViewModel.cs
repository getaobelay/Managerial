using Application.Interfaces;
using Domain.Entites;
using System.Collections.Generic;

namespace Application.ViewModels
{
    public class AllocationViewModel : BaseViewModel, IMapFrom<Allocation>
    {
        public bool IsAvailable { get; set; }
        public bool IsCompleted { get; set; }
        public IEnumerable<WarehouseItemViewModel> WarehouseItems { get; set; }
        public OrderViewModel Order { get; set; }
    }
}