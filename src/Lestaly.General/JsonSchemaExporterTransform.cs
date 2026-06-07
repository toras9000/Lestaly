#if NET9_0_OR_GREATER
using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Schema;

namespace Lestaly;

/// <summary>スキーマデータ型</summary>
public enum JsonSchemaValueType
{
    /// <summary>未指定</summary>
    Unknown,
    /// <summary>文字列</summary>
    String,
    /// <summary>数値</summary>
    Number,
    /// <summary>真偽値</summary>
    Boolean,
    /// <summary>オブジェクト</summary>
    Object,
    /// <summary>配列</summary>
    Array,
}

/// <summary>スキーマカスタマイズ用アノテーション属性</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class JsonSchemaAttribute : Attribute
{
    /// <summary>説明文字列</summary>
    /// <remarks>DescriptionAttribute と同じ</remarks>
    public string? Description { get; init; }

    /// <summary>enum 向けの処理を無効化するか</summary>
    public bool QuietEnum { get; init; }

    /// <summary>プロパティ値の型</summary>
    /// <remarks>整合性は考慮せず、指定のされた通りに設定する。主に Enum </remarks>
    public JsonSchemaValueType ValueType { get; init; }
}

/// <summary>JSONスキーマ抽出カスタマイズ処理</summary>
public static class JsonSchemaExporterTransform
{
    /// <summary>メタデータを考慮したスキーマ抽出カスタマイズ処理</summary>
    /// <param name="context">スキーマ抽出コンテキスト</param>
    /// <param name="schema">スキーマJSON</param>
    /// <returns>カスタマイズされたスキーマJSON</returns>
    public static JsonNode WithMetadata(JsonSchemaExporterContext context, JsonNode schema)
    {
        var descAttr = default(DescriptionAttribute);
        var schemaAttr = default(JsonSchemaAttribute);
        var enumType = default(Type);

        // 属性収集ローカルメソッド
        void collectAttribute(ICustomAttributeProvider? provider)
        {
            if (provider == null) return;
            foreach (var attr in provider.GetCustomAttributes(inherit: true))
            {
                if (attr is DescriptionAttribute desc)
                {
                    descAttr ??= desc;
                    continue;
                }
                else if (attr is JsonSchemaAttribute scheme)
                {
                    schemaAttr ??= scheme;
                    continue;
                }
            }
        }

        // 対象が型かプロパティ化を判定
        if (context.PropertyInfo == null)
        {
            //// 型が対象
            // 型の属性を取得
            collectAttribute(context.TypeInfo.Type);

        }
        else
        {
            //// プロパティが対象
            // プロパティの属性を取得
            collectAttribute(context.PropertyInfo.AttributeProvider);

            // プロパティ型が enum かを判定
            if (schemaAttr?.QuietEnum != true && context.PropertyInfo.PropertyType.IsEnum)
            {
                enumType = context.PropertyInfo.PropertyType;
            }
        }

        // スキーマオブジェクトへのカスタマイズ
        if (schema is JsonObject objectSchema)
        {
            // description があれば設定
            var description = schemaAttr?.Description ?? descAttr?.Description;
            if (description != null) objectSchema.Insert(0, "description", description);

            // enum型であれば列挙値を設定
            if (enumType != null)
            {
                if (schemaAttr?.ValueType == JsonSchemaValueType.Number)
                {
                    objectSchema.Add("enum", JsonSerializer.SerializeToNode(enumType.GetEnumValues()));
                }
                else
                {
                    objectSchema.Add("enum", JsonSerializer.SerializeToNode(enumType.GetEnumNames()));
                }
            }

            // 値の種別があれば設定
            if (schemaAttr?.ValueType is JsonSchemaValueType valueType)
            {
                var jsonType = valueType switch
                {
                    JsonSchemaValueType.String => "string",
                    JsonSchemaValueType.Number => "number",
                    JsonSchemaValueType.Boolean => "boolean",
                    JsonSchemaValueType.Object => "object",
                    JsonSchemaValueType.Array => "object",
                    JsonSchemaValueType.Unknown => enumType == null ? null : "string",
                    _ => null,
                };
                if (jsonType != null) objectSchema["type"] = jsonType;
            }
        }

        return schema;
    }
}
#endif