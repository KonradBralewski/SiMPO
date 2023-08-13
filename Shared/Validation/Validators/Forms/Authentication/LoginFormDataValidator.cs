using FluentValidation;
using Shared.Contracts.Forms.Authentication;
using Shared.Validation.Validators.Base;

namespace Shared.Validation.Validators.Forms.Authentication
{
    public sealed class LoginFormDataValidator : FormValidator<LoginFormData>
    {
        public LoginFormDataValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email is required.")
               .EmailAddress().WithMessage("Email is not valid.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
        }
    }
}
