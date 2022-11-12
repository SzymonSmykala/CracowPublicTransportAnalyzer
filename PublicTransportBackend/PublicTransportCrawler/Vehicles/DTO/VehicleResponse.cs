using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PublicTransportCrawler.Vehicles.DTO
{
    public partial class VehicleResponse
    {
        [JsonProperty("lastUpdate")]
        public long LastUpdate { get; set; }

        [JsonProperty("vehicles")]
        public Vehicle[] Vehicles { get; set; }
    }

    public partial class Vehicle
    {
        [JsonProperty("isDeleted", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsDeleted { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("path", NullValueHandling = NullValueHandling.Ignore)]
        public Path[] Path { get; set; }

        [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
        public Color? Color { get; set; }

        [JsonProperty("heading", NullValueHandling = NullValueHandling.Ignore)]
        public long? Heading { get; set; }

        [JsonProperty("latitude", NullValueHandling = NullValueHandling.Ignore)]
        public long? Latitude { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("tripId", NullValueHandling = NullValueHandling.Ignore)]
        public string TripId { get; set; }

        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        public Category? Category { get; set; }

        [JsonProperty("longitude", NullValueHandling = NullValueHandling.Ignore)]
        public long? Longitude { get; set; }
    }

    public partial class Path
    {
        [JsonProperty("y1")]
        public long Y1 { get; set; }

        [JsonProperty("length")]
        public double Length { get; set; }

        [JsonProperty("x1")]
        public long X1 { get; set; }

        [JsonProperty("y2")]
        public long Y2 { get; set; }

        [JsonProperty("angle")]
        public long Angle { get; set; }

        [JsonProperty("x2")]
        public long X2 { get; set; }
    }

    public enum Category { Tram };

    public enum Color { The0Xf89F05 };

    public partial class VehicleResponse
    {
        public static VehicleResponse FromJson(string json) => JsonConvert.DeserializeObject<VehicleResponse>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this VehicleResponse self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                CategoryConverter.Singleton,
                ColorConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class CategoryConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Category) || t == typeof(Category?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "tram")
            {
                return Category.Tram;
            }
            throw new Exception("Cannot unmarshal type Category");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Category)untypedValue;
            if (value == Category.Tram)
            {
                serializer.Serialize(writer, "tram");
                return;
            }
            throw new Exception("Cannot marshal type Category");
        }

        public static readonly CategoryConverter Singleton = new CategoryConverter();
    }

    internal class ColorConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Color) || t == typeof(Color?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            // if (value == "0xf89f05")
            // {
                return Color.The0Xf89F05;
            // }
            // throw new Exception("Cannot unmarshal type Color");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Color)untypedValue;
            if (value == Color.The0Xf89F05)
            {
                serializer.Serialize(writer, "0xf89f05");
                return;
            }
            throw new Exception("Cannot marshal type Color");
        }

        public static readonly ColorConverter Singleton = new ColorConverter();
    }
}
