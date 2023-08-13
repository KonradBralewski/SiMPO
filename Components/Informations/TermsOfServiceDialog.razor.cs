using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Components.Informations
{
    public partial class TermsOfServiceDialog
    {
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; } = null!;
        private void Accept() => MudDialog.Close(DialogResult.Ok(true));
        private void Cancel() => MudDialog.Cancel();
    }
}
