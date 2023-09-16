namespace Blazor.Pages
{
    public partial class Map
    {
        private bool _isMapLoaded;

        protected override void OnInitialized()
        {
            _isMapLoaded = false;
        }
    }
}
