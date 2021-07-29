using Application.Common.Commands.Responses;
using Application.ViewModels;
using Domain.Common;
using MediatR;

namespace Application.Common.Commands.Requests
{
    public interface ICommandRequest<TEntity, TDto> : IRequest<CommandResponse<TDto>>
              where TEntity : AuditableEntity, new()
        where TDto : BaseViewModel, new()
    {
    }
}