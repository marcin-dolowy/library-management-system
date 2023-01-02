using System.ComponentModel;
using System.Security.Cryptography;
using library_management_system.Exception;
using library_management_system.io;
using library_management_system.io.file;
using library_management_system.model;
using InvalidDataException = System.IO.InvalidDataException;

namespace library_management_system.app;

class LibraryControl
{
    private ConsolePrinter printer = new ConsolePrinter();
    private DataReader dataReader;
    private FileManager fileManager;

    private Library library;

    public LibraryControl()
    {
        dataReader = new DataReader(printer);
        fileManager = new FileManagerBuilder(printer, dataReader).build();
        try
        {
            library = fileManager.importData();
            printer.PrintLine("Zaimportowano dane z pliku");
        }
        catch (System.Exception e) when (e is DataImportException or InvalidDataException)
        {
            printer.PrintLine(e.Message);
            printer.PrintLine("Zainicjowano nową bazę.");
            library = new Library();
        }
    }

    public void controlLoop()
    {
        int option;
        do
        {
            printOptions();
            option = getOption();
            switch (option)
            {
                case (int) Option.ADD_BOOK:
                    addBook();
                    break;
                case (int) Option.ADD_MAGAZINE:
                    addMagazine();
                    break;
                case (int) Option.PRINT_BOOKS:
                    printBooks();
                    break;
                case (int) Option.PRINT_MAGAZINES:
                    printMagazines();
                    break;
                case (int) Option.DELETE_BOOK:
                    deleteBook();
                    break;
                case (int) Option.DELETE_MAGAZINE:
                    deleteMagazine();
                    break;
                case (int) Option.ADD_USER:
                    addUser();
                    break;
                case (int) Option.PRINT_USERS:
                    printUsers();
                    break;
                case (int) Option.FIND_BOOK:
                    findBook();
                    break;
                case (int) Option.EXIT:
                    exit();
                    break;
                default:
                    printer.PrintLine("Brak takiej opcji. Wybierz poprawną.");
                    break;
            }
        } while (option != (int) Option.EXIT);
    }

    private void findBook() // DONE
    {
        printer.PrintLine("Podaj tytuł publikacji:");
        String title = dataReader.GetString();
        String notFoundMessage = "Brak publikacji o takim tytule";

        if (library.Publications.ContainsKey(title))
        {
            printer.PrintLine(library.Publications[title].ToString());
        }
        else
        {
            printer.PrintLine(notFoundMessage);
        }
    }

    private void printUsers()
    {
        printer.PrintUsers(library.getSortedUsers(Comparer<LibraryUser>.Create((x, y) =>
            StringComparer.OrdinalIgnoreCase.Compare(x.LastName, y.LastName))));
    }

    private void addUser()
    {
        LibraryUser libraryUser = dataReader.createLibraryUser();
        try
        {
            library.addUser(libraryUser);
        }
        catch (UserAlreadyExistsException e)
        {
            printer.PrintLine(e.Message);
        }
    }

    private int getOption()
    {
        bool optionOk = false;
        int option = -1;
        while (!optionOk)
        {
            try
            {
                option = dataReader.GetInt();
                optionOk = true;
            }
            catch (NoSuchOptionException e)
            {
                printer.PrintLine("Wprowadzono błędną wartość, podaj ponownie:");
            }
        }

        return option;
    }

    private void printMagazines()
    {
        printer.PrintMagazines(library.getSortedPublications(Comparer<Publication>.Create((x, y) =>
            StringComparer.OrdinalIgnoreCase.Compare(x.Title, y.Title))));
    }

    private void addMagazine()
    {
        try
        {
            Magazine magazine = dataReader.readAndCreateMagazine();
            library.addPublication(magazine);
        }
        catch (System.Exception e)
        {
            printer.PrintLine("Nie udało się utworzyć magazynu, niepoprawne dane.");
        }
    }

    private void deleteMagazine()
    {
        try
        {
            Magazine magazine = dataReader.readAndCreateMagazine();
            if (library.removePublication(magazine))
                printer.PrintLine("Usunięto magazyn");
            else
                printer.PrintLine("Brak wskazanego magazynu");
        }
        catch (System.Exception e)
        {
            printer.PrintLine("Nie udało się utworzyć magazynu, niepoprawne dane");
        }
    }

    private void exit()
    {
        try
        {
            fileManager.exportData(library);
            printer.PrintLine("Export danych do pliku zakończony powodzeniem");
        }
        catch (DataExportException e)
        {
            printer.PrintLine(e.Message);
        }

        printer.PrintLine("Koniec programu, papa!");
    }

    private void printBooks()
    {
        printer.PrintBooks(library.getSortedPublications(Comparer<Publication>.Create((x, y) =>
            StringComparer.OrdinalIgnoreCase.Compare(x.Title, y.Title))));
    }

    private void addBook()
    {
        try
        {
            Book book = dataReader.readAndCreateBook();
            library.addPublication(book);
        }
        catch (System.Exception e)
        {
            printer.PrintLine("Nie udało się utworzyć książki, niepoprawne dane.");
        }
    }

    private void deleteBook()
    {
        try
        {
            Book book = dataReader.readAndCreateBook();
            if (library.removePublication(book))
                printer.PrintLine("Usunięto książkę");
            else
                printer.PrintLine("Brak wskazanej książki");
        }
        catch (System.Exception e)
        {
            printer.PrintLine("Nie udało się utworzyć książki, niepoprawne dane");
        }
    }

    private void printOptions()
    {
        printer.PrintLine("Wybierz opcje:");
        foreach (Option value in Enum.GetValues(typeof(Option)))
        {
            printer.PrintLine($"{(int)value} - {GetEnumDescription(value)}");
        }
    }
    
    public static string GetEnumDescription(Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
    
    private enum Option {
        [Description("wyjście z programu")]
        EXIT = 0,
        [Description("dodanie nowej książki")]
        ADD_BOOK = 1,
        [Description("dodanie nowego magazynu")]
        ADD_MAGAZINE = 2,
        [Description("wyświetl dostępne książki")]
        PRINT_BOOKS = 3,
        [Description("wyświetl dostępne magazyny")]
        PRINT_MAGAZINES = 4,
        [Description("Usuń książkę")]
        DELETE_BOOK = 5,
        [Description("Usuń magazyn")]
        DELETE_MAGAZINE = 6,
        [Description("Dodaj czytelnika")]
        ADD_USER = 7,
        [Description("Wyświetl czytelników")]
        PRINT_USERS = 8,
        [Description("Wyszukaj książkę")]
        FIND_BOOK = 9

        
    }
}