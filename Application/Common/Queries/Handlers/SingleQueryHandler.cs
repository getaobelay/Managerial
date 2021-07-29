using Application.Common.Queries.Requests;
using Application.Extensions;
using Application.Helpers;
using Application.ViewModels;
using Domain.Common;
using Infrastructure.Implementation;
using Infrastructure.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Queries.Handlers
{
    public class SingleQueryHandler<TEntity, TDto, TQuery> : IRequestHandler<TQuery, HandlerResponse<TDto>>
        where TEntity : AuditableEntity, new()
        where TDto : BaseViewModel, new()
        where TQuery : SingleQueryRequest<TEntity, TDto>, new()
    {
        private IDataRepository<TEntity, TDto> repository;

        public SingleQueryHandler(IDataContext context)
        {
            Context = context;
            repository = new DataRepository<TEntity, TDto>(Context.UnitOfWork);
        }

        public IDataContext Context { get; set; }

        public async Task<HandlerResponse<TDto>> Handle(TQuery request, CancellationToken cancellationToken)
        {
            var configurations = Mapping.Mapper.ConfigurationProvider;
            var result = await repository.SingleOrDefaultAsync(request.Filter, configurations);

            if (result != null)
            {
                return HandlerResponse.SingleResponse(Mapping.Mapper.Map<TDto>(result));
            }
            else
            {
                return HandlerResponse.NullResponse<TDto>(errorsMessages: new List<string>() { $"{nameof(TEntity)} With {request.Id} not Found" });
            }
        }
    }
}