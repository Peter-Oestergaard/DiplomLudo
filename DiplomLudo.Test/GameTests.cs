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
    [InlineData(4, 1, 5)]
    [InlineData(14, 2, 16)]
    [InlineData(7, 3, 10)]
    [InlineData(34, 4, 38)]
    [InlineData(43, 5, 48)]
    [InlineData(50, 6, 4)]
    public void CallingNextMainTile_ReturnsCorrectTile(int from, int steps, int expected)
    {
        Dictionary<Color, Player> players = new();
        players[Color.Blue] = new Player(Color.Blue);
        players[Color.Green] = new Player(Color.Green);
        Game game = new Game(players);

        Tile origin = game.Board.MainTiles[from];
        Tile destination = game.NextMainTile(origin, steps)!;

        game.Board.MainTiles.IndexOf(destination).Should().Be(expected);
    }
}