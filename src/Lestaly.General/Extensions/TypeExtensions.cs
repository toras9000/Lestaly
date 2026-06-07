#if NET9_0_OR_GREATER
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Schema;

namespace Lestaly;

/// <summary>Typeに対する拡張メソッド</summary>
public static class TypeExtensions
{
    /// <summary>Typeに対する拡張メソッド</summary>
    /// <param name="self"></param>
    extension(Type self)
    {
        /// <summary>型を元にJSONスキーマを生成する</summary>
        /// <param name="serializerOptions">JSON型解決オプション</param>
        /// <param name="exporterOptions">スキーマ生成オプション</param>
        /// <returns>JSONスキーマ</returns>
        public JsonNode GetJsonSchemaAsNode(JsonSerializerOptions serializerOptions, JsonSchemaExporterOptions? exporterOptions = null) => serializerOptions.GetJsonSchemaAsNode(self, exporterOptions);

        /// <summary>型を元にJSONスキーマを生成する</summary>
        /// <param name="exporterOptions">JSON型解決オプション</param>
        /// <returns>JSONスキーマ</returns>
        public JsonNode GetJsonSchemaAsNode(JsonSchemaExporterOptions? exporterOptions = null) => JsonSerializerOptions.Default.GetJsonSchemaAsNode(self, exporterOptions);

        /// <summary>型を元にJSONスキーマを生成する</summary>
        /// <returns>JSONスキーマ</returns>
        public JsonNode GetJsonSchemaWithMetadata() => JsonSerializerOptions.Default.GetJsonSchemaAsNode(self, new() { TransformSchemaNode = JsonSchemaExporterTransform.WithMetadata });
    }
}
#endif
