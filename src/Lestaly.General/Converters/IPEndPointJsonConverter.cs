using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lestaly.Converters;

/// <summary>IPEndPoint 用 JsonConverter</summary>
public class IPEndPointJsonConverter : JsonConverter<IPEndPoint>
{
    /// <inheritdoc />
    public override IPEndPoint? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (value == null) return null;
        return IPEndPoint.Parse(value);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, IPEndPoint value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString());
    }
}
