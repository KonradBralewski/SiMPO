using Microsoft.AspNetCore.Components;

namespace Components.Shop
{
    public partial class TokensBalanceInfoBox
    {
        [Parameter]
        public virtual string MudContainerClass { get; set; } = null!;
        [Parameter]
        public string MudContainerStyle { get; set; } = null!;

        private readonly int _balance = 0; // get from API
    }
}
