namespace DiplomLudo.Console;

public class CheatingDie : IDie
{
    public int Value { get; private set; }

    public Func<int> cheat = null!;
    
    public void Roll()
    {
        Value = cheat();
    }
}
