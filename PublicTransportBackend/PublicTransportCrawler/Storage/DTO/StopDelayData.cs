namespace PublicTransportCrawler.Vehicles.Adapters;

public class StopDelayStorage{
    public string StopName { get; set; }
    public string StopId { get; set; }
    public string ScheduleTime { get; set; }
    public string ActualTime { get; set; }
    public int? DelayInMinutes { get; set; }
    
}