// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entites
{
    public class ProductCategory : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}