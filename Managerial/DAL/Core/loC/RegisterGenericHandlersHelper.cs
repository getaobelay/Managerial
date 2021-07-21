using Autofac;
using DAL.Core.CommonCQRS;
using DAL.Core.CommonCQRS.Commands.Handlers;
using DAL.Core.CommonCQRS.Commands.Requests;
using DAL.Core.CommonCQRS.Commands.Responses;
using DAL.Core.CommonCQRS.Queries.Handlers;
using DAL.Core.CommonCQRS.Queries.Requests;
using DAL.Core.CommonCQRS.Queries.Responses;
using DAL.Core.Cqrs.Common.Commands.Handlers;
using DAL.Core.Cqrs.Common.Queries.Requests;
using DAL.Core.Helpers.BaseDtos;
using DAL.Models;
using MediatR;
using System.Collections.Generic;
using WarehouseAngularApp.Mediator.CommonCQRS.Commands.Handlers;

namespace DAL.Core.loC
{
    public static class RegisterGenericHandlersHelper
    {
        public static ContainerBuilder RegisterHandlers<TEntity, TDto>(this ContainerBuilder builder)
            where TEntity : AuditableEntity, new()
            where TDto : class, IBaseViewModel, new()

        {

            builder.RegisterType<CreateCommandHandler<TEntity, TDto, CreateCommandRequest<TEntity, TDto>>>()
                   .As<IRequestHandler<CreateCommandRequest<TEntity, TDto>, CommandResponse<TDto>>>();

            builder.RegisterType<DeleteCommandHandler<TEntity, TDto, DeleteCommandRequest<TEntity, TDto>>>()
                   .As<IRequestHandler<DeleteCommandRequest<TEntity, TDto>, CommandResponse<TDto>>>();

            builder.RegisterType<UpdateCommandHandler<TEntity, TDto, UpdateCommandRequest<TEntity, TDto>>>()
                   .As<IRequestHandler<UpdateCommandRequest<TEntity, TDto>, CommandResponse<TDto>>>();

            builder.RegisterType<ListQueryHandler<TEntity, TDto, ListQueryRequest<TEntity, TDto>>>()
                  .As<IRequestHandler<ListQueryRequest<TEntity, TDto>, ListQueryResponse<TDto>>>();

            builder.RegisterType<SingleQueryHandler<TEntity, TDto, SingleQueryRequest<TEntity, TDto>>>()
                   .As<IRequestHandler<SingleQueryRequest<TEntity, TDto>, SingleQueryResponse<TDto>>>();

            builder.RegisterAllProccessors<TEntity, TDto>();

            return builder;
        }

    }
}