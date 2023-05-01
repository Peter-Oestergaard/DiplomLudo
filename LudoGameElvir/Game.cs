namespace LudoGameElvir;

public class Game:IGame
{
    private const int WinningPosition = 40;
    private const int MaxPlayers = 4;
    private int _currentPlayerIndex = 0;
    private int _currentRoll = 0;
    private List<IPlayer> _players;
    private IBoard _board;

    public Game(IEnumerable<IPlayer> players)
    {
        _players = players.ToList();
        _board = new Board(players);
    }

    public IReadOnlyList<IPlayer> GetPlayers()
    {
        return _players.AsReadOnly();
    }
    public bool AddPlayer(IPlayer player)
    {
        if (_players.Contains(player) || _players.Count >= MaxPlayers)
        {
            return false;
        }

        _players.Add(player);
        return true;
    }
    public void CurrentPlayerRoll(int roll)
    {
        _currentRoll = roll;
    }

    public void MoveCurrentPlayerPiece()
    {
        _board.MovePiece(_players[_currentPlayerIndex], _currentRoll);
    }

    public int GetCurrentPlayerPiecePosition()
    {
        return _board.GetPiecePosition(_players[_currentPlayerIndex]);
    }
    public void SetCurrentPlayerPiecePosition(int position)
    {
        _board.MovePiece(_players[_currentPlayerIndex], position - _board.GetPiecePosition(_players[_currentPlayerIndex]));
    }
    public bool HasPlayerWon(IPlayer player)
    {
        return _board.GetPiecePosition(player) == WinningPosition;
    }
}