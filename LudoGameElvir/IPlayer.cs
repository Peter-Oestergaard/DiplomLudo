namespace LudoGameElvir;

public interface IPlayer
{
    ConsoleColor Color { get; }
    void MovePiece(int steps);
    int GetPiecePosition();
    void SetPiecePosition(int position);
}