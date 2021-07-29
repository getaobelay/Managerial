using Application.Interfaces;
using Domain.Entites;

namespace Application.ViewModels
{
    public class ProductViewModel : BaseViewModel, IMapFrom<Product>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string Measurement { get; set; }
        public decimal QuantityPerUnit { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public bool IsActive { get; set; }
        public string categoryName { get; set; }
        public ProductCategoryViewModel productCategory { get; set; }
    }
}