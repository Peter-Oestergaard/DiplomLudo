namespace LudoGameElvir;

public class Board:IBoard
{
    private const int NumberOfPositions = 40;
    private Dictionary<IPlayer, int> _piecePositions;

    public Board(IEnumerable<IPlayer> players)
    {
        _piecePositions = new Dictionary<IPlayer, int>();

        foreach (var player in players)
        {
            _piecePositions[player] = 0;
        }
    }

    public int GetPiecePosition(IPlayer player)
    {
        return _piecePositions[player];
    }

    public void MovePiece(IPlayer player, int steps)
    {
        _piecePositions[player] += steps;

        if (_piecePositions[player] > NumberOfPositions)
        {
            _piecePositions[player] -= NumberOfPositions;
        }
    }
}