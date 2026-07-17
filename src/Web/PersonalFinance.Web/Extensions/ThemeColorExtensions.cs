namespace PersonalFinance.Web.Extensions;

public static class ThemeColorExtensions
{
    public static string ToCssVar(this ThemeColor color)
    {
        string variableName = color switch
        {
            ThemeColor.Beige500    => "--beige-500",
            ThemeColor.Beige100    => "--beige-100",
            ThemeColor.Grey900     => "--grey-900",
            ThemeColor.Grey500     => "--grey-500",
            ThemeColor.Grey300     => "--grey-300",
            ThemeColor.Grey100     => "--grey-100",
            ThemeColor.Green       => "--green",
            ThemeColor.Yellow      => "--yellow",
            ThemeColor.Cyan        => "--cyan",
            ThemeColor.Navy        => "--navy",
            ThemeColor.Red         => "--red",
            ThemeColor.PurpleDark  => "--purple-dark",
            ThemeColor.PurpleLight => "--purple-light",
            ThemeColor.Turquoise   => "--turquoise",
            ThemeColor.Brown       => "--brown",
            ThemeColor.Magenta     => "--magenta",
            ThemeColor.Blue        => "--blue",
            ThemeColor.NavyGrey    => "--navy-grey",
            ThemeColor.ArmyGreen   => "--army-green",
            ThemeColor.Gold        => "--gold",
            ThemeColor.Orange      => "--orange",
            ThemeColor.White       => "--white",
            _ => throw new ArgumentOutOfRangeException(paramName: nameof(color), actualValue: color, message: null)
        };

        return $"var({variableName})";
    }
}

public enum ThemeColor
{
    Beige500,
    Beige100,
    Grey900,
    Grey500,
    Grey300,
    Grey100,
    Green,
    Yellow,
    Cyan,
    Navy,
    Red,
    PurpleDark,
    PurpleLight,
    Turquoise,
    Brown,
    Magenta,
    Blue,
    NavyGrey,
    ArmyGreen,
    Gold,
    Orange,
    White
}