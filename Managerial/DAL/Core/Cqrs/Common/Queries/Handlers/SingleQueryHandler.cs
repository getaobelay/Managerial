using DAL.Core.Cqrs.Common.Queries.Requests;
using DAL.Core.Cqrs.Common.Queries.Responses;
using DAL.Core.Helpers;
using DAL.Models;
using DAL.ViewModels;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Core.Cqrs.Common.Queries.Handlers
{
    public class SingleQueryHandler<TEntity, TDto, TQuery> : IRequestHandler<TQuery, SingleQueryResponse<TDto>>
        where TEntity : AuditableEntity, new()
        where TDto : BaseViewModel, new()
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