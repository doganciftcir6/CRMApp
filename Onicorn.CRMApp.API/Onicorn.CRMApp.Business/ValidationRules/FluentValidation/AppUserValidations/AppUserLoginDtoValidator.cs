using FluentValidation;
using Onicorn.CRMApp.Dtos.AppUserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.ValidationRules.FluentValidation.AppUserValidations
{
    public class AppUserLoginDtoValidator : AbstractValidator<AppUserLoginDto>
    {
        public AppUserLoginDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("The username field cannot be empty!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("The password field cannot be empty!");
        }
    }
}
