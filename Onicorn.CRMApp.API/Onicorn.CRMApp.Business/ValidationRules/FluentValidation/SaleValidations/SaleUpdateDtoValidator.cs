using FluentValidation;
using Onicorn.CRMApp.Dtos.SaleDtos;

namespace Onicorn.CRMApp.Business.ValidationRules.FluentValidation.SaleValidations
{
    public class SaleUpdateDtoValidator : AbstractValidator<SaleUpdateDto>
    {
        public SaleUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("The id field cannot be empty!");
            RuleFor(x => x.SalesAmount).NotEmpty().WithMessage("The sales amount field cannot be empty!");
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("The customer field cannot be empty!");
            RuleFor(x => x.ProjectId).NotEmpty().WithMessage("The project field cannot be empty!");
            RuleFor(x => x.SaleSituationId).NotEmpty().WithMessage("The sale situation field cannot be empty!");
        }
    }
}
