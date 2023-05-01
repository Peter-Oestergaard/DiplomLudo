namespace LudoGameElvir;

public class Game:IGame
{
    private const int WinningPosition = 40;
    private const int MaxPlayers = 4;
    private int _currentPlayerIndex = 0;
    private int _currentRoll = 0;
    private List<IPlayer> _players;

    public Game()
    {
        _players = new List<IPlayer>();
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
        _players[_currentPlayerIndex].MovePiece(_currentRoll);
    }

    public int GetCurrentPlayerPiecePosition()
    {
        return _players[_currentPlayerIndex].GetPiecePosition();
    }
    public void SetCurrentPlayerPiecePosition(int position)
    {
        _players[_currentPlayerIndex].SetPiecePosition(position);
    }

    public bool HasPlayerWon(IPlayer player)
    {
        return player.GetPiecePosition() == WinningPosition;
    }
}