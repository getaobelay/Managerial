using AutoMapper;
using DAL.Models;

namespace DAL.Core.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Inventory, InventoryViewModel>().ReverseMap();
            CreateMap<Stock, StockViewModel>().ReverseMap();
            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<Customer, CustomerViewModel>().ReverseMap();
        }
    }
}