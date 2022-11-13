using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PublicTransportCrawler.Stops
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
        [JsonConverter(typeof(ParseStringConverter))]
        public long StopShortName { get; set; }
    }

    public partial class Actual
    {
        [JsonProperty("actualRelativeTime")]
        public long ActualRelativeTime { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("mixedTime")]
        public string MixedTime { get; set; }

        [JsonProperty("passageid")]
        public string Passageid { get; set; }

        [JsonProperty("patternText")]
        [JsonConverter(typeof(ParseStringConverter))]
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

    public partial class Route
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
        [JsonConverter(typeof(ParseStringConverter))]
        public long Name { get; set; }

        [JsonProperty("routeType")]
        public string RouteType { get; set; }

        [JsonProperty("shortName")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ShortName { get; set; }
    }

    public enum Status { Departed, Planned, Predicted,
        UNKNOWN
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

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }

            return 0.0;
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class StatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Status) || t == typeof(Status?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "DEPARTED":
                    return Status.Departed;
                case "PLANNED":
                    return Status.Planned;
                case "PREDICTED":
                    return Status.Predicted;
                default:
                    return Status.UNKNOWN;
            }
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Status)untypedValue;
            switch (value)
            {
                case Status.Departed:
                    serializer.Serialize(writer, "DEPARTED");
                    return;
                case Status.Planned:
                    serializer.Serialize(writer, "PLANNED");
                    return;
                case Status.Predicted:
                    serializer.Serialize(writer, "PREDICTED");
                    return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly StatusConverter Singleton = new StatusConverter();
    }
}
