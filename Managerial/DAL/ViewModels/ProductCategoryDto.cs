using System;
using DAL.Models;
using DAL.Core.Helpers.BaseDtos;

namespace DAL.Core.Helpers.ProductDtos
{
    public class ProductCategoryDto : IBaseViewModel, IMapFrom<ProductCategory>
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