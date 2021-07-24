// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Core.Helpers;
using DAL.Models;
using System;

namespace DAL.ViewModels
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

        public ProductCategoryViewModel productCategory { get; set; }
    }
}