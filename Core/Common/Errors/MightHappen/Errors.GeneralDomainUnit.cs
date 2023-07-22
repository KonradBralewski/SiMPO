using ErrorOr;
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
            public static class GeneralDomainUnit
            {
                public static Error UnitNotFound(string unitName)
                {
                    return Error.NotFound("DomainModels.NotFound", $"The requested unit {unitName} was not found.");
                }
            }
        }

    }
}
