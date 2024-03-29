﻿using DAL.Core.Cqrs.Common.Commands.Requests;
using DAL.Core.Cqrs.Common.Commands.Responses;
using DAL.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Core.Cqrs.Common.Commands.Handlers
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
        private IUnitOfWork<TEntity> _unitOfWork;

        public DeleteCommandHandler(IUnitOfWork<TEntity> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResponse<TDto>> Handle(TCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _unitOfWork.Generic.SingleOrDefaultAsync(p => p.Id == request.Id);

                if (entity == null)
                {
                    return new CommandResponse<TDto>(model: default, success: false)
                    {
                        ViewModel = default,
                        Success = false
                    };
                }

                _unitOfWork.Generic.Remove(entity);
                await _unitOfWork.SaveChangesAsync();

                return new CommandResponse<TDto>(model: default,
                    success: true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}