using DAL.Core.Helpers.BaseDtos;
using DAL.Models;

namespace DAL.Core.CommonCQRS.Commands.Requests
{
    /// <summary>
    /// this command creates source and destination entities
    /// </summary>
    /// <typeparam name="TEntity">The entity to insert into the database</typeparam>
    /// <typeparam name="TDto">The source dto to map result from</typeparam>
    public class UpdateCommandRequest<TEntity, TDto> : BaseCommandRequest<TEntity, TDto>
      where TEntity : AuditableEntity, new()
      where TDto : class, IBaseViewModel, new()
    {
        public int Id { get; set; }
        public TDto UpdatedObject { get; set; }
    }
}