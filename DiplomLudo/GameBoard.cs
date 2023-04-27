namespace DiplomLudo;

public class GameBoard
{
    public Dictionary<Color, Home> Homes { get; } = new();

    private List<Tile> _mainTiles;

    //private readonly int _numberOfMainTiles = 52;
    // private Dictionary<Piece, Tile> _tokenPositions = new Dictionary<Piece, Tile>();

    public Dictionary<Color, Tile?> StartingTiles { get; } = new();

    public GameBoard()
    {
        _mainTiles = MainTiles();
        
        foreach (Color color in Enum.GetValues(typeof(Color)))
        {
            if(color == Color.None) continue;
            Homes[color] = new Home(color);
            StartingTiles[color] = _mainTiles.Single(t => t.Type == TileType.Start && t.Color == color);
        }
    }
    private List<Tile> MainTiles()
    {
        List<Tile> tiles = new();
        tiles.Add(new Tile(TileType.Start, Color.Red)); // 0
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 1
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 2
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 3
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 4
        tiles.Add(new Tile(TileType.Star, Color.None)); // 5
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 6
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 7
        tiles.Add(new Tile(TileType.Globe, Color.None)); // 8
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 9
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 10
        tiles.Add(new Tile(TileType.Star, Color.None)); // 11
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 12
        tiles.Add(new Tile(TileType.Start, Color.Green)); // 13
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 14
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 15
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 16
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 17
        tiles.Add(new Tile(TileType.Star, Color.None)); // 18
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 19
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 20
        tiles.Add(new Tile(TileType.Globe, Color.None)); // 21
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 22
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 23
        tiles.Add(new Tile(TileType.Star, Color.None)); // 24
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 25
        tiles.Add(new Tile(TileType.Start, Color.Yellow)); // 26
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 27
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 28
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 29
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 30
        tiles.Add(new Tile(TileType.Star, Color.None)); // 31
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 32
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 33
        tiles.Add(new Tile(TileType.Globe, Color.None)); // 34
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 35
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 36
        tiles.Add(new Tile(TileType.Star, Color.None)); // 37
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 38
        tiles.Add(new Tile(TileType.Start, Color.Blue)); // 39
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 40
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 41
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 42
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 43
        tiles.Add(new Tile(TileType.Star, Color.None)); // 44
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 45
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 46
        tiles.Add(new Tile(TileType.Globe, Color.None)); // 47
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 48
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 49
        tiles.Add(new Tile(TileType.Star, Color.None)); // 50
        tiles.Add(new Tile(TileType.Regular, Color.None)); // 51
        return tiles;
    }

    public void PutPlayerPiecesInHome(Player player)
    {
        foreach (Piece piece in player.Pieces)
        {
            Homes[player.Color].AddPiece(piece);
        }
    }
    public void MovePieceToTile(Piece piece, Tile tile)
    {
        piece.MoveTo(tile);

    }

    public Tile? NextMainTile(Tile origin, int steps = 1)
    {
        int originIndex = _mainTiles.IndexOf(origin);

        if (originIndex == -1) return null;

        int destinationIndex = originIndex + steps;
        if (steps > 51) destinationIndex -= 52;

        return _mainTiles[destinationIndex];
    }
}
