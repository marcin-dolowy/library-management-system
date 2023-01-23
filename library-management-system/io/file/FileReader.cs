namespace library_management_system.io.file;

public class FileReader : IDisposable
{
    private bool _disposedValue;
    private readonly StreamReader _reader;

    public FileReader(string path)
    {
        _reader = new StreamReader(path);
    }

    public Task<string> ReadToEndAsync()
    {
        return _reader.ReadToEndAsync();
    }

    public void Dispose() => Dispose(true);

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