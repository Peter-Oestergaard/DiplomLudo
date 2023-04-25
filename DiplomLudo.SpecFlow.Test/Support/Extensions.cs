namespace DiplomLudo.SpecFlow.Test.Support;

static class Extensions
{
    public static Color ToEnum(this string color)
    {
        return color switch
        {
            "red" => Color.Red,
            "green" => Color.Green,
            "yellow" => Color.Yellow,
            "blue" => Color.Blue,
            _ => throw new Exception()
        };
    }
}
