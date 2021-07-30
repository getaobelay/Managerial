using Application.Interfaces;
using Domain.Entites;

namespace Application.ViewModels
{
    public class ProductCategoryViewModel : BaseViewModel, IMapFrom<ProductCategory>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}