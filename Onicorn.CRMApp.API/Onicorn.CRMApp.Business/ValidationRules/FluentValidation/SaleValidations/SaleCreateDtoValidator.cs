using FluentValidation;
using Onicorn.CRMApp.Dtos.SaleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.ValidationRules.FluentValidation.SaleValidations
{
    public class SaleCreateDtoValidator : AbstractValidator<SaleCreateDto>
    {
        public SaleCreateDtoValidator()
        {
            RuleFor(x => x.SalesAmount).NotEmpty().WithMessage("The sales amount field cannot be empty!");
            RuleFor(x => x.SalesDate).NotEmpty().WithMessage("The sales date field cannot be empty!");
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("The customer field cannot be empty!");
            RuleFor(x => x.ProjectId).NotEmpty().WithMessage("The project field cannot be empty!");
            RuleFor(x => x.SaleSituationId).NotEmpty().WithMessage("The sale situation field cannot be empty!");
        }
    }
}
