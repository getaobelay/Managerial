using Application.Interfaces;
using Domain.Entites;
using System.Collections.Generic;

namespace Application.ViewModels
{
    public class WarehouseViewModel : BaseViewModel, IMapFrom<Warehouse>
    {

        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public int? WarehouseItemId { get; set; }
        public IEnumerable<WarehouseItemViewModel> WarehouseItems { get; set; }
    }
}