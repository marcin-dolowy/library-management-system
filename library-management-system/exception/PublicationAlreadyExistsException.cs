namespace library_management_system.Exception;

[Serializable]
public class PublicationAlreadyExistsException : System.Exception
{
    public PublicationAlreadyExistsException()
    {
    }

    public PublicationAlreadyExistsException(string? message) : base(message)
    {
    }
}