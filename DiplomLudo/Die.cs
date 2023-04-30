namespace DiplomLudo;

public class Die : IDie
{
    public int Value { get; private set; }

    public void Roll()
    {
        Value = new Random().Next(1, 7);
    } 
}
