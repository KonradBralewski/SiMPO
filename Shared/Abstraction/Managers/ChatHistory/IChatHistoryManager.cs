using ErrorOr;
using Shared.Contracts.Responses.ChatHistory;


namespace Shared.Abstraction.Managers.ChatHistory
{
    public interface IChatHistoryManager : IManager
    {
        Task<ErrorOr<IEnumerable<SingleChatMessageResponse>>> GetAllMessagesAsync();
        Task<ErrorOr<SingleChatMessageResponse>> GetAsync(string messageId);

    }
}
