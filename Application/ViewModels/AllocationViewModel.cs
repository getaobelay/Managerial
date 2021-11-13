using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class AllocationViewModel:BaseViewModel, IMapFrom<Allocation>
    {

        public string Comments { get; set; }
        public int? WarehouseItemId { get; set; }
        public int? OrderDetailId { get; set; }
        public WarehouseItemViewModel WarehouseItem { get; set; }
        public OrderDetailViewModel OrderDetail { get; set; }
        public bool IsAllocated { get; set; }
        public bool IsAvailable { get; set; }

    }
}
