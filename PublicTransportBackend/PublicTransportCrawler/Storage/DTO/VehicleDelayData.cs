using System.Collections.Generic;

namespace PublicTransportCrawler.Vehicles.Adapters;

public class VehicleDelayStorage
{
    public string Id { get; set; }
    public string TripId { get; set; }
    public List<StopDelayData> Stops { get; set; }
    public string LineNumber { get; set; }
    public string Direction { get; set; }
    public string PartitionKey
    {
        get => TripId;
        set => PartitionKey = value;
    }
}