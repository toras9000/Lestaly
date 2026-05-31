using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Lestaly.Converters;

/// <summary>Regex 用 JsonConverter</summary>
public class RegexJsonConverter : JsonConverter<Regex>
{
    /// <inheritdoc />
    public override Regex? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            var rePattern = default(string);
            var reOptions = RegexOptions.None;
            var reTimeout = 0L;
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject) break;

                var property = reader.GetString();
                reader.Read();

                switch (property?.ToLowerInvariant())
                {
                case "pattern":
                    rePattern = reader.GetString();
                    break;
                case "options":
                    reOptions = (RegexOptions)reader.GetInt32();
                    break;
                case "timeout":
                    reTimeout = reader.GetInt64();
                    break;
                default:
                    reader.Skip();
                    break;
                }
            }
            if (rePattern == null) throw new JsonException();
            return new Regex(rePattern, reOptions, reTimeout <= 0 ? Regex.InfiniteMatchTimeout : new TimeSpan(reTimeout));
        }
        else
        {
            var value = reader.GetString();
            if (value == null) return null;
            return new Regex(value);
        }
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, Regex value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
        }
        else
        {
            writer.WriteStartObject();
            writer.WritePropertyName("Pattern");
            writer.WriteStringValue(value.ToString());
            writer.WritePropertyName("Options");
            writer.WriteNumberValue((int)value.Options);
            writer.WritePropertyName("Timeout");
            writer.WriteNumberValue((long)value.MatchTimeout.Ticks);
            writer.WriteEndObject();
        }
    }
}
