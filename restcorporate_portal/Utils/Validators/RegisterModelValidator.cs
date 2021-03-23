using System;
using FluentValidation;
using restcorporate_portal.Models;
using restcorporate_portal.Utils.Validators.Errors;
using System.Linq;

namespace restcorporate_portal.Utils.Validators
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            // Password
            RuleFor(x => x.Password)
                .Matches(@"[а-яА-Я]{0,}")
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
            // FirstName
            RuleFor(x => x.FirstName)
                .NotEmpty()
                    .WithErrorCode(NamesValidatorErrors.FirstNotEmpty)
                    .WithMessage("Имя не должно быть пустым")
                .Length(3, 25)
                    .WithErrorCode(NamesValidatorErrors.FirstMoreLess)
                    .WithMessage("Имя должно быть больше 3 символов и меньше 25")
                .Must(x => char.IsUpper(x.First()))
                    .WithErrorCode(NamesValidatorErrors.FirstIsUpper)
                    .WithMessage("Имя должно начинаться с заглавной буквы");
            // LastName
            RuleFor(x => x.LastName)
                .NotEmpty()
                    .WithErrorCode(NamesValidatorErrors.LastNotEmpty)
                    .WithMessage("Фамилия не должна быть пустым")
                .Length(3, 25)
                    .WithErrorCode(NamesValidatorErrors.LastMoreLess)
                    .WithMessage("Фамилия должна быть больше 3 символов и меньше 25")
                .Must(x => char.IsUpper(x.First()))
                    .WithErrorCode(NamesValidatorErrors.LastIsUpper)
                    .WithMessage("Фамилия должна начинаться с заглавной буквы");
        }
    }
}