using Lestaly.Data;

namespace Lestaly;

/// <summary>フィールド分割オプション</summary>
/// <param name="Separator">セパレータキャラクタ</param>
/// <param name="Quote">クォートキャラクタ。nullの場合はクォートキャラクタなし。</param>
public record SplitFieldsOptions(char Separator = ',', char? Quote = '"');

/// <summary>
/// string のデータ処理に対する拡張メソッド
/// </summary>
public static class StringDataExtensions
{
    /// <summary>テキストをフィールド分割する</summary>
    /// <remarks><see cref="SplitFields(string, SplitFieldsOptions)"/></remarks>
    /// <param name="self">対象テキスト</param>
    /// <param name="separator">フィールドセパレータ</param>
    /// <returns>フィールド列のシーケンス</returns>
    public static IEnumerable<string[]> SplitFields(this string self, char separator = ',')
    {
        var options = new SplitFieldsOptions()
        {
            Separator = separator,
        };
        return self.SplitFields(options);
    }

    /// <summary>テキストをフィールド分割する</summary>
    /// <remarks>
    /// このメソッドではある程度決め打ちのルールでCSVのようなフィールドの連なるテキストを分割する。
    /// 分割ルールは以下となる。
    /// <list type="bullet">
    /// <item>改行(\r/\n\r\n)をレコードの終わりとしてシーケンス要素とする。</item>
    /// <item>シーケンス要素は指定のセパレータで区切ったフィールド配列とする。</item>
    /// <item>有効なクォートキャラクタが指定されている場合、クォートキャラクタで囲まれた範囲はセパレータも改行も解釈せずにフィールドの一部とする。</item>
    /// <item>クォート文字として指定されたキャラクタはフィールドの途中に現れた場合もクォート開始とみなす。例えば'でクォート時 「a,b'c,d'e」 => 「a」および「bc,de」 </item>
    /// <item>クォート範囲中は2つ連続したクォート文字をクォート文字自体のキャラクタと解釈する。</item>
    /// </list>
    /// </remarks>
    /// <param name="self">対象テキスト</param>
    /// <param name="options">分割オプション</param>
    /// <returns>フィールド列のシーケンス</returns>
    public static IEnumerable<string[]> SplitFields(this string self, SplitFieldsOptions options)
    {
        // 定数
        const char Cr = '\x0D';
        const char Lf = '\x0A';

        // パラメータ検証
        ArgumentNullException.ThrowIfNull(options);
        if (options.Separator == Cr) throw new NotSupportedException("Invalid separator");
        if (options.Separator == Lf) throw new NotSupportedException("Invalid separator");
        if (options.Quote == Cr) throw new NotSupportedException("Invalid quotation");
        if (options.Quote == Lf) throw new NotSupportedException("Invalid quotation");
        if (options.Quote == options.Separator) throw new NotSupportedException("Invalid quotation");

        // 対象テキストが空の場合はすぐに終える。(空のシーケンスとなる)
        if (string.IsNullOrEmpty(self)) yield break;

        // 内部処理状態定義
        const int None = 0;         // 行頭状態
        const int Fielding = 1;     // 通常フィールドテキストの蓄積中
        const int Charging = 2;     // クォートされたフィールドの蓄積中
        const int Closing = 3;      // クォートの終了判定中

        // テキストの分割処理
        var collector = new FieldCollector();
        var hold = default(char);
        var state = None;
        foreach (var chr in self)
        {
            // どの場合もクォート中は特別処理が必要なのでまず処理してしまう。
            if (state == Charging)
            {
                if (chr == options.Quote)
                {
                    state = Closing;
                }
                else
                {
                    collector.Append(chr);
                }
                continue;
            }

            // キャラクター毎の処理
            if (chr == options.Separator)
            {
                // セパレータ
                // 蓄積済みのテキストをフィールドとして確立して、次のフィールド収集状態へ。
                collector.Fetch();
                state = Fielding;
            }
            else if (chr == Cr || chr == Lf)
            {
                // 改行
                if (state == None && hold == Cr && chr == Lf)
                {
                    // 行頭状態で CRに続くLF である場合はスキップ
                }
                else
                {
                    // フィールド収集中であれば空でも1つのフィールドとする。
                    if (state == Fielding) collector.Fetch();
                    // 行のフィールドを列挙
                    yield return collector.Consume();
                    // 改行コードをホールドして行頭状態へ
                    hold = chr;
                    state = None;
                }
            }
            else if (chr == options.Quote)
            {
                // クォートキャラクタ
                if (state == Closing && hold == options.Quote)
                {
                    // クォート終了判定中の場合、連続するクォートキャラクタはそれ自体を1文字としてクォート中に戻る
                    collector.Append(chr);
                    state = Charging;
                }
                else
                {
                    // クォート開始
                    hold = chr;
                    state = Charging;
                }
            }
            else
            {
                // その他。普通にフィールドとして蓄積
                collector.Append(chr);
                state = Fielding;
            }
        }

        // 末尾に達した時点でデータが残っていれば列挙
        if (collector.HasFragment || collector.HasFields)
        {
            yield return collector.Consume();
        }
    }
}