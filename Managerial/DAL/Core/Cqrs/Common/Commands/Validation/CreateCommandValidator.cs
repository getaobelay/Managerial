﻿using DAL.Core.Cqrs.Common.Commands.Requests;
using DAL.Models;
using FluentValidation;

namespace DAL.Core.Cqrs.Common.Commands.Validation
{
    public class CreateCommandValidator<TEntity, TDto> : AbstractValidator<CreateCommandRequest<TEntity, TDto>>
        where TEntity : AuditableEntity, new()
        where TDto : BaseViewModel, new()
    {
        public CreateCommandValidator()
        {
            RuleFor(x => x.CreateObject)
                .NotNull()
                .WithMessage("Entity should not be null");

            RuleFor(x => x.CreateObject.Id)
              .Empty()
              .WithMessage("Id should null");
        }
    }
}