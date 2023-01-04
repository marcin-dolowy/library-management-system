namespace library_management_system.io.file;

public class FileReader : IDisposable
{
    private readonly StreamReader _reader;

    public FileReader(string path)
    {
        _reader = new StreamReader(path);
    }

    public Task<string?> ReadLine()
    {
        return _reader.ReadLineAsync();
    }

    public void Dispose()
    {
        _reader.Close();
        _reader.Dispose();
    }
}