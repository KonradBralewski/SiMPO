using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net;
using Shared.Abstraction.Managers.Authentication;
using Shared.Abstraction.Interceptors;
using Blazor.Infrastracture.State;

namespace Blazor.Infrastracture.Interceptors.Http
{
    public class HttpRequestInterceptor : IHttpRequestInterceptor
    {
        private readonly NavigationManager _navigationManager;
        private readonly ISnackbar _snackBar;
        private readonly ApplicationState _appState;

        public HttpRequestInterceptor(
            NavigationManager navigationManager,
            ISnackbar snackBar,
            ApplicationState appState)
        {
            _navigationManager = navigationManager;
            _snackBar = snackBar;
            _appState = appState;
        }
        public void InterceptBeforeHttpRequest()
        {
            _appState.IsWaitingForResponse = true;
        }

        public void InterceptAfterHttpRequest(HttpResponseMessage? responseMessage)
        {
            _appState.IsWaitingForResponse = false;

            if (responseMessage is null)
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
