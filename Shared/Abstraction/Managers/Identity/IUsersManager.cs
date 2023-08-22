using ErrorOr;
using Shared.Contracts.Requests.Authentication;
using Shared.Contracts.Requests.Identity;
using Shared.Contracts.Responses.Authentication;
using Shared.Contracts.Responses.Identity;

namespace Shared.Abstraction.Managers.Identity
{
    public interface IUserManager : IManager
    {
        Task<ErrorOr<IEnumerable<UserResponse>>> GetAllAsync();
        Task<ErrorOr<UserResponse>> GetAsync(string userId);
        Task<ErrorOr<UserRolesResponse>> GetRolesAsync(string userId);
        Task<ErrorOr<Task>> ResetForgottenPasswordAsync(ForgottenPasswordRequest request);

    }
}
