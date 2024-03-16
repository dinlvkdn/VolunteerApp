using Domain.DTOs;
using FluentValidation;

namespace Volunteer.BL.Helper.ValidatorsDTO
{
    public class VolunteerShortInfoDTOValidator : AbstractValidator<VolunteerShortInfoDTO>
    {
        public VolunteerShortInfoDTOValidator()
        {
            RuleFor(u => u.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters.");

            RuleFor(u => u.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot be longer than 50 characters.");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot be longer than 50 characters.");
        }
    }
}
