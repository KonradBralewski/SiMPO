using Microsoft.AspNetCore.Components;
using MudBlazor;
using Components.Informations;
using Shared.Contracts.Forms.Authentication;
using Shared.Validation.Validators.Forms.Authentication;
using Microsoft.JSInterop;
using Shared.Abstraction.Managers.Identity;
using Shared.Contracts.Requests.Authentication;

namespace Components.Authentication.SignUp
{
    public partial class SignUpDialog
    {
        [Inject]
        private IUserManager _userManager { get; set; } = null!;

        [Inject]
        public IJSRuntime JSRuntime { get; set; } = null!;

        [Inject]
        private RegisterFormDataValidator _registerRequestValidator { get; set; } = null!;

        [Inject]
        public IDialogService DialogService { get; set; } = null!;

        [CascadingParameter] 
        public MudDialogInstance MudDialog { get; set; } = null!;

        private MudForm _mudForm = null!;

        private MudCheckBox<bool> _termsOfServiceCheckbox = null!;

        private RegisterFormData _registerFormData = null!;

        protected override void OnInitialized()
        {
            _registerFormData = new();
        }
        private async void handleTermsOfServiceCheckbox()
        {
            if (_registerFormData.AcceptedTermsOfService)
            {
                _registerFormData.AcceptedTermsOfService = false;
                await _termsOfServiceCheckbox.Validate();
                StateHasChanged();

                return;
            }

            var dialogOptions = new DialogOptions
            {
                CloseButton = true,
                DisableBackdropClick = true
            };

            var dialogRef = await DialogService
                .ShowAsync<TermsOfServiceDialog>("TERMS OF SERVICE", dialogOptions);

            var result = await dialogRef.Result;

            if(result.Canceled)
            {
                _registerFormData.AcceptedTermsOfService = false;
                await _termsOfServiceCheckbox.Validate();
                StateHasChanged();

                return;
            }

            _registerFormData.AcceptedTermsOfService = true;

            await _termsOfServiceCheckbox.Validate();
            StateHasChanged();

        }
        private void Cancel() => MudDialog.Close(DialogResult.Ok(true));
        private async Task Submit()
        {
            await _mudForm.Validate();

            if (_mudForm.IsValid)
            {
                await _userManager.RegisterUserAsync(new RegisterRequest(_registerFormData.Nickname,
                                                                         _registerFormData.Email,
                                                                         _registerFormData.Password,
                                                                         _registerFormData.Description,
                                                                         _registerFormData.DiscordId));
            }
        }
    }
}
