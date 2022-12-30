namespace library_management_system.Exception;

using System;

[Serializable]
public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException()
    {
    }

    public UserAlreadyExistsException(string? message) : base(message)
    {
    }
}