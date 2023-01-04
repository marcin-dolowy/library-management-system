namespace library_management_system.Exception;

[Serializable]
public class NoSuchFileTypeException : System.Exception
{
    public NoSuchFileTypeException()
    {
    }

    public NoSuchFileTypeException(string? message) : base(message)
    {
    }
}