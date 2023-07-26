
using Microsoft.AspNetCore.Identity;

namespace Core.Infrastracture.Persistence.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Nickname { get; set; } = null!;
    }
}
