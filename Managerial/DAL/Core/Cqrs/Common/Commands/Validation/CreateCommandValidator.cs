using DAL;
using DAL.Core.CommonCQRS.Commands.Requests;
using DAL.Core.Helpers.BaseDtos;
using DAL.Models;
using FluentValidation;

namespace DAL.Core.CommonCQRS.Commands.Validation
{

    public class CreateCommandValidator<TEntity, TDto> : AbstractValidator<CreateCommandRequest<TEntity, TDto>>
        where TEntity : AuditableEntity, new()
        where TDto : class, IBaseViewModel, new()
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
