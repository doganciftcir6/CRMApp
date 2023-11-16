using FluentValidation;
using Onicorn.CRMApp.Dtos.AppUserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.ValidationRules.FluentValidation.AppUserValidations
{
    public class RoleAssingSendDtoValidator : AbstractValidator<RoleAssingSendDto>
    {
        public RoleAssingSendDtoValidator()
        {
            RuleFor(x => x.RoleName).NotEmpty().WithMessage("The role name field cannot be empty!");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("The user field cannot be empty!");
        }
    }
}
