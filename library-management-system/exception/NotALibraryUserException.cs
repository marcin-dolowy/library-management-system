namespace library_management_system.Exception;

using System;

[Serializable]
public class NotALibraryUserException : Exception
{
    public NotALibraryUserException()
    {
    }

    public NotALibraryUserException(string? message) : base(message)
    {
    }
}