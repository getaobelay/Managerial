using Application.Interfaces;
using Domain.Entites;
using System.Collections.Generic;

namespace Application.ViewModels
{
    public class ProductViewModel : BaseViewModel, IMapFrom<Product>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public bool IsActive { get; set; }
        public ProductViewModel Product { get; set; }
        public ProductCategoryViewModel productCategory { get; set; }
        public IEnumerable<ProductViewModel> Children { get; set; }
        public IEnumerable<OrderDetailViewModel> OrderDetails { get; set; }
        public StockViewModel Stock { get; set; }
        public InventoryViewModel Inventory { get; set; }
        public List<WarehouseItem> WarehouseItems { get; set; }
        public List<Batch> Batches { get; set; }
    }
}