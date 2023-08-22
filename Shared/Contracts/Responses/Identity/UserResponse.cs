using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.Responses.Identity
{
    public sealed record UserResponse(string Nickname,
                                      string Role,
                                      string Description,
                                      string DiscordId);
}
