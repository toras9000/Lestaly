using Lestaly.Cx;

namespace LestalyTest;

public static class TempDirExtensions
{
    public static async ValueTask<FileInfo> MakeTestExeAsync(this DirectoryInfo self, string name, string code)
    {
        var workDir = self.RelativeDirectory(name).WithCreate();
        var srcFile = workDir.RelativeFile($"{name}.cs");
        srcFile.WriteAllText(code);
        "dotnet".args("build", "--output", workDir, srcFile).silent().result().success().Wait();
        var exeFile = workDir.RelativeFile($"{name}.exe");
        return exeFile;
    }

}
