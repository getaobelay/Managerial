﻿using Application.Interfaces;
using Domain.Entites;
using System;
using System.Collections.Generic;

namespace Application.ViewModels
{
    public class StockViewModel :BaseViewModel, IBaseStockViewModel, IMapFrom<Stock>
    {
        public string Name { get; set; }
        public bool IsQuanityAvailable { get; set; }
        public decimal ProductQuantity { get; set; }
        public decimal BatchQuantity { get; set; }
  
        public IEnumerable<ProductViewModel> Products { get; set; }
        public IEnumerable<BatchViewModel> Batches { get; set; }
        public IEnumerable<WarehouseViewModel> Warehouses { get; set; }
    }
}