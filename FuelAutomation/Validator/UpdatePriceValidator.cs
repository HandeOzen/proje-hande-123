using FluentValidation;
using FuelAutomation.Models;

namespace FuelAutomation.Validator
{
    public class UpdatePriceValidator:AbstractValidator<UpdatePriceViewModel>
    {
        public UpdatePriceValidator()
        {
            //RuleFor(x => x.PriceDiesel).Matches(@"-?\d+(?:\.\d+)?").WithMessage("Lütfen geçerli bir sayı giriniz");
            //RuleFor(x => x.PriceGasoline).Matches(@"-?\d+(?:\.\d+)?").WithMessage("Lütfen geçerli bir sayı giriniz");
            //RuleFor(x => x.PriceLpg).Matches(@"-?\d+(?:\.\d+)?").WithMessage("Lütfen geçerli bir sayı giriniz");
        }
    }
}
