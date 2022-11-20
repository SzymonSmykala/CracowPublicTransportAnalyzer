using System;
using Newtonsoft.Json;

namespace PublicTransportCrawler.Stops.DTO;

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
                return Status.Unknown;
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