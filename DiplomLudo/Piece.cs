namespace DiplomLudo;

public class Piece
{
    public Color Color { get; }
    public bool IsHome { get; set; }

    public Piece(Color color)
    {
        Color = color;
    }
}
