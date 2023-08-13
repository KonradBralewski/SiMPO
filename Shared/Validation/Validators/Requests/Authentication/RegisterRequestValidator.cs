using FluentValidation;
using Shared.Contracts.Requests.Authentication;
using Shared.Validation.Validators.Base;

namespace Shared.Validation.Validators.Requests.Authentication
{
    public sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Nickname)
                .NotEmpty().WithMessage("Nickname is required.");
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email is required.")
               .EmailAddress().WithMessage("Email is not valid.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Must(HasSpecialChars).WithMessage("Password must contain at least one special character.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.DiscordId)
                .NotEmpty().WithMessage("DiscordId is required.");
        }

        private bool HasSpecialChars(string ourString)
        {
            return ourString.Any(ch => !char.IsLetterOrDigit(ch));
        }
    }
}
