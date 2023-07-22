using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiMPO.Core.Common.Interfaces.Services;
using SiMPO.Core.Controllers.Base;
using SiMPO.Shared.Contracts.Requests.Authentication;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SiMPO.Core.Controllers
{
    [AllowAnonymous]
    [Route("auth")]
    public class AuthenticationController : CustomControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            var registerResult = await _authenticationService.Register(request);

            return registerResult.Match(
                authResult => Ok(authResult),
                errors => Problem(errors));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var loginResult = await _authenticationService.Login(request);

            return loginResult.Match(
                authResult => Ok(authResult),
                errors => Problem(errors));
        }
    }

}
