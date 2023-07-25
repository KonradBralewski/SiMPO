using Microsoft.AspNetCore.Components;
using MudBlazor;
using SiMPO.Blazor.Infrastracture.AppSettings;

namespace SiMPO.Blazor.Shared
{
    public partial class MainLayout
    {
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
