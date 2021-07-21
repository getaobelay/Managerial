using DAL.Core.CommonCQRS;
using DAL.Core.CommonCQRS.Queries.Responses;
using DAL.Core.Helpers.BaseDtos;
using DAL.Models;
using MediatR;
using System;
using System.Linq.Expressions;

namespace DAL.Core.CommonCQRS.Queries.Requests
{
    public class SingleQueryRequest<TEntity, TDto> : IRequest<SingleQueryResponse<TDto>>
       where TEntity : AuditableEntity, new()
        where TDto : class, IBaseViewModel, new()
    {
        public int Id { get; set; }
        public Expression<Func<TEntity, bool>> Filter { get; set; }
    }
}