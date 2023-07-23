using MudBlazor;
using SiMPO.Blazor.Infrastracture.AppSettings;

namespace SiMPO.Blazor.Shared
{
    public partial class MainLayout
    {
        private MudTheme _customTheme = null!;
        private bool _isDarkMode;

        protected override void OnInitialized()
        {
            _customTheme = SimpoTheme.CustomTheme;
            _isDarkMode = SimpoSettings.isDarkMode;
        }

        private void ToggleDarkMode()
        {
            _isDarkMode = !_isDarkMode;
            SimpoSettings.isDarkMode = _isDarkMode;
        }
    }
}
