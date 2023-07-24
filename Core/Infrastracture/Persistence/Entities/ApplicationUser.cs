
using Microsoft.AspNetCore.Identity;

namespace SiMPO.Core.Infrastracture.Persistence.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Nickname { get; set; } = null!;
    }
}
