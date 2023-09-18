using Core.Infrastracture.Persistence.Entities;
using Microsoft.AspNetCore.SignalR;
using Shared.Abstraction.Persistence.Repositories;

namespace Core.Infrastracture.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        public ChatHub(IChatMessageRepository chatMessageRepository)
        {
            _chatMessageRepository = chatMessageRepository;   
        }

        public async Task SendMessage(Guid userId, string message)
        {
            var createdMessage = await _chatMessageRepository.AddMessage(userId, message);

            if (createdMessage.IsError)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("SendMessageFailure", createdMessage.Errors.First());
            }

            await Clients.All.SendAsync("ReceiveMessage", createdMessage.Value.Username, createdMessage.Value.Message, createdMessage.Value.TimeStamp);
        }
    }
}
