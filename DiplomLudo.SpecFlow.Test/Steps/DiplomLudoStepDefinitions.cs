namespace DiplomLudo.SpecFlow.Test;

[Binding]
public class DiplomLudoStepDefinitions
{
    private int _numberOfPlayers;
    private Game _game;
    private Player _firstPlayer;
    private Player _secondPlayer;

    [Given(@"the number of players is (.*)")]
    public void GivenTheNumberOfPlayersIs(int numPlayer)
    {
        _numberOfPlayers = numPlayer;
    }
    
    [Given(@"the first player is yellow")]
    public void GivenTheFirstPlayerIsYellow()
    {
        _firstPlayer = new Player(Color.Yellow);
    }
    
    [Given(@"the second player is red")]
    public void GivenTheSecondPlayerIsRed()
    {
        _secondPlayer = new Player(Color.Red);
    }

    [When(@"the game is started")]
    public void WhenTheGameIsStarted()
    {
        _game = new Game(_numberOfPlayers);
    }

}