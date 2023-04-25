namespace DiplomLudo;

public class GameBoard
{
    public Dictionary<Color, Home> Homes { get; } = new();
    
    // private List<Tile> _tiles;
    // private int _numberOfTiles = 52;
    // private Dictionary<Piece, Tile> _tokenPositions = new Dictionary<Piece, Tile>();

    // private Dictionary<Color, int> _startingPositions = new Dictionary<Color, int>()
    // {
    //     {Color.Red, 0},
    //     {Color.Green, 13},
    //     {Color.Yellow, 26},
    //     {Color.Blue, 39}
    // };

    //public List<Tile> Tiles { get; set; }

    public GameBoard()
    {
        foreach (Color color in Enum.GetValues(typeof(Color)))
        {
            Homes[color] = new Home();
        }
        // _tiles = new List<Tile>();
        // for (int i = 0; i < _numberOfTiles; i++)
        // {
        //     _tiles.Add(new Tile());
        // }
    }
    
    public void PutPlayerPiecesInHome(Player player)
    {
        foreach (Piece piece in player.Pieces)
        {
            Homes[player.Color].AddPiece(piece);
        }
    }
}
