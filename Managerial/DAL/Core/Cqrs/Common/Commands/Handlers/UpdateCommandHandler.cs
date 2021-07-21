using DAL.Core.Cqrs.Common.Commands.Requests;
using DAL.Core.Cqrs.Common.Commands.Responses;
using DAL.Core.Helpers;
using DAL.Models;
using DAL.ViewModels.Interfaces;
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
        where TDto : class, IBaseViewModel, new()
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
                var entity = await _unitOfWork.Generic.SingleOrDefaultAsync(p => p.Id == request.Id);

                if (entity == null)
                {
                    return new CommandResponse<TDto>(model: default, success: false)
                    {
                        ViewModel = default,
                        Success = false
                    };
                }

                _unitOfWork.Generic.Update(entity);
                await _unitOfWork.SaveChangesAsync();

                return new CommandResponse<TDto>(model: MappingHelper.Mapper.Map<TDto>(entity),
                    success: true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}