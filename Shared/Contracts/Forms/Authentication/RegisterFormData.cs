using Shared.Validation.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.Forms.Authentication
{
    public class RegisterFormData : IValidatable
    {
        public string Nickname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        
        public string PasswordConfirmation { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string DiscordId { get; set; } = null!;
        public bool AcceptedTermsOfService { get; set; }

        public RegisterFormData()
        {
            Nickname = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            PasswordConfirmation = string.Empty;
            Description = string.Empty;
            DiscordId = string.Empty;
            AcceptedTermsOfService = false;
        }
    }
}
