namespace library_management_system.Exception;

[Serializable]
public class BorrowAlreadyExistsException : System.Exception
{
    public BorrowAlreadyExistsException()
    {
    }

    public BorrowAlreadyExistsException(string? message) : base(message)
    {
    }
}