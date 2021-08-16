using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entites
{
    public class Batch : AuditableEntity
    {
        public string Key { get; set; }
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}