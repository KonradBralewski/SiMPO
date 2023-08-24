using Microsoft.AspNetCore.Components;
using Shared.Abstraction.Managers.Identity;

namespace Blazor.Pages
{
    public partial class Profile
    {
        [Inject]
        private ICurrentUserManager _currentUserManager { get; set; } = null!;

    }
}
