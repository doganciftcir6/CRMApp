using FluentValidation;
using Onicorn.CRMApp.Dtos.ProjectDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.ValidationRules.FluentValidation.ProjectValidations
{
    public class ProjectUpdateDtoValidator : AbstractValidator<ProjectUpdateDto>
    {
        public ProjectUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("The id field cannot be empty!");
            RuleFor(x => x.ProjectName).NotEmpty().WithMessage("The project name field cannot be empty!");
            RuleFor(x => x.ProjectName).MinimumLength(2).WithMessage("The project name field must contain at least 2 characters!");
            RuleFor(x => x.ProjectName).MaximumLength(100).WithMessage("The project name field can contain a maximum of 100 characters!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("The description field cannot be empty!");
            RuleFor(x => x.Description).MinimumLength(2).WithMessage("The description field must contain at least 2 characters!");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("The description field can contain a maximum of 500 characters!");
            RuleFor(x => x.Price).NotEmpty().WithMessage("The price name field cannot be empty!");
        }
    }
}
