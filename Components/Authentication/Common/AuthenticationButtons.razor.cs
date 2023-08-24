using Components.Authentication.Login;
using Components.Authentication.SignUp;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shared.Abstraction.Authentication;
using Shared.Abstraction.Managers.Identity;
using Shared.Abstraction.Managers.Authentication;

namespace Components.Authentication.Common
{
    public partial class AuthenticationButtons
    {
       
        [Parameter]
        public string ContainerClass { get; set; } = null!;
        [Parameter]
        public string ContainerStyle { get; set; } = null!;

        [Parameter]
        public string SignInButtonClass { get; set; } = null!;
        [Parameter]
        public string SignInButtonStyle { get; set; } = null!;
        [Parameter]
        public string SignUpButtonClass { get; set; } = null!;
        [Parameter]
        public string SignUpButtonStyle { get; set; } = null!;
        [Parameter]
        public string LogOutButtonClass { get; set; } = null!;
        [Parameter]
        public string LogOutButtonStyle { get; set; } = null!;
        [Parameter]
        public string ProfileButtonClass { get; set; } = null!;
        [Parameter]
        public string ProfileButtonStyle { get; set; } = null!;

        [Inject]
        private IAuthenticationManager _authenticationManager { get; set; } = null!;

        [Inject]
        private IDialogService dialogService { get; set; } = null!;

        private async Task Logout()
        {
            await _authenticationManager.LogoutUserAsync();
        }

        private async Task ShowLoginDialog()
        {
            var dialogOptions = new DialogOptions
            {
                NoHeader = true,
                DisableBackdropClick = true,
            };

            await dialogService.ShowAsync<LoginDialog>("Log in", dialogOptions);
        }

        private async Task ShowSignUpDialog()
        {
            var dialogOptions = new DialogOptions
            {
                NoHeader = true,
                DisableBackdropClick = true,
            };

            await dialogService.ShowAsync<SignUpDialog>("Sign Up", dialogOptions);
        }
    }
}
