using System;

namespace PublicTransportCrawler.Adapters;

public interface ITimeProvider
{
    DateTime GetCurrentTime();
}