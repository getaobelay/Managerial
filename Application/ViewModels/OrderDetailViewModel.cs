using Application.Interfaces;
using AutoMapper;
using Domain.Entites;

namespace Application.ViewModels
{
    public class OrderDetailViewModel : BaseViewModel, IMapFrom<OrderDetail>
    {

        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }

        public ProductViewModel Product { get; set; }
        public OrderViewModel Order { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderDetail, OrderDetailViewModel>()
                   .ForMember(o => o.ProductName, p => p.MapFrom(o => o.WarehouseItem.Product.Name))
                   .ForMember(o => o.ProductDesc, p => p.MapFrom(o => o.WarehouseItem.Product.Description))
                   .ForMember(o => o.BuyingPrice, p => p.MapFrom(o => o.WarehouseItem.Product.BuyingPrice))
                   .ForMember(o => o.SellingPrice, p => p.MapFrom(o => o.WarehouseItem.Product.SellingPrice))
                   .ReverseMap(); ;
        }
    }
}