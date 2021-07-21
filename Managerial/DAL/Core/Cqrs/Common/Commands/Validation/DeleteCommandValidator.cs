using DAL.Core.CommonCQRS.Commands.Requests;
using DAL.Core.Helpers.BaseDtos;
using DAL.Models;
using FluentValidation;

namespace DAL.Core.CommonCQRS.Commands.Validation
{
    public class DeleteCommandValidator<TEntity, TDto> : AbstractValidator<UpdateCommandRequest<TEntity, TDto>>
        where TEntity : AuditableEntity, new()
        where TDto : class, IBaseViewModel, new()
    {
        public DeleteCommandValidator()
        {

            RuleFor(x => x.Id)
             .NotEmpty()
             .WithMessage("Id should not be null");

        }
    }
}
