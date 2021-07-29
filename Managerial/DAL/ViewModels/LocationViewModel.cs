using DAL.Core.Helpers;
using DAL.Models;
using System;

namespace DAL.ViewModels
{
    public class LocationViewModel : BaseViewModel, IMapFrom<Location>
    {
        public string LocationRow { get; set; }
        public string locationColumn { get; set; }
        public string LocationShelf { get; set; }
    }
}