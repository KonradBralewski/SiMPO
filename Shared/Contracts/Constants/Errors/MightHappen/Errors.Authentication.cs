using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Errors.MightHappen
{
    public static partial class Errors
    {
        public static partial class MightHappen 
        {
            public static class Authentication
            {
                public static Error InvalidCredentials => Error.Validation(
                    code: "Authentication.InvalidCredentials",
                    description: "Invalid credentials");

                public static Error NotAuthenticated => Error.Validation(
                    code: "Authentication.NotAuthenticated",
                    description: "Not authenticated");
            }
        }

    }
}
