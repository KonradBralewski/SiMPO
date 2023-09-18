using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using Shared.Abstraction.Authentication;

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
        private NavigationManager _navigation { get; set; } = null!;

        [Inject]
        private AuthenticationStateProvider _authenticationStateProvider { get; set; } = null!;

        [Inject]
        private  ISnackbar _snackBar { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            _userMessage = String.Empty;

            _hubConnection = new HubConnectionBuilder()
            .WithUrl(_navigation.ToAbsoluteUri("/ChatHub"))
            .Build();

            _hubConnection.On<string, string, DateTime>("ReceiveMessage", OnReceiveMessage);
            _hubConnection.On<string>("SendMessageFailure", OnSendMessageFailure);

            await _hubConnection.StartAsync();
        }

        private void OnReceiveMessage(string username, string message, DateTime timeStamp)
        {
            var encodedMsg = $"{timeStamp}, {username}: {message}";
            _messages.Add(encodedMsg);
            StateHasChanged();
        }

        private void OnSendMessageFailure(string failureReason)
        {
            _snackBar.Add(failureReason, Severity.Error);
        }

        private async Task Send()
        {
            var userId = await ((BaseCustomAuthenticationStateProvider)_authenticationStateProvider).GetUserIdAsync();

            if(userId is null)
            {
                OnSendMessageFailure("Failed to receive your userId. Session reset.");
                _navigation.NavigateTo("/accessDenied");

                return;
            }

            if (_hubConnection is not null)
            {
                await _hubConnection.SendAsync("SendMessage", userId, _userMessage);
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
