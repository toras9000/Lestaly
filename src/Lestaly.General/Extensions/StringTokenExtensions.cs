namespace Lestaly;

/// <summary>
/// string トークンに関する拡張メソッド
/// </summary>
public static class StringTokenExtensions
{
    #region TakeLine
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

    /// <summary>文字列の最初の行を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">先頭の空行をトリムするか否か</param>
    /// <returns>最初の行文字列</returns>
    public static ReadOnlyMemory<char> TakeLine(this ReadOnlyMemory<char> self, bool trim = true)
    {
        var body = self;

        // 先頭空行トリムが指定されていれば空行スキップ
        if (trim) body = self.TrimStart(LineBreakChars);

        // 空でない場合のみ取り出しを行う
        if (!body.IsEmpty)
        {
            // 最初の改行位置を検索
            var breakIdx = body.Span.IndexOfAny(LineBreakChars);
            if (0 <= breakIdx)
            {
                // 改行前までの Span を取得
                body = body[..breakIdx];
            }
        }

        return body;
    }
    #endregion

    #region TakeLastLine
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

    /// <summary>文字列の最後の行を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">末尾の空行をトリムするか否か</param>
    /// <returns>最後の行文字列</returns>
    public static ReadOnlyMemory<char> TakeLastLine(this ReadOnlyMemory<char> self, bool trim = true)
    {
        var body = self;

        // 先頭空行トリムが指定されていれば空行スキップ
        if (trim) body = body.TrimEnd(LineBreakChars);

        // 空でない場合のみ取り出しを行う
        if (!body.IsEmpty)
        {
            // 最終改行位置を検索
            var breakIdx = body.Span.LastIndexOfAny(LineBreakChars);
            if (-0 <= breakIdx)
            {
                // 改行後の Span を取得
                body = body[(breakIdx + 1)..];
            }
        }

        return body;
    }
    #endregion

    #region SkipLine
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

    /// <summary>>文字列の最初の行をスキップしてその後方を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">空行をトリムするか否か</param>
    /// <returns>最初の行をスキップ後の文字列</returns>
    public static ReadOnlyMemory<char> SkipLine(this ReadOnlyMemory<char> self, bool trim = true)
    {
        var body = self;

        // 先頭空行トリムが指定されていれば空行スキップ
        if (trim) body = self.TrimStart(LineBreakChars);

        // 空でない場合のみ取り出しを行う
        if (!body.IsEmpty)
        {
            // 最初の改行位置を検索
            var breakIdx = body.Span.IndexOfAny(LineBreakChars);
            if (breakIdx < 0)
            {
                body = body[body.Length..];
            }
            else
            {
                // 改行キャラクタ長の判別
                var breakLen = body.Span[breakIdx..] is ['\r', '\n', ..] ? 2 : 1;

                // 改行の後ろの Span を取得
                body = body[(breakIdx + breakLen)..];

                // 先頭空行トリムをここにも適用。
                if (trim) body = body.TrimStart(LineBreakChars);
            }
        }

        return body;
    }
    #endregion

    #region TakeSkipLine
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

    /// <summary>文字列の最初の行とその後方を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">空行をトリムするか否か</param>
    /// <param name="next">最初の行の後ろの文字列</param>
    /// <returns>最初の行文字列</returns>
    public static ReadOnlyMemory<char> TakeSkipLine(this ReadOnlyMemory<char> self, out ReadOnlyMemory<char> next, bool trim = true)
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
            var breakIdx = body.Span.IndexOfAny(LineBreakChars);
            if (breakIdx < 0)
            {
                next = body[body.Length..];
            }
            else
            {
                // 改行キャラクタ長の判別
                var breakLen = body.Span[breakIdx..] is ['\r', '\n', ..] ? 2 : 1;

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
    #endregion

    #region IncludeToken
    /// <summary>文字列に指定のトークンが含まれるかを判定する</summary>
    /// <param name="self">文字列</param>
    /// <param name="token">判定するトークン</param>
    /// <param name="delimiter">区切り文字</param>
    /// <param name="comparison">トークン文字列比較方法</param>
    /// <returns>含まれるか否か</returns>
    public static bool IncludeToken(this ReadOnlySpan<char> self, ReadOnlySpan<char> token, char delimiter = ' ', StringComparison comparison = StringComparison.OrdinalIgnoreCase)
    {
        var scan = self;
        while (!scan.IsEmpty)
        {
            var part = scan.TakeSkipToken(out scan, delimiter);
            if (part.Equals(token, comparison)) return true;
        }
        return false;
    }

    /// <summary>文字列に指定のトークンが含まれるかを判定する</summary>
    /// <param name="self">文字列</param>
    /// <param name="token">判定するトークン</param>
    /// <param name="delimiters">区切り文字のセット</param>
    /// <param name="comparison">トークン文字列比較方法</param>
    /// <returns>含まれるか否か</returns>
    public static bool IncludeToken(this ReadOnlySpan<char> self, ReadOnlySpan<char> token, ReadOnlySpan<char> delimiters, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
    {
        var scan = self;
        while (!scan.IsEmpty)
        {
            var part = scan.TakeSkipTokenAny(out scan, delimiters);
            if (part.Equals(token, comparison)) return true;
        }
        return false;
    }
    #endregion

    #region TakeToken
    /// <summary>文字列の最初のトークン部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiter">区切り文字</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlySpan<char> TakeToken(this string self, char delimiter = ' ')
        => self.AsSpan().TakeToken(delimiter);

    /// <summary>文字列の最初のトークン部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiter">区切り文字</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlySpan<char> TakeToken(this ReadOnlySpan<char> self, char delimiter = ' ')
    {
        var idx = self.IndexOf(delimiter);
        if (idx < 0) return self;
        return self[0..idx];
    }

    /// <summary>文字列の最初のトークン部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiter">区切り文字</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlyMemory<char> TakeToken(this ReadOnlyMemory<char> self, char delimiter = ' ')
    {
        var idx = self.Span.IndexOf(delimiter);
        if (idx < 0) return self;
        return self[0..idx];
    }
    #endregion

    #region TakeTokenAny
    /// <summary>文字列の最初のトークン部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiters">区切り文字のセット</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlySpan<char> TakeTokenAny(this string self, ReadOnlySpan<char> delimiters)
        => self.AsSpan().TakeTokenAny(delimiters);

    /// <summary>文字列の最初のトークン部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiters">区切り文字のセット</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlySpan<char> TakeTokenAny(this ReadOnlySpan<char> self, ReadOnlySpan<char> delimiters)
    {
        var idx = self.IndexOfAny(delimiters);
        if (idx < 0) return self;
        return self[0..idx];
    }

    /// <summary>文字列の最初のトークン部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiters">区切り文字のセット</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlyMemory<char> TakeTokenAny(this ReadOnlyMemory<char> self, ReadOnlySpan<char> delimiters)
    {
        var idx = self.Span.IndexOfAny(delimiters);
        if (idx < 0) return self;
        return self[0..idx];
    }
    #endregion

    #region SkipToken
    /// <summary>文字列の最初のトークン部分をスキップした部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiter">区切り文字。</param>
    /// <returns>トークンをスキップした文字列</returns>
    public static ReadOnlySpan<char> SkipToken(this string self, char delimiter = ' ')
        => self.AsSpan().SkipToken(delimiter);

    /// <summary>文字列の最初のトークン部分をスキップした部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiter">区切り文字。</param>
    /// <returns>トークンをスキップした文字列</returns>
    public static ReadOnlySpan<char> SkipToken(this ReadOnlySpan<char> self, char delimiter = ' ')
    {
        var idx = self.IndexOf(delimiter);
        if (idx < 0) return ReadOnlySpan<char>.Empty;
        return self[(idx + 1)..];
    }

    /// <summary>文字列の最初のトークン部分をスキップした部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiter">区切り文字。</param>
    /// <returns>トークンをスキップした文字列</returns>
    public static ReadOnlyMemory<char> SkipToken(this ReadOnlyMemory<char> self, char delimiter = ' ')
    {
        var idx = self.Span.IndexOf(delimiter);
        if (idx < 0) return ReadOnlyMemory<char>.Empty;
        return self[(idx + 1)..];
    }
    #endregion

    #region SkipTokenAny
    /// <summary>文字列の最初のトークン部分をスキップした部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiters">区切り文字のセット</param>
    /// <returns>トークンをスキップした文字列</returns>
    public static ReadOnlySpan<char> SkipTokenAny(this string self, ReadOnlySpan<char> delimiters)
        => self.AsSpan().SkipTokenAny(delimiters);

    /// <summary>文字列の最初のトークン部分をスキップした部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiters">区切り文字のセット</param>
    /// <returns>トークンをスキップした文字列</returns>
    public static ReadOnlySpan<char> SkipTokenAny(this ReadOnlySpan<char> self, ReadOnlySpan<char> delimiters)
    {
        var idx = self.IndexOfAny(delimiters);
        if (idx < 0) return ReadOnlySpan<char>.Empty;
        return self[(idx + 1)..];
    }

    /// <summary>文字列の最初のトークン部分をスキップした部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiters">区切り文字のセット</param>
    /// <returns>トークンをスキップした文字列</returns>
    public static ReadOnlyMemory<char> SkipTokenAny(this ReadOnlyMemory<char> self, ReadOnlySpan<char> delimiters)
    {
        var idx = self.Span.IndexOfAny(delimiters);
        if (idx < 0) return ReadOnlyMemory<char>.Empty;
        return self[(idx + 1)..];
    }
    #endregion

    #region TakeSkipToken
    /// <summary>文字列の最初のトークン部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="next">トークンの次の部分</param>
    /// <param name="delimiter">区切り文字</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlySpan<char> TakeSkipToken(this string self, out ReadOnlySpan<char> next, char delimiter = ' ')
        => self.AsSpan().TakeSkipToken(out next, delimiter);

    /// <summary>文字列の最初のトークン部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="next">トークンの次の部分</param>
    /// <param name="delimiter">区切り文字</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlySpan<char> TakeSkipToken(this ReadOnlySpan<char> self, out ReadOnlySpan<char> next, char delimiter = ' ')
    {
        var idx = self.IndexOf(delimiter);
        if (idx < 0)
        {
            next = self[self.Length..];
            return self;
        }
        next = self[(idx + 1)..];
        return self[..idx];
    }

    /// <summary>文字列の最初のトークン部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="next">トークンの次の部分</param>
    /// <param name="delimiter">区切り文字</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlyMemory<char> TakeSkipToken(this ReadOnlyMemory<char> self, out ReadOnlyMemory<char> next, char delimiter = ' ')
    {
        var idx = self.Span.IndexOf(delimiter);
        if (idx < 0)
        {
            next = self[self.Length..];
            return self;
        }
        next = self[(idx + 1)..];
        return self[..idx];
    }
    #endregion

    #region TakeSkipTokenAny
    /// <summary>文字列の最初のトークン部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="next">トークンの次の部分</param>
    /// <param name="delimiters">区切り文字のセット</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlySpan<char> TakeSkipTokenAny(this string self, out ReadOnlySpan<char> next, ReadOnlySpan<char> delimiters)
        => self.AsSpan().TakeSkipTokenAny(out next, delimiters);

    /// <summary>文字列の最初のトークン部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="next">トークンの次の部分</param>
    /// <param name="delimiters">区切り文字のセット</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlySpan<char> TakeSkipTokenAny(this ReadOnlySpan<char> self, out ReadOnlySpan<char> next, ReadOnlySpan<char> delimiters)
    {
        var idx = self.IndexOfAny(delimiters);
        if (idx < 0)
        {
            next = self[self.Length..];
            return self;
        }
        next = self[(idx + 1)..];
        return self[..idx];
    }

    /// <summary>文字列の最初のトークン部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="next">トークンの次の部分</param>
    /// <param name="delimiters">区切り文字のセット</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlyMemory<char> TakeSkipTokenAny(this ReadOnlyMemory<char> self, out ReadOnlyMemory<char> next, ReadOnlySpan<char> delimiters)
    {
        var idx = self.Span.IndexOfAny(delimiters);
        if (idx < 0)
        {
            next = self[self.Length..];
            return self;
        }
        next = self[(idx + 1)..];
        return self[..idx];
    }
    #endregion

    #region TakeLastToken
    /// <summary>文字列の最後のトークン部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiter">区切り文字</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlySpan<char> TakeLastToken(this string self, char delimiter = ' ')
        => self.AsSpan().TakeLastToken(delimiter);

    /// <summary>文字列の最後のトークン部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiter">区切り文字</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlySpan<char> TakeLastToken(this ReadOnlySpan<char> self, char delimiter = ' ')
    {
        var idx = self.LastIndexOf(delimiter);
        if (idx <= 0) return self;
        return self[(idx + 1)..];
    }

    /// <summary>文字列の最後のトークン部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiter">区切り文字</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlyMemory<char> TakeLastToken(this ReadOnlyMemory<char> self, char delimiter = ' ')
    {
        var idx = self.Span.LastIndexOf(delimiter);
        if (idx <= 0) return self;
        return self[(idx + 1)..];
    }
    #endregion

    #region TakeLastTokenAny
    /// <summary>文字列の最後のトークン部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiters">区切り文字のセット</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlySpan<char> TakeLastTokenAny(this string self, ReadOnlySpan<char> delimiters)
        => self.AsSpan().TakeLastTokenAny(delimiters);

    /// <summary>文字列の最後のトークン部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiters">区切り文字のセット</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlySpan<char> TakeLastTokenAny(this ReadOnlySpan<char> self, ReadOnlySpan<char> delimiters)
    {
        var idx = self.LastIndexOfAny(delimiters);
        if (idx <= 0) return self;
        return self[(idx + 1)..];
    }

    /// <summary>文字列の最後のトークン部分を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiters">区切り文字のセット</param>
    /// <returns>トークン文字列</returns>
    public static ReadOnlyMemory<char> TakeLastTokenAny(this ReadOnlyMemory<char> self, ReadOnlySpan<char> delimiters)
    {
        var idx = self.Span.LastIndexOfAny(delimiters);
        if (idx <= 0) return self;
        return self[(idx + 1)..];
    }
    #endregion

    /// <summary>改行キャラクタ配列</summary>
    private static readonly char[] LineBreakChars = ['\r', '\n',];

}
