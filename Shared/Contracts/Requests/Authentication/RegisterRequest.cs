using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiMPO.Shared.Contracts.Requests.Authentication
{
    public sealed record RegisterRequest(
        string Email,
        string Password);
}
