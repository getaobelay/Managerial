using Application.Common.Commands.Handlers;
using Application.Common.Commands.Requests;
using Application.Common.Commands.Responses;
using Application.Common.Queries.Handlers;
using Application.Common.Queries.Requests;
using Application.Extensions.Containers.Proccessrs;
using Application.Helpers;
using Application.ViewModels;
using Autofac;
using Domain.Common;
using MediatR;

namespace Application.Extensions.Containers.Handlers
{
    public static class RegisterHandlersHelper
    {
        public static ContainerBuilder RegisterHandlers<TEntity, TDto>(this ContainerBuilder builder)
      where TEntity : AuditableEntity, new()
         where TDto : BaseViewModel, new()

        {
            builder.RegisterType<CreateCommandHandler<TEntity, TDto, CreateCommandRequest<TEntity, TDto>>>()
                   .As<IRequestHandler<CreateCommandRequest<TEntity, TDto>, CommandResponse<TDto>>>();

            builder.RegisterType<DeleteCommandHandler<TEntity, TDto, DeleteCommandRequest<TEntity, TDto>>>()
                   .As<IRequestHandler<DeleteCommandRequest<TEntity, TDto>, CommandResponse<TDto>>>();

            builder.RegisterType<UpdateCommandHandler<TEntity, TDto, UpdateCommandRequest<TEntity, TDto>>>()
                   .As<IRequestHandler<UpdateCommandRequest<TEntity, TDto>, CommandResponse<TDto>>>();

            builder.RegisterType<SingleQueryHandler<TEntity, TDto, SingleQueryRequest<TEntity, TDto>>>()
                   .As<IRequestHandler<SingleQueryRequest<TEntity, TDto>, HandlerResponse<TDto>>>();

            builder.RegisterType<ListQueryHandler<TEntity, TDto, ListQueryRequest<TEntity, TDto>>>()
                   .As<IRequestHandler<ListQueryRequest<TEntity, TDto>, HandlerResponse<TDto>>>();

            builder.RegisterAllProccessors<TEntity, TDto>();

            return builder;
        }
    }
}