using Autofac;
using DAL.Core.CommonCQRS.Commands.Requests;
using DAL.Core.CommonCQRS.Commands.Responses;
using DAL.Core.Helpers.BaseDtos;
using DAL.Core.Proccessors;
using DAL.Models;
using MediatR;

namespace DAL.Core.loC
{
    public static class RegisterValidationProccessorsHelper
    {
        public static ContainerBuilder RegisterValidationProccessors<TEntity, TDto>(this ContainerBuilder builder)
     where TEntity : AuditableEntity, new()
     where TDto : class, IBaseViewModel, new()
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
