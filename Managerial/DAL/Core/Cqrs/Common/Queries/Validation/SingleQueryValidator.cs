using DAL.Core.CommonCQRS.Queries.Requests;
using DAL.Core.Helpers.BaseDtos;
using DAL.Models;
using FluentValidation;

namespace DAL.Core.CommonCQRS.Queries.Validation
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
