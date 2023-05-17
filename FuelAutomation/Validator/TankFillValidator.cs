using FluentValidation;
using FuelAutomation.Models;

namespace FuelAutomation.Validator
{
    public class TankFillValidator:AbstractValidator<TankFillViewModel>


    {
        public TankFillValidator()
        {
            RuleFor(x => x.FillQuantity).NotEmpty().WithMessage("Dolum miktarı boş olamaz");
            RuleFor(x =>x.FillQuantity).Matches(@"-?\d+(?:\.\d+)?").WithMessage("Lütfen geçerli bir sayı giriniz");
            //RuleFor(x=>x.FillQuantity).LessThanOrEqualTo(Convert.ToDouble(x=>x.Capacity-x)=>x.Quantity))
        }
    }
}
