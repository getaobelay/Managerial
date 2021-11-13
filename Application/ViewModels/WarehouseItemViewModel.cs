using Application.Interfaces;
using AutoMapper;
using Domain.Entites;

namespace Application.ViewModels
{
    public class WarehouseItemViewModel : BaseViewModel, IMapFrom<WarehouseItem>
    {

        public string ProductName { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string WarehouseName { get; set; }
        public string Location { get; set; }
        public ProductViewModel Product { get; set; }
        public WarehouseViewModel Warehouse { get; set; }
        public AllocationViewModel Allocation { get; set; } 
        public void Mapping(Profile profile)
        {
            profile.CreateMap<WarehouseItem, WarehouseItemViewModel>()
                   .ForMember(o => o.ProductName, p => p.MapFrom(o => o.Product.Name))
                   .ForMember(o => o.BuyingPrice, p => p.MapFrom(o => o.Product.BuyingPrice))
                   .ForMember(o => o.SellingPrice, p => p.MapFrom(o => o.Product.SellingPrice))
                   .ForMember(o => o.WarehouseName, p => p.MapFrom(o => o.Warehouse.Name))
                   .ReverseMap();
        }
    }
}