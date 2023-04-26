namespace DiplomLudo;

public class Game
{
    public GameBoard Board { get; }
    public Player? CurrentPlayer { get; private set; }
    private IDie Die { get; init; }

    private bool _dieRolled;

    public Game(List<Player> players, IDie? die = null)
    {
        Board = new GameBoard();
        Die = die ?? new Die();
        foreach (Player player in players)
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
            foreach (Piece piece in CurrentPlayer.Pieces)
            {
                if (IsPieceMovable(piece, Die.Value))
                {
                    piecesThatCanBeMoved.Add(piece);
                }
            }
        }
        return piecesThatCanBeMoved;
    }
    
    public bool IsPieceMovable(Piece piece, int dieValue)
    {
        if (piece.IsHome && dieValue == 6)
        {
            return true;
        }

        return false;
    }
    
    public Tile? NextTile(Piece piece)
    {
        if (_dieRolled)
        {
            if (piece.IsHome && Die.Value == 6)
            {
                return Board.StartingTiles[piece.Color];
            }
        }
        return null;
    }
}