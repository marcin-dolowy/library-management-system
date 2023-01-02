using library_management_system.Exception;

namespace library_management_system.io.file;

public class FileManagerBuilder
{
    private readonly ConsolePrinter _printer;
    private readonly DataReader _reader;

    public FileManagerBuilder(ConsolePrinter printer, DataReader reader)
    {
        _printer = printer;
        _reader = reader;
    }

    public IFileManager Build()
    {
        _printer.PrintLine("Wybierz format danych:");
        FileType fileType = GetFileType();
        return fileType switch
        {
            FileType.CSV => new CsvFileManager(),
            _ => throw new NoSuchFileTypeException("Nieobsługiwany typ danych")
        };
    }

    private FileType GetFileType()
    {
        bool typeOk = false;
        FileType result = FileType.CSV;
        do
        {
            PrintTypes();
            //serial, SERIAL
            String type = _reader.GetString();
            if (string.Equals(type, "CSV", StringComparison.OrdinalIgnoreCase))
            {
                result = FileType.CSV;
                typeOk = true;
            }
            else
            {
                _printer.PrintLine("Nieobsługiwany typ danych, wybierz ponownie.");
            }
        } while (!typeOk);

        return result;
    }

    private void PrintTypes()
    {
        foreach (FileType value in Enum.GetValues(typeof(FileType)))
        {
            _printer.PrintLine(value.ToString());
        }
    }
}