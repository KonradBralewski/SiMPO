using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.Responses.Identity
{
    public sealed record UserRolesResponse(List<UserRole> UserRoles);

    public sealed record UserRole(string RoleName);
}
