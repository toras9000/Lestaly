using System.Reflection;
using static Lestaly.MemberAccessor;

namespace LestalyTest;

[TestClass]
public class MemberAccessor_CreatePropertyGetter_Tests
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
    public void CreatePropertyGetter_Normal_ByName_Ref()
    {
        var getter = CreatePropertyGetter<Item>(nameof(Item.RefProp));

        var item = new Item();
        item.RefProp = "xxx";
        getter(item).Should().Be("xxx");

        item.RefProp = "yyy";
        getter(item).Should().Be("yyy");
    }

    [TestMethod]
    public void CreatePropertyGetter_Normal_ByName_Val()
    {
        var getter = CreatePropertyGetter<Item>(nameof(Item.ValProp));

        var item = new Item();
        item.ValProp = 10;
        getter(item).Should().Be(10);

        item.ValProp = 20;
        getter(item).Should().Be(20);
    }

    [TestMethod]
    public void CreatePropertyGetter_Normal_ByName_StaticRef()
    {
        var flags = Flags.Static | Flags.Public;
        var getter = CreatePropertyGetter<Item>(nameof(Item.StaticRefProp), flags);

        Item.StaticRefProp = "xxx";
        getter(null).Should().Be("xxx");

        Item.StaticRefProp = "yyy";
        getter(null).Should().Be("yyy");
    }

    [TestMethod]
    public void CreatePropertyGetter_Normal_ByName_StaticVal()
    {
        var flags = Flags.Static | Flags.Public;
        var getter = CreatePropertyGetter<Item>(nameof(Item.StaticValProp), flags);

        Item.StaticValProp = 11;
        getter(null).Should().Be(11);

        Item.StaticValProp = 22;
        getter(null).Should().Be(22);
    }

    [TestMethod]
    public void CreatePropertyGetter_Normal_ByName_PrivateRef()
    {
        var flags = Flags.Instance | Flags.NonPublic;
        var property = typeof(Item).GetProperty("PrivateRefProp", flags)!;
        var getter = CreatePropertyGetter<Item>(property.Name, flags);

        var item = new Item();
        property.SetValue(item, "xxx");
        getter(item).Should().Be("xxx");

        property.SetValue(item, "yyy");
        getter(item).Should().Be("yyy");
    }

    [TestMethod]
    public void CreatePropertyGetter_Normal_ByName_PrivateVal()
    {
        var flags = Flags.Instance | Flags.NonPublic;
        var property = typeof(Item).GetProperty("PrivateValProp", flags)!;
        var getter = CreatePropertyGetter<Item>(property.Name, flags);

        var item = new Item();
        property.SetValue(item, 11);
        getter(item).Should().Be(11);

        property.SetValue(item, 22);
        getter(item).Should().Be(22);
    }

    [TestMethod]
    public void CreatePropertyGetter_Normal_ByName_Fail()
    {
        new Action(() => CreatePropertyGetter<Item>(nameof(Item.RefProp), Flags.Instance | Flags.Public)).Should().NotThrow();

        new Action(() => CreatePropertyGetter<Item>(nameof(Item.RefProp), Flags.Instance | Flags.NonPublic)).Should().Throw<Exception>();

        new Action(() => CreatePropertyGetter<Item>(nameof(Item.RefProp), Flags.Static | Flags.Public)).Should().Throw<Exception>();


        new Action(() => CreatePropertyGetter<Item>(nameof(Item.StaticRefProp), Flags.Static | Flags.Public)).Should().NotThrow();

        new Action(() => CreatePropertyGetter<Item>(nameof(Item.StaticRefProp), Flags.Instance | Flags.Public)).Should().Throw<Exception>();

        new Action(() => CreatePropertyGetter<Item>(nameof(Item.StaticRefProp), Flags.Static | Flags.NonPublic)).Should().Throw<Exception>();


        new Action(() => CreatePropertyGetter<Item>("PrivateRefProp", Flags.Instance | Flags.NonPublic)).Should().NotThrow();

        new Action(() => CreatePropertyGetter<Item>("PrivateRefProp", Flags.Instance | Flags.Public)).Should().Throw<Exception>();

        new Action(() => CreatePropertyGetter<Item>("PrivateRefProp", Flags.Static | Flags.NonPublic)).Should().Throw<Exception>();
    }

    public class PrivateSetItem
    {
        public string RefProp { private get; set; } = "";
        public int ValProp { private get; set; }

        public static string StaticRefProp { private get; set; } = "";
        public static int StaticValProp { private get; set; }

        private string PrivateRefProp { get; set; } = "asd";
        private int PrivateValProp { get; set; } = 123;
    }

    [TestMethod]
    public void CreatePropertyGetter_PrivateSet_ByName_Ref()
    {
        var flags = Flags.Instance | Flags.Public | Flags.NonPublic;
        var getter = CreatePropertyGetter<PrivateSetItem>(nameof(Item.RefProp), flags);

        var item = new PrivateSetItem();
        item.RefProp = "xxx";
        getter(item).Should().Be("xxx");

        item.RefProp = "yyy";
        getter(item).Should().Be("yyy");
    }

    [TestMethod]
    public void CreatePropertyGetter_PrivateSet_ByName_Val()
    {
        var flags = Flags.Instance | Flags.Public | Flags.NonPublic;
        var getter = CreatePropertyGetter<PrivateSetItem>(nameof(Item.ValProp), flags);

        var item = new PrivateSetItem();
        item.ValProp = 10;
        getter(item).Should().Be(10);

        item.ValProp = 20;
        getter(item).Should().Be(20);
    }

    [TestMethod]
    public void CreatePropertyGetter_PrivateSet_ByName_StaticRef()
    {
        var flags = Flags.Static | Flags.Public | Flags.NonPublic;
        var getter = CreatePropertyGetter<PrivateSetItem>(nameof(PrivateSetItem.StaticRefProp), flags);

        PrivateSetItem.StaticRefProp = "xxx";
        getter(null).Should().Be("xxx");

        PrivateSetItem.StaticRefProp = "yyy";
        getter(null).Should().Be("yyy");
    }

    [TestMethod]
    public void CreatePropertyGetter_PrivateSet_ByName_StaticVal()
    {
        var flags = Flags.Static | Flags.Public | Flags.NonPublic;
        var getter = CreatePropertyGetter<PrivateSetItem>(nameof(PrivateSetItem.StaticValProp), flags);

        PrivateSetItem.StaticValProp = 11;
        getter(null).Should().Be(11);

        PrivateSetItem.StaticValProp = 22;
        getter(null).Should().Be(22);
    }

    [TestMethod]
    public void CreatePropertyGetter_PrivateSet_ByName_PrivateRef()
    {
        var flags = Flags.Instance | Flags.NonPublic;
        var property = typeof(PrivateSetItem).GetProperty("PrivateRefProp", flags)!;
        var getter = CreatePropertyGetter<PrivateSetItem>(property.Name, flags);

        var item = new PrivateSetItem();
        property.SetValue(item, "xxx");
        getter(item).Should().Be("xxx");

        property.SetValue(item, "yyy");
        getter(item).Should().Be("yyy");
    }

    [TestMethod]
    public void CreatePropertyGetter_PrivateSet_ByName_PrivateVal()
    {
        var flags = Flags.Instance | Flags.NonPublic;
        var property = typeof(PrivateSetItem).GetProperty("PrivateValProp", flags)!;
        var getter = CreatePropertyGetter<PrivateSetItem>(property.Name, flags);

        var item = new PrivateSetItem();
        property.SetValue(item, 11);
        getter(item).Should().Be(11);

        property.SetValue(item, 22);
        getter(item).Should().Be(22);
    }

    [TestMethod]
    public void CreatePropertyGetter_PrivateSet_ByName_Fail()
    {
        new Action(() => CreatePropertyGetter<PrivateSetItem>(nameof(PrivateSetItem.RefProp), Flags.Instance | Flags.Public | Flags.NonPublic)).Should().NotThrow();

        new Action(() => CreatePropertyGetter<PrivateSetItem>(nameof(PrivateSetItem.RefProp), Flags.Instance | Flags.Public)).Should().Throw<Exception>();

        new Action(() => CreatePropertyGetter<PrivateSetItem>(nameof(PrivateSetItem.RefProp), Flags.Instance | Flags.NonPublic)).Should().Throw<Exception>();

        new Action(() => CreatePropertyGetter<PrivateSetItem>(nameof(PrivateSetItem.RefProp), Flags.Static | Flags.Public | Flags.NonPublic)).Should().Throw<Exception>();


        new Action(() => CreatePropertyGetter<PrivateSetItem>(nameof(PrivateSetItem.StaticRefProp), Flags.Static | Flags.Public | Flags.NonPublic)).Should().NotThrow();

        new Action(() => CreatePropertyGetter<PrivateSetItem>(nameof(PrivateSetItem.StaticRefProp), Flags.Static | Flags.Public)).Should().Throw<Exception>();

        new Action(() => CreatePropertyGetter<PrivateSetItem>(nameof(PrivateSetItem.StaticRefProp), Flags.Static | Flags.NonPublic)).Should().Throw<Exception>();

        new Action(() => CreatePropertyGetter<PrivateSetItem>(nameof(PrivateSetItem.StaticRefProp), Flags.Instance | Flags.Public | Flags.NonPublic)).Should().Throw<Exception>();


        new Action(() => CreatePropertyGetter<PrivateSetItem>("PrivateRefProp", Flags.Instance | Flags.NonPublic)).Should().NotThrow();

        new Action(() => CreatePropertyGetter<PrivateSetItem>("PrivateRefProp", Flags.Instance | Flags.Public)).Should().Throw<Exception>();

        new Action(() => CreatePropertyGetter<PrivateSetItem>("PrivateRefProp", Flags.Static | Flags.NonPublic)).Should().Throw<Exception>();
    }


    public class ReadOnlyItem
    {
        public string RefProp { get; } = "xxx";
        public int ValProp { get; } = 100;

        public static string StaticRefProp { get; } = "yyy";
        public static int StaticValProp { get; } = 200;

        private string PrivateRefProp { get; } = "zzz";
        private int PrivateValProp { get; } = 300;
    }

    [TestMethod]
    public void CreatePropertyGetter_ReadOnly_ByName_Ref()
    {
        var getter = CreatePropertyGetter<ReadOnlyItem>(nameof(Item.RefProp));

        var item = new ReadOnlyItem();
        getter(item).Should().Be("xxx");
    }

    [TestMethod]
    public void CreatePropertyGetter_ReadOnly_ByName_Val()
    {
        var getter = CreatePropertyGetter<ReadOnlyItem>(nameof(Item.ValProp));

        var item = new ReadOnlyItem();
        getter(item).Should().Be(100);
    }

    [TestMethod]
    public void CreatePropertyGetter_ReadOnly_ByName_StaticRef()
    {
        var flags = Flags.Static | Flags.Public | Flags.NonPublic;
        var getter = CreatePropertyGetter<ReadOnlyItem>(nameof(ReadOnlyItem.StaticRefProp), flags);

        getter(null).Should().Be("yyy");
    }

    [TestMethod]
    public void CreatePropertyGetter_ReadOnly_ByName_StaticVal()
    {
        var flags = Flags.Static | Flags.Public | Flags.NonPublic;
        var getter = CreatePropertyGetter<ReadOnlyItem>(nameof(ReadOnlyItem.StaticValProp), flags);

        getter(null).Should().Be(200);
    }

    [TestMethod]
    public void CreatePropertyGetter_ReadOnly_ByName_PrivateRef()
    {
        var flags = Flags.Instance | Flags.NonPublic;
        var getter = CreatePropertyGetter<ReadOnlyItem>("PrivateRefProp", flags);

        var item = new ReadOnlyItem();
        getter(item).Should().Be("zzz");
    }

    [TestMethod]
    public void CreatePropertyGetter_ReadOnly_ByName_PrivateVal()
    {
        var flags = Flags.Instance | Flags.NonPublic;
        var getter = CreatePropertyGetter<ReadOnlyItem>("PrivateValProp", flags);

        var item = new ReadOnlyItem();
        getter(item).Should().Be(300);
    }

    [TestMethod]
    public void CreatePropertyGetter_ReadOnly_ByName_Fail()
    {
        new Action(() => CreatePropertyGetter<ReadOnlyItem>(nameof(ReadOnlyItem.RefProp), Flags.Instance | Flags.Public)).Should().NotThrow();

        new Action(() => CreatePropertyGetter<ReadOnlyItem>(nameof(ReadOnlyItem.RefProp), Flags.Instance | Flags.NonPublic)).Should().Throw<Exception>();

        new Action(() => CreatePropertyGetter<ReadOnlyItem>(nameof(ReadOnlyItem.RefProp), Flags.Static | Flags.Public)).Should().Throw<Exception>();


        new Action(() => CreatePropertyGetter<ReadOnlyItem>(nameof(ReadOnlyItem.StaticRefProp), Flags.Static | Flags.Public)).Should().NotThrow();

        new Action(() => CreatePropertyGetter<ReadOnlyItem>(nameof(ReadOnlyItem.StaticRefProp), Flags.Instance | Flags.Public)).Should().Throw<Exception>();

        new Action(() => CreatePropertyGetter<ReadOnlyItem>(nameof(ReadOnlyItem.StaticRefProp), Flags.Static | Flags.NonPublic)).Should().Throw<Exception>();


        new Action(() => CreatePropertyGetter<ReadOnlyItem>("PrivateRefProp", Flags.Instance | Flags.NonPublic)).Should().NotThrow();

        new Action(() => CreatePropertyGetter<ReadOnlyItem>("PrivateRefProp", Flags.Instance | Flags.Public)).Should().Throw<Exception>();

        new Action(() => CreatePropertyGetter<ReadOnlyItem>("PrivateRefProp", Flags.Static | Flags.NonPublic)).Should().Throw<Exception>();
    }

    public record BaseItem(string Text);
    public record DerivedItem(string Text, int Value) : BaseItem(Text);

    [TestMethod]
    public void CreatePropertyGetter_Derived()
    {
        var txtGetter = CreatePropertyGetter<DerivedItem>(nameof(DerivedItem.Text));
        var valGetter = CreatePropertyGetter<DerivedItem>(nameof(DerivedItem.Value));

        var item = new DerivedItem("asd", 123);
        txtGetter(item).Should().Be("asd");
        valGetter(item).Should().Be(123);
    }

    public record struct StructItem(string Text, int Value);

    [TestMethod]
    public void CreatePropertyGetter_Struct()
    {
        var txtGetter = CreatePropertyGetter<StructItem>(nameof(StructItem.Text));
        var valGetter = CreatePropertyGetter<StructItem>(nameof(StructItem.Value));

        var item = new StructItem("asd", 123);
        txtGetter(item).Should().Be("asd");
        valGetter(item).Should().Be(123);
    }


}
