using DAL.Core.Helpers;
using DAL.Models;
using System;

namespace DAL.ViewModels
{
    public class LocationViewModel : BaseViewModel, IMapFrom<Location>
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string LocationRow { get; set; }
        public string LocationColum { get; set; }
        public string LocationShelf { get; set; }
    }
}