using Application.Common.Commands.Requests;
using Application.Common.Commands.Responses;
using Application.Common.Proccessors;
using Application.ViewModels;
using Autofac;
using Domain.Common;
using MediatR;

namespace Application.Extensions.Containers.Proccessrs
{
    public static class RegisterValidationProccessorsHelper
    {
        public static ContainerBuilder RegisterValidationProccessors<TEntity, TDto>(this ContainerBuilder builder)
     where TEntity : AuditableEntity, new()
     where TDto : BaseViewModel, new()
        {
            builder.RegisterType<CommandValidationProccessor<CreateCommandRequest<TEntity, TDto>, CommandResponse<TDto>, TEntity, TDto>>()
                                           .As<IPipelineBehavior<CreateCommandRequest<TEntity, TDto>, CommandResponse<TDto>>>();

            builder.RegisterType<CommandValidationProccessor<DeleteCommandRequest<TEntity, TDto>, CommandResponse<TDto>, TEntity, TDto>>()
                                       .As<IPipelineBehavior<DeleteCommandRequest<TEntity, TDto>, CommandResponse<TDto>>>();

            builder.RegisterType<CommandValidationProccessor<UpdateCommandRequest<TEntity, TDto>, CommandResponse<TDto>, TEntity, TDto>>()
                                       .As<IPipelineBehavior<UpdateCommandRequest<TEntity, TDto>, CommandResponse<TDto>>>();
            return builder;
        }
    }
}