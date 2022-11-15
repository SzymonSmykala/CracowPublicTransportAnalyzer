using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PublicTransportCrawler.Stops.DTO
{
    public partial class StopResponse
    {
        [JsonProperty("actual")]
        public List<Actual> Actual { get; set; }

        [JsonProperty("directions")]
        public List<object> Directions { get; set; }

        [JsonProperty("firstPassageTime")]
        public long FirstPassageTime { get; set; }

        [JsonProperty("generalAlerts")]
        public List<object> GeneralAlerts { get; set; }

        [JsonProperty("lastPassageTime")]
        public long LastPassageTime { get; set; }

        [JsonProperty("old")]
        public List<Actual> Old { get; set; }

        [JsonProperty("routes")]
        public List<Route> Routes { get; set; }

        [JsonProperty("stopName")]
        public string StopName { get; set; }

        [JsonProperty("stopShortName")]
        [Newtonsoft.Json.JsonConverter(typeof(ParseStringConverter))]
        public long StopShortName { get; set; }
    }

    public class Route
    {
        [JsonProperty("alerts")]
        public List<object> Alerts { get; set; }

        [JsonProperty("authority")]
        public string Authority { get; set; }

        [JsonProperty("directions")]
        public List<string> Directions { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        [Newtonsoft.Json.JsonConverter(typeof(ParseStringConverter))]
        public long Name { get; set; }

        [JsonProperty("routeType")]
        public string RouteType { get; set; }

        [JsonProperty("shortName")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ShortName { get; set; }
    }

    public enum Status { 
        Departed,
        Planned,
        Predicted,
        Unknown
    };

    public partial class StopResponse
    {
        public static StopResponse FromJson(string json) => JsonConvert.DeserializeObject<StopResponse>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this StopResponse self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                StatusConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
