using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.ComponentsCommonAbstraction
{
    public abstract class MudCardDerrived : ComponentBase
    {
        [Parameter]
        public virtual string MudCardClass { get; set; } = null!;
        [Parameter]
        public string MudCardStyle { get; set; } = null!;
        [Parameter]
        public virtual string MudCardHeaderClass { get; set; } = null!;
        [Parameter]
        public string MudCardHeaderStyle { get; set; } = null!;
        [Parameter]
        public string MudCardContentClass { get; set; } = null!;
        [Parameter]
        public string MudCardContentStyle { get; set; } = null!;
        [Parameter]
        public string MudCardActionsClass { get; set; } = null!;
        [Parameter]
        public string MudCardActionsStyle { get; set; } = null!;
    }
}
