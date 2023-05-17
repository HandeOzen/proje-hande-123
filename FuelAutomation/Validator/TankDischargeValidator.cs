using FluentValidation;
using FuelAutomation.Models;

namespace FuelAutomation.Validator
{
    public class TankDischargeValidator:AbstractValidator<TankDischargeViewModel>
    {
        public TankDischargeValidator()
        {
            RuleFor(x => x.DischargeQuantity).NotEmpty().WithMessage("Boşaltım miktarı boş olamaz");
            RuleFor(x => x.DischargeQuantity).Matches(@"-?\d+(?:\.\d+)?").WithMessage("Lütfen geçerli bir sayı giriniz");
            //RuleFor(x=>x.FillQuantity).LessThanOrEqualTo(Convert.ToDouble(x=>x.Capacity-x)=>x.Quantity))
        }
    }
}
