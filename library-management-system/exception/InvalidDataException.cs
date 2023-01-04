namespace library_management_system.Exception;

[Serializable]
public class InvalidDataException : System.Exception
{
    public InvalidDataException()
    {
    }

    public InvalidDataException(string? message) : base(message)
    {
    }
}