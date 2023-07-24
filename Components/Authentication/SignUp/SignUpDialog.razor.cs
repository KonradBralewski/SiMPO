using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SiMPO.Shared.Contracts.Requests.Authentication;

namespace SiMPO.Components.Authentication.SignUp
{
    public partial class SignUpDialog
    {
        private RegisterRequest _user = new();
        private FluentValidationValidator _fluentValidationValidator = null!;
        [CascadingParameter] public MudDialogInstance MudDialog { get; set; } = null!;

        private void Submit() => MudDialog.Close(DialogResult.Ok(true));
        private void Cancel() => MudDialog.Cancel();
    }
}
