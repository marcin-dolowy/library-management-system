namespace library_management_system.Exception;

[Serializable]
public class NoSuchTitleException : System.Exception
{
    public NoSuchTitleException()
    {
    }

    public NoSuchTitleException(string? message) : base(message)
    {
    }
}