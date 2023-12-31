﻿using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Requests.Authentication;
using Core.Common.Interfaces.Services;
using Core.Controllers.Base;
using Core.Controllers.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Core.Controllers
{
    [AllowAnonymous]
    [Route("api/auth")]
    public class AuthenticationController : CustomControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [ModelValidator]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            var registerResult = await _authenticationService.Register(request);

            return registerResult.Match(
                authResult => Ok(authResult),
                errors => Problem(errors));
        }

        [ModelValidator]
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
