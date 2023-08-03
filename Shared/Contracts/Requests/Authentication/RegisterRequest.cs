using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.Requests.Authentication
{
    public sealed class RegisterRequest
    {
        [Required]
        public string Nickname { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public string DiscordLink { get; set; } = null!;
    }
}
