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
        public IEnumerable<WarehouseItemViewModel> WarehouseItems { get; set; }
        public IEnumerable<LocationViewModel> Locations { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
        public IEnumerable<BatchViewModel> Batches { get; set; }
    }
}