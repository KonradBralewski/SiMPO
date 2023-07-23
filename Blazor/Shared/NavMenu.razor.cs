﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SiMPO.Blazor.Infrastracture.AppSettings;

namespace SiMPO.Blazor.Shared
{
    public partial class NavMenu
    {
        [Parameter]
        public EventCallback ToggleDarkMode { get; set; }
        [Parameter]
        public bool IsDarkMode { get; set; }
        private string _togglerLabel = null!;


        protected override void OnInitialized()
        {
            UpdateTogglerLabel();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender) { return; }

            if(IsDarkMode && _togglerLabel.Contains("Light") || !IsDarkMode && _togglerLabel.Contains("Dark")) { return; }

            UpdateTogglerLabel();
            StateHasChanged();
        }

        private void UpdateTogglerLabel()
        {
            _togglerLabel = "Toggle " + (IsDarkMode ? "Light Mode" : "Dark Mode");
        }

    }
}
