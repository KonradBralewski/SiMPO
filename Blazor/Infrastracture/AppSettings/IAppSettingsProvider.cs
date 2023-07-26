using Blazored.LocalStorage;
using MudBlazor;
using Blazor.Infrastracture.AppSettings;

namespace Blazor.Infrastracture.AppSettings
{
    public interface IAppSettingsProvider
    {
        EventHandler? OnSettingsChanged { get; set; }
        public MudTheme GetAppTheme();
        public Task<UserPreferences> GetUserPreferences();

        public Task SetIsDarkMode(bool isDarkMode);

        public void NotifySettingsChanged();
        
    }
}
