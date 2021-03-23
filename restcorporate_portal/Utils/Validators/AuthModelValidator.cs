using System;
using FluentValidation;
using restcorporate_portal.Models;
using restcorporate_portal.Utils.Validators.Errors;
using System.Linq;

namespace restcorporate_portal.Utils.Validators
{
    public class AuthModelValidator : AbstractValidator<AuthModel>
    {
        public AuthModelValidator()
        {
            // Password
            RuleFor(x => x.Password)
                .Matches(@"^[а-яА-Я]{0,}$")
                    .WithErrorCode(PasswordValidatorErrors.ContainLatinChars)
                    .WithMessage("Пароль должен содержать только русские символы")
                .NotEmpty()
                    .WithErrorCode(PasswordValidatorErrors.NotEmpty)
                    .WithMessage("Пароль не должен быть пустым")
                .Length(6, 20)
                    .WithErrorCode(PasswordValidatorErrors.RequireLength)
                    .WithMessage("Пароль должен быть не меньше 6 и не больше 20")
                .Must(x => x.Any(char.IsDigit))
                    .WithErrorCode(PasswordValidatorErrors.RequireDigit)
                    .WithMessage("Пароль должен содержать цифры");
            // Email
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithErrorCode(EmailValidatorErrors.NotEmpty)
                .WithMessage("Email не должен быть пустым")
                .EmailAddress()
                .WithErrorCode(EmailValidatorErrors.Format)
                .WithMessage("Неверный формат Email адреса");
        }
    }
}
