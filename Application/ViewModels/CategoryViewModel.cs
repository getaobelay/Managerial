using Application.Interfaces;
using Domain.Entites;

namespace Application.ViewModels
{
    public class CategoryViewModel : BaseViewModel, IMapFrom<Category>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}