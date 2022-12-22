using System.Collections.Generic;

namespace PublicTransportCrawler.Vehicles.Adapters;

public class VehicleDelayData
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

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(TripId)}: {TripId}, {nameof(Stops)}: {string.Join(",", Stops)}";
    }
}