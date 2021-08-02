using Application.Interfaces;
using Domain.Entites;

namespace Application.ViewModels
{
    public class WarehouseItemViewModel : BaseViewModel, IMapFrom<WarehouseItem>
    {
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int AlloactionId { get; set; }
        public int LocationId { get; set; }
        public ProductViewModel Product { get; set; }
        public WarehouseViewModel Warehouse { get; set; }
        public AllocationViewModel Allocation { get; set; }
        public LocationViewModel Location { get; set; }
        public BatchViewModel Batch { get; set; }
    }
}