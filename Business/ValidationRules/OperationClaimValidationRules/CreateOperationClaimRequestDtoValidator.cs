using Business.Dtos.OperationClaimDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.OperationClaimValidationRules
{
	public class CreateOperationClaimRequestDtoValidator : AbstractValidator<CreateOperationClaimRequestDto>
	{
        public CreateOperationClaimRequestDtoValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Claim name is not allowed to be empty");
            RuleFor(c => c.Name).Length(2,30).WithMessage("Length of claim name can be between 2 and 30 characters long");


        }
    }
}
