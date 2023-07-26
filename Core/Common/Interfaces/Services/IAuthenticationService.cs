using ErrorOr;
using Shared.Contracts.Requests.Authentication;
using Shared.Contracts.Responses.Authentication;

namespace Core.Common.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<ErrorOr<AuthenticationResponse>> Register(RegisterRequest request);
        Task<ErrorOr<AuthenticationResponse>> Login(LoginRequest request);
    }
}
