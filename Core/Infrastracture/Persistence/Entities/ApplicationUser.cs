
using Microsoft.AspNetCore.Identity;

namespace Core.Infrastracture.Persistence.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Nickname { get; set; } = null!;
        public string DiscordId { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? ProfileImageUrl { get; set; } = null!;

        public int Balance { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = null!;
    }
}
