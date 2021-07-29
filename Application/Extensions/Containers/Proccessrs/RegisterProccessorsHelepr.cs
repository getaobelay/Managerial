using Application.Extensions.Containers.Validators;
using Application.ViewModels;
using Autofac;
using Domain.Common;

namespace Application.Extensions.Containers.Proccessrs
{
    public static class RegisterProccessorsHelepr
    {
        public static ContainerBuilder RegisterAllProccessors<TEntity, TDto>(this ContainerBuilder builder)
        where TEntity : AuditableEntity, new()
        where TDto : BaseViewModel, new()
        {
            builder.RegisterValidators<TEntity, TDto>();
            builder.RegisterValidationProccessors<TEntity, TDto>();
            builder.RegisterCommandProccessors<TEntity, TDto>();

            return builder;
        }
    }
}