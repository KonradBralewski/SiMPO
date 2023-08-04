using Microsoft.AspNetCore.Components;
using Blazor.Infrastracture.AppSettings;

namespace Blazor.Pages
{
    public partial class Index : IDisposable
    {
        [CascadingParameter]
        private IAppSettingsProvider _appSettingsProvider { get; set; } = null!;

        private bool _isDarkMode;

        protected override async Task OnInitializedAsync()
        {
            _appSettingsProvider.OnSettingsChanged += this.OnChange;
            _isDarkMode = await GetDarkMode();
        }

        private async Task<bool> GetDarkMode()
        {
            var preferences = await _appSettingsProvider!.GetUserPreferences();

            return preferences.isDarkMode;
        }
        private async void OnChange(object? sender, EventArgs eventArgs)
        {
            _isDarkMode = await GetDarkMode();
            await this.InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            _appSettingsProvider.OnSettingsChanged -= this.OnChange;
        }
    }
}
