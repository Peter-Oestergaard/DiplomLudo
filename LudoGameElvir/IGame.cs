namespace LudoGameElvir;

public interface IGame
{
    bool AddPlayer(IPlayer player);
    IReadOnlyList<IPlayer> GetPlayers();
    void CurrentPlayerRoll(int roll);
    void MoveCurrentPlayerPiece();
    int GetCurrentPlayerPiecePosition();
    void SetCurrentPlayerPiecePosition(int position);
    bool HasPlayerWon(IPlayer player);
}