using System.Text.Json;
using System.Text.Json.Nodes;

namespace LestalyTest.Extensions;

[TestClass()]
public class JsonExtensionsTests
{
    [TestMethod()]
    public void VisitProperties()
    {
        var node = JsonNode.Parse("""
        {
          "c": "c",
          "b": {
            "c": "c",
            "b": "b",
            "a": "a"
          },
          "e": [
            10,
            4,
            {
              "c": "c",
              "b": "b",
              "a": "a"
            },
            "a",
            7
          ],
          "d": "d",
          "a": "a"
        }
        """) ?? throw new Exception();

        var names = new List<string>();
        node.VisitProperties((name, value) => names.Add(name));

        names.Should().Equal([
            "c",
            "b",
                "c",
                "b",
                "a",
            "e",
                    "c",
                    "b",
                    "a",
            "d",
            "a",
        ]);
    }

    [TestMethod()]
    public void UpdateProperties()
    {
        var node = JsonNode.Parse("""
        {
          "c": "c",
          "b": {
            "c": "c",
            "b": "b",
            "a": "a"
          },
          "e": [
            10,
            4,
            {
              "c": "c",
              "b": "b",
              "a": "a"
            },
            "a",
            7
          ],
          "d": "d",
          "a": "a"
        }
        """) ?? throw new Exception();

        var names = new List<string>();
        node.UpdateProperties(walker =>
        {
            if (walker.Name == "a") walker.SetValue(100);
        });

        var options = new JsonSerializerOptions(JsonSerializerOptions.Default) { WriteIndented = true, };
        node.ToJsonString(options).Should().Be("""
        {
          "c": "c",
          "b": {
            "c": "c",
            "b": "b",
            "a": 100
          },
          "e": [
            10,
            4,
            {
              "c": "c",
              "b": "b",
              "a": 100
            },
            "a",
            7
          ],
          "d": "d",
          "a": 100
        }
        """);
    }

    [TestMethod()]
    public void ToSorted()
    {
        var node = JsonNode.Parse("""
        {
          "c": "c",
          "b": {
            "c": "c",
            "b": "b",
            "a": "a"
          },
          "e": [
            10,
            4,
            {
              "c": "c",
              "b": "b",
              "a": "a"
            },
            "a",
            7
          ],
          "d": "d",
          "a": "a"
        }
        """) ?? throw new Exception();

        var sorted = node.ToSorted();
        var options = new JsonSerializerOptions(JsonSerializerOptions.Default) { WriteIndented = true, };
        sorted.ToJsonString(options).Should().Be("""
        {
          "a": "a",
          "b": {
            "a": "a",
            "b": "b",
            "c": "c"
          },
          "c": "c",
          "d": "d",
          "e": [
            10,
            4,
            {
              "a": "a",
              "b": "b",
              "c": "c"
            },
            "a",
            7
          ]
        }
        """);
    }

    [TestMethod()]
    public void ToSortedNode()
    {
        var json = JsonDocument.Parse("""
        {
          "c": "c",
          "b": {
            "c": "c",
            "b": "b",
            "a": "a"
          },
          "e": [
            10,
            4,
            {
              "c": "c",
              "b": "b",
              "a": "a"
            },
            "a",
            7
          ],
          "d": "d",
          "a": "a"
        }
        """);

        var sorted = json.RootElement.ToSortedNode();
        var options = new JsonSerializerOptions { WriteIndented = true, };
        sorted.Should().NotBeNull();
        sorted.ToJsonString(options).Should().Be("""
        {
          "a": "a",
          "b": {
            "a": "a",
            "b": "b",
            "c": "c"
          },
          "c": "c",
          "d": "d",
          "e": [
            10,
            4,
            {
              "a": "a",
              "b": "b",
              "c": "c"
            },
            "a",
            7
          ]
        }
        """);
    }

    [TestMethod()]
    public void Element_ToScalar()
    {
        JsonSerializer.SerializeToElement(true).ToScalar().Should().Be(true);
        JsonSerializer.SerializeToElement(false).ToScalar().Should().Be(false);
        JsonSerializer.SerializeToElement(123).ToScalar().Should().Be(123);
        JsonSerializer.SerializeToElement<byte>(123).ToScalar().Should().Be(123);
        JsonSerializer.SerializeToElement<sbyte>(123).ToScalar().Should().Be(123);
        JsonSerializer.SerializeToElement<short>(123).ToScalar().Should().Be(123);
        JsonSerializer.SerializeToElement<ushort>(123).ToScalar().Should().Be(123);
        JsonSerializer.SerializeToElement<int>(123).ToScalar().Should().Be(123);
        JsonSerializer.SerializeToElement<uint>(123).ToScalar().Should().Be(123);
        JsonSerializer.SerializeToElement<long>(123).ToScalar().Should().Be(123);
        JsonSerializer.SerializeToElement<ulong>(123).ToScalar().Should().Be(123);
        JsonSerializer.SerializeToElement(1.4d).ToScalar().Should().Be(1.4d);
        JsonSerializer.SerializeToElement(1.4f).ToScalar().Should().Be(1.4d);
        JsonSerializer.SerializeToElement(1.4m).ToScalar().Should().Be(1.4d);
        JsonSerializer.SerializeToElement("abc").ToScalar().Should().Be("abc");
        JsonSerializer.SerializeToElement(new { a = "abc", }).ToScalar().Should().Be(null);
        JsonSerializer.SerializeToElement<int[]>([1, 2, 3])!.ToScalar().Should().Be(null);
        JsonSerializer.SerializeToElement<int?>(null).ToScalar().Should().Be(null);

        var json = JsonSerializer.Deserialize<JsonElement>("""
        {
            "TrueProp": true,
            "FalseProp": false,
            "IntegerProp": 123,
            "RealProp": 1.23,
            "StringProp": "abc",
            "ObjectProp": { "Prop": 123 },
            "ArrayProp": [1, 2, 3],
            "NullProp": null
        }
        """);
        json.GetProperty("TrueProp").ToScalar().Should().Be(true);
        json.GetProperty("FalseProp").ToScalar().Should().Be(false);
        json.GetProperty("IntegerProp").ToScalar().Should().Be(123);
        json.GetProperty("RealProp").ToScalar().Should().Be(1.23);
        json.GetProperty("StringProp").ToScalar().Should().Be("abc");
        json.GetProperty("ObjectProp").ToScalar().Should().Be(null);
        json.GetProperty("NullProp").ToScalar().Should().Be(null);
        new JsonElement().ToScalar().Should().Be(null);
    }

    [TestMethod()]
    public void Node_ToScalar()
    {
        JsonValue.Create(true).ToScalar().Should().Be(true);
        JsonValue.Create(false).ToScalar().Should().Be(false);
        JsonValue.Create((byte)123).ToScalar().Should().Be(123);
        JsonValue.Create((sbyte)123).ToScalar().Should().Be(123);
        JsonValue.Create((ushort)123).ToScalar().Should().Be(123);
        JsonValue.Create((short)123).ToScalar().Should().Be(123);
        JsonValue.Create((int)123).ToScalar().Should().Be(123);
        JsonValue.Create((uint)123).ToScalar().Should().Be(123);
        JsonValue.Create((long)123).ToScalar().Should().Be(123);
        JsonValue.Create((ulong)123).ToScalar().Should().Be(123);
        JsonValue.Create(1.4f).ToScalar().Should().Be(1.4f);
        JsonValue.Create(1.4d).ToScalar().Should().Be(1.4d);
        JsonValue.Create(1.4m).ToScalar().Should().Be(1.4m);
        JsonValue.Create("abc").ToScalar().Should().Be("abc");
        JsonNode.Parse("null")!.ToScalar().Should().Be(null);
        JsonNode.Parse("[1,2,3]")!.ToScalar().Should().Be(null);

        var json = JsonSerializer.Deserialize<JsonNode>("""
        {
            "TrueProp": true,
            "FalseProp": false,
            "IntegerProp": 123,
            "RealProp": 1.23,
            "StringProp": "abc",
            "ObjectProp": { "Prop": 123 },
            "ArrayProp": [1, 2, 3],
            "NullProp": null
        }
        """)!;
        json["TrueProp"]!.ToScalar().Should().Be(true);
        json["FalseProp"]!.ToScalar().Should().Be(false);
        json["IntegerProp"]!.ToScalar().Should().Be(123);
        json["RealProp"]!.ToScalar().Should().Be(1.23);
        json["StringProp"]!.ToScalar().Should().Be("abc");
        json["ObjectProp"]!.ToScalar().Should().Be(null);
        json["NullProp"]!.ToScalar().Should().Be(null);
    }
}
