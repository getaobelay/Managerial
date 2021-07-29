using Application.Common.Commands.Requests;
using Application.Common.Commands.Responses;
using Application.ViewModels;
using Domain.Common;
using Infrastructure.Implementation;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Commands.Handlers
{
    /// <summary>
    /// Generic mediator handler creates a new record in the database
    /// </summary>
    /// <typeparam name="TEntity">The entity to insert into the database</typeparam>
    /// <typeparam name="TCommand">Type of ICommandWrapper command</typeparam>
    public class DeleteCommandHandler<TEntity, TDto, TCommand> : IRequestHandler<TCommand, CommandResponse<TDto>>
         where TEntity : AuditableEntity, new()
         where TDto : BaseViewModel, new()
         where TCommand : DeleteCommandRequest<TEntity, TDto>, new()
    {
        private DataRepository<TEntity, TDto> repository;

        public DeleteCommandHandler(IDataContext context)
        {
            repository = new DataRepository<TEntity, TDto>(context.UnitOfWork);
        }

        public async Task<CommandResponse<TDto>> Handle(TCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entityEntry = await repository.SingleOrDefaultAsync(p => p.Id == request.Id);

                if (entityEntry == null)
                {
                    return await Task.FromResult(CommandResponse.CommandFailed<TDto>(message: $"Record with id {request.Id} not found"));
                }

                repository.Context.Entry(entityEntry).State = EntityState.Detached;

                var result = await repository.DeleteAsync(entityEntry);
                await repository.UnitOfWork.SaveChangesAsync();

                if (result)
                {
                    return await Task.FromResult(CommandResponse.CommandExecuted<TDto>(message: "Recored Deleted", default));
                }

                return await Task.FromResult(CommandResponse.CommandFailed<TDto>(message: "Failed to delete record from the database"));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}