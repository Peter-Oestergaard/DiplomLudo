namespace DiplomLudo;

public class Game
{
    public GameBoard Board { get; }
    public Player? CurrentPlayer { get; private set; }
    private IDie Die { get; init; }

    private bool _dieRolled;

    public Game(Dictionary<Color, Player> players, IDie? die = null)
    {
        Board = new GameBoard();
        Die = die ?? new Die();
        foreach (Player player in players.Values)
        {
            Board.PutPlayerPiecesInHome(player);
        }
    }
    public void StartingPlayer(Player player)
    {
        CurrentPlayer ??= player;
    }

    public void RollDie()
    {
        if (!_dieRolled)
        {
            Die.Roll();
            _dieRolled = true;
        }
    }
    public List<Piece> PiecesWithLegalMoves()
    {
        List<Piece> piecesThatCanBeMoved = new();
        if (_dieRolled)
        {
            foreach (Piece piece in CurrentPlayer!.Pieces)
            {
                if (NextTile(piece) != null)
                {
                    piecesThatCanBeMoved.Add(piece);
                }
            }
        }
        return piecesThatCanBeMoved;
    }

    public Tile? NextTile(Piece piece)
    {
        if (piece.Tile.Type == TileType.Home && Die.Value == 6)
        {
            return Board.StartingTiles[piece.Color];
        }
        return null;
    }
}