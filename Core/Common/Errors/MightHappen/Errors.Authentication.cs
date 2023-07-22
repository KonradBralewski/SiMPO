﻿using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiMPO.Core.Common.Errors
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
            }
        }

    }
}
