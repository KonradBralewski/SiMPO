using Core.Infrastracture.Persistence.Entities;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Abstraction.Persistence.Repositories;
using Shared.Contracts.Responses.Identity;

namespace Core.Infrastracture.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task<ErrorOr<IEnumerable<UserResponse>>> GetAllAsync()
        {
            var users = await _userManager.Users
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .ToListAsync();
             

            if(users is null)
            {
                // to do: refactor with already defined message
                return Error.Unexpected();
            }

            var usersResponse = users.Select(u => new UserResponse(
                    u.UserName,
                    String.Join('\n', u.UserRoles.First().Role),
                    u.Description,
                    u.DiscordId
                ));

            return ErrorOrFactory.From(usersResponse);
        }

        public Task<ErrorOr<UserResponse>> GetAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
