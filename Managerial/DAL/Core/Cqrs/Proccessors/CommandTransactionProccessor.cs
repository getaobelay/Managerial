using DAL.Core.Cqrs.Common.Commands.Requests;
using DAL.Core.Cqrs.Common.Commands.Responses;
using DAL.Models;
using DAL.ViewModels.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Core.Cqrs.Proccessors
{
    public class CommandTransactionProccessor<TRequest, TResponse, TEntity, TDto> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : BaseCommandRequest<TEntity, TDto>
        where TResponse : BaseCommandResponse<TDto>
        where TEntity : AuditableEntity, new()
        where TDto : class, IBaseViewModel, new()
    {
        private IUnitOfWork<TEntity> _unitOfWork;

        private readonly ILogger<CommandTransactionProccessor<TRequest, TResponse, TEntity, TDto>> _logger;

        public CommandTransactionProccessor(
            ILogger<CommandTransactionProccessor<TRequest, TResponse, TEntity, TDto>> logger,
            IUnitOfWork<TEntity> unitOfWork)
        {
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            TResponse response = default;

            try
            {
                _logger.LogInformation($"Request from {typeof(TRequest).Name}");

                response = await next();

                _logger.LogInformation($"Committed at {typeof(TRequest).Name}");

                return response;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Failed request at {typeof(TRequest).Name}");

                _logger.LogError(e.Message, e.StackTrace);

                throw;
            }
        }
    }
}