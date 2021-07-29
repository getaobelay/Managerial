using DAL.Core.Cqrs.Common.Commands.Responses;
using DAL.Models;
using MediatR;

namespace DAL.Core.Cqrs.Common.Commands.Requests
{
    public interface ICommandRequest<TEntity, TDto> : IRequest<CommandResponse<TDto>>
        where TEntity : AuditableEntity, new()
        where TDto : BaseViewModel, new()
    {
    }
}