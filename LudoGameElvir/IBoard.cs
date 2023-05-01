namespace LudoGameElvir;

public interface IBoard
{
    int GetPiecePosition(IPlayer player);
    void MovePiece(IPlayer player, int steps);
}