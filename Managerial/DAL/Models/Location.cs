using System.Collections.Generic;

namespace DAL.Models
{
    public class Location : AuditableEntity
    {
        public string LocationRow { get; set; }
        public string locationColumn { get; set; }
        public string LocationShelf { get; set; }
        public int? WarehouseID { get; set; }
        public int? WarehouseItemID { get; set; }

        public virtual ICollection<Warehouse> Warehouses { get; set; }
        public virtual ICollection<WarehouseItem> WarehouseItems { get; set; }
    }
}