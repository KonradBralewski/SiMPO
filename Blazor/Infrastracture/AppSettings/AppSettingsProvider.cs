using Blazored.LocalStorage;
using MudBlazor;
using Blazor.Infrastracture.AppSettings;

namespace Blazor.Infrastracture.AppSettings
{
    public class AppSettingsProvider : IAppSettingsProvider
    {

        private readonly ILocalStorageService _localStorageService;
        public AppSettingsProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public EventHandler? OnSettingsChanged { get; set; }

        public MudTheme GetAppTheme()
        {
            return SimpoTheme.CustomTheme;
        }

        public async Task<UserPreferences> GetUserPreferences()
        {
            // TODO: Implement custom JSON Serializer to make it quick and smooth'

            bool isDarkMode = await _localStorageService.GetItemAsync<bool>(nameof(UserPreferences.isDarkMode));

            return new UserPreferences { isDarkMode = isDarkMode };
        }

        public async Task SetIsDarkMode(bool isDarkMode)
        {
            await _localStorageService.SetItemAsync(nameof(UserPreferences.isDarkMode), isDarkMode);
            NotifySettingsChanged();
        }
        public void NotifySettingsChanged()
        {
            OnSettingsChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
