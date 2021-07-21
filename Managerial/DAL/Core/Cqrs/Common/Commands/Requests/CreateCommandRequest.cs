using DAL.Core.Helpers.BaseDtos;
using DAL.Models;

namespace DAL.Core.CommonCQRS.Commands.Requests
{
    /// <summary>
    /// this command creates source and destination entities
    /// </summary>
    /// <typeparam name="TEntity">The entity to insert into the database</typeparam>
    /// <typeparam name="TDto">The entity dto to map result from</typeparam>
    public class CreateCommandRequest<TEntity, TDto> : BaseCommandRequest<TEntity, TDto>
       where TEntity : AuditableEntity, new()
      where TDto : class, IBaseViewModel, new()
    {
        public TDto CreateObject { get; set; }
    }

}