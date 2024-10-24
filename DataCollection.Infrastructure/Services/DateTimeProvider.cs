using DataCollection.Domain.Common.Interfaces;

namespace DataCollection.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
