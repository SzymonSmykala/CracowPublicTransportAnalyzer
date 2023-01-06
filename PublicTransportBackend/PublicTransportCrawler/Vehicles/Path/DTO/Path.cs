using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PublicTransportCrawler.Vehicles.Path.DTO
{
    public partial class Path
    {
        [JsonProperty("actual")]
        public List<Actual> Actual { get; set; }

        [JsonProperty("directionText")]
        public string DirectionText { get; set; }

        [JsonProperty("old")]
        public List<Actual> Old { get; set; }

        [JsonProperty("routeName")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long RouteName { get; set; }
    }

    public partial class Actual
    {
        [JsonProperty("actualTime")]
        public string ActualTime { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("stop")]
        public Stop Stop { get; set; }

        [JsonProperty("stop_seq_num")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long StopSeqNum { get; set; }
    }

    public partial class Stop
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("shortName")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ShortName { get; set; }
    }

    public enum Status { Departed, Predicted,
        Stopping,
        Planned
    };

    public partial class Path
    {
        public static Path FromJson(string json) => JsonConvert.DeserializeObject<Path>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Path self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
                case "PREDICTED":
                    return Status.Predicted;
                case "STOPPING":
                    return Status.Stopping;
                case "PLANNED":
                    return Status.Planned;
            }
            throw new Exception($"Cannot unmarshal type Status. The status: {value}");
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
                case Status.Predicted:
                    serializer.Serialize(writer, "PREDICTED");
                    return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly StatusConverter Singleton = new StatusConverter();
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
            throw new Exception("Cannot unmarshal type long");
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
}
