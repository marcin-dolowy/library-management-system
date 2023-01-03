namespace library_management_system.Exception;

using System;

[Serializable]
public class NoSuchTitleException : Exception
{
    public NoSuchTitleException()
    {
    }

    public NoSuchTitleException(string? message) : base(message)
    {
    }
}