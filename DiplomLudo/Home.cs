namespace DiplomLudo;

public class Home
{
    public int PiecesCount => Tile.PiecesCount;
    
    public Tile Tile { get; }

    public Home(Color color)
    {
        Tile = new Tile(TileType.Home, color, -1);
    }

    public void AddPiece(Piece piece)
    {
        piece.MoveTo(Tile);
    }

    public Piece? GetAnyPiece()
    {
        return Tile.AnyPiece;
    }
}
