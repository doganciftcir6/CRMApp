using FluentValidation;
using Onicorn.CRMApp.Dtos.TaskDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.ValidationRules.FluentValidation.TaskValidations
{
    public class TaskUpdateDtoValidator : AbstractValidator<TaskUpdateDto>
    {
        public TaskUpdateDtoValidator()
        {
            RuleFor(x => x.Taskname).NotEmpty().WithMessage("The task name field cannot be empty!");
            RuleFor(x => x.Taskname).MinimumLength(2).WithMessage("The task name field must contain at least 2 characters!");
            RuleFor(x => x.Taskname).MaximumLength(100).WithMessage("The task name field can contain a maximum of 100 characters!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("The description field cannot be empty!");
            RuleFor(x => x.Description).MinimumLength(2).WithMessage("The description field must contain at least 2 characters!");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("The description field can contain a maximum of 500 characters!");
            RuleFor(x => x.TaskSituationId).NotEmpty().WithMessage("The task situation field cannot be empty!");
            RuleFor(x => x.AppUserId).NotEmpty().WithMessage("The app user field cannot be empty!");
        }
    }
}
