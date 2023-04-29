namespace DiplomLudo;

public class GameBoard
{
    public Dictionary<Color, Home> Homes { get; } = new();

    public List<Tile> MainTiles { get; private set; }
    public Dictionary<Color, Tile> TileBeforeHomeStretch { get; } = new();
    
    // private Dictionary<Piece, Tile> _tokenPositions = new Dictionary<Piece, Tile>();

    public Dictionary<Color, Tile> StartingTiles { get; } = new();
    public Dictionary<Color, List<Tile>> HomeStretch { get; } = new();
    public Dictionary<Color, List<Tile>> PlayerPaths { get; } = new();

    public GameBoard()
    {
        MainTiles = GetMainTiles();
        
        foreach (Color color in Enum.GetValues(typeof(Color)))
        {
            if(color == Color.None) continue;
            Homes[color] = new Home(color);
            StartingTiles[color] = MainTiles.Single(t => t.Type == TileType.Start && t.Color == color);
            TileBeforeHomeStretch[color] = MainTiles[(StartingTiles[color].Index + 50) % 52];

            HomeStretch[color] = new List<Tile>();
            for (int i = 0; i < 6; i++)
            {
                HomeStretch[color].Add(new Tile(TileType.Finish, color, i));
            }

            PlayerPaths[color] = new List<Tile>();
            PlayerPaths[color].Add(Homes[color].Tile);
            int startIndex = StartingTiles[color].Index;
            for (int i = 0; i < 51; i++)
            {
                PlayerPaths[color].Add(MainTiles[(startIndex + i) % 52]);
            }
            PlayerPaths[color].AddRange(HomeStretch[color]);
        }
    }
    private List<Tile> GetMainTiles()
    {
        List<Tile> tiles = new();
        tiles.Add(new Tile(TileType.Start, Color.Red, tiles.Count)); // 0
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 1
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 2
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 3
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 4
        tiles.Add(new Tile(TileType.Star, Color.None, tiles.Count)); // 5
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 6
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 7
        tiles.Add(new Tile(TileType.Globe, Color.None, tiles.Count)); // 8
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 9
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 10
        tiles.Add(new Tile(TileType.Star, Color.None, tiles.Count)); // 11
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 12
        tiles.Add(new Tile(TileType.Start, Color.Green, tiles.Count)); // 13
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 14
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 15
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 16
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 17
        tiles.Add(new Tile(TileType.Star, Color.None, tiles.Count)); // 18
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 19
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 20
        tiles.Add(new Tile(TileType.Globe, Color.None, tiles.Count)); // 21
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 22
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 23
        tiles.Add(new Tile(TileType.Star, Color.None, tiles.Count)); // 24
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 25
        tiles.Add(new Tile(TileType.Start, Color.Yellow, tiles.Count)); // 26
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 27
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 28
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 29
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 30
        tiles.Add(new Tile(TileType.Star, Color.None, tiles.Count)); // 31
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 32
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 33
        tiles.Add(new Tile(TileType.Globe, Color.None, tiles.Count)); // 34
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 35
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 36
        tiles.Add(new Tile(TileType.Star, Color.None, tiles.Count)); // 37
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 38
        tiles.Add(new Tile(TileType.Start, Color.Blue, tiles.Count)); // 39
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 40
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 41
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 42
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 43
        tiles.Add(new Tile(TileType.Star, Color.None, tiles.Count)); // 44
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 45
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 46
        tiles.Add(new Tile(TileType.Globe, Color.None, tiles.Count)); // 47
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 48
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 49
        tiles.Add(new Tile(TileType.Star, Color.None, tiles.Count)); // 50
        tiles.Add(new Tile(TileType.Regular, Color.None, tiles.Count)); // 51
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
}
