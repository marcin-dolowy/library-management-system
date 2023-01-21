using library_management_system.Exception;
using library_management_system.model;
using InvalidDataException = System.IO.InvalidDataException;

namespace library_management_system.io.file;

public class CsvFileManager : IFileManager
{
    private static readonly string FileName = "Library.csv";

    private static readonly string UsersFileName = "Library_users.csv";

    private static readonly string BorrowedFileName = "Borrowed.csv";

    public Library ImportData()
    {
        if (!File.Exists(FileName) || !File.Exists(UsersFileName) || !File.Exists(BorrowedFileName))
        {
            throw new DataImportException(
                $"Plik {FileName}, {UsersFileName} lub/i {BorrowedFileName} nie został znaleziony.");
        }
        
        Library library = new Library();
        
        
        Task<string> importPublicationsTask = ImportFromFile(FileName);
        importPublicationsTask.ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                throw new DataImportException($"Błąd odczytu pliku {FileName}.");
            }

            string[] split = task.Result.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            foreach (var line in split)
            {
                var publication = CreateObjectFromString(line);
                library.AddPublication(publication);
            }
        });
        
        Task<string> importUsersTask = ImportFromFile(UsersFileName);
        importUsersTask.ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                throw new DataImportException($"Błąd odczytu pliku {UsersFileName}.");
            }

            string[] split = task.Result.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            foreach (var line in split)
            {
                var user = CreateUserFromString(line);
                library.AddUser(user);
            }
        });
        
        Task<string> importBorrowedTask = ImportFromFile(BorrowedFileName);
        importBorrowedTask.ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                throw new DataImportException($"Błąd odczytu pliku {BorrowedFileName}.");
            }

            string[] split = task.Result.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            foreach (var line in split)
            {
                var borrow = CreateBorrowFromString(line);
                library.AddBorrowed(borrow);
            }
        });
        
        Task.WaitAll(importPublicationsTask, importUsersTask, importBorrowedTask);
        return library;
    }
    
    private static async Task<string> ImportFromFile(string fileName)
    {
        try
        {
            using FileReader reader = new FileReader(fileName);
            return await reader.ReadToEndAsync();
        }
        catch (FileNotFoundException)
        {
            throw new DataImportException($"Plik {BorrowedFileName} nie został znaleziony.");
        }
        catch (IOException)
        {
            throw new DataImportException($"Błąd odczytu pliku {BorrowedFileName}.");
        }
    }
    
    private static LibraryUser CreateUserFromString(string csvText)
    {
        string[] split = csvText.Split(";");
        string firstName = split[0];
        string lastName = split[1];
        string pesel = split[2];
        string password = split[3];
        return new LibraryUser(firstName, lastName, pesel, password);
    }

    private static Publication CreateObjectFromString(string line)
    {
        string[] split = line.Split(";");
        string type = split[0];
        switch (type)
        {
            case Book.Type:
                return CreateBook(split);
            case Magazine.Type:
                return CreateMagazine(split);
            default:
                throw new InvalidDataException("Nieznany typ publikacji " + type);
        }
    }

    private static Borrow CreateBorrowFromString(string line)
    {
        string[] split = line.Split(";");
        string userPesel = split[0];
        string publicationTitle = split[1];
        return new Borrow(userPesel, publicationTitle);
    }

    private static Magazine CreateMagazine(string[] data)
    {
        string title = data[1];
        string publisher = data[2];
        int year = int.Parse(data[3]);
        int month = int.Parse(data[4]);
        int day = int.Parse(data[5]);
        string language = data[6];
        return new Magazine(title, publisher, language, year, month, day);
    }

    private static Book CreateBook(string[] data)
    {
        string title = data[1];
        string publisher = data[2];
        int year = int.Parse(data[3]);
        string author = data[4];
        int pages = int.Parse(data[5]);
        string isbn = data[6];
        return new Book(title, author, year, pages, publisher, isbn);
    }

    private static void ExportUsers(Library library)
    {
        ICollection<LibraryUser> users = library.Users.Values;
        ExportToCsv(users, UsersFileName);
    }

    private static void ExportPublications(Library library)
    {
        ICollection<Publication> publications = library.Publications.Values;
        ExportToCsv(publications, FileName);
    }

    private static void ExportBorrows(Library library)
    {
        ICollection<Borrow> borrows = library.Borrows;
        ExportToCsv(borrows, BorrowedFileName);
    }

    private static void ExportToCsv<T>(IEnumerable<T> collection, string fileName) where T : ICsvConvertible
    {
        try
        {
            using StreamWriter file = new(fileName);
            foreach (T element in collection)
            {
                file.WriteLine(element.ToCsv());
            }
        }
        catch (IOException)
        {
            throw new DataExportException("Błąd zapisu danych do pliku " + fileName);
        }
    }

    public void ExportData(Library library)
    {
        ExportPublications(library);
        ExportUsers(library);
        ExportBorrows(library);
    }
}