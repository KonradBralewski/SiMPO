using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shared.Contracts.Requests.Authentication;
using Shared.Validation.Validators.Base;
using FluentValidation;
using Shared.Validation.Validators.Requests.Authentication;
using Shared.Validation.Validators.Forms.Authentication;
using Shared.Contracts.Forms.Authentication;

namespace Components.Authentication.Login
{
    public partial class LoginDialog
    {
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
            var result = _loginFormDataValidator.ValidateValue;

            await _mudForm.Validate();

            if(_mudForm.IsValid)
            {
            }
        }
        void Cancel() => MudDialog.Cancel();
    }
}
