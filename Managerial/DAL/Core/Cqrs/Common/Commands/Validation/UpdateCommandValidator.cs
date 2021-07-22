using DAL.Core.Cqrs.Common.Commands.Requests;
using DAL.Models;
using DAL.ViewModels;
using FluentValidation;

namespace DAL.Core.Cqrs.Common.Commands.Validation
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