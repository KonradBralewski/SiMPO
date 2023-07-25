using Blazored.LocalStorage;
using MudBlazor;

namespace SiMPO.Blazor.Infrastracture.AppSettings
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
