using DAL.Core.CommonCQRS.Commands.Responses;
using DAL.Core.Helpers.BaseDtos;
using DAL.Models;
using MediatR;

namespace DAL.Core.CommonCQRS.Commands.Requests
{
    public interface ICommandRequest<TEntity, TDto> : IRequest<CommandResponse<TDto>>
        where TEntity : AuditableEntity, new()
        where TDto : class, IBaseViewModel, new()
    {
    }
}
