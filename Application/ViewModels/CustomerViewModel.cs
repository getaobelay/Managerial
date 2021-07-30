using Application.Interfaces;
using Domain.Entites;
using FluentValidation;
using System.Collections.Generic;

namespace Application.ViewModels
{
    public class CustomerViewModel : BaseViewModel, IMapFrom<Customer>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }

        public List<OrderViewModel> Orders { get; set; }
    }

    public class CustomerViewModelValidator : AbstractValidator<CustomerViewModel>
    {
        public CustomerViewModelValidator()
        {
            RuleFor(register => register.Name).NotEmpty().WithMessage("Customer name cannot be empty");
            RuleFor(register => register.Gender).NotEmpty().WithMessage("Gender cannot be empty");
        }
    }
}