﻿// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Core.Helpers;
using DAL.Models;
using DAL.ViewModels.Interfaces;
using System;
using DAL.Core.Helpers;

namespace DAL.ViewModels
{
    public class ProductViewModel : IBaseViewModel, IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int UnitsInStock { get; set; }
        public bool IsActive { get; set; }
        public bool IsDiscontinued { get; set; }
        public string ProductCategoryName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}