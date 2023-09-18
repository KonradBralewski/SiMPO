using Microsoft.AspNetCore.Components.Authorization;
using Shared.Abstraction.Authentication;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;
using Blazored.LocalStorage;
using Blazor.Extensions;
using Shared.Contracts.Constants.Storage;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Blazor.Infrastracture.Managers.Http;
using System.Text.Json;

namespace Blazor.Infrastracture.Authentication
{
    public class CustomAuthenticationStateProvider : BaseCustomAuthenticationStateProvider
    {
        private readonly HttpClientOwner _httpClientOwner;
        private readonly ILocalStorageService _localStorage;
        public CustomAuthenticationStateProvider(ILocalStorageService localStorage, HttpClientOwner httpClientOwner)
        {
            _localStorage = localStorage;
            _httpClientOwner = httpClientOwner; 
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await _localStorage.GetItemAsync<string>(LocalStorageKeys.JWT);

            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            _httpClientOwner.GetWrappedHttpClient().DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);

            var state = new AuthenticationState(DecodeJwt(savedToken));

            return state;
        }

        private ClaimsPrincipal DecodeJwt(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            var principal = new ClaimsPrincipal(new ClaimsIdentity(token.Claims, "jwt"));

            return principal;
        }

        public override async Task MarkUserAsLoggedOutAsync()
        {
            await _localStorage.RemoveItemAsync(LocalStorageKeys.JWT);

            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());

            var authState = Task.FromResult(new AuthenticationState(anonymousUser));

            NotifyAuthenticationStateChanged(authState);
        }

        public override async Task<Guid?> GetUserIdAsync()
        {
            var authState = await GetAuthenticationStateAsync();

            var claims = authState.User.Claims.ToList();

            var userId = claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;

            if(userId is null)
            {
                return null;
            }

            return Guid.Parse(userId);
        }

        public async Task StateChangedAsync()
        {
            var authStateResult = Task.FromResult(await GetAuthenticationStateAsync());

            NotifyAuthenticationStateChanged(authStateResult);
        }

    }
}
