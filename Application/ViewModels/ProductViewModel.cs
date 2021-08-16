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
        public decimal UnitsInStock { get; set; }
        public decimal ReorderLevel { get; set; }
        public ProductViewModel Parent { get; set; }
        public ProductCategoryViewModel ProductCategory { get; set; }
        public IEnumerable<ProductViewModel> Children { get; set; }
        public IEnumerable<OrderDetailViewModel> OrderDetails { get; set; }
        public IEnumerable<WarehouseItemViewModel> WarehouseItems { get; set; }
    }
}