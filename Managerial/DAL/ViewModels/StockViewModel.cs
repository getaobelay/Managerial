using DAL.Models;
using System;
using System.Collections.Generic;
using DAL.Core.Helpers.ProductDtos;
using DAL.ViewModels.Interfaces;
using DAL.Core.Helpers;
using Managerial.ViewModels;

namespace DAL.ViewModels
{
    public class StockViewModel : IBaseStockViewModel, IMapFrom<Stock>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsQuanityAvailable { get; set; }
        public decimal ProductQuantity { get; set; }
        public decimal BatchQuantity { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
        public IEnumerable<BatchDto> Batches { get; set; }
        public IEnumerable<WarehouseViewModel> Warehouses { get; set; }

    }
}