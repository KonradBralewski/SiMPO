using Microsoft.AspNetCore.Components;
using Blazor.Infrastracture.AppSettings;

namespace Blazor.Pages
{
    public partial class Index : IDisposable
    {
        [CascadingParameter]
        private IAppSettingsProvider _appSettingsProvider { get; set; } = null!;

        private string _pageBackgroundLight = "images/indexPageBackground/indexBackgroundLight.png";
        private string _pageBackgroundDark = "images/indexPageBackground/indexBackgroundDark.png";

        private string correctPath = null!;

        protected override async Task OnInitializedAsync()
        {
            _appSettingsProvider.OnSettingsChanged += this.OnChange;
            correctPath = await GetBackgroundPath();
        }

        private async Task<string> GetBackgroundPath()
        {
            var preferences = await _appSettingsProvider!.GetUserPreferences();

            if (preferences.isDarkMode)
            {
                return _pageBackgroundDark;
            }

            return _pageBackgroundLight;
        }
        private async void OnChange(object? sender, EventArgs eventArgs)
        {
            correctPath = await GetBackgroundPath();
            await this.InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            _appSettingsProvider.OnSettingsChanged -= this.OnChange;
        }
    }
}
