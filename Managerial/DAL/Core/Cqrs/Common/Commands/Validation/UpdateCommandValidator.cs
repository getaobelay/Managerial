using DAL.Core.CommonCQRS.Commands.Requests;
using DAL.Core.Helpers.BaseDtos;
using DAL.Models;
using FluentValidation;

namespace DAL.Core.CommonCQRS.Commands.Validation
{

    public class UpdateCommandValidator<TEntity, TDto> : AbstractValidator<UpdateCommandRequest<TEntity, TDto>>
        where TEntity : AuditableEntity, new()
        where TDto : class, IBaseViewModel, new()
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
