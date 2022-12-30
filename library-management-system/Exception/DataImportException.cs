namespace library_management_system.Exception;

using System;

[Serializable]
public class DataImportException : Exception
{
    public DataImportException()
    {
    }

    public DataImportException(string? message) : base(message)
    {
    }
}