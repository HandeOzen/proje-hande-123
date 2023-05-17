using FluentValidation;
using FuelAutomation.Models;

namespace FuelAutomation.Validator
{
    public class UserUpdateValidator:AbstractValidator<UserUpdateViewModel>
    {
        public UserUpdateValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email boş olamaz");

            RuleFor(x => x.Email).EmailAddress().WithMessage("Formata uygun bir adres giriniz");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş olamaz");
            RuleFor(x => x.Password).MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır");

       //     RuleFor(x => x.Password).Matches("^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$").WithMessage("Şifre 1 rakam, özel karakter,büyük ve küçük harf içermelidir");

        }

    }
}
