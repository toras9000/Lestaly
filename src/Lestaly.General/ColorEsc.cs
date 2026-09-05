using System.ComponentModel;

namespace Lestaly;

/// <summary>色関係のエスケープシーケンス定数</summary>
public static class ColorEsc
{
    /// <summary>ANSIエスケープシーケンス前景色定数</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static ForegroundColor Foreground { get; } = new ForegroundColor();

    /// <summary>ANSIエスケープシーケンス前景色定数</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static ForegroundColor Fore => Foreground;

    /// <summary>ANSIエスケープシーケンス前景色定数</summary>
    public static ForegroundColor FG => Foreground;

    /// <summary>ANSIエスケープシーケンス前景色定数</summary>
    public static ExForegroundColor FGx { get; } = new ExForegroundColor();

    /// <summary>ANSIエスケープシーケンス背景色定数</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static BackgroundColor Background { get; } = new BackgroundColor();

    /// <summary>ANSIエスケープシーケンス背景色定数</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static BackgroundColor Back => Background;

    /// <summary>ANSIエスケープシーケンス背景色定数</summary>
    public static BackgroundColor BG => Background;

    /// <summary>ANSIエスケープシーケンス背景色定数</summary>
    public static ExBackgroundColor BGx { get; } = new ExBackgroundColor();


    /// <summary>ANSIエスケープシーケンス前景色定数</summary>
    public class ForegroundColor
    {
        /// <summary>デフォルト</summary>
        public string Default { get; } = "\e[39m";

        /// <summary>黒</summary>
        public string Black { get; } = "\e[30m";

        /// <summary>赤</summary>
        public string DarkRed { get; } = "\e[31m";

        /// <summary>緑</summary>
        public string DarkGreen { get; } = "\e[32m";

        /// <summary>黄</summary>
        public string DarkYellow { get; } = "\e[33m";

        /// <summary>青</summary>
        public string DarkBlue { get; } = "\e[34m";

        /// <summary>マゼンタ</summary>
        public string DarkMagenta { get; } = "\e[35m";

        /// <summary>シアン</summary>
        public string DarkCyan { get; } = "\e[36m";

        /// <summary>ライトグレー</summary>
        public string LightGray { get; } = "\e[37m";

        /// <summary>ダークグレー</summary>
        public string DarkGray { get; } = "\e[90m";

        /// <summary>赤</summary>
        public string Red { get; } = "\e[91m";

        /// <summary>緑</summary>
        public string Green { get; } = "\e[92m";

        /// <summary>黄</summary>
        public string Yellow { get; } = "\e[93m";

        /// <summary>青</summary>
        public string Blue { get; } = "\e[94m";

        /// <summary>マゼンタ</summary>
        public string Magenta { get; } = "\e[95m";

        /// <summary>シアン</summary>
        public string Cyan { get; } = "\e[96m";

        /// <summary>白</summary>
        public string White { get; } = "\e[97m";

        /// <summary>RGB</summary>
        /// <param name="r">R</param>
        /// <param name="g">G</param>
        /// <param name="b">B</param>
        /// <returns>RGBカラーエスケープコード</returns>
        public string RGB(byte r, byte g, byte b) => $"\e[38;2;{r};{g};{b}m";

        /// <summary>カラーパレット</summary>
        /// <param name="number">パレット番号</param>
        /// <returns>パレットカラーエスケープコード</returns>
        public string Palette(byte number) => $"\e[38;5;{number}m";

        /// <summary>内部専用コンストラクタ</summary>
        internal ForegroundColor() { }
    }

    /// <summary>ANSIエスケープシーケンス前景色定数</summary>
    public class ExForegroundColor
    {
        /// <summary>AliceBlue (#F0F8FF)</summary>
        public string AliceBlue = $"\e[38;2;240;248;255m";

        /// <summary>AntiqueWhite (#FAEBD7)</summary>
        public string AntiqueWhite = $"\e[38;2;250;235;215m";

        /// <summary>Aqua (#00FFFF)</summary>
        public string Aqua = $"\e[38;2;0;255;255m";

        /// <summary>Aquamarine (#7FFFD4)</summary>
        public string Aquamarine = $"\e[38;2;127;255;212m";

        /// <summary>Azure (#F0FFFF)</summary>
        public string Azure = $"\e[38;2;240;255;255m";

        /// <summary>Beige (#F5F5DC)</summary>
        public string Beige = $"\e[38;2;245;245;220m";

        /// <summary>Bisque (#FFE4C4)</summary>
        public string Bisque = $"\e[38;2;255;228;196m";

        /// <summary>Black (#000000)</summary>
        public string Black = $"\e[38;2;0;0;0m";

        /// <summary>BlanchedAlmond (#FFEBCD)</summary>
        public string BlanchedAlmond = $"\e[38;2;255;235;205m";

        /// <summary>Blue (#0000FF)</summary>
        public string Blue = $"\e[38;2;0;0;255m";

        /// <summary>BlueViolet (#8A2BE2)</summary>
        public string BlueViolet = $"\e[38;2;138;43;226m";

        /// <summary>Brown (#A52A2A)</summary>
        public string Brown = $"\e[38;2;165;42;42m";

        /// <summary>BurlyWood (#DEB887)</summary>
        public string BurlyWood = $"\e[38;2;222;184;135m";

        /// <summary>CadetBlue (#5F9EA0)</summary>
        public string CadetBlue = $"\e[38;2;95;158;160m";

        /// <summary>Chartreuse (#7FFF00)</summary>
        public string Chartreuse = $"\e[38;2;127;255;0m";

        /// <summary>Chocolate (#D2691E)</summary>
        public string Chocolate = $"\e[38;2;210;105;30m";

        /// <summary>Coral (#FF7F50)</summary>
        public string Coral = $"\e[38;2;255;127;80m";

        /// <summary>CornflowerBlue (#6495ED)</summary>
        public string CornflowerBlue = $"\e[38;2;100;149;237m";

        /// <summary>Cornsilk (#FFF8DC)</summary>
        public string Cornsilk = $"\e[38;2;255;248;220m";

        /// <summary>Crimson (#DC143C)</summary>
        public string Crimson = $"\e[38;2;220;20;60m";

        /// <summary>Cyan (#00FFFF)</summary>
        public string Cyan = $"\e[38;2;0;255;255m";

        /// <summary>DarkBlue (#00008B)</summary>
        public string DarkBlue = $"\e[38;2;0;0;139m";

        /// <summary>DarkCyan (#008B8B)</summary>
        public string DarkCyan = $"\e[38;2;0;139;139m";

        /// <summary>DarkGoldenrod (#B8860B)</summary>
        public string DarkGoldenrod = $"\e[38;2;184;134;11m";

        /// <summary>DarkGray (#A9A9A9)</summary>
        public string DarkGray = $"\e[38;2;169;169;169m";

        /// <summary>DarkGreen (#006400)</summary>
        public string DarkGreen = $"\e[38;2;0;100;0m";

        /// <summary>DarkKhaki (#BDB76B)</summary>
        public string DarkKhaki = $"\e[38;2;189;183;107m";

        /// <summary>DarkMagenta (#8B008B)</summary>
        public string DarkMagenta = $"\e[38;2;139;0;139m";

        /// <summary>DarkOliveGreen (#556B2F)</summary>
        public string DarkOliveGreen = $"\e[38;2;85;107;47m";

        /// <summary>DarkOrange (#FF8C00)</summary>
        public string DarkOrange = $"\e[38;2;255;140;0m";

        /// <summary>DarkOrchid (#9932CC)</summary>
        public string DarkOrchid = $"\e[38;2;153;50;204m";

        /// <summary>DarkRed (#8B0000)</summary>
        public string DarkRed = $"\e[38;2;139;0;0m";

        /// <summary>DarkSalmon (#E9967A)</summary>
        public string DarkSalmon = $"\e[38;2;233;150;122m";

        /// <summary>DarkSeaGreen (#8FBC8F)</summary>
        public string DarkSeaGreen = $"\e[38;2;143;188;143m";

        /// <summary>DarkSlateBlue (#483D8B)</summary>
        public string DarkSlateBlue = $"\e[38;2;72;61;139m";

        /// <summary>DarkSlateGray (#2F4F4F)</summary>
        public string DarkSlateGray = $"\e[38;2;47;79;79m";

        /// <summary>DarkTurquoise (#00CED1)</summary>
        public string DarkTurquoise = $"\e[38;2;0;206;209m";

        /// <summary>DarkViolet (#9400D3)</summary>
        public string DarkViolet = $"\e[38;2;148;0;211m";

        /// <summary>DeepPink (#FF1493)</summary>
        public string DeepPink = $"\e[38;2;255;20;147m";

        /// <summary>DeepSkyBlue (#00BFFF)</summary>
        public string DeepSkyBlue = $"\e[38;2;0;191;255m";

        /// <summary>DimGray (#696969)</summary>
        public string DimGray = $"\e[38;2;105;105;105m";

        /// <summary>DodgerBlue (#1E90FF)</summary>
        public string DodgerBlue = $"\e[38;2;30;144;255m";

        /// <summary>Firebrick (#B22222)</summary>
        public string Firebrick = $"\e[38;2;178;34;34m";

        /// <summary>FloralWhite (#FFFAF0)</summary>
        public string FloralWhite = $"\e[38;2;255;250;240m";

        /// <summary>ForestGreen (#228B22)</summary>
        public string ForestGreen = $"\e[38;2;34;139;34m";

        /// <summary>Fuchsia (#FF00FF)</summary>
        public string Fuchsia = $"\e[38;2;255;0;255m";

        /// <summary>Gainsboro (#DCDCDC)</summary>
        public string Gainsboro = $"\e[38;2;220;220;220m";

        /// <summary>GhostWhite (#F8F8FF)</summary>
        public string GhostWhite = $"\e[38;2;248;248;255m";

        /// <summary>Gold (#FFD700)</summary>
        public string Gold = $"\e[38;2;255;215;0m";

        /// <summary>Goldenrod (#DAA520)</summary>
        public string Goldenrod = $"\e[38;2;218;165;32m";

        /// <summary>Gray (#808080)</summary>
        public string Gray = $"\e[38;2;128;128;128m";

        /// <summary>Green (#008000)</summary>
        public string Green = $"\e[38;2;0;128;0m";

        /// <summary>GreenYellow (#ADFF2F)</summary>
        public string GreenYellow = $"\e[38;2;173;255;47m";

        /// <summary>Honeydew (#F0FFF0)</summary>
        public string Honeydew = $"\e[38;2;240;255;240m";

        /// <summary>HotPink (#FF69B4)</summary>
        public string HotPink = $"\e[38;2;255;105;180m";

        /// <summary>IndianRed (#CD5C5C)</summary>
        public string IndianRed = $"\e[38;2;205;92;92m";

        /// <summary>Indigo (#4B0082)</summary>
        public string Indigo = $"\e[38;2;75;0;130m";

        /// <summary>Ivory (#FFFFF0)</summary>
        public string Ivory = $"\e[38;2;255;255;240m";

        /// <summary>Khaki (#F0E68C)</summary>
        public string Khaki = $"\e[38;2;240;230;140m";

        /// <summary>Lavender (#E6E6FA)</summary>
        public string Lavender = $"\e[38;2;230;230;250m";

        /// <summary>LavenderBlush (#FFF0F5)</summary>
        public string LavenderBlush = $"\e[38;2;255;240;245m";

        /// <summary>LawnGreen (#7CFC00)</summary>
        public string LawnGreen = $"\e[38;2;124;252;0m";

        /// <summary>LemonChiffon (#FFFACD)</summary>
        public string LemonChiffon = $"\e[38;2;255;250;205m";

        /// <summary>LightBlue (#ADD8E6)</summary>
        public string LightBlue = $"\e[38;2;173;216;230m";

        /// <summary>LightCoral (#F08080)</summary>
        public string LightCoral = $"\e[38;2;240;128;128m";

        /// <summary>LightCyan (#E0FFFF)</summary>
        public string LightCyan = $"\e[38;2;224;255;255m";

        /// <summary>LightGoldenrodYellow (#FAFAD2)</summary>
        public string LightGoldenrodYellow = $"\e[38;2;250;250;210m";

        /// <summary>LightGreen (#90EE90)</summary>
        public string LightGreen = $"\e[38;2;144;238;144m";

        /// <summary>LightGray (#D3D3D3)</summary>
        public string LightGray = $"\e[38;2;211;211;211m";

        /// <summary>LightPink (#FFB6C1)</summary>
        public string LightPink = $"\e[38;2;255;182;193m";

        /// <summary>LightSalmon (#FFA07A)</summary>
        public string LightSalmon = $"\e[38;2;255;160;122m";

        /// <summary>LightSeaGreen (#20B2AA)</summary>
        public string LightSeaGreen = $"\e[38;2;32;178;170m";

        /// <summary>LightSkyBlue (#87CEFA)</summary>
        public string LightSkyBlue = $"\e[38;2;135;206;250m";

        /// <summary>LightSlateGray (#778899)</summary>
        public string LightSlateGray = $"\e[38;2;119;136;153m";

        /// <summary>LightSteelBlue (#B0C4DE)</summary>
        public string LightSteelBlue = $"\e[38;2;176;196;222m";

        /// <summary>LightYellow (#FFFFE0)</summary>
        public string LightYellow = $"\e[38;2;255;255;224m";

        /// <summary>Lime (#00FF00)</summary>
        public string Lime = $"\e[38;2;0;255;0m";

        /// <summary>LimeGreen (#32CD32)</summary>
        public string LimeGreen = $"\e[38;2;50;205;50m";

        /// <summary>Linen (#FAF0E6)</summary>
        public string Linen = $"\e[38;2;250;240;230m";

        /// <summary>Magenta (#FF00FF)</summary>
        public string Magenta = $"\e[38;2;255;0;255m";

        /// <summary>Maroon (#800000)</summary>
        public string Maroon = $"\e[38;2;128;0;0m";

        /// <summary>MediumAquamarine (#66CDAA)</summary>
        public string MediumAquamarine = $"\e[38;2;102;205;170m";

        /// <summary>MediumBlue (#0000CD)</summary>
        public string MediumBlue = $"\e[38;2;0;0;205m";

        /// <summary>MediumOrchid (#BA55D3)</summary>
        public string MediumOrchid = $"\e[38;2;186;85;211m";

        /// <summary>MediumPurple (#9370DB)</summary>
        public string MediumPurple = $"\e[38;2;147;112;219m";

        /// <summary>MediumSeaGreen (#3CB371)</summary>
        public string MediumSeaGreen = $"\e[38;2;60;179;113m";

        /// <summary>MediumSlateBlue (#7B68EE)</summary>
        public string MediumSlateBlue = $"\e[38;2;123;104;238m";

        /// <summary>MediumSpringGreen (#00FA9A)</summary>
        public string MediumSpringGreen = $"\e[38;2;0;250;154m";

        /// <summary>MediumTurquoise (#48D1CC)</summary>
        public string MediumTurquoise = $"\e[38;2;72;209;204m";

        /// <summary>MediumVioletRed (#C71585)</summary>
        public string MediumVioletRed = $"\e[38;2;199;21;133m";

        /// <summary>MidnightBlue (#191970)</summary>
        public string MidnightBlue = $"\e[38;2;25;25;112m";

        /// <summary>MintCream (#F5FFFA)</summary>
        public string MintCream = $"\e[38;2;245;255;250m";

        /// <summary>MistyRose (#FFE4E1)</summary>
        public string MistyRose = $"\e[38;2;255;228;225m";

        /// <summary>Moccasin (#FFE4B5)</summary>
        public string Moccasin = $"\e[38;2;255;228;181m";

        /// <summary>NavajoWhite (#FFDEAD)</summary>
        public string NavajoWhite = $"\e[38;2;255;222;173m";

        /// <summary>Navy (#000080)</summary>
        public string Navy = $"\e[38;2;0;0;128m";

        /// <summary>OldLace (#FDF5E6)</summary>
        public string OldLace = $"\e[38;2;253;245;230m";

        /// <summary>Olive (#808000)</summary>
        public string Olive = $"\e[38;2;128;128;0m";

        /// <summary>OliveDrab (#6B8E23)</summary>
        public string OliveDrab = $"\e[38;2;107;142;35m";

        /// <summary>Orange (#FFA500)</summary>
        public string Orange = $"\e[38;2;255;165;0m";

        /// <summary>OrangeRed (#FF4500)</summary>
        public string OrangeRed = $"\e[38;2;255;69;0m";

        /// <summary>Orchid (#DA70D6)</summary>
        public string Orchid = $"\e[38;2;218;112;214m";

        /// <summary>PaleGoldenrod (#EEE8AA)</summary>
        public string PaleGoldenrod = $"\e[38;2;238;232;170m";

        /// <summary>PaleGreen (#98FB98)</summary>
        public string PaleGreen = $"\e[38;2;152;251;152m";

        /// <summary>PaleTurquoise (#AFEEEE)</summary>
        public string PaleTurquoise = $"\e[38;2;175;238;238m";

        /// <summary>PaleVioletRed (#DB7093)</summary>
        public string PaleVioletRed = $"\e[38;2;219;112;147m";

        /// <summary>PapayaWhip (#FFEFD5)</summary>
        public string PapayaWhip = $"\e[38;2;255;239;213m";

        /// <summary>PeachPuff (#FFDAB9)</summary>
        public string PeachPuff = $"\e[38;2;255;218;185m";

        /// <summary>Peru (#CD853F)</summary>
        public string Peru = $"\e[38;2;205;133;63m";

        /// <summary>Pink (#FFC0CB)</summary>
        public string Pink = $"\e[38;2;255;192;203m";

        /// <summary>Plum (#DDA0DD)</summary>
        public string Plum = $"\e[38;2;221;160;221m";

        /// <summary>PowderBlue (#B0E0E6)</summary>
        public string PowderBlue = $"\e[38;2;176;224;230m";

        /// <summary>Purple (#800080)</summary>
        public string Purple = $"\e[38;2;128;0;128m";

        /// <summary>RebeccaPurple (#663399)</summary>
        public string RebeccaPurple = $"\e[38;2;102;51;153m";

        /// <summary>Red (#FF0000)</summary>
        public string Red = $"\e[38;2;255;0;0m";

        /// <summary>RosyBrown (#BC8F8F)</summary>
        public string RosyBrown = $"\e[38;2;188;143;143m";

        /// <summary>RoyalBlue (#4169E1)</summary>
        public string RoyalBlue = $"\e[38;2;65;105;225m";

        /// <summary>SaddleBrown (#8B4513)</summary>
        public string SaddleBrown = $"\e[38;2;139;69;19m";

        /// <summary>Salmon (#FA8072)</summary>
        public string Salmon = $"\e[38;2;250;128;114m";

        /// <summary>SandyBrown (#F4A460)</summary>
        public string SandyBrown = $"\e[38;2;244;164;96m";

        /// <summary>SeaGreen (#2E8B57)</summary>
        public string SeaGreen = $"\e[38;2;46;139;87m";

        /// <summary>SeaShell (#FFF5EE)</summary>
        public string SeaShell = $"\e[38;2;255;245;238m";

        /// <summary>Sienna (#A0522D)</summary>
        public string Sienna = $"\e[38;2;160;82;45m";

        /// <summary>Silver (#C0C0C0)</summary>
        public string Silver = $"\e[38;2;192;192;192m";

        /// <summary>SkyBlue (#87CEEB)</summary>
        public string SkyBlue = $"\e[38;2;135;206;235m";

        /// <summary>SlateBlue (#6A5ACD)</summary>
        public string SlateBlue = $"\e[38;2;106;90;205m";

        /// <summary>SlateGray (#708090)</summary>
        public string SlateGray = $"\e[38;2;112;128;144m";

        /// <summary>Snow (#FFFAFA)</summary>
        public string Snow = $"\e[38;2;255;250;250m";

        /// <summary>SpringGreen (#00FF7F)</summary>
        public string SpringGreen = $"\e[38;2;0;255;127m";

        /// <summary>SteelBlue (#4682B4)</summary>
        public string SteelBlue = $"\e[38;2;70;130;180m";

        /// <summary>Tan (#D2B48C)</summary>
        public string Tan = $"\e[38;2;210;180;140m";

        /// <summary>Teal (#008080)</summary>
        public string Teal = $"\e[38;2;0;128;128m";

        /// <summary>Thistle (#D8BFD8)</summary>
        public string Thistle = $"\e[38;2;216;191;216m";

        /// <summary>Tomato (#FF6347)</summary>
        public string Tomato = $"\e[38;2;255;99;71m";

        /// <summary>Turquoise (#40E0D0)</summary>
        public string Turquoise = $"\e[38;2;64;224;208m";

        /// <summary>Violet (#EE82EE)</summary>
        public string Violet = $"\e[38;2;238;130;238m";

        /// <summary>Wheat (#F5DEB3)</summary>
        public string Wheat = $"\e[38;2;245;222;179m";

        /// <summary>White (#FFFFFF)</summary>
        public string White = $"\e[38;2;255;255;255m";

        /// <summary>WhiteSmoke (#F5F5F5)</summary>
        public string WhiteSmoke = $"\e[38;2;245;245;245m";

        /// <summary>Yellow (#FFFF00)</summary>
        public string Yellow = $"\e[38;2;255;255;0m";

        /// <summary>YellowGreen (#9ACD32)</summary>
        public string YellowGreen = $"\e[38;2;154;205;50m";

        /// <summary>内部専用コンストラクタ</summary>
        internal ExForegroundColor() { }
    }

    /// <summary>ANSIエスケープシーケンス背景色定数</summary>
    public class BackgroundColor
    {
        /// <summary>デフォルト</summary>
        public string Default { get; } = "\e[49m";

        /// <summary>黒</summary>
        public string Black { get; } = "\e[40m";

        /// <summary>赤</summary>
        public string DarkRed { get; } = "\e[41m";

        /// <summary>緑</summary>
        public string DarkGreen { get; } = "\e[42m";

        /// <summary>黄</summary>
        public string DarkYellow { get; } = "\e[43m";

        /// <summary>青</summary>
        public string DarkBlue { get; } = "\e[44m";

        /// <summary>マゼンタ</summary>
        public string DarkMagenta { get; } = "\e[45m";

        /// <summary>シアン</summary>
        public string DarkCyan { get; } = "\e[46m";

        /// <summary>ライトグレー</summary>
        public string LightGray { get; } = "\e[47m";

        /// <summary>ダークグレー</summary>
        public string DarkGray { get; } = "\e[100m";

        /// <summary>赤</summary>
        public string Red { get; } = "\e[101m";

        /// <summary>緑</summary>
        public string Green { get; } = "\e[102m";

        /// <summary>黄</summary>
        public string Yellow { get; } = "\e[103m";

        /// <summary>青</summary>
        public string Blue { get; } = "\e[104m";

        /// <summary>マゼンタ</summary>
        public string Magenta { get; } = "\e[105m";

        /// <summary>シアン</summary>
        public string Cyan { get; } = "\e[106m";

        /// <summary>白</summary>
        public string White { get; } = "\e[107m";

        /// <summary>RGB</summary>
        /// <param name="r">R</param>
        /// <param name="g">G</param>
        /// <param name="b">B</param>
        /// <returns>RGBカラーエスケープコード</returns>
        public string RGB(byte r, byte g, byte b) => $"\e[48;2;{r};{g};{b}m";

        /// <summary>カラーパレット</summary>
        /// <param name="number">パレット番号</param>
        /// <returns>パレットカラーエスケープコード</returns>
        public string Palette(byte number) => $"\e[48;5;{number}m";


        /// <summary>内部専用コンストラクタ</summary>
        internal BackgroundColor() { }
    }

    /// <summary>ANSIエスケープシーケンス背景色定数</summary>
    public class ExBackgroundColor
    {
        /// <summary>AliceBlue (#F0F8FF)</summary>
        public string AliceBlue = $"\e[48;2;240;248;255m";

        /// <summary>AntiqueWhite (#FAEBD7)</summary>
        public string AntiqueWhite = $"\e[48;2;250;235;215m";

        /// <summary>Aqua (#00FFFF)</summary>
        public string Aqua = $"\e[48;2;0;255;255m";

        /// <summary>Aquamarine (#7FFFD4)</summary>
        public string Aquamarine = $"\e[48;2;127;255;212m";

        /// <summary>Azure (#F0FFFF)</summary>
        public string Azure = $"\e[48;2;240;255;255m";

        /// <summary>Beige (#F5F5DC)</summary>
        public string Beige = $"\e[48;2;245;245;220m";

        /// <summary>Bisque (#FFE4C4)</summary>
        public string Bisque = $"\e[48;2;255;228;196m";

        /// <summary>Black (#000000)</summary>
        public string Black = $"\e[48;2;0;0;0m";

        /// <summary>BlanchedAlmond (#FFEBCD)</summary>
        public string BlanchedAlmond = $"\e[48;2;255;235;205m";

        /// <summary>Blue (#0000FF)</summary>
        public string Blue = $"\e[48;2;0;0;255m";

        /// <summary>BlueViolet (#8A2BE2)</summary>
        public string BlueViolet = $"\e[48;2;138;43;226m";

        /// <summary>Brown (#A52A2A)</summary>
        public string Brown = $"\e[48;2;165;42;42m";

        /// <summary>BurlyWood (#DEB887)</summary>
        public string BurlyWood = $"\e[48;2;222;184;135m";

        /// <summary>CadetBlue (#5F9EA0)</summary>
        public string CadetBlue = $"\e[48;2;95;158;160m";

        /// <summary>Chartreuse (#7FFF00)</summary>
        public string Chartreuse = $"\e[48;2;127;255;0m";

        /// <summary>Chocolate (#D2691E)</summary>
        public string Chocolate = $"\e[48;2;210;105;30m";

        /// <summary>Coral (#FF7F50)</summary>
        public string Coral = $"\e[48;2;255;127;80m";

        /// <summary>CornflowerBlue (#6495ED)</summary>
        public string CornflowerBlue = $"\e[48;2;100;149;237m";

        /// <summary>Cornsilk (#FFF8DC)</summary>
        public string Cornsilk = $"\e[48;2;255;248;220m";

        /// <summary>Crimson (#DC143C)</summary>
        public string Crimson = $"\e[48;2;220;20;60m";

        /// <summary>Cyan (#00FFFF)</summary>
        public string Cyan = $"\e[48;2;0;255;255m";

        /// <summary>DarkBlue (#00008B)</summary>
        public string DarkBlue = $"\e[48;2;0;0;139m";

        /// <summary>DarkCyan (#008B8B)</summary>
        public string DarkCyan = $"\e[48;2;0;139;139m";

        /// <summary>DarkGoldenrod (#B8860B)</summary>
        public string DarkGoldenrod = $"\e[48;2;184;134;11m";

        /// <summary>DarkGray (#A9A9A9)</summary>
        public string DarkGray = $"\e[48;2;169;169;169m";

        /// <summary>DarkGreen (#006400)</summary>
        public string DarkGreen = $"\e[48;2;0;100;0m";

        /// <summary>DarkKhaki (#BDB76B)</summary>
        public string DarkKhaki = $"\e[48;2;189;183;107m";

        /// <summary>DarkMagenta (#8B008B)</summary>
        public string DarkMagenta = $"\e[48;2;139;0;139m";

        /// <summary>DarkOliveGreen (#556B2F)</summary>
        public string DarkOliveGreen = $"\e[48;2;85;107;47m";

        /// <summary>DarkOrange (#FF8C00)</summary>
        public string DarkOrange = $"\e[48;2;255;140;0m";

        /// <summary>DarkOrchid (#9932CC)</summary>
        public string DarkOrchid = $"\e[48;2;153;50;204m";

        /// <summary>DarkRed (#8B0000)</summary>
        public string DarkRed = $"\e[48;2;139;0;0m";

        /// <summary>DarkSalmon (#E9967A)</summary>
        public string DarkSalmon = $"\e[48;2;233;150;122m";

        /// <summary>DarkSeaGreen (#8FBC8F)</summary>
        public string DarkSeaGreen = $"\e[48;2;143;188;143m";

        /// <summary>DarkSlateBlue (#483D8B)</summary>
        public string DarkSlateBlue = $"\e[48;2;72;61;139m";

        /// <summary>DarkSlateGray (#2F4F4F)</summary>
        public string DarkSlateGray = $"\e[48;2;47;79;79m";

        /// <summary>DarkTurquoise (#00CED1)</summary>
        public string DarkTurquoise = $"\e[48;2;0;206;209m";

        /// <summary>DarkViolet (#9400D3)</summary>
        public string DarkViolet = $"\e[48;2;148;0;211m";

        /// <summary>DeepPink (#FF1493)</summary>
        public string DeepPink = $"\e[48;2;255;20;147m";

        /// <summary>DeepSkyBlue (#00BFFF)</summary>
        public string DeepSkyBlue = $"\e[48;2;0;191;255m";

        /// <summary>DimGray (#696969)</summary>
        public string DimGray = $"\e[48;2;105;105;105m";

        /// <summary>DodgerBlue (#1E90FF)</summary>
        public string DodgerBlue = $"\e[48;2;30;144;255m";

        /// <summary>Firebrick (#B22222)</summary>
        public string Firebrick = $"\e[48;2;178;34;34m";

        /// <summary>FloralWhite (#FFFAF0)</summary>
        public string FloralWhite = $"\e[48;2;255;250;240m";

        /// <summary>ForestGreen (#228B22)</summary>
        public string ForestGreen = $"\e[48;2;34;139;34m";

        /// <summary>Fuchsia (#FF00FF)</summary>
        public string Fuchsia = $"\e[48;2;255;0;255m";

        /// <summary>Gainsboro (#DCDCDC)</summary>
        public string Gainsboro = $"\e[48;2;220;220;220m";

        /// <summary>GhostWhite (#F8F8FF)</summary>
        public string GhostWhite = $"\e[48;2;248;248;255m";

        /// <summary>Gold (#FFD700)</summary>
        public string Gold = $"\e[48;2;255;215;0m";

        /// <summary>Goldenrod (#DAA520)</summary>
        public string Goldenrod = $"\e[48;2;218;165;32m";

        /// <summary>Gray (#808080)</summary>
        public string Gray = $"\e[48;2;128;128;128m";

        /// <summary>Green (#008000)</summary>
        public string Green = $"\e[48;2;0;128;0m";

        /// <summary>GreenYellow (#ADFF2F)</summary>
        public string GreenYellow = $"\e[48;2;173;255;47m";

        /// <summary>Honeydew (#F0FFF0)</summary>
        public string Honeydew = $"\e[48;2;240;255;240m";

        /// <summary>HotPink (#FF69B4)</summary>
        public string HotPink = $"\e[48;2;255;105;180m";

        /// <summary>IndianRed (#CD5C5C)</summary>
        public string IndianRed = $"\e[48;2;205;92;92m";

        /// <summary>Indigo (#4B0082)</summary>
        public string Indigo = $"\e[48;2;75;0;130m";

        /// <summary>Ivory (#FFFFF0)</summary>
        public string Ivory = $"\e[48;2;255;255;240m";

        /// <summary>Khaki (#F0E68C)</summary>
        public string Khaki = $"\e[48;2;240;230;140m";

        /// <summary>Lavender (#E6E6FA)</summary>
        public string Lavender = $"\e[48;2;230;230;250m";

        /// <summary>LavenderBlush (#FFF0F5)</summary>
        public string LavenderBlush = $"\e[48;2;255;240;245m";

        /// <summary>LawnGreen (#7CFC00)</summary>
        public string LawnGreen = $"\e[48;2;124;252;0m";

        /// <summary>LemonChiffon (#FFFACD)</summary>
        public string LemonChiffon = $"\e[48;2;255;250;205m";

        /// <summary>LightBlue (#ADD8E6)</summary>
        public string LightBlue = $"\e[48;2;173;216;230m";

        /// <summary>LightCoral (#F08080)</summary>
        public string LightCoral = $"\e[48;2;240;128;128m";

        /// <summary>LightCyan (#E0FFFF)</summary>
        public string LightCyan = $"\e[48;2;224;255;255m";

        /// <summary>LightGoldenrodYellow (#FAFAD2)</summary>
        public string LightGoldenrodYellow = $"\e[48;2;250;250;210m";

        /// <summary>LightGreen (#90EE90)</summary>
        public string LightGreen = $"\e[48;2;144;238;144m";

        /// <summary>LightGray (#D3D3D3)</summary>
        public string LightGray = $"\e[48;2;211;211;211m";

        /// <summary>LightPink (#FFB6C1)</summary>
        public string LightPink = $"\e[48;2;255;182;193m";

        /// <summary>LightSalmon (#FFA07A)</summary>
        public string LightSalmon = $"\e[48;2;255;160;122m";

        /// <summary>LightSeaGreen (#20B2AA)</summary>
        public string LightSeaGreen = $"\e[48;2;32;178;170m";

        /// <summary>LightSkyBlue (#87CEFA)</summary>
        public string LightSkyBlue = $"\e[48;2;135;206;250m";

        /// <summary>LightSlateGray (#778899)</summary>
        public string LightSlateGray = $"\e[48;2;119;136;153m";

        /// <summary>LightSteelBlue (#B0C4DE)</summary>
        public string LightSteelBlue = $"\e[48;2;176;196;222m";

        /// <summary>LightYellow (#FFFFE0)</summary>
        public string LightYellow = $"\e[48;2;255;255;224m";

        /// <summary>Lime (#00FF00)</summary>
        public string Lime = $"\e[48;2;0;255;0m";

        /// <summary>LimeGreen (#32CD32)</summary>
        public string LimeGreen = $"\e[48;2;50;205;50m";

        /// <summary>Linen (#FAF0E6)</summary>
        public string Linen = $"\e[48;2;250;240;230m";

        /// <summary>Magenta (#FF00FF)</summary>
        public string Magenta = $"\e[48;2;255;0;255m";

        /// <summary>Maroon (#800000)</summary>
        public string Maroon = $"\e[48;2;128;0;0m";

        /// <summary>MediumAquamarine (#66CDAA)</summary>
        public string MediumAquamarine = $"\e[48;2;102;205;170m";

        /// <summary>MediumBlue (#0000CD)</summary>
        public string MediumBlue = $"\e[48;2;0;0;205m";

        /// <summary>MediumOrchid (#BA55D3)</summary>
        public string MediumOrchid = $"\e[48;2;186;85;211m";

        /// <summary>MediumPurple (#9370DB)</summary>
        public string MediumPurple = $"\e[48;2;147;112;219m";

        /// <summary>MediumSeaGreen (#3CB371)</summary>
        public string MediumSeaGreen = $"\e[48;2;60;179;113m";

        /// <summary>MediumSlateBlue (#7B68EE)</summary>
        public string MediumSlateBlue = $"\e[48;2;123;104;238m";

        /// <summary>MediumSpringGreen (#00FA9A)</summary>
        public string MediumSpringGreen = $"\e[48;2;0;250;154m";

        /// <summary>MediumTurquoise (#48D1CC)</summary>
        public string MediumTurquoise = $"\e[48;2;72;209;204m";

        /// <summary>MediumVioletRed (#C71585)</summary>
        public string MediumVioletRed = $"\e[48;2;199;21;133m";

        /// <summary>MidnightBlue (#191970)</summary>
        public string MidnightBlue = $"\e[48;2;25;25;112m";

        /// <summary>MintCream (#F5FFFA)</summary>
        public string MintCream = $"\e[48;2;245;255;250m";

        /// <summary>MistyRose (#FFE4E1)</summary>
        public string MistyRose = $"\e[48;2;255;228;225m";

        /// <summary>Moccasin (#FFE4B5)</summary>
        public string Moccasin = $"\e[48;2;255;228;181m";

        /// <summary>NavajoWhite (#FFDEAD)</summary>
        public string NavajoWhite = $"\e[48;2;255;222;173m";

        /// <summary>Navy (#000080)</summary>
        public string Navy = $"\e[48;2;0;0;128m";

        /// <summary>OldLace (#FDF5E6)</summary>
        public string OldLace = $"\e[48;2;253;245;230m";

        /// <summary>Olive (#808000)</summary>
        public string Olive = $"\e[48;2;128;128;0m";

        /// <summary>OliveDrab (#6B8E23)</summary>
        public string OliveDrab = $"\e[48;2;107;142;35m";

        /// <summary>Orange (#FFA500)</summary>
        public string Orange = $"\e[48;2;255;165;0m";

        /// <summary>OrangeRed (#FF4500)</summary>
        public string OrangeRed = $"\e[48;2;255;69;0m";

        /// <summary>Orchid (#DA70D6)</summary>
        public string Orchid = $"\e[48;2;218;112;214m";

        /// <summary>PaleGoldenrod (#EEE8AA)</summary>
        public string PaleGoldenrod = $"\e[48;2;238;232;170m";

        /// <summary>PaleGreen (#98FB98)</summary>
        public string PaleGreen = $"\e[48;2;152;251;152m";

        /// <summary>PaleTurquoise (#AFEEEE)</summary>
        public string PaleTurquoise = $"\e[48;2;175;238;238m";

        /// <summary>PaleVioletRed (#DB7093)</summary>
        public string PaleVioletRed = $"\e[48;2;219;112;147m";

        /// <summary>PapayaWhip (#FFEFD5)</summary>
        public string PapayaWhip = $"\e[48;2;255;239;213m";

        /// <summary>PeachPuff (#FFDAB9)</summary>
        public string PeachPuff = $"\e[48;2;255;218;185m";

        /// <summary>Peru (#CD853F)</summary>
        public string Peru = $"\e[48;2;205;133;63m";

        /// <summary>Pink (#FFC0CB)</summary>
        public string Pink = $"\e[48;2;255;192;203m";

        /// <summary>Plum (#DDA0DD)</summary>
        public string Plum = $"\e[48;2;221;160;221m";

        /// <summary>PowderBlue (#B0E0E6)</summary>
        public string PowderBlue = $"\e[48;2;176;224;230m";

        /// <summary>Purple (#800080)</summary>
        public string Purple = $"\e[48;2;128;0;128m";

        /// <summary>RebeccaPurple (#663399)</summary>
        public string RebeccaPurple = $"\e[48;2;102;51;153m";

        /// <summary>Red (#FF0000)</summary>
        public string Red = $"\e[48;2;255;0;0m";

        /// <summary>RosyBrown (#BC8F8F)</summary>
        public string RosyBrown = $"\e[48;2;188;143;143m";

        /// <summary>RoyalBlue (#4169E1)</summary>
        public string RoyalBlue = $"\e[48;2;65;105;225m";

        /// <summary>SaddleBrown (#8B4513)</summary>
        public string SaddleBrown = $"\e[48;2;139;69;19m";

        /// <summary>Salmon (#FA8072)</summary>
        public string Salmon = $"\e[48;2;250;128;114m";

        /// <summary>SandyBrown (#F4A460)</summary>
        public string SandyBrown = $"\e[48;2;244;164;96m";

        /// <summary>SeaGreen (#2E8B57)</summary>
        public string SeaGreen = $"\e[48;2;46;139;87m";

        /// <summary>SeaShell (#FFF5EE)</summary>
        public string SeaShell = $"\e[48;2;255;245;238m";

        /// <summary>Sienna (#A0522D)</summary>
        public string Sienna = $"\e[48;2;160;82;45m";

        /// <summary>Silver (#C0C0C0)</summary>
        public string Silver = $"\e[48;2;192;192;192m";

        /// <summary>SkyBlue (#87CEEB)</summary>
        public string SkyBlue = $"\e[48;2;135;206;235m";

        /// <summary>SlateBlue (#6A5ACD)</summary>
        public string SlateBlue = $"\e[48;2;106;90;205m";

        /// <summary>SlateGray (#708090)</summary>
        public string SlateGray = $"\e[48;2;112;128;144m";

        /// <summary>Snow (#FFFAFA)</summary>
        public string Snow = $"\e[48;2;255;250;250m";

        /// <summary>SpringGreen (#00FF7F)</summary>
        public string SpringGreen = $"\e[48;2;0;255;127m";

        /// <summary>SteelBlue (#4682B4)</summary>
        public string SteelBlue = $"\e[48;2;70;130;180m";

        /// <summary>Tan (#D2B48C)</summary>
        public string Tan = $"\e[48;2;210;180;140m";

        /// <summary>Teal (#008080)</summary>
        public string Teal = $"\e[48;2;0;128;128m";

        /// <summary>Thistle (#D8BFD8)</summary>
        public string Thistle = $"\e[48;2;216;191;216m";

        /// <summary>Tomato (#FF6347)</summary>
        public string Tomato = $"\e[48;2;255;99;71m";

        /// <summary>Turquoise (#40E0D0)</summary>
        public string Turquoise = $"\e[48;2;64;224;208m";

        /// <summary>Violet (#EE82EE)</summary>
        public string Violet = $"\e[48;2;238;130;238m";

        /// <summary>Wheat (#F5DEB3)</summary>
        public string Wheat = $"\e[48;2;245;222;179m";

        /// <summary>White (#FFFFFF)</summary>
        public string White = $"\e[48;2;255;255;255m";

        /// <summary>WhiteSmoke (#F5F5F5)</summary>
        public string WhiteSmoke = $"\e[48;2;245;245;245m";

        /// <summary>Yellow (#FFFF00)</summary>
        public string Yellow = $"\e[48;2;255;255;0m";

        /// <summary>YellowGreen (#9ACD32)</summary>
        public string YellowGreen = $"\e[48;2;154;205;50m";

        /// <summary>内部専用コンストラクタ</summary>
        internal ExBackgroundColor() { }
    }
}
