namespace Lestaly;

/// <summary>
/// string トークンに関する拡張メソッド
/// </summary>
public static class StringTokenExtensions
{
    /// <summary>文字列の最初の行を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">先頭の空行をトリムするか否か</param>
    /// <returns>最初の行文字列</returns>
    public static ReadOnlySpan<char> TakeLine(this string? self, bool trim = true)
        => self.AsSpan().TakeLine(trim);

    /// <summary>文字列の最初の行を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">先頭の空行をトリムするか否か</param>
    /// <returns>最初の行文字列</returns>
    public static ReadOnlySpan<char> TakeLine(this ReadOnlySpan<char> self, bool trim = true)
    {
        var body = self;

        // 先頭空行トリムが指定されていれば空行スキップ
        if (trim) body = self.TrimStart(LineBreakChars);

        // 空でない場合のみ取り出しを行う
        if (!body.IsEmpty)
        {
            // 最初の改行位置を検索
            var breakIdx = body.IndexOfAny(LineBreakChars);
            if (0 <= breakIdx)
            {
                // 改行前までの Span を取得
                body = body[..breakIdx];
            }
        }

        return body;
    }

    /// <summary>文字列の最後の行を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">末尾の空行をトリムするか否か</param>
    /// <returns>最後の行文字列</returns>
    public static ReadOnlySpan<char> TakeLastLine(this string? self, bool trim = true)
        => self.AsSpan().TakeLastLine(trim);

    /// <summary>文字列の最後の行を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">末尾の空行をトリムするか否か</param>
    /// <returns>最後の行文字列</returns>
    public static ReadOnlySpan<char> TakeLastLine(this ReadOnlySpan<char> self, bool trim = true)
    {
        var body = self;

        // 先頭空行トリムが指定されていれば空行スキップ
        if (trim) body = body.TrimEnd(LineBreakChars);

        // 空でない場合のみ取り出しを行う
        if (!body.IsEmpty)
        {
            // 最終改行位置を検索
            var breakIdx = body.LastIndexOfAny(LineBreakChars);
            if (-0 <= breakIdx)
            {
                // 改行後の Span を取得
                body = body[(breakIdx + 1)..];
            }
        }

        return body;
    }

    /// <summary>>文字列の最初の行をスキップしてその後方を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">空行をトリムするか否か</param>
    /// <returns>最初の行をスキップ後の文字列</returns>
    public static ReadOnlySpan<char> SkipLine(this string? self, bool trim = true)
        => self.AsSpan().SkipLine(trim);

    /// <summary>>文字列の最初の行をスキップしてその後方を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">空行をトリムするか否か</param>
    /// <returns>最初の行をスキップ後の文字列</returns>
    public static ReadOnlySpan<char> SkipLine(this ReadOnlySpan<char> self, bool trim = true)
    {
        var body = self;

        // 先頭空行トリムが指定されていれば空行スキップ
        if (trim) body = self.TrimStart(LineBreakChars);

        // 空でない場合のみ取り出しを行う
        if (!body.IsEmpty)
        {
            // 最初の改行位置を検索
            var breakIdx = body.IndexOfAny(LineBreakChars);
            if (breakIdx < 0)
            {
                body = body[body.Length..];
            }
            else
            {
                // 改行キャラクタ長の判別
                var breakLen = body[breakIdx..] is ['\r', '\n', ..] ? 2 : 1;

                // 改行の後ろの Span を取得
                body = body[(breakIdx + breakLen)..];

                // 先頭空行トリムをここにも適用。
                if (trim) body = body.TrimStart(LineBreakChars);
            }
        }

        return body;
    }

    /// <summary>>文字列の最初の行とその後方を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">空行をトリムするか否か</param>
    /// <param name="next">最初の行の後ろの文字列</param>
    /// <returns>最初の行文字列</returns>
    public static ReadOnlySpan<char> TakeSkipLine(this string? self, out ReadOnlySpan<char> next, bool trim = true)
        => self.AsSpan().TakeSkipLine(out next, trim);

    /// <summary>>文字列の最初の行とその後方を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">空行をトリムするか否か</param>
    /// <param name="next">最初の行の後ろの文字列</param>
    /// <returns>最初の行文字列</returns>
    public static ReadOnlySpan<char> TakeSkipLine(this ReadOnlySpan<char> self, out ReadOnlySpan<char> next, bool trim = true)
    {
        var body = self;

        // 先頭空行トリムが指定されていれば空行スキップ
        if (trim) body = self.TrimStart(LineBreakChars);

        // 空であるか
        if (body.IsEmpty)
        {
            // 後方文字列も同じにしておく
            next = body;
        }
        else
        {
            // 最初の改行位置を検索
            var breakIdx = body.IndexOfAny(LineBreakChars);
            if (breakIdx < 0)
            {
                next = body[body.Length..];
            }
            else
            {
                // 改行キャラクタ長の判別
                var breakLen = body[breakIdx..] is ['\r', '\n', ..] ? 2 : 1;

                // 改行の後ろの Span を取得
                next = body[(breakIdx + breakLen)..];

                // 先頭空行トリムをここにも適用。
                if (trim) next = next.TrimStart(LineBreakChars);

                // 改行前までの Span を取得
                body = body[..breakIdx];
            }
        }

        return body;
    }

    /// <summary>文字列の最初のトークン部分を取得する</summary>
    /// <remarks>連続する区切り文字は1まとまりとみなす。先頭に区切り文字がある場合はスキップする。空のトークンという概念はない。</remarks>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiter">区切り文字</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlySpan<char> TakeToken(this string self, char delimiter = ' ')
        => self.AsSpan().TakeToken(delimiter);

    /// <summary>文字列の最初のトークン部分を取得する</summary>
    /// <remarks>連続する区切り文字は1まとまりとみなす。先頭に区切り文字がある場合はスキップする。空のトークンという概念はない。</remarks>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiter">区切り文字</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlySpan<char> TakeToken(this ReadOnlySpan<char> self, char delimiter = ' ')
    {
        var token = self.TrimStart(delimiter);
        var idx = token.IndexOf(delimiter);
        if (idx <= 0) return token;
        return token[0..idx];
    }

    /// <summary>文字列の最初のトークン部分を取得する</summary>
    /// <remarks>連続する区切り文字は1まとまりとみなす。先頭に区切り文字がある場合はスキップする。空のトークンという概念はない。</remarks>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiters">区切り文字のセット</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlySpan<char> TakeTokenAny(this string self, ReadOnlySpan<char> delimiters)
        => self.AsSpan().TakeTokenAny(delimiters);

    /// <summary>文字列の最初のトークン部分を取得する</summary>
    /// <remarks>連続する区切り文字は1まとまりとみなす。先頭に区切り文字がある場合はスキップする。空のトークンという概念はない。</remarks>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiters">区切り文字のセット</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlySpan<char> TakeTokenAny(this ReadOnlySpan<char> self, ReadOnlySpan<char> delimiters)
    {
        var token = self.TrimStart(delimiters);
        var idx = token.IndexOfAny(delimiters);
        if (idx <= 0) return token;
        return token[0..idx];
    }

    /// <summary>文字列の最初のトークン部分をスキップした部分を取得する</summary>
    /// <remarks>連続する区切り文字は1まとまりとみなす。先頭に区切り文字がある場合はスキップする。空のトークンという概念はない。</remarks>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiter">区切り文字。</param>
    /// <returns>トークンをスキップした文字列</returns>
    public static ReadOnlySpan<char> SkipToken(this string self, char delimiter = ' ')
        => self.AsSpan().SkipToken(delimiter);

    /// <summary>文字列の最初のトークン部分をスキップした部分を取得する</summary>
    /// <remarks>連続する区切り文字は1まとまりとみなす。先頭に区切り文字がある場合はスキップする。空のトークンという概念はない。</remarks>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiter">区切り文字。</param>
    /// <returns>トークンをスキップした文字列</returns>
    public static ReadOnlySpan<char> SkipToken(this ReadOnlySpan<char> self, char delimiter = ' ')
    {
        var token = self.TrimStart(delimiter);
        var idx = token.IndexOf(delimiter);
        if (idx <= 0) return ReadOnlySpan<char>.Empty;
        return token[idx..].TrimStart(delimiter);
    }

    /// <summary>文字列の最初のトークン部分をスキップした部分を取得する</summary>
    /// <remarks>連続する区切り文字は1まとまりとみなす。先頭に区切り文字がある場合はスキップする。空のトークンという概念はない。</remarks>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiters">区切り文字のセット</param>
    /// <returns>トークンをスキップした文字列</returns>
    public static ReadOnlySpan<char> SkipTokenAny(this string self, ReadOnlySpan<char> delimiters)
        => self.AsSpan().SkipTokenAny(delimiters);

    /// <summary>文字列の最初のトークン部分をスキップした部分を取得する</summary>
    /// <remarks>連続する区切り文字は1まとまりとみなす。先頭に区切り文字がある場合はスキップする。空のトークンという概念はない。</remarks>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiters">区切り文字のセット</param>
    /// <returns>トークンをスキップした文字列</returns>
    public static ReadOnlySpan<char> SkipTokenAny(this ReadOnlySpan<char> self, ReadOnlySpan<char> delimiters)
    {
        var token = self.TrimStart(delimiters);
        var idx = token.IndexOfAny(delimiters);
        if (idx <= 0) return ReadOnlySpan<char>.Empty;
        return token[idx..].TrimStart(delimiters);
    }

    /// <summary>文字列の最初のトークン部分を取得する</summary>
    /// <remarks>連続する区切り文字は1まとまりとみなす。先頭に区切り文字がある場合はスキップする。空のトークンという概念はない。</remarks>
    /// <param name="self">対象文字列</param>
    /// <param name="next">トークンの次の部分</param>
    /// <param name="delimiter">区切り文字</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlySpan<char> TakeSkipToken(this string self, out ReadOnlySpan<char> next, char delimiter = ' ')
        => self.AsSpan().TakeSkipToken(out next, delimiter);

    /// <summary>文字列の最初のトークン部分を取得する</summary>
    /// <remarks>連続する区切り文字は1まとまりとみなす。先頭に区切り文字がある場合はスキップする。空のトークンという概念はない。</remarks>
    /// <param name="self">対象文字列</param>
    /// <param name="next">トークンの次の部分</param>
    /// <param name="delimiter">区切り文字</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlySpan<char> TakeSkipToken(this ReadOnlySpan<char> self, out ReadOnlySpan<char> next, char delimiter = ' ')
    {
        var origin = self.TrimStart(delimiter);
        var idx = origin.IndexOf(delimiter);
        var token = (idx <= 0) ? origin : origin[0..idx];
        next = origin[token.Length..].TrimStart(delimiter);
        return token;
    }

    /// <summary>文字列の最初のトークン部分を取得する</summary>
    /// <remarks>連続する区切り文字は1まとまりとみなす。先頭に区切り文字がある場合はスキップする。空のトークンという概念はない。</remarks>
    /// <param name="self">対象文字列</param>
    /// <param name="next">トークンの次の部分</param>
    /// <param name="delimiters">区切り文字のセット</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlySpan<char> TakeSkipTokenAny(this string self, out ReadOnlySpan<char> next, ReadOnlySpan<char> delimiters)
        => self.AsSpan().TakeSkipTokenAny(out next, delimiters);

    /// <summary>文字列の最初のトークン部分を取得する</summary>
    /// <remarks>連続する区切り文字は1まとまりとみなす。先頭に区切り文字がある場合はスキップする。空のトークンという概念はない。</remarks>
    /// <param name="self">対象文字列</param>
    /// <param name="next">トークンの次の部分</param>
    /// <param name="delimiters">区切り文字のセット</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlySpan<char> TakeSkipTokenAny(this ReadOnlySpan<char> self, out ReadOnlySpan<char> next, ReadOnlySpan<char> delimiters)
    {
        var origin = self.TrimStart(delimiters);
        var idx = origin.IndexOfAny(delimiters);
        var token = (idx <= 0) ? origin : origin[0..idx];
        next = origin[token.Length..].TrimStart(delimiters);
        return token;
    }

    /// <summary>改行キャラクタ配列</summary>
    private static readonly char[] LineBreakChars = new[] { '\r', '\n', };
}
