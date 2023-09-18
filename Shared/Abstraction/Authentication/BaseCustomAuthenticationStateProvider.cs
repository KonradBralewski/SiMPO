using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Abstraction.Authentication
{
    public abstract class BaseCustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        public abstract Task MarkUserAsLoggedOutAsync();
        public abstract Task<Guid?> GetUserIdAsync();
    }
}
