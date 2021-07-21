using DAL.Core.Cqrs.Common.Queries.Requests;
using DAL.Models;
using DAL.ViewModels.Interfaces;
using FluentValidation;

namespace DAL.Core.Cqrs.Common.Queries.Validation
{
    /// <summary>
    /// validater
    /// </summary>
    public class SingleQueryValidator<TEntity, TDto> : AbstractValidator<SingleQueryRequest<TEntity, TDto>>
        where TEntity : AuditableEntity, new()
        where TDto : class, IBaseViewModel, new()
    {
        public SingleQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id should not be null");
        }
    }
}