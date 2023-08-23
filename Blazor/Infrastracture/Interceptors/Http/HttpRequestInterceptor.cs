using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net;
using Shared.Abstraction.Managers.Authentication;
using Shared.Abstraction.Interceptors;

namespace Blazor.Infrastracture.Interceptors.Http
{
    public class HttpRequestInterceptor : IHttpRequestInterceptor
    {
        private readonly NavigationManager _navigationManager;
        private readonly ISnackbar _snackBar;

        public HttpRequestInterceptor(
            NavigationManager navigationManager,
            ISnackbar snackBar)
        {
            _navigationManager = navigationManager;
            _snackBar = snackBar;
        }

        public void InterceptAfterHttpRequest(HttpResponseMessage? responseMessage)
        {
            if(responseMessage is null)
            {
                return;
            }

            RedirectIfNotAuthorized(responseMessage);
        }

        private void RedirectIfNotAuthorized(HttpResponseMessage responseMessage)
        {
            if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                _snackBar.Add("You have not logged in yet or your session expired.", Severity.Error);
                _navigationManager.NavigateTo("/accessDenied");
            }
            
        }

    }
}
