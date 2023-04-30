namespace DiplomLudo.Exceptions;

public class DieNotRolledException : Exception
{
    public DieNotRolledException(string message) : base(message) {}
}
