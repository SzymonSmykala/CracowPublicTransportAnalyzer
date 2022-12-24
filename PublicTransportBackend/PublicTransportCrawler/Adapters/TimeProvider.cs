using System;

namespace PublicTransportCrawler.Adapters;

internal class TimeProvider : ITimeProvider
{
    public DateTime GetCurrentTime()
    {
        var centralEuropeanTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, centralEuropeanTimeZone);
    }
}