namespace library_management_system.Exception;

[Serializable]
public class DataExportException : System.Exception
{
    public DataExportException()
    {
    }

    public DataExportException(string? message) : base(message)
    {
    }
}