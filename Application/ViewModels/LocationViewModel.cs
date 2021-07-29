using Application.Interfaces;
using Domain.Entites;

namespace Application.ViewModels
{
    public class LocationViewModel : BaseViewModel, IMapFrom<Location>
    {
        public string LocationRow { get; set; }
        public string locationColumn { get; set; }
        public string LocationShelf { get; set; }
    }
}