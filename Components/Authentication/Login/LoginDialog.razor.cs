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

namespace Components.Authentication.Login
{
    public partial class LoginDialog
    {
        [Inject]
        private IUserManager _userManager { get; set; } = null!;

        private MudForm _mudForm = null!;

        [Inject]
        private LoginFormDataValidator _loginFormDataValidator { get; set; } = null!;

        [CascadingParameter] MudDialogInstance MudDialog { get; set; } = null!;
        private LoginFormData _loginFormData { get; set; } = null!;
        protected override void OnInitialized()
        {
            _loginFormData = new();
        }
        private async Task Submit() 
        {
            await _mudForm.Validate();

            if(_mudForm.IsValid)
            {
                var result = await _userManager.LoginUserAsync(new LoginRequest(_loginFormData.Email,
                                                                                _loginFormData.Password));
                Console.WriteLine(JsonSerializer.Serialize(result));
            }
        }
        void Cancel() => MudDialog.Cancel();
    }
}
