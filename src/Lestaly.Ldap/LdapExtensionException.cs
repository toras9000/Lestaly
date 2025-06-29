namespace Lestaly;

/// <summary>LDAP関連処理用の例外クラス</summary>
public class LdapExtensionException : Exception
{
    /// <summary>エラーメッセージを指定するコンストラクタ</summary>
    /// <param name="message">エラーメッセージ</param>
    public LdapExtensionException(string message) : base(message) { }

    /// <summary>エラーメッセージと内部例外を指定するコンストラクタ</summary>
    /// <param name="message">エラーメッセージ</param>
    /// <param name="innerException">内部例外</param>
    public LdapExtensionException(string message, Exception innerException) : base(message, innerException) { }
}
