using Application.Helpers;
using Application.ViewModels;
using Domain.Common;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Application.Common.Queries.Requests
{
    public class ListQueryRequest<TEntity, TDto> : IRequest<HandlerResponse<TDto>>
           where TEntity : AuditableEntity, new()
      where TDto : BaseViewModel, new()
    {
        public Expression<Func<TEntity, bool>> Filter { get; set; }
        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; set; }
    }
}