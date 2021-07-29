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
    public class ListQueryHandler<TEntity, TDto, TQuery> : IRequestHandler<TQuery, HandlerResponse<TDto>>
        where TEntity : AuditableEntity, new()
        where TDto : BaseViewModel, new()
        where TQuery : ListQueryRequest<TEntity, TDto>, new()
    {
        private IBaseRepository<TEntity, TDto> repository;

        public ListQueryHandler(IDataContext context)
        {
            Context = context;
            repository = new DataRepository<TEntity, TDto>(Context.UnitOfWork);
        }

        public IDataContext Context { get; set; }

        public async Task<HandlerResponse<TDto>> Handle(TQuery request, CancellationToken cancellationToken)
        {
            var configurations = Mapping.Mapper.ConfigurationProvider;
            var result = await repository.GetAllAsync(configurations);

            return HandlerResponse.ListResponse(Mapping.Mapper.Map<IEnumerable<TDto>>(result));
        }
    }
}