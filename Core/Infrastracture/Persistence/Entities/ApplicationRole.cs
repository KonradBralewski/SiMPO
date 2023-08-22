using Microsoft.AspNetCore.Identity;

namespace Core.Infrastracture.Persistence.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = null!;
    }
}
