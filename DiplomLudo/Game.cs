namespace DiplomLudo;

public enum Color
{
    Red,
    Green,
    Yellow,
    Blue
}

public class Game
{
    
    private GameBoard _gameBoard;

    public Game(int numberOfPlayers)
    {
        _gameBoard = new GameBoard();
        _gameBoard.Init(new List<Player>
        {
            new Player(Color.Red),
            new Player(Color.Yellow)
        });
    }

    public GameBoard Board { get; set; }
}

public class Player
{
    public Player(Color color){}
}

public class GameBoard
{
    private List<Tile> _tiles;
    private int _numberOfTiles = 52;
    private Dictionary<Piece, Tile> _tokenPositions = new Dictionary<Piece, Tile>();

    private Dictionary<Color, int> _startingPositions = new Dictionary<Color, int>()
    {
        {Color.Red, 0},
        {Color.Green, 13},
        {Color.Yellow, 26},
        {Color.Blue, 39}
    };

    public List<Tile> Tiles { get; set; }

    public GameBoard()
    {
        _tiles = new List<Tile>();
        for (int i = 0; i < _numberOfTiles; i++)
        {
            _tiles.Add(new Tile());
        }
    }
    
    public void Init(List<Player> players)
    {
        foreach (Player player in players)
        {
            throw new NotImplementedException();
        }
    }
}

public class Piece
{
}

public class Tile
{
}

/*

0-51,bbbb,yyyy,gggg,rrrr,