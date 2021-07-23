using DAL.Core.Helpers;
using DAL.Models;
using System;

namespace DAL.ViewModels
{
    public class ProductCategoryDto : BaseViewModel, IMapFrom<ProductCategory>
    {
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public CategoryViewModel CategoryViewModel { get;set;}
   
    }
}