using ErrorOr;
using Shared.Contracts.Responses.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Abstraction.Persistence.Repositories
{
    public interface IUserRepository
    {
        Task<ErrorOr<UserResponse>> GetAsync(Guid userId);
        Task<ErrorOr<IEnumerable<UserResponse>>> GetAllAsync();
    }
}
