using Core.Common.Errors.MightHappen;
using Core.Infrastracture.Persistence.Entities;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Abstraction.Persistence.Repositories;
using Shared.Contracts.Responses.ChatHistory;
using Shared.Contracts.Responses.Identity;

namespace Core.Infrastracture.Persistence.Repositories
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ChatMessageRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<SingleChatMessageResponse>> AddMessage(Guid userId, string message)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if(user is null)
            {
                return Errors.MightHappen.GeneralDomainUnit.UnitNotFound(nameof(ApplicationUser));
            }

            var newMessage = new ChatMessage
            {
                Message = message,
                TimeStamp = DateTime.UtcNow,
                User = user 
            };

            _dbContext.Messages.Add(newMessage);

            await _dbContext.SaveChangesAsync();

            var messageResponse = new SingleChatMessageResponse(newMessage.Id.ToString(),
                                                                newMessage.TimeStamp,
                                                                newMessage.User.UserName,
                                                                newMessage.Message);

            return messageResponse;
        }

        public async Task<ErrorOr<SingleChatMessageResponse>> GetAsync(Guid messageId)
        {
            var message = await _dbContext.Messages
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == messageId);

            if (message is null)
            {
                return Errors.MightHappen.GeneralDomainUnit.UnitNotFound(nameof(SingleChatMessageResponse));
            }

            var messageResponse = new SingleChatMessageResponse(message.Id.ToString(),
                                                                message.TimeStamp,
                                                                message.User.UserName,
                                                                message.Message);

            return messageResponse;
        }

        public async Task<ErrorOr<IEnumerable<SingleChatMessageResponse>>> GetMessagesAsync(int page, int entriesPerPage)
        {
            var messages = _dbContext.Messages
                .Include(x => x.User)
                .OrderByDescending(x => x.TimeStamp)
                .Skip(page * entriesPerPage)
                .Take(entriesPerPage)
                .Include(x => x.User);

            var messagesResponse = await messages.Select(x => new SingleChatMessageResponse(x.Id.ToString(),
                                                                x.TimeStamp,
                                                                x.User.UserName,
                                                                x.Message)).ToListAsync();

            return messagesResponse;
        }
    }
}
