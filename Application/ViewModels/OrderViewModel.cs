// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using Application.Interfaces;
using Domain.Entites;
using Domain.Entites.Identity;
using System.Collections.Generic;

namespace Application.ViewModels
{
    public class OrderViewModel: BaseViewModel, IMapFrom<Order>
    {
        public decimal Discount { get; set; }
        public string Comments { get; set; }
        public CustomerViewModel Customer { get; set; }
        public IEnumerable<AllocationViewModel> Allocations { get; set; }
        public IEnumerable<OrderDetailViewModel> OrderDetails { get; set; }
    }
}