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

namespace Blazor.Infrastracture.Managers.Identity
{
    public class CurrentUserManager : ICurrentUserManager
    {
        private readonly HttpClientOwner _httpClientOwner;

        public CurrentUserManager(HttpClientOwner httpClientOwner)
        {
            _httpClientOwner = httpClientOwner;
        }

        public async Task<ErrorOr<UserResponse>> GetCurrentUserAsync()
        {
            var response = await _httpClientOwner.GetAsync(Routes.UserEndpoints.GetCurrentUser);
            return await response.ToResult<UserResponse>();
        }
    }
}
