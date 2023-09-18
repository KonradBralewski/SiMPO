using Blazor.Infrastracture.Extensions.Http;
using ErrorOr;
using Shared.Abstraction.Managers.Identity;
using Shared.Contracts.Requests.Authentication;
using Shared.Contracts.Requests.Identity;
using Shared.Contracts.Responses.Authentication;
using Shared.Contracts.Responses.Identity;
using System.Net.Http.Json;
using Blazor.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Shared.Contracts.Constants.Storage;
using Blazor.Infrastracture.Authentication;
using Shared.Abstraction.Managers.Authentication;
using Blazor.Infrastracture.Managers.Http;

namespace Blazor.Infrastracture.Managers.Authentication
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly HttpClientOwner _httpClientOwner;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationManager(HttpClientOwner httpClientOwner,
            AuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorage)
        {
            _httpClientOwner = httpClientOwner;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<ErrorOr<AuthenticationResponse>> RegisterUserAsync(RegisterRequest request)
        {
            var response = await _httpClientOwner.PostAsJsonAsync(Routes.UserEndpoints.Register, request);

            var parsedResult = await response.ToResult<AuthenticationResponse>();

            if (!parsedResult.IsError)
            {
                var token = parsedResult.Value.Token;

                await HandleTokenReceive(token);

                await ((CustomAuthenticationStateProvider)this._authenticationStateProvider).StateChangedAsync();

            }

            return parsedResult;
        }

        public async Task<ErrorOr<AuthenticationResponse>> LoginUserAsync(LoginRequest request)
        {
            var response = await _httpClientOwner.PostAsJsonAsync(Routes.UserEndpoints.Login, request);

            var parsedResult = await response.ToResult<AuthenticationResponse>();

            if (!parsedResult.IsError)
            {
                var token = parsedResult.Value.Token;

                await HandleTokenReceive(token);

                await ((CustomAuthenticationStateProvider)this._authenticationStateProvider).StateChangedAsync();

            }

            return parsedResult;
        }

        public async Task<ErrorOr<Task>> LogoutUserAsync()
        {
            await ((CustomAuthenticationStateProvider)this._authenticationStateProvider).MarkUserAsLoggedOutAsync();

            return Task.CompletedTask;
        }
        private async Task HandleTokenReceive(string jwt)
        {
            await _localStorage.SetItemAsync(LocalStorageKeys.JWT, jwt);
            _httpClientOwner.GetWrappedHttpClient().DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        }

    }
}
