
using SiMPO.Core.Infrastracture.Persistence.Entities;

namespace SiMPO.Core.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser user);
    }
}
