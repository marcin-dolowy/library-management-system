namespace library_management_system.Exception;

[Serializable]
public class DataImportException : System.Exception
{
    public DataImportException()
    {
    }

    public DataImportException(string? message) : base(message)
    {
    }
}