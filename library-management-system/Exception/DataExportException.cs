namespace library_management_system.Exception;

using System;

[Serializable]
public class DataExportException : Exception
{
    public DataExportException()
    {
    }

    public DataExportException(string? message) : base(message)
    {
    }
}