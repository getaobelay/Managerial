using AutoMapper;
using DAL.Core.Helpers.InventoryViewModels;
using DAL.Core.ViewModels;
using DAL.Models;
using DAL.ViewModels;
using Managerial.ViewModels;

namespace ADAL.Core.Helpers.MappingProfiles
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