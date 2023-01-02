using library_management_system.Exception;

namespace library_management_system.io.file;

public class FileManagerBuilder {
    private ConsolePrinter printer;
    private DataReader reader;

    public FileManagerBuilder(ConsolePrinter printer, DataReader reader) {
        this.printer = printer;
        this.reader = reader;
    }

    public IFileManager build() {
        printer.PrintLine("Wybierz format danych:");
        FileType fileType = getFileType();
        switch (fileType) {
            case FileType.CSV:
                return new CsvFileManager();
            default:
                throw new NoSuchFileTypeException("Nieobsługiwany typ danych");
        }
    }

    private FileType getFileType() {
        bool typeOk = false;
        FileType result = FileType.CSV;
        do {
            printTypes();
            //serial, SERIAL
            String type = reader.GetString();
            if (string.Equals(type, "CSV", StringComparison.OrdinalIgnoreCase))
            {
                result = FileType.CSV;
                typeOk = true;
            }
            else
            {
                printer.PrintLine("Nieobsługiwany typ danych, wybierz ponownie.");
            }
        } while (!typeOk);
        return result;
    }

    private void printTypes() {
        foreach (FileType value in Enum.GetValues(typeof(FileType)))
        {
            printer.PrintLine(value.ToString());
        }
    }
}