using DAL.Models;
using DAL.ViewModels;

namespace DAL.Core.Cqrs.Common.Commands.Requests
{
    public abstract class BaseCommandRequest<TEntity, TDto> : ICommandRequest<TEntity, TDto>
        where TEntity : AuditableEntity, new()
        where TDto : BaseViewModel, new()
    {
        protected BaseCommandRequest()
        {
        }
    }
}