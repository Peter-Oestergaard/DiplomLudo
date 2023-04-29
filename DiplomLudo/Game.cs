using DiplomLudo.Exceptions;

namespace DiplomLudo;

public class Game
{
    public GameBoard Board { get; }
    public Player? CurrentPlayer { get; private set; }
    private IDie Die { get; init; }
    private SortedDictionary<Color, Player> _playersInGame;
    public int CurrentPlayerNumberOfDieRolls { get; private set; }

    private bool _dieRolled;

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

    public void RollDie()
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
            return;
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
                if (NextTile(piece) != null)
                {
                    piecesThatCanBeMoved.Add(piece);
                }
            }
        }
        return piecesThatCanBeMoved;
    }

    public void Move(Piece piece)
    {
        Tile? destination = NextTile(piece);
        if (destination == null) return;

        if (destination.PiecesCount == 1 && destination.AnyPiece!.Color != piece.Color)
        {
            // The piece on the tile is knocked home
            Board.MovePieceToTile(destination.AnyPiece, Board.Homes[destination.AnyPiece.Color].Tile);
        }

        Board.MovePieceToTile(piece, destination);
        PassTurnToNextPlayer();
    }

    public void RemovePiece(Piece piece)
    {
        piece.Tile?.Remove(piece);
        _playersInGame[piece.Color].Pieces.Remove(piece);
    }
    
    private void PassTurnToNextPlayer()
    {
        int current = (int)CurrentPlayer!.Color;
        do
        {
            current = (current + 1) % 4;
        } while (!_playersInGame.ContainsKey((Color)current));
        CurrentPlayer = _playersInGame[(Color)current];
    }
    
    public Piece? GetAnyPieceFromHome()
    {
        return Board.Homes[CurrentPlayer!.Color].GetAnyPiece();
    }
    
    public Tile? NextTile(Piece piece)
    {
        if (piece.Tile!.Type == TileType.Home && Die.Value == 6)
        {
            return Board.StartingTiles[piece.Color];
        }
        if (piece.Tile!.Type == TileType.Home && Die.Value != 6)
        {
            return null;
        }

        return NextTileInPath(piece.Tile, piece.Color, Die.Value);
    }
    
    public Tile? NextTileInPath(Tile origin,Color color, int steps = 1)
    {
        int originIndex = Board.PlayerPaths[color].IndexOf(origin);
        
        if (originIndex == -1) return null;
        
        return Board.PlayerPaths[color][originIndex + steps];
    }
}
