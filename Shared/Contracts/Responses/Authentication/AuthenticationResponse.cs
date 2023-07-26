using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.Responses.Authentication
{
    public sealed record AuthenticationResponse(
        string UserId,
        string UserEmail,
        string Token);
}
