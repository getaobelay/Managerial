using Application.Interfaces;
using AutoMapper;
using Domain.Entites;
using Domain.Entites.Identity;
using System.Collections.Generic;

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
        public IEnumerable<OrderDetailViewModel> OrderDetails { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderViewModel>()
                   .ForMember(o => o.CustomerName, p => p.MapFrom(o => o.Customer.Name))
                   .ForMember(o => o.CustomerNumber, p => p.MapFrom(o => o.Customer.PhoneNumber))
                   .ReverseMap(); ;
        }
    }
}