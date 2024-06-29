using System.Text;
using System.Text.Json;

namespace LestalyTest.Extensions;

[TestClass()]
public class FileExtensionsTests
{
    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext context)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    #region Name
    [TestMethod]
    public void BaseName()
    {
        new FileInfo("testfile.ext").BaseName().Should().Be("testfile");
        new FileInfo("testfile.tar.gz").BaseName().Should().Be("testfile.tar");
        new FileInfo(".testfile").BaseName().Should().Be("");
        new FileInfo(".testfile.ext").BaseName().Should().Be(".testfile");
        new FileInfo("testfile").BaseName().Should().Be("testfile");

        new Action(() => default(FileInfo)!.BaseName()).Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void Extension()
    {
        new FileInfo("testfile.ext").Extension().Should().Be(".ext");
        new FileInfo("testfile.tar.gz").Extension().Should().Be(".gz");
        new FileInfo(".testfile").Extension().Should().Be(".testfile");
        new FileInfo(".testfile.ext").Extension().Should().Be(".ext");
        new FileInfo("testfile").Extension().Should().Be("");

        new Action(() => default(FileInfo)!.Extension()).Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void GetAnotherExtension()
    {
        new FileInfo("testfile.ext").GetAnotherExtension(".other").Name.Should().Be("testfile.other");
        new FileInfo("testfile.ext").GetAnotherExtension("other").Name.Should().Be("testfile.other");
        new FileInfo("testfile").GetAnotherExtension("other").Name.Should().Be("testfile.other");

        new Action(() => default(FileInfo)!.GetAnotherExtension("other")).Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void HasAnyExtension_enum()
    {
        new FileInfo("testfile.ext").HasAnyExtension(new[] { ".extension", ".ex" }, ignoreCase: true).Should().BeFalse();
        new FileInfo("testfile.ext").HasAnyExtension(new[] { "extension", "ex" }, ignoreCase: true).Should().BeFalse();

        new FileInfo("testfile.ext").HasAnyExtension(new[] { ".extension", ".ex" }, ignoreCase: false).Should().BeFalse();
        new FileInfo("testfile.ext").HasAnyExtension(new[] { "extension", "ex" }, ignoreCase: false).Should().BeFalse();

        new FileInfo("testfile.ext").HasAnyExtension(new[] { ".extension", ".ex", ".ext" }, ignoreCase: true).Should().BeTrue();
        new FileInfo("testfile.ext").HasAnyExtension(new[] { "extension", ".ex", "ext" }, ignoreCase: true).Should().BeTrue();
        new FileInfo("testfile.ext").HasAnyExtension(new[] { ".extension", ".ex", ".EXT" }, ignoreCase: true).Should().BeTrue();
        new FileInfo("testfile.ext").HasAnyExtension(new[] { "extension", ".ex", "EXT" }, ignoreCase: true).Should().BeTrue();

        new FileInfo("testfile.ext").HasAnyExtension(new[] { ".extension", ".ex", ".ext" }, ignoreCase: false).Should().BeTrue();
        new FileInfo("testfile.ext").HasAnyExtension(new[] { "extension", ".ex", "ext" }, ignoreCase: false).Should().BeTrue();
        new FileInfo("testfile.ext").HasAnyExtension(new[] { ".extension", ".ex", ".EXT" }, ignoreCase: false).Should().BeFalse();
        new FileInfo("testfile.ext").HasAnyExtension(new[] { "extension", ".ex", "EXT" }, ignoreCase: false).Should().BeFalse();
    }

    [TestMethod]
    public void HasAnyExtension_params()
    {
        new FileInfo("testfile.ext").HasAnyExtension(".extension", ".ex").Should().BeFalse();
        new FileInfo("testfile.ext").HasAnyExtension("extension", "ex").Should().BeFalse();

        new FileInfo("testfile.ext").HasAnyExtension(".extension", ".ex", ".ext").Should().BeTrue();
        new FileInfo("testfile.ext").HasAnyExtension("extension", ".ex", "ext").Should().BeTrue();
        new FileInfo("testfile.ext").HasAnyExtension(".extension", ".ex", ".EXT").Should().BeTrue();
        new FileInfo("testfile.ext").HasAnyExtension("extension", ".ex", "EXT").Should().BeTrue();
    }
    #endregion

    #region FileSystemInfo
    [TestMethod]
    public void RelativeFileAt()
    {
        new FileInfo(@"X:\abc\def")
            .RelativeFileAt(@"ghi\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\abc\ghi\jkl");

        new FileInfo(@"X:\abc\def")
            .RelativeFileAt(@".\ghi\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\abc\ghi\jkl");
    }

    [TestMethod]
    public void RelativeFileAt_Traversal()
    {
        new FileInfo(@"X:\abc\def")
            .RelativeFileAt(@"..\ghi\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\ghi\jkl");

        new FileInfo(@"X:\abc\def")
            .RelativeFileAt(@"..\def\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\def\jkl");

        new FileInfo(@"X:\abc\def")
            .RelativeFileAt(@"..\..\..\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\jkl");
    }

    [TestMethod]
    public void RelativeFileAt_Absolute()
    {
        new FileInfo(@"X:\abc\def")
            .RelativeFileAt(@"V:\hoge\fuga")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"V:\hoge\fuga");
    }

    [TestMethod]
    public void RelativeFileAt_EmptyRelativeFail()
    {
        new FileInfo(@"X:\abc\def").RelativeFileAt("").Should().BeNull();

        new FileInfo(@"X:\abc\def").RelativeFileAt(" ").Should().BeNull();

        new FileInfo(@"X:\abc\def").RelativeFileAt(null).Should().BeNull();
    }

    [TestMethod]
    public void RelativeFileAt_NullSelfFail()
    {
        FluentActions.Invoking(() => default(FileInfo)!.RelativeFileAt(@"V:\hoge\fuga"))
            .Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void RelativeFile()
    {
        new FileInfo(@"X:\abc\def")
            .RelativeFile(@"ghi\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\abc\ghi\jkl");

        new FileInfo(@"X:\abc\def")
            .RelativeFile(@".\ghi\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\abc\ghi\jkl");
    }

    [TestMethod]
    public void RelativeFile_Traversal()
    {
        new FileInfo(@"X:\abc\def")
            .RelativeFile(@"..\ghi\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\ghi\jkl");

        new FileInfo(@"X:\abc\def")
            .RelativeFile(@"..\def\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\def\jkl");

        new FileInfo(@"X:\abc\def")
            .RelativeFile(@"..\..\..\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\jkl");
    }

    [TestMethod]
    public void RelativeFile_Absolute()
    {
        new FileInfo(@"X:\abc\def")
            .RelativeFile(@"V:\hoge\fuga")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"V:\hoge\fuga");
    }

    [TestMethod]
    public void RelativeFile_EmptyRelativeFail()
    {
        FluentActions.Invoking(() => new FileInfo(@"X:\abc\def").RelativeFile(""))
            .Should().Throw<ArgumentException>();

        FluentActions.Invoking(() => new FileInfo(@"X:\abc\def").RelativeFile(" "))
            .Should().Throw<ArgumentException>();

        FluentActions.Invoking(() => new FileInfo(@"X:\abc\def").RelativeFile(null!))
            .Should().Throw<ArgumentException>();
    }

    [TestMethod]
    public void RelativeFile_NullSelfFail()
    {
        FluentActions.Invoking(() => default(FileInfo)!.RelativeFile(@"V:\hoge\fuga"))
            .Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void RelativeDirectoryAt()
    {
        new FileInfo(@"X:\abc\def")
            .RelativeDirectoryAt(@"ghi\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\abc\ghi\jkl");

        new FileInfo(@"X:\abc\def")
            .RelativeDirectoryAt(@".\ghi\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\abc\ghi\jkl");
    }

    [TestMethod]
    public void RelativeDirectoryAt_Traversal()
    {
        new FileInfo(@"X:\abc\def")
            .RelativeDirectoryAt(@"..\ghi\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\ghi\jkl");

        new FileInfo(@"X:\abc\def")
            .RelativeDirectoryAt(@"..\def\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\def\jkl");

        new FileInfo(@"X:\abc\def")
            .RelativeDirectoryAt(@"..\..\..\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\jkl");
    }

    [TestMethod]
    public void RelativeDirectoryAt_Absolute()
    {
        new FileInfo(@"X:\abc\def")
            .RelativeDirectoryAt(@"V:\hoge\fuga")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"V:\hoge\fuga");
    }

    [TestMethod]
    public void RelativeDirectoryAt_EmptyRelative()
    {
        new FileInfo(@"X:\abc\def")
            .RelativeDirectoryAt("")
            .Should().BeNull();

        new FileInfo(@"X:\abc\def")
            .RelativeDirectoryAt(" ")
            .Should().BeNull();

        new FileInfo(@"X:\abc\def")
            .RelativeDirectoryAt(null)
            .Should().BeNull();
    }

    [TestMethod]
    public void RelativeDirectoryAt_NullFail()
    {
        FluentActions.Invoking(() => default(FileInfo)!.RelativeDirectoryAt(@"V:\hoge\fuga"))
            .Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void RelativeDirectory()
    {
        new FileInfo(@"X:\abc\def")
            .RelativeDirectory(@"ghi\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\abc\ghi\jkl");

        new FileInfo(@"X:\abc\def")
            .RelativeDirectory(@".\ghi\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\abc\ghi\jkl");
    }

    [TestMethod]
    public void RelativeDirectory_Traversal()
    {
        new FileInfo(@"X:\abc\def")
            .RelativeDirectory(@"..\ghi\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\ghi\jkl");

        new FileInfo(@"X:\abc\def")
            .RelativeDirectory(@"..\def\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\def\jkl");

        new FileInfo(@"X:\abc\def")
            .RelativeDirectory(@"..\..\..\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\jkl");
    }

    [TestMethod]
    public void RelativeDirectory_Absolute()
    {
        new FileInfo(@"X:\abc\def")
            .RelativeDirectory(@"V:\hoge\fuga")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"V:\hoge\fuga");
    }

    [TestMethod]
    public void RelativeDirectory_EmptyRelative()
    {
        new FileInfo(@"X:\abc\def")
            .RelativeDirectory("")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\abc");

        new FileInfo(@"X:\abc\def")
            .RelativeDirectory(null)
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\abc");
    }

    [TestMethod]
    public void RelativeDirectory_NullFail()
    {
        FluentActions.Invoking(() => default(FileInfo)!.RelativeDirectory(@"V:\hoge\fuga"))
            .Should().Throw<ArgumentNullException>();
    }
    #endregion

    #region Read
    [TestMethod]
    public void ReadAllBytes()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var data = Enumerable.Range(30, 256).Select(n => (byte)n).ToArray();
        File.WriteAllBytes(target.FullName, data);

        // テスト対象実行＆検証
        var actual = target.ReadAllBytes();
        actual.Should().Equal(data);
    }

    [TestMethod]
    public void ReadAllText()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var text = "あいう\nえおか";
        File.WriteAllText(target.FullName, text);

        // テスト対象実行＆検証
        var actual = target.ReadAllText();
        actual.Should().Be(text);
    }

    [TestMethod]
    public void ReadAllText_enc()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        var text = "あいう\nえおか";
        File.WriteAllText(target.FullName, text, enc);

        // テスト対象実行＆検証
        var actual = target.ReadAllText(enc);
        actual.Should().Be(text);
    }

    [TestMethod]
    public void ReadAllLines()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var texts = new[]
        {
            "あいう",
            "えおか",
            "きくけ",
        };
        File.WriteAllLines(target.FullName, texts);

        // テスト対象実行＆検証
        var actual = target.ReadAllLines();
        actual.Should().Equal(texts);
    }

    [TestMethod]
    public void ReadAllLines_enc()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        var texts = new[]
        {
            "あいう",
            "えおか",
            "きくけ",
        };
        File.WriteAllLines(target.FullName, texts, enc);

        // テスト対象実行＆検証
        var actual = target.ReadAllLines(enc);
        actual.Should().Equal(texts);
    }

    [TestMethod]
    public void ReadLines()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var texts = new[]
        {
            "あいう",
            "えおか",
            "きくけ",
        };
        File.WriteAllLines(target.FullName, texts);

        // テスト対象実行＆検証
        var actual = target.ReadLines();
        actual.Should().Equal(texts);
    }

    [TestMethod]
    public void ReadLines_enc()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        var texts = new[]
        {
            "あいう",
            "えおか",
            "きくけ",
        };
        File.WriteAllLines(target.FullName, texts, enc);

        // テスト対象実行＆検証
        var actual = target.ReadLines(enc);
        actual.Should().Equal(texts);
    }

    [TestMethod]
    public async Task ReadAllBytesAsync()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var data = Enumerable.Range(30, 256).Select(n => (byte)n).ToArray();
        File.WriteAllBytes(target.FullName, data);

        // テスト対象実行＆検証
        var actual = await target.ReadAllBytesAsync();
        actual.Should().Equal(data);
    }

    [TestMethod]
    public async Task ReadAllTextAsync()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var text = "あいう\nえおか";
        File.WriteAllText(target.FullName, text);

        // テスト対象実行＆検証
        var actual = await target.ReadAllTextAsync();
        actual.Should().Be(text);
    }

    [TestMethod]
    public async Task ReadAllTextAsync_enc()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        var text = "あいう\nえおか";
        File.WriteAllText(target.FullName, text, enc);

        // テスト対象実行＆検証
        var actual = await target.ReadAllTextAsync(enc);
        actual.Should().Be(text);
    }

    [TestMethod]
    public async Task ReadAllLinesAsync()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var texts = new[]
        {
            "あいう",
            "えおか",
            "きくけ",
        };
        File.WriteAllLines(target.FullName, texts);

        // テスト対象実行＆検証
        var actual = await target.ReadAllLinesAsync();
        actual.Should().Equal(texts);
    }

    [TestMethod]
    public async Task ReadAllLinesAsync_enc()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        var texts = new[]
        {
            "あいう",
            "えおか",
            "きくけ",
        };
        File.WriteAllLines(target.FullName, texts, enc);

        // テスト対象実行＆検証
        var actual = await target.ReadAllLinesAsync(enc);
        actual.Should().Equal(texts);
    }

    [TestMethod]
    public async Task CreateTextReader()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        var text = "あいう\nえおか\nきくけ";
        await File.WriteAllTextAsync(target.FullName, text, enc);

        // テスト対象実行＆検証
        using var reader = target.CreateTextReader(detectBom: false, options: null, enc);
        var actual = await reader.ReadToEndAsync();
        actual.Should().Be(text);
    }
    #endregion

    #region Write
    [TestMethod]
    public void WriteAllBytes()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストデータ
        var data = Enumerable.Range(30, 256).Select(n => (byte)n).ToArray();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象実行
        target.WriteAllBytes(data);

        // 検証
        File.ReadAllBytes(target.FullName).Should().Equal(data);
    }

    [TestMethod]
    public void WriteAllBytes_span()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストデータ
        var data = Enumerable.Range(30, 256).Select(n => (byte)n).ToArray();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象実行
        target.WriteAllBytes(data.AsSpan(), options: new() { Mode = FileMode.Create, BufferSize = 1, });

        // 検証
        File.ReadAllBytes(target.FullName).Should().Equal(data);
    }

    [TestMethod]
    public void WriteAllBytes_span_Truncate()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストデータ
        var data = Enumerable.Range(30, 256).Select(n => (byte)n).ToArray();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テストデータより大きな適当なサイズのデータを書いておく
        File.WriteAllBytes(target.FullName, new byte[data.Length * 2]);

        // テスト対象実行
        target.WriteAllBytes(data.AsSpan(), options: new() { Mode = FileMode.Create, BufferSize = 1, });

        // 検証
        File.ReadAllBytes(target.FullName).Should().Equal(data);
    }

    [TestMethod]
    public void WriteAllText()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストデータ
        var text = "あいう\nえおか";

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象実行
        target.WriteAllText(text);

        // 検証
        File.ReadAllText(target.FullName).Should().Be(text);
    }

    [TestMethod]
    public void WriteAllText_enc()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストデータ
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        var text = "あいう\nえおか";

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象実行
        target.WriteAllText(text, enc);

        // 検証
        File.ReadAllText(target.FullName, enc).Should().Be(text);
    }

    [TestMethod]
    public void WriteAllText_span()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストデータ
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        var text = "あいう\nえおか";

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象実行
        target.WriteAllText(text.AsSpan(), encoding: enc, options: new() { Mode = FileMode.Create, BufferSize = 1, });

        // 検証
        File.ReadAllText(target.FullName, enc).Should().Be(text);
    }

    [TestMethod]
    public void WriteAllLines()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストデータ
        var texts = new[]
        {
            "あいう",
            "えおか",
            "きくけ",
        };

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象実行
        target.WriteAllLines(texts);

        // 検証
        File.ReadAllLines(target.FullName).Should().Equal(texts);
    }

    [TestMethod]
    public void WriteAllLines_enc()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストデータ
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        var texts = new[]
        {
            "あいう",
            "えおか",
            "きくけ",
        };

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象実行
        target.WriteAllLines(texts, enc);

        // 検証
        File.ReadAllLines(target.FullName, enc).Should().Equal(texts);
    }

    [TestMethod]
    public async Task WriteAllBytesAsync()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストデータ
        var data = Enumerable.Range(30, 256).Select(n => (byte)n).ToArray();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象実行
        await target.WriteAllBytesAsync(data);

        // 検証
        File.ReadAllBytes(target.FullName).Should().Equal(data);
    }

    [TestMethod]
    public async Task WriteAllBytesAsync_span()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストデータ
        var data = Enumerable.Range(30, 256).Select(n => (byte)n).ToArray();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象実行
        await target.WriteAllBytesAsync(data, options: new() { Mode = FileMode.Create, BufferSize = 1, });

        // 検証
        File.ReadAllBytes(target.FullName).Should().Equal(data);
    }

    [TestMethod]
    public async Task WriteAllTextAsync()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストデータ
        var text = "あいう\nえおか";

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象実行
        await target.WriteAllTextAsync(text);

        // 検証
        File.ReadAllText(target.FullName).Should().Be(text);
    }

    [TestMethod]
    public async Task WriteAllTextAsync_enc()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストデータ
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        var text = "あいう\nえおか";

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象実行
        await target.WriteAllTextAsync(text, enc);

        // 検証
        File.ReadAllText(target.FullName, enc).Should().Be(text);
    }

    [TestMethod]
    public async Task WriteAllLinesAsync()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストデータ
        var texts = new[]
        {
            "あいう",
            "えおか",
            "きくけ",
        };

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象実行
        await target.WriteAllLinesAsync(texts);

        // 検証
        File.ReadAllLines(target.FullName).Should().Equal(texts);
    }

    [TestMethod]
    public async Task WriteAllLinesAsync_enc()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストデータ
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        var texts = new[]
        {
            "あいう",
            "えおか",
            "きくけ",
        };

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象実行
        await target.WriteAllLinesAsync(texts, enc);

        // 検証
        File.ReadAllLines(target.FullName, enc).Should().Equal(texts);
    }

    [TestMethod]
    public async Task WriteAllLinesAsync_span()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストデータ
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        var texts = new[]
        {
            "あいう",
            "えおか",
            "きくけ",
        };

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象実行
        await target.WriteAllLinesAsync(texts, enc);

        // 検証
        File.ReadAllLines(target.FullName, enc).Should().Equal(texts);
    }

    [TestMethod]
    public async Task CreateTextWriter()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テストデータ
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        var text = "あいう\nえおか\nきくけ";

        // テスト対象実行
        using (var writer = target.CreateTextWriter(encoding: enc))
        {
            await writer.WriteAsync(text);
        }

        // 検証
        File.ReadAllText(target.FullName, enc).Should().Be(text);
    }

    [TestMethod]
    public async Task CreateTextWriter_append()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // 準備
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        await File.WriteAllTextAsync(target.FullName, "あいう", enc);

        // テスト対象実行
        using (var writer = target.CreateTextWriter(append: true, encoding: enc))
        {
            await writer.WriteAsync("えおか");
        }

        // 検証
        File.ReadAllText(target.FullName, enc).Should().Be("あいうえおか");
    }
    #endregion

    private static TObject? decodeJsonByTemplate<TObject>(string json, TObject template)
        => (TObject?)JsonSerializer.Deserialize(json, template!.GetType());

    private record TestData(string String, int Integer);

    #region Read JSON
    [TestMethod()]
    public async Task ReadJsonAsync()
    {
        using var tempDir = new TempDir();

        var file = tempDir.Info.RelativeFile("asd.txt");
        var data = new TestData("test", 123);
        await file.WriteJsonAsync(data);

        var actual = await file.ReadJsonAsync<TestData>();
        actual.Should().Be(data);
    }

    [TestMethod()]
    public async Task ReadJsonByTemplateAsync()
    {
        using var tempDir = new TempDir();

        var file = tempDir.Info.RelativeFile("asd.txt");
        var data = new
        {
            Text = "test",
            Number = 123,
        };
        await file.WriteJsonAsync(data);

        var actual = await file.ReadJsonByTemplateAsync(data);
        actual.Should().Be(data);
    }
    #endregion

    #region Write JSON
    [TestMethod()]
    public async Task WriteJsonAsync()
    {
        using var tempDir = new TempDir();

        var file = tempDir.Info.RelativeFile("asd.txt");
        var data = new
        {
            Text = "test",
            Number = 123,
        };
        await file.WriteJsonAsync(data);

        var json = await file.ReadAllTextAsync();
        var actual = decodeJsonByTemplate(json, data);
        actual.Should().Be(data);
    }

    [TestMethod()]
    public async Task WriteJsonAsync_Truncate()
    {
        using var tempDir = new TempDir();

        var file = tempDir.Info.RelativeFile("asd.txt");

        // テストデータより大きな適当なサイズのデータを書いておく
        File.WriteAllBytes(file.FullName, new byte[8192]);

        var data = new
        {
            Text = "test",
            Number = 123,
        };
        await file.WriteJsonAsync(data);

        var json = await file.ReadAllTextAsync();
        var actual = decodeJsonByTemplate(json, data);
        actual.Should().Be(data);
    }
    #endregion

    #region FileSystem
    [TestMethod]
    public void WithDirectoryCreate()
    {
        using var testDir = new TempDir();

        var tempDir = testDir.Info.FullName;
        var subDirFile = Path.GetFullPath(Path.Combine(tempDir, "abc/def.txt"));
        new FileInfo(subDirFile).WithDirectoryCreate().FullName.Should().Be(subDirFile);
        Directory.Exists(Path.GetDirectoryName(subDirFile)).Should().BeTrue();
    }

    [TestMethod()]
    public async Task Touch()
    {
        using var tempDir = new TempDir();

        var file = tempDir.Info.RelativeFile("asd.txt");
        file.Exists.Should().Be(false);
        file.Touch();
        file.Exists.Should().Be(true);

        var timestamp = file.LastWriteTimeUtc;
        await Task.Delay(3000);
        file.Touch();
        file.LastWriteTimeUtc.Should().NotBe(timestamp);
    }
    #endregion

    #region Path
    [TestMethod]
    public void GetPathSegments()
    {
        new FileInfo(@"c:/abc/def/zxc/asd/qwe.txt").GetPathSegments()
            .Should().Equal(new[] { @"c:\", "abc", "def", "zxc", "asd", "qwe.txt", });

        new FileInfo(@"c:/abc/../asd/qwe.txt").GetPathSegments()
            .Should().Equal(new[] { @"c:\", "asd", "qwe.txt", });

        new FileInfo(@"/abc/def/qwe.txt").GetPathSegments()
            .Should().HaveElementAt(0, Path.GetPathRoot(Environment.CurrentDirectory))
            .And.Subject.Skip(1).Should().Equal(new[] { "abc", "def", "qwe.txt", });

        new FileInfo(@"\\aaa\bbb\ccc\ddd").GetPathSegments()
            .Should().Equal(new[] { @"\\aaa\bbb", "ccc", "ddd", });
    }

    [TestMethod]
    public void IsDescendantOf()
    {
        new FileInfo(@"c:/abc/def/zxc/asd/qwe.txt")
            .IsDescendantOf(new DirectoryInfo(@"c:/abc/def/zxc/asd"))
            .Should().BeTrue();

        new FileInfo(@"c:/abc/def/zxc/asd/qwe.txt")
            .IsDescendantOf(new DirectoryInfo(@"c:/abc/def/zxc/"))
            .Should().BeTrue();

        new FileInfo(@"c:/abc/def/zxc/asd/qwe.txt")
            .IsDescendantOf(new DirectoryInfo(@"c:"))
            .Should().BeTrue();

        new FileInfo(@"c:/abc/def/zxc/asd/qwe.txt")
            .IsDescendantOf(new DirectoryInfo(@"C:/ABC/DEF/ZXC/ASD"))
            .Should().BeTrue();

        new FileInfo(@"c:/abc/def/zxc/asd/qwe.txt")
            .IsDescendantOf(new DirectoryInfo(@"c:/abc/def/zxc/asd/qwe"))
            .Should().BeFalse();
    }

    [TestMethod]
    public void RelativePathFrom()
    {
        new FileInfo(@"c:/abc/def/ghi/asd/qwe.txt").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: true)
            .Should().Be(@"asd\qwe.txt");

        new FileInfo(@"c:/abc/def/ghi/qwe.txt").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: true)
            .Should().Be(@"qwe.txt");

        new FileInfo(@"c:/abc/def/zxc/asd/qwe.txt").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: true)
            .Should().Be(@"..\zxc\asd\qwe.txt");

        new FileInfo(@"D:/abc/def/zxc/asd/qwe.txt").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: true)
            .Should().Be(@"D:\abc\def\zxc\asd\qwe.txt");

        new FileInfo(@"\\aaa\bbb\ccc").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: true)
            .Should().Be(@"\\aaa\bbb\ccc");

        new FileInfo(@"\\aaa\bbb\ccc\eee\fff").RelativePathFrom(new DirectoryInfo(@"\\aaa\bbb\ccc\ddd"), ignoreCase: true)
            .Should().Be(@"..\eee\fff");

        new FileInfo(@"\\aaa\ggg\ccc\eee\fff").RelativePathFrom(new DirectoryInfo(@"\\aaa\bbb\ccc\ddd"), ignoreCase: true)
            .Should().Be(@"\\aaa\ggg\ccc\eee\fff");
    }

    [TestMethod]
    public void RelativePathFrom_IgnoreCase()
    {
        new FileInfo(@"c:/abc/def/ghi/asd/qwe.txt").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: false)
            .Should().Be(@"asd\qwe.txt");

        new FileInfo(@"c:/abc/def/Ghi/asd/qwe.txt").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: false)
            .Should().Be(@"..\Ghi\asd\qwe.txt");
    }
    #endregion

}
