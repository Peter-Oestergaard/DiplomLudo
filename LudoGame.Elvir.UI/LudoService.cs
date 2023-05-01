using LudoGameElvir;

namespace LudoGame.Elvir.UI;

public class LudoService
{
    private IGame _game;
    private int _numberOfPlayers;

    public LudoService(IGame game)
    {
        _game = game;
    }

    public void Start()
    {
        Console.WriteLine("Welcome to Ludo!");
        InitializePlayers();
        PlayGame();
    }

    private void InitializePlayers()
    {
        Console.Write("Enter the number of players (2-4): ");
        _numberOfPlayers = int.Parse(Console.ReadLine());

        ConsoleColor[] colors = new ConsoleColor[] { ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Green, ConsoleColor.Yellow };

        for (int i = 0; i < _numberOfPlayers; i++)
        {
            _game.AddPlayer(new Player(colors[i]));
        }
    }

    private void PlayGame()
    {
        bool gameOver = false;

        while (!gameOver)
        {
            for (int i = 0; i < _numberOfPlayers; i++)
            {
                var currentPlayer = _game.GetPlayers()[i];
                Console.WriteLine($"Player {i + 1} ({currentPlayer.Color}) turn:");

                Console.Write("Press Enter to roll the dice: ");
                Console.ReadLine();

                int roll = new Random().Next(1, 7);
                Console.WriteLine($"You rolled a {roll}!");

                _game.CurrentPlayerRoll(roll);
                _game.MoveCurrentPlayerPiece();

                if (_game.HasPlayerWon(currentPlayer))
                {
                    Console.WriteLine($"Player {i + 1} ({currentPlayer.Color}) wins!");
                    gameOver = true;
                    break;
                }
            }
        }
    }
}