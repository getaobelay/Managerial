using DAL.Models;

namespace DAL.Core.Cqrs.Common.Commands.Requests
{
    /// <summary>
    /// this command deletes source and destination entities
    /// </summary>
    /// <typeparam name="TEntity">The entity to insert into the database</typeparam>
    /// <typeparam name="TDto">The source dto to map result from</typeparam>
    public class DeleteCommandRequest<TEntity, TDto> : BaseCommandRequest<TEntity, TDto>
      where TEntity : AuditableEntity, new()
      where TDto : BaseViewModel, new()
    {
        public int Id { get; set; }
    }
}