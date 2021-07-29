using Domain.Common;

namespace Domain.Entites
{
    public class Category : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}