
using Core.Common.Interfaces.Services;

namespace Core.Infrastracture.Services
{
    internal sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
