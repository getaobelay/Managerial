using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entites
{
    public class Warehouse : AuditableEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public int? WarehouseItemId { get; set; }
        public ICollection<WarehouseItem> WarehouseItems { get; set; }
    }
}