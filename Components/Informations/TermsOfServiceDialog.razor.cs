using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SiMPO.Shared.Contracts.Requests.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiMPO.Components.Informations
{
    public partial class TermsOfServiceDialog
    {
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; } = null!;
        private void Submit() => MudDialog.Close(DialogResult.Ok(true));
    }
}
