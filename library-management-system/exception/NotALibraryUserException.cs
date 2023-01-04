namespace library_management_system.Exception;

[Serializable]
public class NotALibraryUserException : System.Exception
{
    public NotALibraryUserException()
    {
    }

    public NotALibraryUserException(string? message) : base(message)
    {
    }
}