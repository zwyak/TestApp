using System.Globalization;
using FluentValidation;

namespace TestApp
{
    public class PhoneValidator: AbstractValidator<Phone>
    {
        public PhoneValidator()
        {
            ValidatorOptions.LanguageManager.Culture = new CultureInfo("en");
            RuleFor(p => p.Name).NotNull().NotEmpty().MinimumLength(5);
            RuleFor(p => p.Surname).NotNull().NotEmpty().MinimumLength(5);
            RuleFor(p => p.PhoneNumber).NotNull().NotEmpty().MinimumLength(10);
            RuleFor(p => p.Email).EmailAddress().Unless(e => string.IsNullOrEmpty(e.Email));
        }
    }
}
