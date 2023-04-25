namespace DiplomLudo;

public class GameBoard
{
    public Dictionary<Color, Home> Homes { get; } = new();

    private List<Tile?> _mainTiles = new();

    private readonly int _numberOfMainTiles = 52;
    // private Dictionary<Piece, Tile> _tokenPositions = new Dictionary<Piece, Tile>();

    public Dictionary<Color, Tile?> StartingTiles { get; } = new();

    private Dictionary<Color, int> _startingTilesIndices = new()
    {
        { Color.Red, 0 },
        { Color.Green, 13 },
        { Color.Yellow, 26 },
        { Color.Blue, 39 },
    };

    public GameBoard()
    {
        for (int i = 0; i < _numberOfMainTiles; i++)
        {
            _mainTiles.Add(new Tile());
        }

        foreach (Color color in Enum.GetValues(typeof(Color)))
        {
            Homes[color] = new Home();
            StartingTiles[color] = _mainTiles[_startingTilesIndices[color]];
        }
    }

    public void PutPlayerPiecesInHome(Player player)
    {
        foreach (Piece piece in player.Pieces)
        {
            Homes[player.Color].AddPiece(piece);
        }
    }
    public bool IsPieceMovable(Piece piece, int dieValue)
    {
        if (piece.IsHome && dieValue == 6)
        {
            return true;
        }

        return false;
    }
}
