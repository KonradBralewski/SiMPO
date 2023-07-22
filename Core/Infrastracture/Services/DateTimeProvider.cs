
using SiMPO.Core.Common.Interfaces.Services;

namespace SiMPO.Core.Infrastracture.Services
{
    internal sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
