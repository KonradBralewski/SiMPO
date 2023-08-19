using ErrorOr;
using Microsoft.AspNetCore.Components;
using Shared.Contracts.Responses.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Authentication.Common
{
    public partial class RequestResultDialogExtension
    {
        [Parameter]
        public ErrorOr<AuthenticationResponse>? RequestResult { get; set; }
        [Parameter]
        public bool IsWaiting { get; set; }
    }
}
