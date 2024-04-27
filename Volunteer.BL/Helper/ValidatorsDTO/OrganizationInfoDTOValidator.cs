using Domain.DTOs;
using FluentValidation;

namespace Volunteer.BL.Helper.ValidatorsDTO
{
    public class OrganizationInfoDTOValidator : AbstractValidator<OrganizationInfoDTO>
    {
        public OrganizationInfoDTOValidator()
        {
            RuleFor(u => u.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(900).WithMessage("Description cannot be longer than 900 characters.");

            RuleFor(u => u.YearOfFoundation)
            .NotEmpty()
            .WithMessage("Year of foundation is required.")
            .Must(BeAValidYear)
            .WithMessage("Please enter a valid year of foundation.");

            RuleFor(u => u.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(20)
            .WithMessage("Name cannot be longer than 20 characters.");
        }
        private bool BeAValidYear(int year)
        {
            return year >= 1000 && year <= DateTime.Now.Year; 
        }
    }
}
