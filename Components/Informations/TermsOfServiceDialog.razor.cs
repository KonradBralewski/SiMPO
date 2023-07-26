using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Components.Informations
{
    public partial class TermsOfServiceDialog
    {
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; } = null!;
        private void Submit() => MudDialog.Close(DialogResult.Ok(true));
    }
}
