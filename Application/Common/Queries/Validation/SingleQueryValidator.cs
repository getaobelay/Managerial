using Application.Common.Queries.Requests;
using Application.ViewModels;
using Domain.Common;
using FluentValidation;

namespace Application.Common.Queries.Validation
{
    public class SingleQueryValidator<TEntity, TDto> : AbstractValidator<SingleQueryRequest<TEntity, TDto>>
             where TEntity : AuditableEntity, new()
        where TDto : BaseViewModel, new()
    {
        public SingleQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id should not be null");
        }
    }
}