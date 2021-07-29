using Application.Common.Commands.Requests;
using Application.Common.Commands.Responses;
using Application.Extensions;
using Application.ViewModels;
using Domain.Common;
using Infrastructure.Implementation;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Commands.Handlers
{
    /// <summary>
    /// Generic mediator handler creates a new record in the database
    /// </summary>
    /// <typeparam name="TEntity">The entity to insert into the database</typeparam>
    /// <typeparam name="TRequest">Type of ICommandWrapper command</typeparam>
    public class CreateCommandHandler<TEntity, TDto, TRequest> : IRequestHandler<TRequest, CommandResponse<TDto>>
      where TEntity : AuditableEntity, new()
      where TDto : BaseViewModel, new()
    where TRequest : CreateCommandRequest<TEntity, TDto>, new()
    {
        private DataRepository<TEntity, TDto> repository;

        public CreateCommandHandler(IDataContext context)
        {
            repository = new DataRepository<TEntity, TDto>(context.UnitOfWork);
        }

        public async Task<CommandResponse<TDto>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var mappedInsertObject = Mapping.Mapper.Map<TEntity>(request.CreateObject);

                var result = await repository.InsertAsync(mappedInsertObject);
                if (result != null)
                {
                    await repository.UnitOfWork.SaveChangesAsync();
                    var mapped = Mapping.Mapper.Map<TDto>(result);
                    return await Task.FromResult(CommandResponse.CommandExecuted(message: "Recored Created", mapped));
                }

                return await Task.FromResult(CommandResponse.CommandFailed<TDto>(message: "Failed to insert record into the database"));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}