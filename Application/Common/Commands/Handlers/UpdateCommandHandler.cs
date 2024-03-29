﻿using Application.Common.Commands.Requests;
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
    /// <typeparam name="TCommand">Type of ICommandWrapper command</typeparam>
    public class UpdateCommandHandler<TEntity, TDto, TCommand> : IRequestHandler<TCommand, CommandResponse<TDto>>
       where TEntity : AuditableEntity, new()
        where TDto : BaseViewModel, new()
        where TCommand : UpdateCommandRequest<TEntity, TDto>, new()
    {
        private DataRepository<TEntity, TDto> repository;

        public UpdateCommandHandler(IDataContext context)
        {
            repository = new DataRepository<TEntity, TDto>(context.UnitOfWork);
        }

        [Obsolete]
        public async Task<CommandResponse<TDto>> Handle(TCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var configurations = Mapping.Mapper.ConfigurationProvider;
                var entityEntry = await repository.SingleOrDefaultAsync(p => p.Id == request.Id);

                if (entityEntry == null)
                {
                    return await Task.FromResult(CommandResponse.CommandFailed<TDto>(message: $"Record with id {request.Id} not found"));
                }

                var mappedObject = Mapping.Mapper.Map<TEntity>(request.UpdatedObject);
                mappedObject.Id = entityEntry.Id;

                var result = await repository.UpdateAsync(mappedObject, entityEntry);
                await repository.UnitOfWork.SaveChangesAsync();

                if (result != null)
                {
                    var mapped = Mapping.Mapper.Map<TDto>(result);
                    return await Task.FromResult(CommandResponse.CommandExecuted(message: "Recored Updated", mapped));
                }

                return await Task.FromResult(CommandResponse.CommandFailed<TDto>(message: "Failed to updated record from the database"));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}