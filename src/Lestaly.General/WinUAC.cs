using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Principal;

namespace Lestaly;

/// <summary>Windows UAC関連ユーティリティ</summary>
public static partial class WinUAC
{
    /// <summary>UAC昇格状態であるかを判定する</summary>
    /// <returns>昇格有無</returns>
    [SupportedOSPlatform("Windows")]
    public static bool IsElevated()
    {
        // Windows 以外では利用不可
        if (!OperatingSystem.IsWindows()) throw new PlatformNotSupportedException();

        // 現在のプロセス取得
        using var process = Process.GetCurrentProcess();

        var tokenHandle = nint.Zero;
        try
        {
            // プロセスのアクセストークンを開く
            var openResult = NativeMethods.OpenProcessToken(process.Handle, NativeMethods.TokenAccessMask_TOKEN_QUERY, out tokenHandle);
            if (!openResult) throw new TargetInvocationException(default);

            unsafe
            {
                // 昇格状態を取得
                var tokenResult = NativeMethods.GetTokenInformation(tokenHandle, NativeMethods.TOKEN_INFORMATION_CLASS_TokenElevation, out var tokenElevated, sizeof(uint), out _);
                if (!openResult) throw new TargetInvocationException(default);

                // 昇格状態を判定
                return tokenElevated != 0;
            }
        }
        finally
        {
            if (tokenHandle != nint.Zero)
            {
                NativeMethods.CloseHandle(tokenHandle);
            }
        }
    }

    /// <summary>管理者権限での実行中であるかを判定する</summary>
    /// <returns>昇格有無</returns>
    [SupportedOSPlatform("Windows")]
    public static bool IsAdminRole()
    {
        // 現在のユーザーの WindowsID を取得
        using var identity = WindowsIdentity.GetCurrent();

        // WindowsPrincipal オブジェクトを作成
        var principal = new WindowsPrincipal(identity);

        // 組み込みの管理者（Administrator）ロールに属しているか判定
        return principal.IsInRole(WindowsBuiltInRole.Administrator);

    }

    private static partial class NativeMethods
    {
        public const uint TokenAccessMask_TOKEN_QUERY = 0x0008;
        public const int TOKEN_INFORMATION_CLASS_TokenElevation = 20;

        [LibraryImport("Kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool CloseHandle(nint hObject);

        [LibraryImport("Advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool OpenProcessToken(nint ProcessHandle, uint DesiredAccess, out nint TokenHandle);

        [LibraryImport("Advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool GetTokenInformation(nint TokenHandle, int TokenInformationClass, out uint TokenInformation, uint TokenInformationLength, out uint ReturnLength);
    }
}
