namespace DiplomLudo.SpecFlow.Test;

[Binding]
public class DiplomLudoStepDefinitions
{
    private int _numberOfPlayers;
    private Game _game;

    [Given(@"the number of players is (.*)")]
    public void GivenTheNumberOfPlayersIs(int numPlayer)
    {
        _numberOfPlayers = numPlayer;
    }

    [When(@"the game is started")]
    public void WhenTheGameIsStarted()
    {
        _game = new Game(_numberOfPlayers);
    }

    [Then(@"the gameboard will be empty")]
    public void ThenTheGameboardWillBeEmpty()
    {
        _game.Board.Tiles.
    }
}