using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lestaly.Converters;

/// <summary>DirectoryInfo 用 JsonConverter</summary>
public class DirectoryInfoJsonConverter : JsonConverter<DirectoryInfo>
{
    /// <inheritdoc />
    public override DirectoryInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (value == null) return null;
        return new DirectoryInfo(value);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, DirectoryInfo value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.FullName);
    }
}
