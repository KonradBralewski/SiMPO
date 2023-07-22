using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SiMPO.Core.Infrastracture.Persistence.Entities;
using SiMPO.Core.Common.Interfaces.Authentication;
using SiMPO.Shared.Contracts.Responses.Authentication;
using SiMPO.Shared.Contracts.Requests.Authentication;
using SiMPO.Core.Common.Interfaces.Services;
using SiMPO.Core.Common.Errors;
using ErrorOr;

namespace SiMPO.Core.Infrastracture.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public AuthenticationService(UserManager<ApplicationUser> userManager, IJwtTokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }
        public async Task<ErrorOr<AuthenticationResponse>> Login(LoginRequest request)
        {
            // to do requests validation

            var user = await _userManager.FindByEmailAsync(request.Email);

            if(user is null)
            {
                return Errors.MightHappen.Authentication.InvalidCredentials;
            }

            if(await _userManager.CheckPasswordAsync(user, request.Password) is false)
            {
                return Errors.MightHappen.Authentication.InvalidCredentials;
            }

            var token = _tokenGenerator.GenerateToken(user);

            return new AuthenticationResponse(user.Id,
                                              user.Email,
                                              token);
        }

        public async Task<ErrorOr<AuthenticationResponse>> Register(RegisterRequest request)
        {
            var userSearchResult = await _userManager.FindByEmailAsync(request.Email);

            if (userSearchResult is not null)
            {
                return Errors.MightHappen.User.DuplicateEmail;
            }

            // to do requests validation

            ApplicationUser user = new()
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
            }

            //await _userManager.AddToRoleAsync(user, UserRoles.User);

            var token = _tokenGenerator.GenerateToken(user);

            return new AuthenticationResponse(user.Id,
                                             user.Email,
                                             token);
        }
    }
}
