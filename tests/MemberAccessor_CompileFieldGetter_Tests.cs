using System.Reflection;
using static Lestaly.MemberAccessor;

namespace LestalyTest;

[TestClass]
public class MemberAccessor_CompileFieldGetter_Tests
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
        public string RefField = "";
        public int ValField;

        public static string StaticRefField = "";
        public static int StaticValField;

        private string PrivateRefField = "asd";
        private int PrivateValField = 123;
    }

    [TestMethod]
    public void CompileFieldGetter_Normal_Ref()
    {
        var field = typeof(Item).GetField(nameof(Item.RefField))!;
        var getter = CompileFieldGetter<Item>(field);

        var item = new Item();
        item.RefField = "xxx";
        getter(item).Should().Be("xxx");

        item.RefField = "yyy";
        getter(item).Should().Be("yyy");
    }

    [TestMethod]
    public void CompileFieldGetter_Normal_Val()
    {
        var field = typeof(Item).GetField(nameof(Item.ValField))!;
        var getter = CompileFieldGetter<Item>(field);

        var item = new Item();
        item.ValField = 10;
        getter(item).Should().Be(10);

        item.ValField = 20;
        getter(item).Should().Be(20);
    }

    [TestMethod]
    public void CompileFieldGetter_Normal_StaticRef()
    {
        var field = typeof(Item).GetField(nameof(Item.StaticRefField), Flags.Static | Flags.Public)!;
        var getter = CompileFieldGetter<Item>(field);

        Item.StaticRefField = "xxx";
        getter(null).Should().Be("xxx");

        Item.StaticRefField = "yyy";
        getter(null).Should().Be("yyy");
    }

    [TestMethod]
    public void CompileFieldGetter_Normal_StaticVal()
    {
        var field = typeof(Item).GetField(nameof(Item.StaticValField), Flags.Static | Flags.Public)!;
        var getter = CompileFieldGetter<Item>(field);

        Item.StaticValField = 11;
        getter(null).Should().Be(11);

        Item.StaticValField = 22;
        getter(null).Should().Be(22);
    }

    [TestMethod]
    public void CompileFieldGetter_Normal_PrivateRef()
    {
        var field = typeof(Item).GetField("PrivateRefField", Flags.Instance | Flags.NonPublic)!;
        var getter = CompileFieldGetter<Item>(field, nonPublic: true);

        var item = new Item();
        field.SetValue(item, "xxx");
        getter(item).Should().Be("xxx");

        field.SetValue(item, "yyy");
        getter(item).Should().Be("yyy");
    }

    [TestMethod]
    public void CompileFieldGetter_Normal_PrivateVal()
    {
        var field = typeof(Item).GetField("PrivateValField", Flags.Instance | Flags.NonPublic)!;
        var getter = CompileFieldGetter<Item>(field, nonPublic: true);

        var item = new Item();
        field.SetValue(item, 11);
        getter(item).Should().Be(11);

        field.SetValue(item, 22);
        getter(item).Should().Be(22);
    }

    [TestMethod]
    public void CompileFieldGetter_Normal_Fail()
    {
        var field = typeof(Item).GetField("PrivateValField", Flags.Static | Flags.Instance | Flags.Public | Flags.NonPublic)!;

        new Action(() => CompileFieldGetter<Item>(field, nonPublic: true)).Should().NotThrow();

        new Action(() => CompileFieldGetter<Item>(field, nonPublic: false)).Should().Throw<Exception>();
    }
}
