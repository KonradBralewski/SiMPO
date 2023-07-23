﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiMPO.Components.Contact
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
