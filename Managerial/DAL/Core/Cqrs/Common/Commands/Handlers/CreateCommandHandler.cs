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
    /// <typeparam name="TRequest">Type of ICommandWrapper command</typeparam>
    public class CreateCommandHandler<TEntity, TDto, TRequest> : IRequestHandler<TRequest, CommandResponse<TDto>>
    where TEntity : AuditableEntity, new()
    where TDto : BaseViewModel, new()
    where TRequest : CreateCommandRequest<TEntity, TDto>, new()
    {
        private IUnitOfWork<TEntity> _unitOfWork;

        public CreateCommandHandler(IUnitOfWork<TEntity> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResponse<TDto>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var mappedInsertObject = MappingHelper.Mapper.Map<TEntity>(request.CreateObject);
                await _unitOfWork.Generic.AddAsync(mappedInsertObject);
                await _unitOfWork.SaveChangesAsync();

                return new CommandResponse<TDto>(model: MappingHelper.Mapper.Map<TDto>(mappedInsertObject),
                    success: true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}