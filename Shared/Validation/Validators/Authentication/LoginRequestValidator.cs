﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Contracts.Requests.Authentication;

namespace Shared.Validation.Validators.Authentication
{
    public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email is required")
               .EmailAddress().WithMessage("Email is not valid");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
        }
    }
}
