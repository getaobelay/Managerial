using DAL;
using DAL.Core.CommonCQRS;
using DAL.Core.CommonCQRS.Queries.Requests;
using DAL.Core.CommonCQRS.Queries.Responses;
using DAL.Core.Helpers;
using DAL.Core.Helpers.BaseDtos;
using DAL.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Core.CommonCQRS.Queries.Handlers
{
    public class SingleQueryHandler<TEntity, TDto, TQuery> : IRequestHandler<TQuery, SingleQueryResponse<TDto>>
        where TEntity : AuditableEntity, new()
        where TDto : class, IBaseViewModel, new()
        where TQuery : SingleQueryRequest<TEntity, TDto>, new()
    {
        private readonly IUnitOfWork<TEntity> _unitOfWork;

        public SingleQueryHandler(IUnitOfWork<TEntity> unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<SingleQueryResponse<TDto>> Handle(TQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var entity = await _unitOfWork.Generic.GetAsync(request.Id);

                if (entity == null)
                {
                    return new SingleQueryResponse<TDto>
                    {
                        ViewModal = default,
                        Succes = false

                    };
                }



                return new SingleQueryResponse<TDto>
                {
                    ViewModal = MappingHelper.Mapper.Map<TDto>(entity),
                    Succes = true

                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}