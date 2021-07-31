using Application.Common.Commands.Requests;
using Application.Common.Commands.Responses;
using Application.Common.Proccessors;
using Application.ViewModels;
using Autofac;
using Domain.Common;
using MediatR;

namespace Application.Extensions.Containers.Proccessrs
{
    public static class RegisterCommandProccessorHelper
    {
        public static ContainerBuilder RegisterCommandProccessors<TEntity, TDto>(this ContainerBuilder builder)
     where TEntity : AuditableEntity, new()
     where TDto : BaseViewModel, new()
        {
            builder.RegisterType<CommandProccessor<CreateCommandRequest<TEntity, TDto>, CommandResponse<TDto>, TEntity, TDto>>()
              .As<IPipelineBehavior<CreateCommandRequest<TEntity, TDto>, CommandResponse<TDto>>>();

            builder.RegisterType<CommandProccessor<DeleteCommandRequest<TEntity, TDto>, CommandResponse<TDto>, TEntity, TDto>>()
                                .As<IPipelineBehavior<DeleteCommandRequest<TEntity, TDto>, CommandResponse<TDto>>>();

            builder.RegisterType<CommandProccessor<UpdateCommandRequest<TEntity, TDto>, CommandResponse<TDto>, TEntity, TDto>>()
                                .As<IPipelineBehavior<UpdateCommandRequest<TEntity, TDto>, CommandResponse<TDto>>>();
            return builder;
        }

        public static ContainerBuilder RegisterPreCommandProccessors<TEntity, TDto>(this ContainerBuilder builder)
  where TEntity : AuditableEntity, new()
  where TDto : BaseViewModel, new()
        {
            builder.RegisterType<CommandProccessor<CreateCommandRequest<TEntity, TDto>, CommandResponse<TDto>, TEntity, TDto>>()
              .As<IPipelineBehavior<CreateCommandRequest<TEntity, TDto>, CommandResponse<TDto>>>();

            builder.RegisterType<CommandProccessor<DeleteCommandRequest<TEntity, TDto>, CommandResponse<TDto>, TEntity, TDto>>()
                                .As<IPipelineBehavior<DeleteCommandRequest<TEntity, TDto>, CommandResponse<TDto>>>();

            builder.RegisterType<CommandProccessor<UpdateCommandRequest<TEntity, TDto>, CommandResponse<TDto>, TEntity, TDto>>()
                                .As<IPipelineBehavior<UpdateCommandRequest<TEntity, TDto>, CommandResponse<TDto>>>();
            return builder;
        }
    }
}