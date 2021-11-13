using Application.Interfaces;
using AutoMapper;
using Domain.Entites;
using Domain.Entites.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Application.ViewModels
{
    public class OrderViewModel: BaseViewModel, IMapFrom<Order>
    {
        public decimal Discount { get; set; }
        public string Comments { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public ApplicationUser Cashier { get; set; }
        public CustomerViewModel Customer { get; set; }
        public int TotalPrice { get; set; }
        public int TotalPriceAfterDiscount { get; set; }
        public int TotalItems { get; set; }

        public List<OrderDetailViewModel> OrderDetails { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderViewModel>()
                   .ForMember(o => o.CustomerName, p => p.MapFrom(o => o.Customer.Name))
                   .ForMember(o => o.CustomerNumber, p => p.MapFrom(o => o.Customer.PhoneNumber))
                   .ForMember(o => o.TotalPrice, p => p.MapFrom(o => o.OrderDetails.Sum(p => p.WarehouseItem.Product.SellingPrice) ))
                   .ForMember(o => o.TotalItems, p => p.MapFrom(o => o.OrderDetails.Count()))
                   .ReverseMap();
        }
    }
}