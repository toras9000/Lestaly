using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lestaly.Converters;

/// <summary>FileInfo 用 JsonConverter</summary>
public class FileInfoJsonConverter : JsonConverter<FileInfo>
{
    /// <inheritdoc />
    public override FileInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (value == null) return null;
        return new FileInfo(value);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, FileInfo? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.FullName);
    }
}
