using FluentValidation;
using Onicorn.CRMApp.Dtos.AppUserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.ValidationRules.FluentValidation.AppUserValidations
{
    public class UpdateAppUserDtoValidator : AbstractValidator<UpdateAppUserDto>
    {
        public UpdateAppUserDtoValidator()
        {
            RuleFor(x => x.Firstname).NotEmpty().WithMessage("The name field cannot be empty!");
            RuleFor(x => x.Firstname).MaximumLength(30).WithMessage("The name field can contain a maximum of 30 characters!");
            RuleFor(x => x.Firstname).MinimumLength(2).WithMessage("The name field must contain at least 2 characters!");
            RuleFor(x => x.Lastname).NotEmpty().WithMessage("The lastname field cannot be empty!");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("The phone number field cannot be empty!");
            RuleFor(x => x.PhoneNumber).MinimumLength(11).WithMessage("The name field must contain at least 11 characters!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("The email field cannot be empty!");
            RuleFor(x => x.Email).EmailAddress().WithMessage("You did not enter a valid email address!");
        }
    }
}
