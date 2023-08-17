using Shared.Validation;
using Shared.Validation.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.Forms.Authentication
{
    public sealed class LoginFormData : IValidatable
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public LoginFormData()
        {
            Email = string.Empty;
            Password = string.Empty;
        }
    }
}
