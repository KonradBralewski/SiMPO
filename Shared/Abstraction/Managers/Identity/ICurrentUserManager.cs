using ErrorOr;
using Shared.Contracts.Responses.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Abstraction.Managers.Identity
{
    public interface ICurrentUserManager : IManager
    {
        Task<ErrorOr<UserResponse>> GetCurrentUserAsync();
    }
}
