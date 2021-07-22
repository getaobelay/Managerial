using Autofac;
using DAL.Core.Cqrs.Common.Commands.Validation;
using DAL.Models;
using DAL.ViewModels;
using FluentValidation;

namespace DAL.Core.loC
{
    public static class RegisterGenericValidatorsHelper
    {
        public static ContainerBuilder RegisterValidators<TEntity, TDto>(this ContainerBuilder builder)
    where TEntity : AuditableEntity, new()
    where TDto : BaseViewModel, new()

        {
            builder.RegisterGeneric(typeof(CreateCommandValidator<,>)).As(typeof(IValidator<>));
            builder.RegisterGeneric(typeof(UpdateCommandValidator<,>)).As(typeof(IValidator<>));
            builder.RegisterGeneric(typeof(DeleteCommandValidator<,>)).As(typeof(IValidator<>));

            return builder;
        }
    }
}