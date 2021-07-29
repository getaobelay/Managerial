using Application.Common.Commands.Requests;
using Application.ViewModels;
using Domain.Common;
using FluentValidation;
using Infrastructure.Implementation;
using Infrastructure.Interfaces;

namespace Application.Common.Commands.Validation
{
    public class CreateCommandValidator<TEntity, TDto> : AbstractValidator<CreateCommandRequest<TEntity, TDto>>
          where TEntity : AuditableEntity, new()
         where TDto : BaseViewModel, new()
    {
        private DataRepository<TEntity, TDto> repository;

        public CreateCommandValidator(IDataContext context)
        {
            repository = new DataRepository<TEntity, TDto>(context.UnitOfWork);
        }

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