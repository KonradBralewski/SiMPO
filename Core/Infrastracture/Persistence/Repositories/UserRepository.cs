using Core.Common.Errors.MightHappen;
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
                    u.Nickname,
                    String.Join('\n', u.UserRoles.First().Role),
                    u.Description,
                    u.DiscordId
                ));

            return ErrorOrFactory.From(usersResponse);
        }

        public async Task<ErrorOr<UserResponse>> GetAsync(Guid userId)
        {
            var user = await _userManager.Users
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == userId);

            if(user is null)
            {
                return Errors.MightHappen.GeneralDomainUnit.UnitNotFound(nameof(ApplicationUser));
            }

            var userResponse = new UserResponse(user.Nickname,
                                                String.Join('\n', user.UserRoles.First().Role),
                                                user.Description,
                                                user.DiscordId);

            return userResponse;
        }
    }
}
