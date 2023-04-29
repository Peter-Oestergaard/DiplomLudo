using DiplomLudo.SpecFlow.Test.Support;
using FluentAssertions;

namespace DiplomLudo.SpecFlow.Test.Steps;

[Binding]
public class DiplomLudoStepDefinitions
{
    private Game _game = null!;
    private Dictionary<Color, Player> _players = new();
    private static IDie? _die;
    private Piece _pieceUnderTest;

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
        _game.Move(_game.GetAnyPieceFromHome()!);
    }

    [Then(@"(red|green|yellow|blue)s piece is returned to (red|green|yellow|blue)s home")]
    public void ThenColorsPieceIsReturnedToColorsHome(string playerColor, string homeColor)
    {
        _game.Board.Homes[homeColor.ToEnum()].PiecesCount.Should().Be(4);
    }

    [Given(@"doesn't roll a six until attempt ([1-3])")]
    public void GivenDoesntRollASixUntilAttempt(int attempts)
    {
        for (int i = 0; i < attempts - 1; i++)
        {
            (_die as CheatingDie)!.cheat = () => 1;
            _game.RollDie();
            _game.PiecesWithLegalMoves().Should().BeEmpty();
        }
        (_die as CheatingDie)!.cheat = () => 6;
        _game.RollDie();
    }

    [Then(@"they will have rolled the die ([1-3]) times")]
    public void ThenColorWillHaveRolledTheDieTimes(int attempts)
    {
        _game.CurrentPlayerNumberOfDieRolls.Should().Be(attempts);
    }

    [Then(@"have four legal moves to (red|green|yellow|blue)s starting tile")]
    public void ThenHaveOneLegalMoveToColorsStartingTile(string _)
    {
        _game.PiecesWithLegalMoves().Count.Should().Be(4);
    }

    [Then(@"(red|green|yellow|blue) will have ([0-4]) piece\(s\) on the (red|green|yellow|blue) starting tile")]
    public void ThenColorWillHaveNPiecesOnTheColorStartingTile(string playerColor, int count, string startingTileColor)
    {
        _game.Board.StartingTiles[startingTileColor.ToEnum()]!.PiecesCount.Should().Be(count);
        _game.Board.StartingTiles[startingTileColor.ToEnum()]!.AnyPiece!.Color.Should().Be(playerColor.ToEnum());
    }
    
    [Then(@"it will be (red|green|yellow|blue)s turn")]
    public void ThenItWillBeColorsTurn(string currentPlayerColor)
    {
        _game.CurrentPlayer!.Color.Should().Be(currentPlayerColor.ToEnum());
    }
    
    [Given(@"(red|green|yellow|blue) have ([1-4]) left")]
    public void GivenColorHaveLeft(string playerColor, int piecesLeft)
    {
        while (_players[playerColor.ToEnum()].Pieces.Count > piecesLeft)
        {
            _game.RemovePiece(_players[playerColor.ToEnum()].Pieces.First());
        }
    }
    
    [Given(@"one of (red|green|yellow|blue)s pieces is ([1-51]) tiles in front of the star before (red|green|yellow|blue)s home stretch")]
    public void GivenOneOfColorsPiecesIsTilesInFrontOfTheStarBeforeColorsHomeStretch(string playerColor, int tiles, string homeStretchColor)
    {
        List<Tile> pp = _game.Board.PlayerPaths[playerColor.ToEnum()];
        _pieceUnderTest = _players[playerColor.ToEnum()].Pieces.First();
        Tile star = _game.Board.TileBeforeHomeStretch[homeStretchColor.ToEnum()];
        _game.Board.MovePieceToTile(_pieceUnderTest, pp[pp.IndexOf(star) - tiles]);

    }
    
    [Then(@"that piece is ([0-51]) tiles away from the finish tile")]
    public void ThenThatPieceIsTilesAwayFromTheFinishTile(int tiles)
    {
        List<Tile> pp = _game.Board.PlayerPaths[_pieceUnderTest.Color];
        Tile finishTile = pp.Last();
        //int i = pp.IndexOf(finishTile) - tiles;
        Tile tileWithPiece = pp[pp.IndexOf(finishTile)-tiles];
        tileWithPiece.Pieces.Should().Contain(_pieceUnderTest);
    }
    
    [Then(@"(red|green|yellow|blue) have [0-4] pieces in game")]
    public void ThenColorHavePiecesInGame(string playerColor, int pieces)
    {
        _players[playerColor.ToEnum()].Pieces.Count.Should().Be(pieces);
    }
    
    [When(@"current player moves that piece")]
    public void WhenCurrentPlayerMovesThatPiece()
    {
        _game.Move(_pieceUnderTest);
    }
}
