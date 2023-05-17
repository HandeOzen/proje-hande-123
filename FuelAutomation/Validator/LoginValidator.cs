using FluentValidation;
using FuelAutomation.Models;

namespace FuelAutomation.Validator
{
    public class LoginValidator:AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {

            RuleFor(x => x.Email).NotNull().WithMessage("Email Boş Olamaz").NotEmpty().WithMessage("Email Boş Olamaz");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen geçerli bir mail adresi giriniz");


            RuleFor(x=>x.Password).NotEmpty().WithMessage("Şifre gereklidir");
            RuleFor(x=>x.Password).NotNull().WithMessage("Şifre gereklidir");
           
        }
    }
}
