using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.Requests.Identity
{
    public sealed record ForgottenPasswordRequest(string? Nickname,
                                               string? Email);
}
