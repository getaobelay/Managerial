using Application.Common.Commands.Requests;
using Application.ViewModels;
using Domain.Common;
using FluentValidation;

namespace Application.Common.Commands.Validation
{
    public class DeleteCommandValidator<TEntity, TDto> : AbstractValidator<UpdateCommandRequest<TEntity, TDto>>
         where TEntity : AuditableEntity, new()
         where TDto : BaseViewModel, new()
    {
        public DeleteCommandValidator()
        {
            RuleFor(x => x.Id)
             .NotEmpty()
             .WithMessage("Id should not be null");
        }
    }
}