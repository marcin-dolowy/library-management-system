namespace library_management_system.Exception;

using System;

[Serializable]
public class PublicationAlreadyExistsException : Exception
{
    public PublicationAlreadyExistsException()
    {
    }

    public PublicationAlreadyExistsException(string? message) : base(message)
    {
    }
}