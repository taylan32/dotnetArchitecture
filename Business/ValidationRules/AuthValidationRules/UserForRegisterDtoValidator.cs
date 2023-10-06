using Business.Dtos.AuthDtos;
using FluentValidation;

namespace Business.ValidationRules.AuthValidationRules
{
	public class UserForRegisterDtoValidator : AbstractValidator<UserForRegisterDto>
	{
        public UserForRegisterDtoValidator()
        {
            RuleFor(e => e.Email).NotEmpty().WithMessage("Email is not allowed to be empty");
            RuleFor(e => e.Email).EmailAddress().WithMessage("Invalid email");

            RuleFor(e => e.Password).NotEmpty().WithMessage("Password is not allowed to be empty");

            RuleFor(e => e.FirstName).NotEmpty().WithMessage("Name is not allowed to be empty");
            RuleFor(e => e.LastName).NotEmpty().WithMessage("Last name is not allowed to be empty");


        }
    }
}
