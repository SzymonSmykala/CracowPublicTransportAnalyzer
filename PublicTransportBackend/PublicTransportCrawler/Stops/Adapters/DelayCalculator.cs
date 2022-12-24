using System;
using Azure.Core.Pipeline;
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

    public int Execute(string actualTime, string plannedTime)
    {
        if (actualTime == null || plannedTime == null)
        {
            return 0;
        }
        
        var actual = new TimeSpan(int.Parse(actualTime.Split(':')[0]), 
            int.Parse(actualTime.Split(':')[1]),
            0);
        var planet = new TimeSpan(int.Parse(plannedTime.Split(':')[0]), 
            int.Parse(plannedTime.Split(':')[1]),
            0);
        var result = actual - planet;

        return (int) result.TotalMinutes;
    }
}