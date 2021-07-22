using DAL.Core.Helpers;
using DAL.Models;
using System;

namespace DAL.ViewModels
{
    public class ProductCategoryDto : BaseViewModel, IMapFrom<ProductCategory>
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}