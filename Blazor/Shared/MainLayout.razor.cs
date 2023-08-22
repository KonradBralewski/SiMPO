using Blazor.Infrastracture.AppSettings;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;

namespace Blazor.Shared
{
    public partial class MainLayout
    {
        [Inject]
        private IAppSettingsProvider _appSettingsProvider { get; set; } = null!;

        private MudTheme _customTheme = null!;
        private bool _isDarkMode;

        protected override async Task OnInitializedAsync()
        {
            _customTheme = _appSettingsProvider.GetAppTheme();

            var preferences = await _appSettingsProvider.GetUserPreferences();

            _isDarkMode = preferences.isDarkMode;

        }

        private void ToggleDarkMode()
        {
            _isDarkMode = !_isDarkMode;
            _appSettingsProvider!.SetIsDarkMode(_isDarkMode);
        }
    }
}
