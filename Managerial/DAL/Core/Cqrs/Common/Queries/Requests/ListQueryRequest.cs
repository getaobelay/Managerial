﻿using DAL.Core.Cqrs.Common.Queries.Responses;
using DAL.Models;
using DAL.ViewModels.Interfaces;
using MediatR;

namespace DAL.Core.Cqrs.Common.Queries.Requests
{
    public class ListQueryRequest<TEntity, TDto> : IRequest<ListQueryResponse<TDto>>
       where TEntity : AuditableEntity, new()
        where TDto : class, IBaseViewModel, new()
    {
    }
}