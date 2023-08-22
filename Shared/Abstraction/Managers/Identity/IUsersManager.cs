using ErrorOr;
using Shared.Contracts.Requests.Authentication;
using Shared.Contracts.Requests.Identity;
using Shared.Contracts.Responses.Authentication;
using Shared.Contracts.Responses.Identity;

namespace Shared.Abstraction.Managers.Identity
{
    public interface IUserManager : IManager
    {
        Task<ErrorOr<AuthenticationResponse>> RegisterUserAsync(RegisterRequest request);

        Task<ErrorOr<AuthenticationResponse>> LoginUserAsync(LoginRequest request);

        Task<ErrorOr<Task>> ResetForgottenPasswordAsync(ForgottenPasswordRequest request);

        Task<ErrorOr<UserResponse>> GetAsync(string userId);
        Task<ErrorOr<IEnumerable<UserResponse>>> GetAllAsync();

        Task<ErrorOr<UserRolesResponse>> GetRolesAsync(string userId);

    }
}
