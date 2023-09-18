using Blazor.Infrastracture.Managers.Http;
using ErrorOr;
using Shared.Abstraction.Managers.ChatHistory;
using Shared.Contracts.Requests.Identity;
using Shared.Contracts.Responses.ChatHistory;
using Shared.Contracts.Responses.Identity;

namespace Blazor.Infrastracture.Managers.ChatHistory
{
    public class ChatHistoryManager : IChatHistoryManager
    {
        private readonly HttpClientOwner _httpClientOwner;

        public ChatHistoryManager(HttpClientOwner httpClientOwner)
        {
            _httpClientOwner = httpClientOwner;
        }

        public async Task<ErrorOr<IEnumerable<SingleChatMessageResponse>>> GetAllMessagesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ErrorOr<SingleChatMessageResponse>> GetAsync(string messageId)
        {
            throw new NotImplementedException();
        }
    }
}
