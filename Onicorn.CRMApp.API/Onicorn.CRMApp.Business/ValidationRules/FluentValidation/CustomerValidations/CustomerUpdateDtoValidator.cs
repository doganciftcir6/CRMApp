using FluentValidation;
using Onicorn.CRMApp.Dtos.CustomerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.ValidationRules.FluentValidation.CustomerValidations
{
    public class CustomerUpdateDtoValidator : AbstractValidator<CustomerUpdateDto>
    {
        public CustomerUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("The id field cannot be empty!");
            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("The company name field cannot be empty!");
            RuleFor(x => x.CompanyName).MinimumLength(2).WithMessage("The company name field must contain at least 2 characters!");
            RuleFor(x => x.CompanyName).MaximumLength(100).WithMessage("The company name field can contain a maximum of 100 characters!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("The email date field cannot be empty!");
            RuleFor(x => x.Email).EmailAddress().WithMessage("You did not enter a valid email address!");
            RuleFor(x => x.Address).NotEmpty().WithMessage("The address field cannot be empty!");
            RuleFor(x => x.Address).MinimumLength(2).WithMessage("The address field must contain at least 2 characters!");
            RuleFor(x => x.Address).MaximumLength(200).WithMessage("The address field can contain a maximum of 200 characters!");
            RuleFor(x => x.Province).NotEmpty().WithMessage("The province field cannot be empty!");
            RuleFor(x => x.Province).MinimumLength(2).WithMessage("The province field must contain at least 2 characters!");
            RuleFor(x => x.Province).MaximumLength(50).WithMessage("The province field can contain a maximum of 50 characters!");
            RuleFor(x => x.District).NotEmpty().WithMessage("The district field cannot be empty!");
            RuleFor(x => x.District).MinimumLength(2).WithMessage("The district field must contain at least 2 characters!");
            RuleFor(x => x.District).MaximumLength(50).WithMessage("The district field can contain a maximum of 50 characters!");
        }
    }
}
