using library_management_system.Exception;
using library_management_system.model;
using InvalidDataException = System.IO.InvalidDataException;

namespace library_management_system.io.file;

public class CsvFileManager : FileManager
{
    private static string FILE_NAME =
        Path.Combine(
            Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName)!
                .FullName)!.FullName, "io", "file", "Library.csv");

    private static string USERS_FILE_NAME =
        Path.Combine(
            Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName)!
                .FullName)!.FullName, "io", "file", "Library_users.csv");

    public Library importData()
    {
        Library library = new Library();
        importPublications(library);
        importUsers(library);
        return library;
    }

    private void importPublications(Library library)
    {
        try
        {
            string[] lines = File.ReadAllLines(FILE_NAME);
            foreach (string line in lines)
            {
                var publication = createObjectFromString(line);
                library.AddPublication(publication);
            }
        }
        catch (FileNotFoundException e)
        {
            throw new DataImportException($"Plik {FILE_NAME} nie został znaleziony.");
        }
        catch (IOException e)
        {
            throw new DataImportException($"Błąd odczytu pliku {FILE_NAME}.");
        }
    }

    private void importUsers(Library library)
    {
        try
        {
            string[] lines = File.ReadAllLines(USERS_FILE_NAME);
            foreach (string line in lines)
            {
                var user = createUserFromString(line);
                library.AddUser(user);
            }
        }
        catch (FileNotFoundException e)
        {
            throw new DataImportException($"Plik {USERS_FILE_NAME} nie został znaleziony.");
        }
        catch (IOException e)
        {
            throw new DataImportException($"Błąd odczytu pliku {USERS_FILE_NAME}.");
        }
    }


    private LibraryUser createUserFromString(string csvText)
    {
        string[] split = csvText.Split(";");
        string firstName = split[0];
        string lastName = split[1];
        string pesel = split[2];
        return new LibraryUser(firstName, lastName, pesel);
    }

    private Publication createObjectFromString(string line)
    {
        string[] split = line.Split(";");
        string type = split[0];
        if (Book.Type.Equals(type))
        {
            return createBook(split);
        }
        else if (Magazine.Type.Equals(type))
        {
            return createMagazine(split);
        }

        throw new InvalidDataException("Nieznany typ publikacji " + type);
    }

    private Magazine createMagazine(string[] data)
    {
        string title = data[1];
        string publisher = data[2];
        int year = int.Parse(data[3]);
        int month = int.Parse(data[4]);
        int day = int.Parse(data[5]);
        string language = data[6];
        return new Magazine(title, publisher, language, year, month, day);
    }

    private Book createBook(string[] data)
    {
        string title = data[1];
        string publisher = data[2];
        int year = int.Parse(data[3]);
        string author = data[4];
        int pages = int.Parse(data[5]);
        string isbn = data[6];
        return new Book(title, author, year, pages, publisher, isbn);
    }

    private void exportUsers(Library library)
    {
        ICollection<LibraryUser> users = library.Users.Values;
        exportToCsv(users, USERS_FILE_NAME);
    }

    private void exportPublications(Library library)
    {
        ICollection<Publication> publications = library.Publications.Values;
        exportToCsv(publications, FILE_NAME);
    }

    private void exportToCsv<T>(ICollection<T> collection, string fileName) where T : ICsvConvertible
    {
        try
        {
            using StreamWriter file = new(fileName);
            foreach (T element in collection)
            {
                file.WriteLine(element.ToCsv());
            }
        }
        catch (IOException e)
        {
            throw new DataExportException("Błąd zapisu danych do pliku " + fileName);
        }
    }

    public void exportData(Library library)
    {
        exportPublications(library);
        exportUsers(library);
    }
}