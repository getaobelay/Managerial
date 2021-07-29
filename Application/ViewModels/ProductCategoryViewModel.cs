using Application.Interfaces;
using Domain.Entites;

namespace Application.ViewModels
{
    public class ProductCategoryViewModel : BaseViewModel, IMapFrom<ProductCategory>
    {
        public CategoryViewModel CategoryViewModel { get; set; }
    }
}