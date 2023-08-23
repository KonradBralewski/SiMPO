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
using Blazor.Infrastracture.Managers.Http;

namespace Blazor.Infrastracture.Managers.Identity.UsersManager
{
    public class UserManager : IUserManager
    {
        private readonly HttpClientOwner _httpClientOwner;

        public UserManager(HttpClientOwner httpClientOwner)
        {
            _httpClientOwner = httpClientOwner;
        }

        public async Task<ErrorOr<UserResponse>> GetAsync(string userId)
        {
            var response = await _httpClientOwner.GetAsync(Routes.UserEndpoints.Get(userId));
            return await response.ToResult<UserResponse>();
        }

        public async Task<ErrorOr<IEnumerable<UserResponse>>> GetAllAsync()
        {
            var response = await _httpClientOwner.GetAsync(Routes.UserEndpoints.GetAll);
            return await response.ToResult<IEnumerable<UserResponse>>();
        }


        public async Task<ErrorOr<UserRolesResponse>> GetRolesAsync(string userId)
        {
            var response = await _httpClientOwner.GetAsync(Routes.UserEndpoints.GetUserRoles(userId));
            return await response.ToResult<UserRolesResponse>();
        }


        public async Task<ErrorOr<Task>> ResetForgottenPasswordAsync(ForgottenPasswordRequest model)
        {
            var response = await _httpClientOwner.PostAsJsonAsync(Routes.UserEndpoints.ForgotPassword, model);
            return await response.ToResult();
        }
    }
}
