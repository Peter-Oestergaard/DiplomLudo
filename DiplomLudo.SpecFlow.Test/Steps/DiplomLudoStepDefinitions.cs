using DiplomLudo.SpecFlow.Test.Support;
using FluentAssertions;

namespace DiplomLudo.SpecFlow.Test.Steps;

[Binding]
public class DiplomLudoStepDefinitions
{
    private Game? _game;
    private List<Player> _players = new ();
    private static IDie? _die = null;
    
    
    [BeforeScenario("RequiresCheatingDie")]
    public static void SetupCheatingDie()
    {
        _die = new CheatingDie();
    }


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
    [Given(@"a two player game in it's initial state with players (red|green|yellow|blue) and (red|green|yellow|blue)")]
    public void GivenAGameInItsInitialStateWithPlayersRedAndBlue(string p1Color, string p2Color)
    {
        _players.Add(new Player(p1Color.ToEnum()));
        _players.Add(new Player(p2Color.ToEnum()));
        _game = new Game(_players, _die);
    }
    
    [Given(@"(red|green|yellow|blue) is the current player")]
    public void GivenColorIsTheCurrentPlayer(string color)
    {
        _game!.StartingPlayer(_players.Single(p => p.Color == color.ToEnum()));
    }
    
    [When(@"the current player rolls a ([1-6]) with the die")]
    public void WhenTheCurrentPlayerRollsAWithTheDie(int value)
    {
        (_die as CheatingDie)!.cheat = () => value;
        _game!.RollDie();
    }
    [Then(@"(red|green|yellow|blue) can move any of their pieces to the (red|green|yellow|blue) starting tile")]
    public void ThenColorCanMoveAnyOfTheirPiecesToTheColorStartingTile(string playerColor, string startingTileColor)
    {
        List<Piece> movablePieces = _game!.PiecesWithLegalMoves();
        List<Piece> playerPieces = _players.Single(p => p.Color == playerColor.ToEnum()).Pieces;
        
        movablePieces.Should().Contain(playerPieces);

        movablePieces.Should().AllSatisfy(p =>
        {
            _game.NextTile(p).Should().BeEquivalentTo(_game.Board.StartingTiles[startingTileColor.ToEnum()]);
        });
    }
}