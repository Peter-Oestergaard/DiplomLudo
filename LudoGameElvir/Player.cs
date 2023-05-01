namespace LudoGameElvir;

public class Player:IPlayer
{
    public ConsoleColor Color { get; }
    private int _piecePosition = 0;

    public Player(ConsoleColor color)
    {
        Color = color;
    }
    public void MovePiece(int steps)
    {
        _piecePosition += steps;
    }

    public int GetPiecePosition()
    {
        return _piecePosition;
    }
    public void SetPiecePosition(int position)
    {
        _piecePosition = position;
    }
}