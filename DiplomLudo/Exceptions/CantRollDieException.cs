﻿namespace DiplomLudo.Exceptions;

public class CantRollDieException : Exception
{
    public CantRollDieException(string message) : base(message) {}
}