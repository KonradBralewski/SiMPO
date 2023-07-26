using Core.Infrastracture.Persistence.Entities;

namespace Core.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser user);
    }
}
