using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lestaly.Converters;

/// <summary>IPAddress 用 JsonConverter</summary>
public class IPAddressJsonConverter : JsonConverter<IPAddress>
{
    /// <inheritdoc />
    public override IPAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (value == null) return null;
        return IPAddress.Parse(value);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, IPAddress value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString());
    }
}
