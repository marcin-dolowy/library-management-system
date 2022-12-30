namespace library_management_system.Exception;

using System;

[Serializable]
public class InvalidDataException : Exception
{
    public InvalidDataException()
    {
    }

    public InvalidDataException(string? message) : base(message)
    {
    }
}