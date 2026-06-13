using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Lestaly.Converters;

namespace Lestaly;

/// <summary>JSONを雑に扱うための定義</summary>
public static class RoughJson
{
    /// <summary>大雑把な読み取り用オプション</summary>
    public static JsonSerializerOptions Options { get; }

    /// <summary>整形保存用オプション</summary>
    public static JsonSerializerOptions PrettyOptions { get; }

    // 静的コンストラクタ
    static RoughJson()
    {
        Options = new JsonSerializerOptions();
        Options.AllowTrailingCommas = true;
        Options.ReadCommentHandling = JsonCommentHandling.Skip;
        Options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        Options.WriteIndented = true;
        Options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        Options.NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.AllowNamedFloatingPointLiterals;
        Options.PropertyNameCaseInsensitive = true;
        Options.Converters.Add(new RegexJsonConverter());
        Options.Converters.Add(new FileInfoJsonConverter());
        Options.Converters.Add(new DirectoryInfoJsonConverter());
        Options.Converters.Add(new IPAddressJsonConverter());
        Options.Converters.Add(new IPEndPointJsonConverter());
        Options.TypeInfoResolver = new DefaultJsonTypeInfoResolver();
        Options.MakeReadOnly();

        PrettyOptions = new JsonSerializerOptions();
        PrettyOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        PrettyOptions.WriteIndented = true;
        PrettyOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        PrettyOptions.Converters.Add(new RegexJsonConverter());
        PrettyOptions.Converters.Add(new FileInfoJsonConverter());
        PrettyOptions.Converters.Add(new DirectoryInfoJsonConverter());
        PrettyOptions.Converters.Add(new IPAddressJsonConverter());
        PrettyOptions.Converters.Add(new IPEndPointJsonConverter());
        PrettyOptions.TypeInfoResolver = new DefaultJsonTypeInfoResolver();
        PrettyOptions.MakeReadOnly();
    }

}
