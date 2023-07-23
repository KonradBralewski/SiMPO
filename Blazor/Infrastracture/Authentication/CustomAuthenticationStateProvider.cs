using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace SiMPO.Blazor.Infrastracture.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        /// TO DO, implement revalidation & setup http only cookie with spreaded JWT.
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, "mrfibuli"),
                }, "Custom Authentication");

            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }

        public async Task StateChangedAsync()
        {
            var authStateResult = Task.FromResult(await GetAuthenticationStateAsync());

            NotifyAuthenticationStateChanged(authStateResult);
        }

    }
}
