using ErrorOr;
using SiMPO.Shared.Contracts.Requests.Authentication;
using SiMPO.Shared.Contracts.Responses.Authentication;

namespace SiMPO.Core.Common.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<ErrorOr<AuthenticationResponse>> Register(RegisterRequest request);
        Task<ErrorOr<AuthenticationResponse>> Login(LoginRequest request);
    }
}
