using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Components.Authentication.Login
{
    public partial class LoginDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; } = null!;

        void Submit() => MudDialog.Close(DialogResult.Ok(true));
        void Cancel() => MudDialog.Cancel();
    }
}
