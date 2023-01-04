namespace library_management_system.Exception;

[Serializable]
public class NoSuchOptionException : System.Exception
{
    public NoSuchOptionException()
    {
    }

    public NoSuchOptionException(string? message) : base(message)
    {
    }
}