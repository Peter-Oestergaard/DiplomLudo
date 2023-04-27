using FluentAssertions;
using Xunit;

namespace DiplomLudo.Test;

public class DieTests
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
}