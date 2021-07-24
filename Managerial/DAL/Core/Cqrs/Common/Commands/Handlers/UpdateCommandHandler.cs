using DAL.Core.Cqrs.Common.Commands.Requests;
using DAL.Core.Cqrs.Common.Commands.Responses;
using DAL.Core.Helpers;
using DAL.Models;
using DAL.ViewModels;
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
    public class UpdateCommandHandler<TEntity, TDto, TCommand> : IRequestHandler<TCommand, CommandResponse<TDto>>
       where TEntity : AuditableEntity, new()
        where TDto : BaseViewModel, new()
        where TCommand : UpdateCommandRequest<TEntity, TDto>, new()
    {
        private IUnitOfWork<TEntity> _unitOfWork;

        public UpdateCommandHandler(IUnitOfWork<TEntity> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResponse<TDto>> Handle(TCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entityEntry = await _unitOfWork.Generic.SingleOrDefaultAsync(p => p.Id == request.Id);

                if (entityEntry == null)
                {
                    return new CommandResponse<TDto>(model: default, success: false)
                    {
                        ViewModel = default,
                        Success = false
                    };
                }


                var mappedObject = MappingHelper.Mapper.Map<TEntity>(request.UpdatedObject);
                mappedObject.Id = entityEntry.Id;
                _unitOfWork.Generic.Update(mappedObject, entityEntry);
                await _unitOfWork.SaveChangesAsync();
                return new CommandResponse<TDto>(model: MappingHelper.Mapper.Map<TDto>(entityEntry),
                    success: true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}