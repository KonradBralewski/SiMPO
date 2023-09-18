using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.Responses.ChatHistory
{
    public sealed record SingleChatMessageResponse(string Id,
                                                   DateTime TimeStamp,
                                                   string Username,
                                                   string Message);
}
