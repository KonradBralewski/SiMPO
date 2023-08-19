using Microsoft.AspNetCore.Identity;
using ErrorOr;
using Shared.Contracts.Requests.Authentication;
using Shared.Contracts.Responses.Authentication;
using Core.Common.Interfaces.Authentication;
using Core.Common.Interfaces.Services;
using Core.Infrastracture.Persistence.Entities;
using Core.Common.Errors.MightHappen;

namespace Core.Infrastracture.Authentication
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

            ApplicationUser user = new()
            {
                UserName = request.Email,
                Email = request.Email,
                Nickname = request.Nickname,
                DiscordId = request.DiscordId,
                Description = request.Description,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                // should never happen, to do: log error in database
                return Error.Unexpected();
            }

            //await _userManager.AddToRoleAsync(user, UserRoles.User);

            var token = _tokenGenerator.GenerateToken(user);

            return new AuthenticationResponse(user.Id,
                                             user.Email,
                                             token);
        }
    }
}
