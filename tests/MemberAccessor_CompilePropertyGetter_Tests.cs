using System.Reflection;
using static Lestaly.MemberAccessor;

namespace LestalyTest;

[TestClass]
public class MemberAccessor_CompilePropertyGetter_Tests
{
    public static class Flags
    {
        public const BindingFlags Default = BindingFlags.Default;
        public const BindingFlags IgnoreCase = BindingFlags.IgnoreCase;
        public const BindingFlags DeclaredOnly = BindingFlags.DeclaredOnly;
        public const BindingFlags Instance = BindingFlags.Instance;
        public const BindingFlags Static = BindingFlags.Static;
        public const BindingFlags Public = BindingFlags.Public;
        public const BindingFlags NonPublic = BindingFlags.NonPublic;
        public const BindingFlags FlattenHierarchy = BindingFlags.FlattenHierarchy;
        public const BindingFlags InvokeMethod = BindingFlags.InvokeMethod;
        public const BindingFlags CreateInstance = BindingFlags.CreateInstance;
        public const BindingFlags GetField = BindingFlags.GetField;
        public const BindingFlags SetField = BindingFlags.SetField;
        public const BindingFlags GetProperty = BindingFlags.GetProperty;
        public const BindingFlags SetProperty = BindingFlags.SetProperty;
        public const BindingFlags PutDispProperty = BindingFlags.PutDispProperty;
        public const BindingFlags PutRefDispProperty = BindingFlags.PutRefDispProperty;
        public const BindingFlags ExactBinding = BindingFlags.ExactBinding;
        public const BindingFlags SuppressChangeType = BindingFlags.SuppressChangeType;
        public const BindingFlags OptionalParamBinding = BindingFlags.OptionalParamBinding;
        public const BindingFlags IgnoreReturn = BindingFlags.IgnoreReturn;
        public const BindingFlags DoNotWrapExceptions = BindingFlags.DoNotWrapExceptions;
    }

    public class Item
    {
        public string RefProp { get; set; } = "";
        public int ValProp { get; set; }

        public static string StaticRefProp { get; set; } = "";
        public static int StaticValProp { get; set; }

        private string PrivateRefProp { get; set; } = "asd";
        private int PrivateValProp { get; set; } = 123;
    }

    [TestMethod]
    public void CompilePropertyGetter_Normal_Ref()
    {
        var property = typeof(Item).GetProperty(nameof(Item.RefProp))!;
        var getter = CompilePropertyGetter<Item>(property);

        var item = new Item();
        item.RefProp = "xxx";
        getter(item).Should().Be("xxx");

        item.RefProp = "yyy";
        getter(item).Should().Be("yyy");
    }

    [TestMethod]
    public void CreatePropertyGetter_Normal_Val()
    {
        var property = typeof(Item).GetProperty(nameof(Item.ValProp))!;
        var getter = CompilePropertyGetter<Item>(property);

        var item = new Item();
        item.ValProp = 10;
        getter(item).Should().Be(10);

        item.ValProp = 20;
        getter(item).Should().Be(20);
    }

    [TestMethod]
    public void CreatePropertyGetter_Normal_StaticRef()
    {
        var property = typeof(Item).GetProperty(nameof(Item.StaticRefProp), Flags.Static | Flags.Public)!;
        var getter = CompilePropertyGetter<Item>(property);

        Item.StaticRefProp = "xxx";
        getter(null).Should().Be("xxx");

        Item.StaticRefProp = "yyy";
        getter(null).Should().Be("yyy");
    }

    [TestMethod]
    public void CreatePropertyGetter_Normal_StaticVal()
    {
        var property = typeof(Item).GetProperty(nameof(Item.StaticValProp), Flags.Static | Flags.Public)!;
        var getter = CompilePropertyGetter<Item>(property);

        Item.StaticValProp = 11;
        getter(null).Should().Be(11);

        Item.StaticValProp = 22;
        getter(null).Should().Be(22);
    }

    [TestMethod]
    public void CreatePropertyGetter_Normal_PrivateRef()
    {
        var property = typeof(Item).GetProperty("PrivateRefProp", Flags.Instance | Flags.NonPublic)!;
        var getter = CompilePropertyGetter<Item>(property, nonPublic: true);

        var item = new Item();
        property.SetValue(item, "xxx");
        getter(item).Should().Be("xxx");

        property.SetValue(item, "yyy");
        getter(item).Should().Be("yyy");
    }

    [TestMethod]
    public void CreatePropertyGetter_Normal_PrivateVal()
    {
        var property = typeof(Item).GetProperty("PrivateValProp", Flags.Instance | Flags.NonPublic)!;
        var getter = CompilePropertyGetter<Item>(property, nonPublic: true);

        var item = new Item();
        property.SetValue(item, 11);
        getter(item).Should().Be(11);

        property.SetValue(item, 22);
        getter(item).Should().Be(22);
    }

    [TestMethod]
    public void CreatePropertyGetter_Normal_Fail()
    {
        var property = typeof(Item).GetProperty("PrivateValProp", Flags.Static | Flags.Instance | Flags.Public | Flags.NonPublic)!;

        new Action(() => CompilePropertyGetter<Item>(property, nonPublic: true)).Should().NotThrow();

        new Action(() => CompilePropertyGetter<Item>(property, nonPublic: false)).Should().Throw<Exception>();
    }
}
