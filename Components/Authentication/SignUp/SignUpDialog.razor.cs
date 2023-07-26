using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Components.Informations;
using Shared.Contracts.Requests.Authentication;
using Blazorise.Animate;
using Blazorise;

namespace Components.Authentication.SignUp
{
    public partial class SignUpDialog
    {
        private FluentValidationValidator _fluentValidationValidator = null!;
        [CascadingParameter] 
        public MudDialogInstance MudDialog { get; set; } = null!;

        [Inject]
        public IDialogService DialogService { get; set; } = null!;


        private RegisterRequest _user = null!;
        private string _passwordConfirmation = null!;

        private bool _checkedTOS = false;

        // TOS Hover Animation
        private bool _shouldAnimateTOS;
        private int _tosAnimationDuration;

        // TOS Hover Animation

        protected override void OnInitialized()
        {
            _shouldAnimateRegister = false;
            _shouldAnimateRegister = false;

            _checkedTOS = false;
            _shouldAnimateTOS = false;
            _tosAnimationDuration = 500;

            _passwordConfirmation = String.Empty;
            _user = new();
        }

        private async void ShowTOS()
        {
            var dialogOptions = new DialogOptions
            {
                CloseButton = true,
                FullScreen = true
            };

            await DialogService.ShowAsync<TermsOfServiceDialog>("TERMS OF SERVICE", dialogOptions);
        }

        private async Task OnTermsOfServiceHover()
        {
            _shouldAnimateTOS = true;
            StateHasChanged();
            await Task.Delay(_tosAnimationDuration);
        }
        private async Task OnTermsOfServiceUnhover()
        {
            _shouldAnimateTOS = false;
            StateHasChanged();
        }

        private void Cancel() => MudDialog.Close(DialogResult.Ok(true));
        private void Submit() => MudDialog.Close(DialogResult.Ok(true));
    }
}
