using DAL.Core.Helpers.BaseDtos;
using DAL.Models;

namespace DAL.Core.CommonCQRS.Commands.Requests
{
    public abstract class BaseCommandRequest<TEntity, TDto> : ICommandRequest<TEntity, TDto>
        where TEntity : AuditableEntity, new()
        where TDto : class, IBaseViewModel, new()
    {
    }

}
