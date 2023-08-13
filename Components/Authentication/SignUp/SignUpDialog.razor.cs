using Microsoft.AspNetCore.Components;
using MudBlazor;
using Components.Informations;
using Shared.Contracts.Forms.Authentication;
using Shared.Validation.Validators.Forms.Authentication;

namespace Components.Authentication.SignUp
{
    public partial class SignUpDialog
    {
        [Inject]
        private RegisterFormDataValidator _registerRequestValidator { get; set; } = null!;

        [Inject]
        public IDialogService DialogService { get; set; } = null!;

        [CascadingParameter] 
        public MudDialogInstance MudDialog { get; set; } = null!;

        private MudForm _mudForm = null!;

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
                StateHasChanged();

                return;
            }

            // true is being passed after pressing the accept button
            if((bool)result.Data == true)
            {
                _registerFormData.AcceptedTermsOfService = true;
                StateHasChanged();
            }
        }
        private void Cancel() => MudDialog.Close(DialogResult.Ok(true));
        private void Submit()
        {
            _mudForm.Validate();
        }
    }
}
