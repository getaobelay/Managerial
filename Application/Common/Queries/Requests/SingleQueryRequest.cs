using Application.Helpers;
using Application.ViewModels;
using Domain.Common;
using MediatR;
using System;
using System.Linq.Expressions;

namespace Application.Common.Queries.Requests
{
    public class SingleQueryRequest<TEntity, TDto> : IRequest<HandlerResponse<TDto>>
       where TEntity : AuditableEntity, new()
        where TDto : BaseViewModel, new()
    {
        public int Id { get; set; }
        public Expression<Func<TEntity, bool>> Filter { get; set; }
    }
}