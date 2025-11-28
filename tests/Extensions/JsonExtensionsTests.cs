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
}
