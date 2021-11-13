using Domain.Common;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class Allocation : AuditableEntity
    {
        public string Comments { get; set; }
        public int? WarehouseItemId { get; set; }
        public int? OrderDetailId { get; set; }
        public WarehouseItem WarehouseItem { get; set; }
        public OrderDetail OrderDetail { get; set; }
        public bool IsAllocated { get; set; }
        public bool IsAvailable { get; set; }

    }
}
