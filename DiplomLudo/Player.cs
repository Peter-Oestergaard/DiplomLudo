namespace DiplomLudo;

public class Player
{
    public Color Color { get; }
    public List<Piece> Pieces { get; } = new();
    public Player(Color color)
    {
        Color = color;
        
        for (int i = 0; i < 4; i++)
        {
            Pieces.Add(new Piece(color));
        }
    }
}
