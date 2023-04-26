using DiplomLudo.SpecFlow.Test.Support;
using FluentAssertions;

namespace DiplomLudo.SpecFlow.Test.Steps;

[Binding]
public class DiplomLudoStepDefinitions
{
    private Game _game = null!;
    private Dictionary<Color, Player> _players = new ();
    private static IDie? _die;
    
    [BeforeScenario("RequiresCheatingDie")]
    public static void SetupCheatingDie()
    {
        _die = new CheatingDie();
    }
    
    [Given(@"the (first|second|third|fourth) player is (red|green|yellow|blue)")]
    public void GivenTheNthPlayerWithColorIs(string _, string color)
    {
        _players[color.ToEnum()] = new Player(color.ToEnum());
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
        home.PiecesCount.Should().Be(numberOfPieces);
    }
    
    [Given(@"a two player game in its initial state with players (red|green|yellow|blue) and (red|green|yellow|blue)")]
    public void GivenAGameInItsInitialStateWithPlayersRedAndBlue(string p1Color, string p2Color)
    {
        _players[p1Color.ToEnum()] = new Player(p1Color.ToEnum());
        _players[p2Color.ToEnum()] = new Player(p2Color.ToEnum());
        _game = new Game(_players, _die);
    }
    
    [Given(@"(red|green|yellow|blue) is the current player")]
    public void GivenColorIsTheCurrentPlayer(string color)
    {
        _game.StartingPlayer(_players[color.ToEnum()]);
    }
    
    [When(@"the current player rolls a ([1-6]) with the die")]
    public void WhenTheCurrentPlayerRollsAWithTheDie(int value)
    {
        (_die as CheatingDie)!.cheat = () => value;
        _game.RollDie();
    }
    
    [Then(@"(red|green|yellow|blue) can move any of their pieces to the (red|green|yellow|blue) starting tile")]
    public void ThenColorCanMoveAnyOfTheirPiecesToTheColorStartingTile(string playerColor, string startingTileColor)
    {
        List<Piece> movablePieces = _game.PiecesWithLegalMoves();
        List<Piece> playerPieces = _players[playerColor.ToEnum()].Pieces;
        
        movablePieces.Should().Contain(playerPieces);

        movablePieces.Should().AllSatisfy(p =>
        {
            _game.NextTile(p).Should().BeEquivalentTo(_game.Board.StartingTiles[startingTileColor.ToEnum()]);
        });
    }


    [Given(@"one of (red|green|yellow|blue)s pieces is on (red|green|yellow|blue)s starting tile")]
    public void GivenOneOfColorsPiecesOnColorsStartingTile(string p1Color, string p2Color)
    {
        _game.Board.MovePieceToTile(_players[p1Color.ToEnum()].Pieces[0], _game.Board.StartingTiles[p2Color.ToEnum()]!);
        _game.Board.Homes[Color.Yellow].PiecesCount.Should().Be(3);
    }
    
    [When(@"(red|green|yellow|blue) moves a piece to (red|green|yellow|blue)s starting tile")]
    public void WhenColorMovesAPieceToColorsStartingTile(string playerColor, string startingTileColor)
    {
        _game.StartingPlayer(_players[playerColor.ToEnum()]);
        (_die as CheatingDie)!.cheat = () => 6;
        _game.RollDie();
        _game.Move(_game.GetAnyPieceFromHome()!);
    }
    
    [Then(@"(red|green|yellow|blue)s piece is returned to (red|green|yellow|blue)s home")]
    public void ThenColorsPieceIsReturnedToColorsHome(string playerColor, string homeColor)
    {
        _game.Board.Homes[homeColor.ToEnum()].PiecesCount.Should().Be(4);
    }
}