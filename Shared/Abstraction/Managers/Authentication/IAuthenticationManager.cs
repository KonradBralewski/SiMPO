using ErrorOr;
using Shared.Contracts.Requests.Authentication;
using Shared.Contracts.Responses.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Abstraction.Managers.Authentication
{
    public interface IAuthenticationManager : IManager
    {
        Task<ErrorOr<AuthenticationResponse>> RegisterUserAsync(RegisterRequest request);

        Task<ErrorOr<AuthenticationResponse>> LoginUserAsync(LoginRequest request);
        Task<ErrorOr<Task>> LogoutUserAsync();
    }
}
