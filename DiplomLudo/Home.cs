namespace DiplomLudo;

public class Home
{
    public List<Tile> Tiles { get; } = new();

    public Home()
    {
        for (int i = 0; i < 4; i++)
        {
            Tiles.Add(new Tile());
        }
    }
    
    public void AddPiece(Piece piece)
    {
        foreach (Tile tile in Tiles)
        {
            if (tile.Piece == null)
            {
                tile.Piece = piece;
                piece.IsHome = true;
                return;
            }
        }
    }
}
