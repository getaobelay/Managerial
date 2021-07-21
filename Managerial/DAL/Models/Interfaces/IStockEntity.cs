using System.Collections.Generic;

namespace DAL.Models.Interfaces
{
    public interface IStockEntity : IAuditableEntity
    {
        public string Name { get; set; }
        public bool IsQuanityAvailable { get; set; }
        public decimal TotalUnitsQuantity { get; set; }
        public decimal ProductQuantity { get; set; }
        public decimal BatchQuantity { get; set; }
        public int? ProductId { get; set; }
        public int? BatchId { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<Batch> Batches { get; set; }
    }
}