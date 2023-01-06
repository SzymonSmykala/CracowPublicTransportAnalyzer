using System;
using PublicTransportCrawler.Stops.DTO;

namespace PublicTransportCrawler.Stops.Adapters;

public interface IDelayCalculator
{
    TimeSpan Execute(Actual actual);
    int Execute(string actualTime, string plannedTime);
}