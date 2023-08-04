using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Contact
{
    public partial class ServerInvitationCard
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; } = null!;

        private async Task GoToDiscord()
        {
            await JSRuntime.InvokeAsync<object>("open", "https://www.google.com/", "_blank");
        }
    }
}
