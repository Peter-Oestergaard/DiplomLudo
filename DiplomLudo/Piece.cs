namespace DiplomLudo;

public class Piece
{
    public Color Color { get; }
    public Tile? Tile { get; private set; }

    public Piece(Color color)
    {
        Color = color;
    }

    public void MoveTo(Tile tile)
    {
        Tile?.Remove(this);
        tile.Put(this);
        Tile = tile;
    }
}
