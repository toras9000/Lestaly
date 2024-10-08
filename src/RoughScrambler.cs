﻿using System.Buffers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Lestaly;

/// <summary>
/// 雑なデータスクランブル処理クラス
/// </summary>
/// <remarks>
/// このクラスではデータの可読性を落とす可逆のデータ加工を行う。
/// データ加工は収集可能な以下の情報を元にして暗号化を行うので、秘密情報を厳密に保護する強度はない。
/// <list type="bullet">
/// <item>インスタンス生成で指定する任意の目的文字列</item>
/// <item>インスタンス生成で指定するコンテキスト文字列(通常は指定を省略して呼び出し元ファイルパスを利用する)</item>
/// <item>実行固有情報(デフォルトではマシン名とユーザ名)</item>
/// </list>
/// 用途としてそれほど厳密に保護する必要のない情報を、すぐには読み取りできない形で保存しておくような場合を想定している。
/// </remarks>
public class RoughScrambler
{
    // 構築
    #region コンストラクタ
    /// <summary>暗号化の情報を指定するコンストラクタ</summary>
    /// <param name="purpose">任意の目的文字列</param>
    /// <param name="context">コンテキスト文字列。指定しても良いが、通常は指定省略して呼び出し元のファイルパスを渡す形を想定している。</param>
    /// <param name="envtokens">暗号キーを作るために利用する環境固有文字列のリスト。省略するとマシン名(ホスト名)とユーザ名を利用する</param>
    public RoughScrambler(string purpose = "", [CallerFilePath] string context = "", string[]? envtokens = default)
    {
        // 暗号化のための情報を収集
        // 同じ環境であれば同じ情報になるようなものを集めている。
        var writer = new ArrayBufferWriter<byte>(256);
        Encoding.UTF8.GetBytes(purpose ?? "", writer);
        Encoding.UTF8.GetBytes(context ?? "", writer);
        if (envtokens == null || envtokens.Length <= 0)
        {
            Encoding.UTF8.GetBytes(Environment.MachineName ?? "", writer);
            Encoding.UTF8.GetBytes(Environment.UserName ?? "", writer);
        }
        else
        {
            foreach (var env in envtokens)
            {
                Encoding.UTF8.GetBytes(env ?? "", writer);
            }
        }
        if (writer.WrittenCount < 32)
        {
            var padding = (stackalloc byte[32 - writer.WrittenCount]);
            padding.Fill((byte)'x');
            writer.Write(padding);
        }

        // 暗号化のキー・初期ベクタをハッシュ化を用いて生成する
        var key = (stackalloc byte[32]);
        var iv = (stackalloc byte[32]);
        if (!SHA256.TryHashData(writer.WrittenSpan, key, out var writtenKey)) throw new InvalidOperationException();
        if (!SHA256.TryHashData(key, iv, out var writtenIv)) throw new InvalidOperationException();

        // キー・初期ベクタを保持
        this.key = key.ToArray();
        this.iv = iv[..16].ToArray();
    }
    #endregion

    // 公開メソッド
    #region スクランブル処理
    /// <summary>データのスクランブル処理</summary>
    /// <param name="data">データ</param>
    /// <returns>スクランブルされたバイト列</returns>
    public byte[] Scramble(ReadOnlySpan<byte> data)
    {
        using var aes = Aes.Create();
        aes.Key = this.key;
        aes.IV = this.iv;
        return aes.EncryptCbc(data, this.iv);
    }

    /// <summary>データのスクランブル解除処理</summary>
    /// <param name="data">スクランブルされたバイト列</param>
    /// <returns>スクランブル解除されたデータ</returns>
    public byte[] Descramble(ReadOnlySpan<byte> data)
    {
        using var aes = Aes.Create();
        aes.Key = this.key;
        aes.IV = this.iv;
        return aes.DecryptCbc(data, this.iv);
    }
    #endregion

    // 非公開フィールド
    #region 暗号化情報
    /// <summary>暗号化キー</summary>
    private readonly byte[] key;

    /// <summary>暗号化初期ベクタ</summary>
    private readonly byte[] iv;
    #endregion
}
