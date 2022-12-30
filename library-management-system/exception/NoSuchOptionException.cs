namespace library_management_system.Exception;

using System;

[Serializable]
public class NoSuchOptionException : Exception
{
    public NoSuchOptionException()
    {
    }

    public NoSuchOptionException(string? message) : base(message)
    {
    }
}