using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Players
{
    public partial class PlayerCard
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; } = null!;

        [Parameter]
        public string? Nickname { get; set; }
        [Parameter]
        public string? PlayerRole { get; set; }
        [Parameter]
        public string? Description { get; set; }
        [Parameter]
        public string? DiscordId { get; set; }

        private string _playerRoleStyling { get; set; } = null!;
        protected override void OnInitialized()
        {
            Nickname ??= "No name was given :(";
            Description ??= "No description was given :(";
            PlayerRole ??= "Regular player";
            
            _playerRoleStyling = ReturnProperPlayerRoleTextStyling();
        }

        private async Task GoToDiscord()
        {
            await JSRuntime.InvokeAsync<object>("open", "https://www.google.com/", "_blank");
        }

        private string ReturnProperPlayerRoleTextStyling() => PlayerRole switch
        {
            "Admin" => "color: green; font-weight: bold;",
            "RegularPlayer" => "color: orange; font-weight: 600;",
            _ => "display: none;"
        };
    }

}
