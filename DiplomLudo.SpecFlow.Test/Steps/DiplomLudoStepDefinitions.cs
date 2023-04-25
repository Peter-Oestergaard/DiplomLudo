using FluentAssertions;

namespace DiplomLudo.SpecFlow.Test;

[Binding]
public class DiplomLudoStepDefinitions
{
    private Game? _game;
    private List<Player> _players = new ();

    [Given(@"the (first|second|third|fourth) player is (red|green|yellow|blue)")]
    public void GivenTheNthPlayerWithColorIs(string _, string color)
    {
        _players.Add(new Player(color.ToEnum()));
    }

    [When(@"the game is started")]
    public void WhenTheGameIsStarted()
    {
        _game = new Game(_players);
    }
    
    [Then(@"(red|green|yellow|blue) home will have ([0-4]*) (red|green|yellow|blue) pieces")]
    public void ThenColorHomeWillHaveColorPieces(string homeColor, int numberOfPieces, string pieceColor)
    {
        var home = _game.Board.Homes[homeColor.ToEnum()];
        int total = 0;
        foreach (var tile in home.Tiles)
        {
            if (tile.Piece?.Color == pieceColor.ToEnum()) total++;
        }
        total.Should().Be(numberOfPieces);
    }
}
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
