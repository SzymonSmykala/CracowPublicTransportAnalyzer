using System;
using PublicTransportCrawler.Stops.DTO;

namespace PublicTransportCrawler.Stops.Adapters;

internal class DelayCalculator : IDelayCalculator
{
    public TimeSpan Execute(Actual actual)
    {
        if (string.IsNullOrEmpty(actual.ActualTime))
        {
            actual.ActualTime = actual.PlannedTime;
        }
        
        var actualTime = new TimeSpan(int.Parse(actual.ActualTime.Split(':')[0]), 
            int.Parse(actual.ActualTime.Split(':')[1]),
            0);
        var plannedTime = new TimeSpan(int.Parse(actual.PlannedTime.Split(':')[0]), 
            int.Parse(actual.PlannedTime.Split(':')[1]),
            0);
        
        return actualTime - plannedTime;
    }
}