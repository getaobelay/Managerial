using DAL.Core.Cqrs.Common.Queries.Responses;
using DAL.Models;
using MediatR;

namespace DAL.Core.Cqrs.Common.Queries.Requests
{
    public class ListQueryRequest<TEntity, TDto> : IRequest<ListQueryResponse<TDto>>
       where TEntity : AuditableEntity, new()
        where TDto : BaseViewModel, new()
    {
    }
}