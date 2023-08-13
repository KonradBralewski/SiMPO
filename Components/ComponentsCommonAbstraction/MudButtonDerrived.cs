using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.ComponentsCommonAbstraction
{
    public abstract class MudButtonDerrived : ComponentBase
    {
        [Parameter]
        public virtual string MudButtonClass { get; set; } = null!;
        [Parameter]
        public string MudButtonStyle { get; set; } = null!;

        [Parameter]
        public EventCallback<MouseEventArgs> MudButtonOnClick { get; set; }
    }
}
