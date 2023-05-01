using FluentAssertions;
using LudoGameElvir;

namespace LudoGame.Elvir.SpecFlow.Test.Steps;
[Binding]
public class AddPleyer
{
    private IGame _game;
    private IPlayer _player;
    private bool _addPlayerResult;

    [Given(@"I have a Ludo game")]
    public void GivenIHaveALudoGame()
    {
        _game = new Game();
    }

    [Given(@"I have added a player with color ""(.*)""")]
    public void GivenIHaveAddedAPlayerWithColor(string color)
    {
        _player = new Player((ConsoleColor)Enum.Parse(typeof(ConsoleColor), color));
        _game.AddPlayer(_player);
    }

    [When(@"I add a player with color ""(.*)""")]
    public void WhenIAddAPlayerWithColor(string color)
    {
        _player = new Player((ConsoleColor)Enum.Parse(typeof(ConsoleColor), color));
        _addPlayerResult = _game.AddPlayer(_player);
    }

    [When(@"I try to add the same player again")]
    public void WhenITryToAddTheSamePlayerAgain()
    {
        _addPlayerResult = _game.AddPlayer(_player);
    }

    [Then(@"The player should be added to the game")]
    public void ThenThePlayerShouldBeAddedToTheGame()
    {
        _game.GetPlayers().Should().Contain(_player);
    }

    [Then(@"The player should not be added twice")]
    public void ThenThePlayerShouldNotBeAddedTwice()
    {
        _addPlayerResult.Should().BeFalse();
        _game.GetPlayers().Count(p => p.Color == _player.Color).Should().Be(1);
    }
    [Given(@"I have a Ludo game with 4 players")]
    public void GivenIHaveALudoGameWith4Players()
    {
        _game = new Game();
        _game.AddPlayer(new Player(ConsoleColor.Red));
        _game.AddPlayer(new Player(ConsoleColor.Blue));
        _game.AddPlayer(new Player(ConsoleColor.Green));
        _game.AddPlayer(new Player(ConsoleColor.Magenta));
    }
    [When(@"I try to add another player with color ""(.*)""")]
    public void WhenITryToAddAnotherPlayerWithColor(string color)
    {
        var newPlayer = new Player((ConsoleColor)Enum.Parse(typeof(ConsoleColor), color));
        _addPlayerResult = _game.AddPlayer(newPlayer);
    }

    [Then(@"The game should not allow more than 4 players")]
    public void ThenTheGameShouldNotAllowMoreThan4Players()
    {
        _addPlayerResult.Should().BeFalse();
        _game.GetPlayers().Count.Should().Be(4);
    }
    [Given(@"I have a Ludo game with 2 players")]
    public void GivenIHaveALudoGameWith2Players()
    {
        _game = new Game();
        _game.AddPlayer(new Player(ConsoleColor.Red));
        _game.AddPlayer(new Player(ConsoleColor.Blue));
    }

    [Given(@"the first player rolls a ""(.*)""")]
    public void GivenTheFirstPlayerRollsA(int roll)
    {
        _game.CurrentPlayerRoll(roll);
    }

    [When(@"the first player moves their piece")]
    public void WhenTheFirstPlayerMovesTheirPiece()
    {
        _game.MoveCurrentPlayerPiece();
    }

    [Then(@"the piece should be at position ""(.*)""")]
    public void ThenThePieceShouldBeAtPosition(int expectedPosition)
    {
        _game.GetCurrentPlayerPiecePosition().Should().Be(expectedPosition);
    }
    [Given(@"the first player has their piece at position ""(.*)""")]
    public void GivenTheFirstPlayerHasTheirPieceAtPosition(int position)
    {
        _game.SetCurrentPlayerPiecePosition(position);
    }

    [Then(@"the first player should win the game")]
    public void ThenTheFirstPlayerShouldWinTheGame()
    {
        _game.HasPlayerWon(_game.GetPlayers().First()).Should().BeTrue();
    }
}