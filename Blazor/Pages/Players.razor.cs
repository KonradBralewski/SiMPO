using Microsoft.AspNetCore.Components;
using Shared.Abstraction.Managers.Identity;
using Shared.Contracts.Responses.Identity;

namespace Blazor.Pages
{
    public partial class Players
    {
        [Inject]
        private IUserManager _userManager { get; set; } = null!;

        IEnumerable<UserResponse>? _users = null!;

        private bool _isWaitingForResponse = false;
        protected override async Task OnInitializedAsync()
        {
            _users = null;

            _isWaitingForResponse = true;
            var usersResponse = await _userManager.GetAllAsync();
            _isWaitingForResponse = false;

            if(!usersResponse.IsError)
            {
                _users = usersResponse.Value;
            }
        }
    }
}
