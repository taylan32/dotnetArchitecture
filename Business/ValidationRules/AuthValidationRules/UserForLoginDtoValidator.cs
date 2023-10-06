using Business.Dtos.AuthDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.AuthValidationRules
{
	public class UserForLoginDtoValidator : AbstractValidator<UserForLoginDto>
	{
        public UserForLoginDtoValidator()
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email is not allowed to be empty");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Invalid email");

            RuleFor(u => u.Password).NotEmpty().WithMessage("Password is not allowed to be empty");
        }
    }
}
