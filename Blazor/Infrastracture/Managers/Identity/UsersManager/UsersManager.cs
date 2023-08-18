using Blazor.Infrastracture.Extensions.Http;
using ErrorOr;
using Shared.Abstraction.Managers.Identity;
using Shared.Contracts.Requests.Authentication;
using Shared.Contracts.Requests.Identity;
using Shared.Contracts.Responses.Authentication;
using Shared.Contracts.Responses.Identity;
using System.Net.Http.Json;
using Blazor.Extensions;

namespace Blazor.Infrastracture.Managers.Identity.UsersManager
{
    public class UserManager : IUserManager
    {
        private readonly HttpClient _httpClient;

        public UserManager(IHttpClientFactory _httpClientFactory)
        {
            _httpClient = _httpClientFactory.CreateClient(WebAssemblyHostBuilderExtensions.HttpClientName);
        }

        public async Task<ErrorOr<AuthenticationResponse>> RegisterUserAsync(RegisterRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.UserEndpoints.Register, request);
            return await response.ToResult<AuthenticationResponse>();
        }

        public async Task<ErrorOr<AuthenticationResponse>> LoginUserAsync(LoginRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.UserEndpoints.Login, request);

            return await response.ToResult<AuthenticationResponse>();
        }

        public async Task<ErrorOr<UserResponse>> GetAsync(string userId)
        {
            var response = await _httpClient.GetAsync(Routes.UserEndpoints.Get(userId));
            return await response.ToResult<UserResponse>();
        }


        public async Task<ErrorOr<UserRolesResponse>> GetRolesAsync(string userId)
        {
            var response = await _httpClient.GetAsync(Routes.UserEndpoints.GetUserRoles(userId));
            return await response.ToResult<UserRolesResponse>();
        }


        public async Task<ErrorOr<Task>> ResetForgottenPasswordAsync(ForgottenPasswordRequest model)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.UserEndpoints.ForgotPassword, model);
            return await response.ToResult();
        }

    }
}
