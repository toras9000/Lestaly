using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Lestaly;

/// <summary>FormUrlEncoded 関係の補助処理</summary>
public static class FormUrlEncoded
{
    /// <summary>オブジェクトから FormUrlEncodedContent を構築する</summary>
    /// <typeparam name="T">オブジェクト型</typeparam>
    /// <param name="value">オブジェクトインスタンス</param>
    /// <returns>構築された FormUrlEncodedContent</returns>
    public static FormUrlEncodedContent CreateContent<T>(T value)
    {
        static IEnumerable<KeyValuePair<string, string>> enumerateMembers(T obj)
        {
            foreach (var prop in typeof(T).GetProperties())
            {
                var dispAttr = prop.GetCustomAttribute<DisplayAttribute>();
                var name = dispAttr?.Name ?? prop.Name;
                var value = prop.GetValue(obj);
                yield return new(name, $"{value}");
            }
        }

        return new FormUrlEncodedContent(enumerateMembers(value));
    }
}
