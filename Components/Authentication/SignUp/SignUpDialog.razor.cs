﻿using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Components.Informations;
using Shared.Contracts.Requests.Authentication;

namespace Components.Authentication.SignUp
{
    public partial class SignUpDialog
    {
        private FluentValidationValidator _fluentValidationValidator = null!;
        [CascadingParameter] 
        public MudDialogInstance MudDialog { get; set; } = null!;

        [Inject]
        public IDialogService DialogService { get; set; } = null!;


        private RegisterRequest _user = new();
        private string _passwordConfirmation = null!;
        private bool _checkedTOS = false;

        private async void ShowTOS()
        {
            var dialogOptions = new DialogOptions
            {
                CloseButton = true,
                FullScreen = true
            };

            await DialogService.ShowAsync<TermsOfServiceDialog>("TERMS OF SERVICE", dialogOptions);
        }
        private void Cancel() => MudDialog.Close(DialogResult.Ok(true));
        private void Submit() => MudDialog.Close(DialogResult.Ok(true));
    }
}
