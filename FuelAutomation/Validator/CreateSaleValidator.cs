using Business.Abstract;
using FluentValidation;
using FuelAutomation.Models;

namespace FuelAutomation.Validator
{
    public class CreateSaleValidator:AbstractValidator<CreateSaleModel>

      


    {
        ITanksService _tankservice;
        public CreateSaleValidator(ITanksService tanksService)
        { 
            _tankservice = tanksService;
            //plaka
            RuleFor(x=>x.CarPlate).NotEmpty().WithMessage("Plaka boş olamaz");
            RuleFor(x => x.CarPlate).MaximumLength(20).WithMessage("Plaka uzunluğu çok fazla");


            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Miktar alanı boş geçilemez");
            RuleFor(x => x.Quantity).Matches(@"-?\d+(?:\.\d+)?").WithMessage("Lütfen geçerli bir sayı giriniz");
            
            //double quantityTank=_tankservice.GetById( )
           // RuleFor(x => x.Quantity).LessThanOrEqualTo( x);
          


        }
    }
}
