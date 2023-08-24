using Core.Controllers.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Abstraction.Persistence.Repositories;
using Shared.Contracts.Requests.Authentication;

namespace Core.Controllers.Identity
{
    [Route("api/identity/[controller]")]
    [ApiController]
    public class UsersController : CustomControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet("me")]
        public async Task<IActionResult> GetCallingUserAsync()
        {
            var getUsersResult = await _userRepository.GetAsync(RetrieveRequestSendingUserId());

            return getUsersResult.Match(
                usersResult => Ok(usersResult),
                errors => Problem(errors));
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            var getUsersResult = await _userRepository.GetAllAsync();

            return getUsersResult.Match(
                usersResult => Ok(usersResult),
                errors => Problem(errors));
        }

    }
}
