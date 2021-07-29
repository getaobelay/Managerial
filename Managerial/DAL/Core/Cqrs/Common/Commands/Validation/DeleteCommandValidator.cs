using DAL.Core.Cqrs.Common.Commands.Requests;
using DAL.Models;
using FluentValidation;

namespace DAL.Core.Cqrs.Common.Commands.Validation
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