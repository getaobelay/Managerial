﻿using AutoMapper;
using DAL.Core.CommonCQRS.Queries.Responses;
using DAL.Core.Cqrs.Common.Queries.Requests;
using DAL.Core.Helpers;
using DAL.Core.Helpers.BaseDtos;
using DAL.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Core.CommonCQRS.Queries.Handlers
{
    public class ListQueryHandler<TEntity, TDto, TQuery> : IRequestHandler<TQuery, ListQueryResponse<TDto>>
        where TEntity : AuditableEntity, new()
        where TDto : class, IBaseViewModel, new()
        where TQuery : ListQueryRequest<TEntity, TDto>, new()
    {
        private readonly IUnitOfWork<TEntity> _unitOfWork;

        public ListQueryHandler(IUnitOfWork<TEntity> unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<ListQueryResponse<TDto>> Handle(TQuery request, CancellationToken cancellationToken)
        {

            var result = await _unitOfWork.Generic.GetAllAsync();
            return new ListQueryResponse<TDto>
            {
                Dtos = MappingHelper.Mapper.Map<IEnumerable<TDto>>(
             await _unitOfWork.Generic.GetAllAsync()
             )
            };


        }
    }
}