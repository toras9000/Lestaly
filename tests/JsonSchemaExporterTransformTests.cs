#if NET9_0_OR_GREATER
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Schema;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace LestalyTest.Extensions;

[TestClass]
public class JsonSchemaExporterTransformTests
{
    private class Description
    {
        public enum Kind
        {
            Alpha,
            Bravo,
            Charlie,
        };
        [Description("type=Item")]
        public record Item(
            [property: Description("Item Number")] int Number,
            [property: Description("Item Text")] string Text,
            [property: Description("Item Flag")] bool Flag,
            [property: Description("Item Kind")] Kind Kind
        );
        [Description("type=Option")]
        public record Option(
            [property: Description("Option Number")] int? Number,
            [property: Description("Option Number")] string? Text,
            [property: Description("Option Number")] bool? Flag,
            [property: Description("Option Kind")] Kind? Kind
        );
        [Description("type=Contaier")]
        public record Contaier(
            [property: Description("Contaier Items")] Item[] Items,
            [property: Description("Contaier Options")] Option[]? Options
        );
    }

    [TestMethod()]
    public void TransformWithMetadata_DescriptionAttribute()
    {
        var options = new JsonSerializerOptions();
        options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        options.WriteIndented = true;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
        options.TypeInfoResolver = new DefaultJsonTypeInfoResolver();
        options.MakeReadOnly();

        var schema = typeof(Description.Contaier).GetJsonSchemaAsNode(new() { TransformSchemaNode = JsonSchemaExporterTransform.WithMetadata, });
        schema["properties"]!["Items"]!["description"]!.GetValue<string>().Should().Be("Contaier Items");
        schema["properties"]!["Options"]!["description"]!.GetValue<string>().Should().Be("Contaier Options");
    }

    private class JsonSchemaDescription
    {
        public enum Kind
        {
            Alpha,
            Bravo,
            Charlie,
        };
        [JsonSchema(Description = "type=Item")]
        public record Item(
            [property: JsonSchema(Description = "Item Number")] int Number,
            [property: JsonSchema(Description = "Item Text")] string Text,
            [property: JsonSchema(Description = "Item Flag")] bool Flag,
            [property: JsonSchema(Description = "Item Kind")] Kind Kind
        );
        [JsonSchema(Description = "type=Option")]
        public record Option(
            [property: JsonSchema(Description = "Option Number")] int? Number,
            [property: JsonSchema(Description = "Option Number")] string? Text,
            [property: JsonSchema(Description = "Option Number")] bool? Flag,
            [property: JsonSchema(Description = "Option Kind")] Kind? Kind
        );
        [JsonSchema(Description = "type=Contaier")]
        public record Contaier(
            [property: JsonSchema(Description = "Contaier Items")] Item[] Items,
            [property: JsonSchema(Description = "Contaier Options")] Option[]? Options
        );
    }

    [TestMethod()]
    public void TransformWithMetadata_JsonSchemaAttribute_Description()
    {
        var options = new JsonSerializerOptions();
        options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        options.WriteIndented = true;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
        options.TypeInfoResolver = new DefaultJsonTypeInfoResolver();
        options.MakeReadOnly();

        var schema = typeof(JsonSchemaDescription.Contaier).GetJsonSchemaAsNode(new() { TransformSchemaNode = JsonSchemaExporterTransform.WithMetadata, });
        schema["properties"]!["Items"]!["description"]!.GetValue<string>().Should().Be("Contaier Items");
        schema["properties"]!["Options"]!["description"]!.GetValue<string>().Should().Be("Contaier Options");
    }

    private class JsonSchema_ValueType
    {
        public enum Kind
        {
            Alpha,
            Bravo,
            Charlie,
        };
        public record Contaier(
            [property: JsonSchema(ValueType = JsonSchemaValueType.String)] string Text1,
            [property: JsonSchema(ValueType = JsonSchemaValueType.Number)] string Text2,
            [property: JsonSchema(ValueType = JsonSchemaValueType.Boolean)] string Text3,
            [property: JsonSchema(ValueType = JsonSchemaValueType.Object)] string Text4,
            [property: JsonSchema(ValueType = JsonSchemaValueType.Array)] string Text5,
            [property: JsonSchema(ValueType = JsonSchemaValueType.String)] Kind Enum1,
            [property: JsonSchema(ValueType = JsonSchemaValueType.Number)] Kind Enum2,
            [property: JsonSchema(ValueType = JsonSchemaValueType.Boolean)] Kind Enum3,
            [property: JsonSchema(ValueType = JsonSchemaValueType.Object)] Kind Enum4,
            [property: JsonSchema(ValueType = JsonSchemaValueType.Array)] Kind Enum5
        );
    }

    [TestMethod()]
    public void TransformWithMetadata_JsonSchemaAttribute_ValueType()
    {
        var options = new JsonSerializerOptions();
        options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        options.WriteIndented = true;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
        options.TypeInfoResolver = new DefaultJsonTypeInfoResolver();
        options.MakeReadOnly();

        var schema = typeof(JsonSchema_ValueType.Contaier).GetJsonSchemaAsNode(new() { TransformSchemaNode = JsonSchemaExporterTransform.WithMetadata, });
        schema["properties"]!["Text1"]!["type"]!.GetValue<string>().Should().Be("string");
        schema["properties"]!["Text2"]!["type"]!.GetValue<string>().Should().Be("number");
        schema["properties"]!["Text3"]!["type"]!.GetValue<string>().Should().Be("boolean");
        schema["properties"]!["Text4"]!["type"]!.GetValue<string>().Should().Be("object");
        schema["properties"]!["Text5"]!["type"]!.GetValue<string>().Should().Be("object");
        schema["properties"]!["Enum1"]!["type"]!.GetValue<string>().Should().Be("string");
        schema["properties"]!["Enum2"]!["type"]!.GetValue<string>().Should().Be("number");
        schema["properties"]!["Enum3"]!["type"]!.GetValue<string>().Should().Be("boolean");
        schema["properties"]!["Enum4"]!["type"]!.GetValue<string>().Should().Be("object");
        schema["properties"]!["Enum5"]!["type"]!.GetValue<string>().Should().Be("object");
    }

    private class JsonSchema_EnumValues
    {
        public enum Kind
        {
            Alpha,
            Bravo,
            Charlie,
        };
        public record ValueTypeTest(
            [property: JsonSchema(ValueType = JsonSchemaValueType.Number)] Kind EnumNumbers,
            [property: JsonSchema(ValueType = JsonSchemaValueType.String)] Kind EnumStrings
        );
        public record NullableTest(
            [property: JsonSchema(ValueType = JsonSchemaValueType.Number)] Kind? EnumNumbers,
            [property: JsonSchema(ValueType = JsonSchemaValueType.String)] Kind? EnumStrings
        );
        public record QuietTest(
            [property: JsonSchema(QuietEnum = false)] Kind TreatEnum,
            [property: JsonSchema(QuietEnum = true)] Kind QuietEnum
        );
    }

    [TestMethod()]
    public void TransformWithMetadata_JsonSchemaAttribute_Enum_ValueType()
    {
        var options = new JsonSerializerOptions();
        options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        options.WriteIndented = true;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
        options.TypeInfoResolver = new DefaultJsonTypeInfoResolver();
        options.MakeReadOnly();

        var schema = typeof(JsonSchema_EnumValues.ValueTypeTest).GetJsonSchemaAsNode(new() { TransformSchemaNode = JsonSchemaExporterTransform.WithMetadata, });
        schema["properties"]!["EnumNumbers"]!["enum"]!.AsArray().GetValues<int>().Should().BeEquivalentTo([0, 1, 2]);
        schema["properties"]!["EnumNumbers"]!["type"]!.GetValue<string>().Should().BeEquivalentTo("number");
        schema["properties"]!["EnumStrings"]!["enum"]!.AsArray().GetValues<string>().Should().BeEquivalentTo(["Alpha", "Bravo", "Charlie"]);
        schema["properties"]!["EnumStrings"]!["type"]!.GetValue<string>().Should().BeEquivalentTo("string");
    }

    [TestMethod()]
    public void TransformWithMetadata_JsonSchemaAttribute_Enum_Nullable()
    {
        var options = new JsonSerializerOptions();
        options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        options.WriteIndented = true;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
        options.TypeInfoResolver = new DefaultJsonTypeInfoResolver();
        options.MakeReadOnly();

        var schema = typeof(JsonSchema_EnumValues.NullableTest).GetJsonSchemaAsNode(new() { TransformSchemaNode = JsonSchemaExporterTransform.WithMetadata, });
        schema["properties"]!["EnumNumbers"]!["enum"]!.AsArray().GetValues<int>().Should().BeEquivalentTo([0, 1, 2]);
        schema["properties"]!["EnumNumbers"]!["type"]!.AsArray().GetValues<string>().Should().BeEquivalentTo(["number", "null"]);
        schema["properties"]!["EnumStrings"]!["enum"]!.AsArray().GetValues<string>().Should().BeEquivalentTo(["Alpha", "Bravo", "Charlie"]);
        schema["properties"]!["EnumStrings"]!["type"]!.AsArray().GetValues<string>().Should().BeEquivalentTo(["string", "null"]);
    }

    [TestMethod()]
    public void TransformWithMetadata_JsonSchemaAttribute_Enum_Quiet()
    {
        var options = new JsonSerializerOptions();
        options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        options.WriteIndented = true;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
        options.TypeInfoResolver = new DefaultJsonTypeInfoResolver();
        options.MakeReadOnly();

        var schema = typeof(JsonSchema_EnumValues.QuietTest).GetJsonSchemaAsNode(new() { TransformSchemaNode = JsonSchemaExporterTransform.WithMetadata, });
        schema["properties"]!["TreatEnum"]!["type"]!.GetValue<string>().Should().Be("string");
        schema["properties"]!["TreatEnum"]!["enum"].Should().NotBeNull();
        schema["properties"]!["QuietEnum"]!["type"]!.GetValue<string>().Should().Be("integer");
        schema["properties"]!["QuietEnum"]!["enum"].Should().BeNull();
    }
}
#endif
