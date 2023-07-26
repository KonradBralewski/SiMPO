using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Components.Contact
{
    public partial class ContactCard
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; } = null!;

        [Parameter]
        public string? Name { get; set; }
        [Parameter]
        public string? Description { get; set; }
        [Parameter]
        public string? DiscordLink { get; set; }

        [Parameter]
        public string? EmailAddress { get; set; }

        protected override void OnInitialized()
        {
            Name ??= "No name was given :(";
            Description ??= "No description was given :(";
        }

        private async Task GoToDiscord()
        {
            await JSRuntime.InvokeAsync<object>("open", "https://www.google.com/", "_blank");
        }
    }
    
}
