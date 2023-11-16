using FluentValidation;
using Onicorn.CRMApp.Dtos.CommunicationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.ValidationRules.FluentValidation.CommunicationValidations
{
    public class CommunicationUpdateDtoValidator : AbstractValidator<CommunicationUpdateDto>
    {
        public CommunicationUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("The id field cannot be empty!");
            RuleFor(x => x.CommunicationDate).NotEmpty().WithMessage("The communication date field cannot be empty!");
            RuleFor(x => x.Detail).NotEmpty().WithMessage("The detail field cannot be empty!");
            RuleFor(x => x.Detail).MaximumLength(2000).WithMessage("The name field can contain a maximum of 2000 characters!");
            RuleFor(x => x.Detail).MinimumLength(5).WithMessage("The name field must contain at least 5 characters!");
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("The customer field cannot be empty!");
            RuleFor(x => x.CommunicationTypeId).NotEmpty().WithMessage("The communication type field cannot be empty!");
        }
    }
}
