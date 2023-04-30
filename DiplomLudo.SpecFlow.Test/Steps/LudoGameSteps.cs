using FluentAssertions;
using Xunit;

namespace DiplomLudo.SpecFlow.Test.Steps;

[Binding]
public class LudoGameSteps
{

    private GameBoard _gameBoard;
    private Player _redPlayer;
    private Player _bluePlayer;
    [Given(@"a Ludo game board")]
    public void GivenALudoGameBoard()
    {
       //TODO
    }
    [Given(@"a Red player with a piece at tile (\d+)")]
    public void GivenARedPlayerWithAPieceAtTile(int tileIndex)
    {
        _redPlayer = new Player(Color.Red);
        Piece redPiece = _redPlayer.Pieces[0];
        _gameBoard.StartingTiles[Color.Red]!.Piece = redPiece;
    }
    [Given(@"a Blue player with a piece at tile (\d+)")]
    public void GivenABluePlayerWithAPieceAtTile(int tileIndex)
    {
    //TODO
    }
    [When(@"the Red player rolls a (\d+)")]
    public void WhenTheRedPlayerRollsA(int dieValue)
    {
        // Implement logic to move the Red piece based on the die roll.
    }
    [Then(@"the Red piece should move to tile (\d+)")]
    public void ThenTheRedPieceShouldMoveToTile(int destinationTileIndex)
    {
     //TODO
    }
    [Then(@"the Blue piece should return to its home")]
    public void ThenTheBluePieceShouldReturnToItsHome()
    {
    //TODO
    }
}