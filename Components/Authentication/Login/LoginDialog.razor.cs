using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shared.Contracts.Requests.Authentication;
using Shared.Validation.Validators.Base;
using FluentValidation;
using Shared.Validation.Validators.Requests.Authentication;
using Shared.Validation.Validators.Forms.Authentication;
using Shared.Contracts.Forms.Authentication;
using Shared.Abstraction.Managers.Identity;
using System.Text.Json;
using ErrorOr;
using Shared.Contracts.Responses.Authentication;
using Shared.Abstraction.Managers.Authentication;

namespace Components.Authentication.Login
{
    public partial class LoginDialog
    {
        [Inject]
        private IAuthenticationManager _authenticationManager { get; set; } = null!;

        [Inject]
        private NavigationManager _navigationManager { get; set; } = null!;

        private MudForm _mudForm = null!;

        [Inject]
        private LoginFormDataValidator _loginFormDataValidator { get; set; } = null!;

        [CascadingParameter] 
        public MudDialogInstance MudDialog { get; set; } = null!;
        private LoginFormData _loginFormData { get; set; } = null!;

        private ErrorOr<AuthenticationResponse>? _loginRequestResult;
        private bool _isWaitingForRequestResult;
        protected override void OnInitialized()
        {
            _loginFormData = new();
            _loginRequestResult = null;
            _isWaitingForRequestResult = false;

            _loginFormData.Email = "Shlee@Shlee.com";
            _loginFormData.Password = "Shlee!@#$5";
        }
        private async Task Submit() 
        {
            await _mudForm.Validate();

            if(_mudForm.IsValid)
            {
                _isWaitingForRequestResult = true;
                _loginRequestResult = await _authenticationManager.LoginUserAsync(new LoginRequest(_loginFormData.Email,
                                                                                _loginFormData.Password));
                _isWaitingForRequestResult = false;

                if(_loginRequestResult is not null && !_loginRequestResult.Value.IsError) 
                {
                    MudDialog.Close();
                    _navigationManager.NavigateTo("/");
                }
            }
        }
        void Cancel() => MudDialog.Cancel();
    }
}
