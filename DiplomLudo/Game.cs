using DiplomLudo.Exceptions;

namespace DiplomLudo;

public class Game
{
    public GameBoard Board { get; }
    public Player? CurrentPlayer { get; private set; }
    private IDie Die { get; init; }
    private SortedDictionary<Color, Player> _playersInGame;
    public int CurrentPlayerNumberOfDieRolls { get; private set; }

    private bool _dieRolled = false;

    public Game(Dictionary<Color, Player> players, IDie? die = null)
    {
        _playersInGame = new SortedDictionary<Color, Player>(players);
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

    public int RollDie()
    {
        // Rolled the die, can make move
        if (_dieRolled && PiecesWithLegalMoves().Count > 0)
        {
            throw new CantRollDieException("You already rolled the die and can make a move");
        }

        if (!_dieRolled || (Board.Homes[CurrentPlayer!.Color].PiecesCount == 4 && CurrentPlayerNumberOfDieRolls < 3))
        {
            Die.Roll();
            _dieRolled = true;
            CurrentPlayerNumberOfDieRolls++;
            return Die.Value;
        }

        if (_dieRolled && PiecesWithLegalMoves().Count == 0)
        {
            PassTurnToNextPlayer();
            return -1;
        }
        
        throw new CantRollDieException("You already rolled the die");
    }

    public List<Piece> PiecesWithLegalMoves()
    {
        List<Piece> piecesThatCanBeMoved = new();
        if (_dieRolled)
        {
            foreach (Piece piece in CurrentPlayer!.Pieces)
            {
                if (NextTile(piece, Die.Value) != null)
                {
                    piecesThatCanBeMoved.Add(piece);
                }
            }
        }
        return piecesThatCanBeMoved;
    }

    public void Move(Piece piece)
    {
        if (!_dieRolled) throw new DieNotRolledException("You need to roll the die before you can make a move");
        
        Tile? destination = NextTile(piece, Die.Value);
        
        //if (destination == null) return;

        if (destination.PiecesCount == 1 && destination.AnyPiece!.Color != piece.Color)
        {
            // The piece on the tile is knocked home
            Board.MovePieceToTile(destination.AnyPiece, Board.Homes[destination.AnyPiece.Color].Tile);
        }

        Board.MovePieceToTile(piece, destination);
        
        // Check winning conditions here
        // Board.HomeStretch[piece.Color][^1].PiecesCount == 4

        PassTurnToNextPlayer();
    }

    // public void RemovePiece(Piece piece)
    // {
    //     piece.Tile?.Remove(piece);
    //     _playersInGame[piece.Color].Pieces.Remove(piece);
    // }
    
    public void PassTurnToNextPlayer()
    {
        int current = (int)CurrentPlayer!.Color;
        do
        {
            current = (current + 1) % 4;
        } while (!_playersInGame.ContainsKey((Color)current));
        CurrentPlayer = _playersInGame[(Color)current];
        _dieRolled = false;
        CurrentPlayerNumberOfDieRolls = 0;
    }
    
    public Piece? GetAnyPieceFromHome()
    {
        return Board.Homes[CurrentPlayer!.Color].GetAnyPiece();
    }

    /// <summary>
    /// NextTile will return the next valid tile the piece can move to.
    /// This is where the majority of the game rules are handled
    /// </summary>
    /// <param name="piece"></param>
    /// <param name="dieValue"></param>
    /// <returns>Next valid tile</returns>
    public Tile? NextTile(Piece piece, int dieValue)
    {
        if (piece.Tile!.Type == TileType.Home && dieValue == 6)
        {
            return Board.StartingTiles[piece.Color];
        }
        if (
            (piece.Tile!.Type == TileType.Home && dieValue != 6)
            || piece.Tile! == Board.HomeStretch[piece.Color][5]
            )
        {
            return null;
        }

        return NextTileInPath(piece.Tile, piece.Color, dieValue);
    }
    
    public Tile? NextTileInPath(Tile origin, Color color, int steps = 1)
    {
        List<Tile> path = Board.PlayerPaths[color];
        int originIndex = path.IndexOf(origin);
        
        if (originIndex == -1) return null;

        int destinationIndex = originIndex + steps;

        if (destinationIndex > path.Count-1)
        {
            // int stepsToFinish = path.Count - 1 - originIndex;
            // int stepsRemaining = steps - stepsToFinish;
            // destinationIndex = path.Count - 1 - stepsRemaining;
            destinationIndex = 2 * path.Count - originIndex - steps - 2;
        }
        
        return Board.PlayerPaths[color][destinationIndex];
    }
}
