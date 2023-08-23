using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shared.Abstraction.Managers.Authentication;

namespace Blazor.Pages
{
    public partial class AccessDenied
    {
        [Inject]
        private IAuthenticationManager _authenticationManager { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await _authenticationManager.LogoutUserAsync();
        }
    }
}
