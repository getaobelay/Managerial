using Application.Common.Commands.Requests;
using Application.Common.Commands.Responses;
using Application.ViewModels;
using Domain.Common;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Proccessors
{
    public class CommandProccessor<TRequest, TResponse, TEntity, TDto> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : BaseCommandRequest<TEntity, TDto>
        where TResponse : BaseCommandResponse<TDto>
        where TEntity : AuditableEntity, new()
        where TDto : BaseViewModel, new()
    {
        private readonly ILogger<CommandProccessor<TRequest, TResponse, TEntity, TDto>> _logger;

        public CommandProccessor(ILogger<CommandProccessor<TRequest, TResponse, TEntity, TDto>> logger, IDataContext context)
        {
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
            Context = context;
        }

        public IDataContext Context { get; }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            TResponse response = default;

            try
            {
                if (Context.CurrentUser.IsAuthenticated())
                {
                    var username = Context.CurrentUser.GetUsername();

                    if (request is CreateCommandRequest<TEntity, TDto> createCommand)
                    {
                        createCommand.CreateObject.CreatedBy = username;
                        createCommand.CreateObject.CreatedDate = DateTime.UtcNow;
                        createCommand.CreateObject.UpdatedBy = username;
                        createCommand.CreateObject.UpdatedDate = DateTime.UtcNow;

                        response = await next();
                        return response;
                    }
                    else if (request is UpdateCommandRequest<TEntity, TDto> updateCommand)
                    {
                        updateCommand.UpdatedObject.UpdatedBy = username;
                        updateCommand.UpdatedObject.UpdatedDate = DateTime.UtcNow;

                        response = await next();
                        return response;
                    }
                    else
                    {
                        response = await next();
                        return response;
                    }
                }

                response.Error = true;
                response.ErrorMessages.Add("User is not authenticated");
                return response;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Rollback transaction executed {typeof(TRequest).Name}");
                _logger.LogError(e.Message, e.StackTrace);
                throw;
            }
        }
    }
}