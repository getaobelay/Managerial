using Application.ViewModels;
using Domain.Common;

namespace Application.Common.Commands.Requests
{
    public abstract class BaseCommandRequest<TEntity, TDto> : ICommandRequest<TEntity, TDto>
      where TEntity : AuditableEntity, new()
      where TDto : BaseViewModel, new()
    {
    }
}