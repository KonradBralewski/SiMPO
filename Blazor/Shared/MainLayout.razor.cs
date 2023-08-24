using Blazor.Infrastracture.AppSettings;
using Blazor.Infrastracture.State;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;

namespace Blazor.Shared
{
    public partial class MainLayout : IDisposable
    {
        [Inject]
        private IAppSettingsProvider _appSettingsProvider { get; set; } = null!;

        [Inject]
        private ApplicationState _appState { get; set; } = null!;

        private MudTheme _customTheme = null!;
        private bool _isDarkMode;

        protected override async Task OnInitializedAsync()
        {
            _appState.StateChanged += StateHasChanged;

            _customTheme = _appSettingsProvider.GetAppTheme();

            var preferences = await _appSettingsProvider.GetUserPreferences();

            _isDarkMode = preferences.isDarkMode;

        }

        private void ToggleDarkMode()
        {
            _isDarkMode = !_isDarkMode;
            _appSettingsProvider!.SetIsDarkMode(_isDarkMode);
        }

        public void Dispose()
        {
            _appState.StateChanged -= StateHasChanged;
        }
    }
}
