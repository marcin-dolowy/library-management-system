namespace library_management_system.Exception;

using System;

[Serializable]
public class NoSuchFileTypeException : Exception
{
    public NoSuchFileTypeException()
    {
    }

    public NoSuchFileTypeException(string? message) : base(message)
    {
    }
}