using FluentAssertions;
using Xunit;

namespace DiplomLudo.Test;

public class GameBoardTests
{
    [Theory]
    //[InlineData(TileType.Start, Color.Red, 0, 56, Color.Red, TileType.Finish, 5)]
    [InlineData(TileType.Regular, Color.Green, 1, 1, Color.None, TileType.Regular, 2)]
    [InlineData(TileType.Regular, Color.Red, 49, 3, Color.Red, TileType.Finish, 1)]
    public void TilesDistance(TileType originType, Color pieceColor, int originIndex, int distance,
        Color destinationColor, TileType destinationType, int destinationIndex)
    {
        // Arrange
        GameBoard gameBoard = new GameBoard();
        Tile origin = null!;
        switch (originType)
        {
            case TileType.Regular:
                origin = gameBoard.MainTiles[originIndex];
                break;
            case TileType.Start:
                origin = gameBoard.StartingTiles[pieceColor];
                break;
        }
        
        // Act
        Tile tileToTest = gameBoard.GetTileDistanceAway(origin, distance, pieceColor);
        
        // Assert
        tileToTest.Color.Should().Be(destinationColor);
        tileToTest.Type.Should().Be(destinationType);
        tileToTest.Index.Should().Be(destinationIndex);
    }
}
