using Application.ViewModels;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IBaseStockViewModel : IBaseViewModel
    {
        public string Name { get; set; }
        public bool IsQuanityAvailable { get; set; }
        public decimal ProductQuantity { get; set; }
        public decimal BatchQuantity { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
        public IEnumerable<BatchViewModel> Batches { get; set; }
        public IEnumerable<WarehouseViewModel> Warehouses { get; set; }
    }
}