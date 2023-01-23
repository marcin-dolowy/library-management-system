namespace library_management_system.io.file;

public class FileReader : IDisposable
{
    private bool _disposedValue;
    private readonly StreamReader _reader;

    public FileReader(string path)
    {
        _reader = new StreamReader(path);
    }

    public string ReadLine()
    {
        return _reader.ReadLine();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _reader.Dispose();
            }

            _disposedValue = true;
        }
    }
}