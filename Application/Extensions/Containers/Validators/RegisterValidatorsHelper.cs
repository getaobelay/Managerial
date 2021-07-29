using Application.Common.Commands.Validation;
using Application.ViewModels;
using Autofac;
using Domain.Common;
using FluentValidation;

namespace Application.Extensions.Containers.Validators
{
    public static class RegisterValidatorsHelper
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