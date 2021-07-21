using Autofac;
using DAL.Core.CommonCQRS.Commands.Validation;
using DAL.Core.Helpers.BaseDtos;
using DAL.Models;
using FluentValidation;

namespace DAL.Core.loC
{
    public static class RegisterGenericValidatorsHelper
    {
        public static ContainerBuilder RegisterValidators<TEntity, TDto>(this ContainerBuilder builder)
    where TEntity : AuditableEntity, new()
    where TDto : class, IBaseViewModel, new()

        {
            builder.RegisterGeneric(typeof(CreateCommandValidator<,>)).As(typeof(IValidator<>));
            builder.RegisterGeneric(typeof(UpdateCommandValidator<,>)).As(typeof(IValidator<>));
            builder.RegisterGeneric(typeof(DeleteCommandValidator<,>)).As(typeof(IValidator<>));

            return builder;
        }

    }
}
