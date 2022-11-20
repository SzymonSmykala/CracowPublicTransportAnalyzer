using System;

namespace PublicTransportCrawler.Storage.DTO;

public class DelayStorage
{
    public int DelayInMinutes { get; set; }
    public string TripId { get; set; }
    public DateTime Timestamp { get; set; }
    public string StopId { get; set; }
    public string id { get; set; }
}