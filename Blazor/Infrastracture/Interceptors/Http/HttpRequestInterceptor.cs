using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net;
using Shared.Abstraction.Managers.Authentication;

namespace Blazor.Infrastracture.Interceptors.Http
{
    public class HttpRequestInterceptor  : DelegatingHandler
    {
        private readonly NavigationManager _navigationManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly ISnackbar _snackBar;

        public HttpRequestInterceptor(
            NavigationManager navigationManager,
            ISnackbar snackBar,
            IAuthenticationManager authenticationManager)
        {
            _navigationManager = navigationManager;
            _snackBar = snackBar;
            _authenticationManager = authenticationManager;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            await InterceptAfterHttpAsync(response);

            return response;
        }

        public async Task InterceptAfterHttpAsync(HttpResponseMessage? responseMessage)
        {
            if(responseMessage is null)
            {
                return;
            }

            await RedirectIfNotAuthorized(responseMessage);
        }

        private async Task RedirectIfNotAuthorized(HttpResponseMessage responseMessage)
        {
            if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                _snackBar.Add("Your session expired or you was not logged in.", Severity.Error);
                await _authenticationManager.LogoutUserAsync();
                _navigationManager.NavigateTo("/sessionExpired");
            }
            
        }

    }
}
