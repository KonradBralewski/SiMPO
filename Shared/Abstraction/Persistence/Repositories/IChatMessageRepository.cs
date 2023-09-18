using ErrorOr;
using Shared.Contracts.Responses.ChatHistory;
using Shared.Contracts.Responses.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Abstraction.Persistence.Repositories
{
    public interface IChatMessageRepository
    {
        Task<ErrorOr<SingleChatMessageResponse>> AddMessage(Guid userId, string message);
        Task<ErrorOr<SingleChatMessageResponse>> GetAsync(Guid messageId);
        Task<ErrorOr<IEnumerable<SingleChatMessageResponse>>> GetMessagesAsync(int page, int entriesPerPage);
    }
}
