using DAL.Core.CommonCQRS.Queries.Responses;
using DAL.Core.Helpers.BaseDtos;
using DAL.Models;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Core.Cqrs.Common.Queries.Requests
{
    public class ListQueryRequest<TEntity, TDto> : IRequest<ListQueryResponse<TDto>>
       where TEntity : AuditableEntity, new()
        where TDto : class, IBaseViewModel, new()
    {
    }
}