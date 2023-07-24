﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using SiMPO.Blazor.Infrastracture.Authentication;
using SiMPO.Components.Authentication.Login;
using SiMPO.Components.Authentication.SignUp;

namespace SiMPO.Blazor.Shared
{
    public partial class NavMenu
    {
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

        [Inject]
        public IDialogService DialogService { get; set; } = null!;

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

        private void Logout()
        {
            ((CustomAuthenticationStateProvider)AuthenticationStateProvider).MarkUserAsLoggedOut();
        }

        private async Task ShowLoginDialog()
        {
            await DialogService.ShowAsync<LoginDialog>();
        }

        private async Task ShowSignUpDialog()
        {
            await DialogService.ShowAsync<SignUpDialog>();
        }

    }
}
