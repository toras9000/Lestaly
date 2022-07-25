using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lestaly;

/// <summary>
/// CSV(Charactor Separated Field)テキスト保存オプション
/// </summary>
public class SaveToCsvOptions : TypeColumnOptions
{
    /// <summary>セパレートキャラクタ</summary>
    public char Separator { get; set; } = ',';

    /// <summary>クォートキャラクタ</summary>
    public char Quote { get; set; } = '"';
}
