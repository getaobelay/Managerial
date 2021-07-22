using Autofac;
using DAL.Models;
using DAL.ViewModels;

namespace DAL.Core.loC
{
    public static class RegisterGenericProccessorsHelepr
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