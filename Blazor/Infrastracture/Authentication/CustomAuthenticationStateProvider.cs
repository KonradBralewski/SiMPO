using Microsoft.AspNetCore.Components.Authorization;
using Shared.Abstraction.Authentication;
using System.Security.Claims;

namespace Blazor.Infrastracture.Authentication
{
    public class CustomAuthenticationStateProvider : BaseCustomAuthenticationStateProvider
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

        public override void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));

            NotifyAuthenticationStateChanged(authState);
        }

        public async Task StateChangedAsync()
        {
            var authStateResult = Task.FromResult(await GetAuthenticationStateAsync());

            NotifyAuthenticationStateChanged(authStateResult);
        }

    }
}
