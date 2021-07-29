using DAL.Core.Cqrs.Common.Queries.Responses;
using DAL.Models;
using MediatR;
using System;
using System.Linq.Expressions;

namespace DAL.Core.Cqrs.Common.Queries.Requests
{
    public class SingleQueryRequest<TEntity, TDto> : IRequest<SingleQueryResponse<TDto>>
       where TEntity : AuditableEntity, new()
        where TDto : BaseViewModel, new()
    {
        public int Id { get; set; }
        public Expression<Func<TEntity, bool>> Filter { get; set; }
    }
}