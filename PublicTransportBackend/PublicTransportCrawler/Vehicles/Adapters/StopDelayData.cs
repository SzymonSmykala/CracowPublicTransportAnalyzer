namespace PublicTransportCrawler.Vehicles.Adapters;

public class StopDelayData{
    public string StopName { get; set; }
    public string StopId { get; set; }
    public string ScheduleTime { get; set; }
    public string ActualTime { get; set; }
    public int? DelayInMinutes { get; set; }

    public override string ToString()
    {
        return $"{{{nameof(StopName)}: {StopName}, {nameof(StopId)}: {StopId}, {nameof(ScheduleTime)}: {ScheduleTime}, {nameof(ActualTime)}: {ActualTime}, {nameof(DelayInMinutes)} :{DelayInMinutes} }}";
    }
}