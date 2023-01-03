namespace library_management_system.Exception;

using System;

[Serializable]
public class BorrowAlreadyExistsException : Exception
{
    public BorrowAlreadyExistsException()
    {
    }

    public BorrowAlreadyExistsException(string? message) : base(message)
    {
    }
}