namespace DiplomLudo;

public class Game
{
    public GameBoard Board { get; }
    
    public Game(List<Player> players)
    {
        Board = new GameBoard();
        foreach (Player player in players)
        {
            Board.PutPlayerPiecesInHome(player);
        }
    }
}