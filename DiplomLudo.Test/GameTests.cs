using DiplomLudo.Exceptions;
using FluentAssertions;
using Xunit;

namespace DiplomLudo.Test;

public class GameTests
{
    [Fact]
    public void RollingDieTwoTimesInRowWithoutMakingMove_ThrowsException()
    {
        // IDie _die = new CheatingDie();
        // (_die as CheatingDie)!.cheat = () => 1;

        Dictionary<Color, Player> players = new();
        players[Color.Blue] = new Player(Color.Blue);
        players[Color.Green] = new Player(Color.Green);
        Game game = new Game(players);
        game.Board.MovePieceToTile(game.Board.Homes[Color.Blue].GetAnyPiece()!, game.Board.StartingTiles[Color.Blue]!);
        game.StartingPlayer(players[Color.Blue]);
        game.RollDie();
        game.Invoking(g => g.RollDie())
            .Should().Throw<CantRollDieException>()
            .WithMessage("You already rolled the die and can make a move");
    }

    [Theory]
    [InlineData(4, Color.Blue, 1, 5)]
    [InlineData(14, Color.Blue, 2, 16)]
    [InlineData(7, Color.Blue, 3, 10)]
    [InlineData(34, Color.Blue, 4, 0)]
    [InlineData(43, Color.Blue, 5, 48)]
    [InlineData(50, Color.Blue, 6, 4)]
    public void CallingNextTileInPath_ReturnsCorrectTile(int from, Color color, int steps, int expected)
    {
        Dictionary<Color, Player> players = new()
        {
            [Color.Blue] = new Player(Color.Blue),
            [Color.Green] = new Player(Color.Green)
        };
        Game game = new Game(players);

        Tile origin = game.Board.MainTiles[from];
        Tile destination = game.NextTileInPath(origin, color, steps)!;

        //game.Board.PlayerPaths[color].IndexOf(destination).Should().Be(expected);
        destination.Index.Should().Be(expected);
    }

    [Fact]
    public void PiecesOnFinish_HaveNoLegalMoves()
    {
        // Arrange
        Dictionary<Color, Player> players = new()
        {
            [Color.Red] = new Player(Color.Red),
            [Color.Green] = new Player(Color.Green)
        };
        Game game = new Game(players, new CheatingDie {cheat = () => 6});
        players[Color.Green].Pieces[0].MoveTo(game.Board.HomeStretch[Color.Green][5]);
        players[Color.Green].Pieces[1].MoveTo(game.Board.HomeStretch[Color.Green][5]);
        players[Color.Green].Pieces[2].MoveTo(game.Board.HomeStretch[Color.Green][5]);
        game.StartingPlayer(players[Color.Green]);
        
        // Act
        game.RollDie();
        
        // Assert
        game.PiecesWithLegalMoves().Should().Equal(players[Color.Green].Pieces[3]);
    }

    [Fact]
    public void MovingPieceBeforeDieRoll_ThrowsDieNotRolledException()
    {
        // Arrange
        Dictionary<Color, Player> players = new()
        {
            [Color.Red] = new Player(Color.Red),
            [Color.Green] = new Player(Color.Green)
        };
        Game game = new Game(players, new CheatingDie { cheat = () => 1});
        players[Color.Green].Pieces.First().MoveTo(game.Board.HomeStretch[Color.Green][4]);
        game.StartingPlayer(players[Color.Green]);
        
        // Assert
        game.Invoking(g => g.Move(players[Color.Green].Pieces.First()))
            .Should().Throw<DieNotRolledException>()
            .WithMessage("You need to roll the die before you can make a move");
    }
}