using Newtonsoft.Json;

namespace PublicTransportCrawler.Stops.DTO;

public class Actual
{
    [JsonProperty("actualRelativeTime")]
    public long ActualRelativeTime { get; set; }

    [JsonProperty("direction")]
    public string Direction { get; set; }

    [JsonProperty("mixedTime")]
    public string MixedTime { get; set; }

    [JsonProperty("passageid")]
    public string PassageId { get; set; } 

    [JsonProperty("patternText")]
    [Newtonsoft.Json.JsonConverter(typeof(ParseStringConverter))]
    public long PatternText { get; set; }

    [JsonProperty("plannedTime")]
    public string PlannedTime { get; set; }

    [JsonProperty("routeId")]
    public string RouteId { get; set; }

    [JsonProperty("status")]
    public Status Status { get; set; }

    [JsonProperty("tripId")]
    public string TripId { get; set; }

    [JsonProperty("actualTime", NullValueHandling = NullValueHandling.Ignore)]
    public string ActualTime { get; set; }

    [JsonProperty("vehicleId", NullValueHandling = NullValueHandling.Ignore)]
    public string VehicleId { get; set; }
}