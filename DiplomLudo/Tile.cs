namespace DiplomLudo;

public class Tile
{
    public TileType Type { get; }
    public Color Color { get; }
    public int PiecesCount => _pieces.Count;
    public Piece? AnyPiece => _pieces.FirstOrDefault();
    
    private HashSet<Piece> _pieces = new();

    public Tile(TileType type, Color color)
    {
        Type = type;
        Color = color;
    }

    public void Put(Piece piece)
    {
        _pieces.Add(piece);
    }
    
    public void Remove(Piece piece)
    {
        _pieces.Remove(piece);
    }
}