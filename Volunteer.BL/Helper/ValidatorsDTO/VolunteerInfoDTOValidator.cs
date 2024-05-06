using Domain.DTOs;
using FluentValidation;

namespace Volunteer.BL.Helper.ValidatorsDTO
{
    public class VolunteerInfoDTOValidator : AbstractValidator<VolunteerInfoDTO>
    {
        public VolunteerInfoDTOValidator()
        {
            RuleFor(u => u.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .Must(BeAValidDate).WithMessage("Please enter a valid date of birth.");

            RuleFor(u => u.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(900).WithMessage("Description cannot be longer than 900 characters.");

            RuleFor(u => u.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot be longer than 50 characters.");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot be longer than 50 characters.");
        }
        private bool BeAValidDate(DateTime date)
        {
            return date <= DateTime.Now;//this must depend on user timezone, but OK for now
        }
    }
}
