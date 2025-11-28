using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Lestaly;


/// <summary>
/// Json 関連の拡張メソッド
/// </summary>
public static class JsonExtensions
{
    extension(JsonNode self)
    {
        /// <summary>オブジェクトプロパティをソートした複製を生成する</summary>
        /// <returns>複製されたオブジェクト</returns>
        /// <exception cref="NotSupportedException">不明なノードタイプに遭遇</exception>
        public JsonNode ToSorted()
            => self switch
            {
                JsonArray ary => ary.ToSorted(),
                JsonObject obj => obj.ToSorted(),
                JsonValue val => val.DeepClone(),
                _ => throw new NotSupportedException(),
            };

        /// <summary>JsonNode配下のプロパティを走査する。</summary>
        /// <param name="walker">プロパティ名と値を受け取るハンドラ</param>
        public void VisitProperties(Action<string, JsonNode?> walker)
        {
            void visitNodeTree(JsonNode node)
            {
                if (node is JsonObject obj)
                {
                    foreach (var prop in obj)
                    {
                        walker(prop.Key, prop.Value);

                        if (prop.Value != null) visitNodeTree(prop.Value);
                    }
                }
                else if (node is JsonArray ary)
                {
                    foreach (var elem in ary)
                    {
                        if (elem == null) continue;
                        visitNodeTree(elem);
                    }
                }

            }

            visitNodeTree(self);
        }

        /// <summary>JsonNode配下のプロパティを走査する。</summary>
        /// <param name="walker">プロパティ名と値を受け取るハンドラ</param>
        public void UpdateProperties(Action<IJsonPropUpdater> walker)
        {
            var updater = new JsonPropUpdater(default!, default!, default);

            void visitNodeTree(JsonNode node)
            {
                if (node is JsonObject obj)
                {
                    var properties = obj.ToArray();
                    foreach (var prop in properties)
                    {
                        updater.ChangeContext(obj, prop.Key, prop.Value);

                        walker(updater);

                        if (prop.Value != null) visitNodeTree(prop.Value);
                    }
                }
                else if (node is JsonArray ary)
                {
                    foreach (var elem in ary)
                    {
                        if (elem == null) continue;
                        visitNodeTree(elem);
                    }
                }

            }

            visitNodeTree(self);
        }
    }

    /// <summary>JSONプロパティ更新インタフェース</summary>
    /// <remarks>
    /// このインタフェースは単一のプロパティを対象に、値を更新するための手段を提供する。
    /// </remarks>
    public interface IJsonPropUpdater
    {
        /// <summary>対象プロパティ名</summary>
        string Name { get; }

        /// <summary>対象プロパティ値</summary>
        JsonNode? Value { get; }

        /// <summary>プロパティ値を設定する</summary>
        /// <typeparam name="TValue">設定する値の型</typeparam>
        /// <param name="value">設定するプロパティ値</param>
        void SetValue<TValue>(TValue? value);
    }

    /// <summary>JSONプロパティ更新アダプタ</summary>
    private class JsonPropUpdater : IJsonPropUpdater
    {
        // 構築
        #region コンストラクタ
        /// <summary>プロパティコンテキストを指定するコンストラクタ</summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="name">プロパティ名</param>
        /// <param name="value">プロパティ値</param>
        public JsonPropUpdater(JsonObject parent, string name, JsonNode? value)
        {
            this.ChangeContext(parent, name, value);
        }
        #endregion

        // 公開プロパティ
        #region JSONプロパティ
        /// <inheritdoc />
        public string Name => this.name;

        /// <inheritdoc />
        public JsonNode? Value => this.value;
        #endregion

        // 公開プロパティ
        #region 更新
        /// <inheritdoc />
        public void SetValue<TValue>(TValue? value)
        {
            if (value == null)
            {
                this.parent[this.name] = null;
            }
            else
            {
                if (this.value == null)
                {
                    this.parent[this.name] = value is JsonNode node ? node : JsonValue.Create(value);
                }
                else
                {
                    this.value.ReplaceWith(value);
                }
            }
        }
        #endregion

        #region 内部I/F
        /// <summary>プロパティコンテキストを変更する</summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="name">プロパティ名</param>
        /// <param name="value">プロパティ値</param>
        [MemberNotNull(nameof(parent))]
        [MemberNotNull(nameof(name))]
        public void ChangeContext(JsonObject parent, string name, JsonNode? value)
        {
            this.parent = parent;
            this.name = name;
            this.value = value;
        }
        #endregion

        // 非公開フィールド
        #region 対象プロパティ情報
        /// <summary>親オブジェクト</summary>
        private JsonObject parent;
        /// <summary>プロパティ名</summary>
        private string name;
        /// <summary>プロパティ値</summary>
        private JsonNode? value;
        #endregion
    }

    extension(JsonArray self)
    {
        /// <summary>オブジェクトプロパティをソートした複製を生成する</summary>
        /// <returns>プロパティをソートした複製オブジェクト</returns>
        public JsonArray ToSorted()
            => new JsonArray(self.Select(e => e?.ToSorted()).ToArray());
    }

    extension(JsonObject self)
    {
        /// <summary>オブジェクトプロパティをソートした複製を生成する</summary>
        /// <returns>プロパティをソートした複製オブジェクト</returns>
        public JsonObject ToSorted()
            => new JsonObject(self.OrderBy(p => p.Key).Select(p => KeyValuePair.Create(p.Key, p.Value?.ToSorted())).ToArray());
    }

    extension(JsonElement self)
    {
        /// <summary>オブジェクトプロパティをソートしたJsonNodeを生成する</summary>
        /// <returns>プロパティがソートされたJsonNodeオブジェクト</returns>
        public JsonNode? ToSortedNode()
            => self.ValueKind switch
            {
                JsonValueKind.Object => jsonObjectToSorted(self),
                JsonValueKind.Array => jsonArrayToSorted(self),
                _ => JsonValue.Create(self),
            };

        /// <summary>オブジェクト要素からプロパティをソートしたJsonNodeを生成する</summary>
        /// <param name="element">オブジェクト要素</param>
        /// <returns>プロパティがソートされたJsonNodeオブジェクト</returns>
        private static JsonObject jsonObjectToSorted(JsonElement element)
            => new JsonObject(
                element.EnumerateObject()
                    .OrderBy(p => p.Name)
                    .Select(p => KeyValuePair.Create(p.Name, p.Value.ToSortedNode()))
            );

        /// <summary>配列要素からプロパティをソートしたJsonNodeを生成する</summary>
        /// <param name="element">配列要素</param>
        /// <returns>プロパティがソートされたJsonNodeオブジェクト</returns>
        private static JsonArray jsonArrayToSorted(JsonElement element)
            => new JsonArray(
                element.EnumerateArray().Select(e => e.ToSortedNode()).ToArray()
            );
    }

}
