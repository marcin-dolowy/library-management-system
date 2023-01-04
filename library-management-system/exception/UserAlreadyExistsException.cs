namespace library_management_system.Exception;

[Serializable]
public class UserAlreadyExistsException : System.Exception
{
    public UserAlreadyExistsException()
    {
    }

    public UserAlreadyExistsException(string? message) : base(message)
    {
    }
}