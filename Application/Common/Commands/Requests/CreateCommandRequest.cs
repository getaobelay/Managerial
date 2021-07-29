using Application.ViewModels;
using Domain.Common;

namespace Application.Common.Commands.Requests
{
    /// <summary>
    /// this command creates source and destination entities
    /// </summary>
    /// <typeparam name="TEntity">The entity to insert into the database</typeparam>
    /// <typeparam name="TDto">The entity ViewModel to map result from</typeparam>
    public class CreateCommandRequest<TEntity, TDto> : BaseCommandRequest<TEntity, TDto>
       where TEntity : AuditableEntity, new()
      where TDto : BaseViewModel, new()
    {
        public TDto CreateObject { get; set; }
    }
}