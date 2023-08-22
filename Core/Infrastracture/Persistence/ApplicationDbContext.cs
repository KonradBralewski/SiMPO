using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Core.Infrastracture.Persistence.Entities;
using Core.Infrastracture.Persistence.Configuration;
using Microsoft.AspNetCore.Identity;

namespace Core.Infrastracture.Persistence
{
    public class ApplicationDbContext : 
        IdentityDbContext<ApplicationUser, ApplicationRole, Guid, IdentityUserClaim<Guid>,
            ApplicationUserRole, IdentityUserLogin<Guid>,IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationUserConfiguration).Assembly);

        }
    }
}
