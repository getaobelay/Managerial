using DAL.Core.Helpers;
using DAL.Models;

namespace DAL.ViewModels
{
    public class CategoryViewModel: BaseViewModel, IMapFrom<Category>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}