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
            public static class User
            {
                public static Error DuplicateEmail => Error.Conflict(
                    code: "User.DuplicateEmail",
                    description: "User email is already in use.");
            }
        }
    }
}
