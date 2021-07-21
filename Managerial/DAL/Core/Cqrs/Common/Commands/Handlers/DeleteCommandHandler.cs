using DAL;
using DAL.Core.CommonCQRS.Commands.Requests;
using DAL.Core.CommonCQRS.Commands.Responses;
using DAL.Core.Helpers;
using DAL.Core.Helpers.BaseDtos;
using DAL.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WarehouseAngularApp.Mediator.CommonCQRS.Commands.Handlers
{
    /// <summary>
    /// Generic mediator handler creates a new record in the database
    /// </summary>
    /// <typeparam name="TEntity">The entity to insert into the database</typeparam>
    /// <typeparam name="TCommand">Type of ICommandWrapper command</typeparam>
    public class DeleteCommandHandler<TEntity, TDto, TCommand> : IRequestHandler<TCommand, CommandResponse<TDto>>
         where TEntity : AuditableEntity, new()
         where TDto : class, IBaseViewModel, new()
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