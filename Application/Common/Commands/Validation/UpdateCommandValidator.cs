using Application.Common.Commands.Requests;
using Application.ViewModels;
using Domain.Common;
using FluentValidation;

namespace Application.Common.Commands.Validation
{
    public class UpdateCommandValidator<TEntity, TDto> : AbstractValidator<UpdateCommandRequest<TEntity, TDto>>
           where TEntity : AuditableEntity, new()
         where TDto : BaseViewModel, new()
    {
        public UpdateCommandValidator()
        {
            RuleFor(x => x.UpdatedObject)
                .NotEmpty()
                .WithMessage("Entity should not be null");

            RuleFor(x => x.UpdatedObject.Id)
                .NotEmpty()
                .WithMessage("Id should not be null");
        }
    }
}