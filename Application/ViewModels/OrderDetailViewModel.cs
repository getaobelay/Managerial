// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using Application.Interfaces;
using Domain.Entites;

namespace Application.ViewModels
{
    public class OrderDetailViewModel: BaseViewModel, IMapFrom<OrderDetail>
    {
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public ProductViewModel Product { get; set; }
        public OrderViewModel Order { get; set; }
    }
}