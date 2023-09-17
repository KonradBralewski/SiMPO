using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;

namespace Components.Chat
{
    public partial class ChatDialog : IAsyncDisposable
    {
        private HubConnection? _hubConnection = null!;

        private string _userMessage = null!;
        
        private List<string> _messages = new List<string>();

        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; } = null!;

        [Inject]
        private NavigationManager Navigation { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            _userMessage = String.Empty;

            _hubConnection = new HubConnectionBuilder()
            .WithUrl(Navigation.ToAbsoluteUri("/ChatHub"))
            .Build();

            _hubConnection.On<string>("ReceiveMessage", (message) =>
            {
                var encodedMsg = $"{DateTime.Now.ToString()}: {message}";
                _messages.Add(message);
                StateHasChanged();
            });

            await _hubConnection.StartAsync();
        }

        private async Task Send()
        {
            if (_hubConnection is not null)
            {
                await _hubConnection.SendAsync("SendMessage", _userMessage);
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection is not null)
            {
                await _hubConnection.DisposeAsync();
            }
        }
    }
}
