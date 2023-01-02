using System.ComponentModel;
using library_management_system.Exception;
using library_management_system.io;
using library_management_system.io.file;
using library_management_system.model;
using InvalidDataException = System.IO.InvalidDataException;

namespace library_management_system.app;

public class LibraryControl
{
    private readonly ConsolePrinter _printer = new();
    private readonly DataReader _dataReader;
    private readonly IFileManager _fileManager;

    private readonly Library _library;

    public LibraryControl()
    {
        _dataReader = new DataReader(_printer);
        _fileManager = new FileManagerBuilder(_printer, _dataReader).Build();
        try
        {
            _library = _fileManager.ImportData();
            _printer.PrintLine("Zaimportowano dane z pliku");
        }
        catch (System.Exception e) when (e is DataImportException or InvalidDataException)
        {
            _printer.PrintLine(e.Message);
            _printer.PrintLine("Zainicjowano nową bazę.");
            _library = new Library();
        }
    }

    public void ControlLoop()
    {
        int option;
        do
        {
            PrintOptions();
            option = GetOption();
            switch (option)
            {
                case (int)Option.AddBook:
                    AddBook();
                    break;
                case (int)Option.AddMagazine:
                    AddMagazine();
                    break;
                case (int)Option.PrintBooks:
                    PrintBooks();
                    break;
                case (int)Option.PrintMagazines:
                    PrintMagazines();
                    break;
                case (int)Option.DeleteBook:
                    DeleteBook();
                    break;
                case (int)Option.DeleteMagazine:
                    DeleteMagazine();
                    break;
                case (int)Option.AddUser:
                    AddUser();
                    break;
                case (int)Option.PrintUsers:
                    PrintUsers();
                    break;
                case (int)Option.FindBook:
                    FindBook();
                    break;
                case (int)Option.Exit:
                    Exit();
                    break;
                default:
                    _printer.PrintLine("Brak takiej opcji. Wybierz poprawną.");
                    break;
            }
        } while (option != (int)Option.Exit);
    }

    private void FindBook()
    {
        _printer.PrintLine("Podaj tytuł publikacji:");
        String title = _dataReader.GetString();
        String notFoundMessage = "Brak publikacji o takim tytule";

        if (_library.Publications.ContainsKey(title))
        {
            _printer.PrintLine(_library.Publications[title].ToString());
        }
        else
        {
            _printer.PrintLine(notFoundMessage);
        }
    }

    private void PrintUsers()
    {
        _printer.PrintUsers(_library.GetSortedUsers(Comparer<LibraryUser>.Create((x, y) =>
            StringComparer.OrdinalIgnoreCase.Compare(x.LastName, y.LastName))));
    }

    private void AddUser()
    {
        LibraryUser libraryUser = _dataReader.CreateLibraryUser();
        try
        {
            _library.AddUser(libraryUser);
        }
        catch (UserAlreadyExistsException e)
        {
            _printer.PrintLine(e.Message);
        }
    }

    private int GetOption()
    {
        bool optionOk = false;
        int option = -1;
        while (!optionOk)
        {
            try
            {
                option = _dataReader.GetInt();
                optionOk = true;
            }
            catch (NoSuchOptionException)
            {
                _printer.PrintLine("Wprowadzono błędną wartość, podaj ponownie:");
            }
        }

        return option;
    }

    private void PrintMagazines()
    {
        _printer.PrintMagazines(_library.GetSortedPublications(Comparer<Publication>.Create((x, y) =>
            StringComparer.OrdinalIgnoreCase.Compare(x.Title, y.Title))));
    }

    private void AddMagazine()
    {
        try
        {
            Magazine magazine = _dataReader.ReadAndCreateMagazine();
            _library.AddPublication(magazine);
        }
        catch (System.Exception)
        {
            _printer.PrintLine("Nie udało się utworzyć magazynu, niepoprawne dane.");
        }
    }

    private void DeleteMagazine()
    {
        try
        {
            Magazine magazine = _dataReader.ReadAndCreateMagazine();
            _printer.PrintLine(_library.RemovePublication(magazine) ? "Usunięto magazyn" : "Brak wskazanego magazynu");
        }
        catch (System.Exception)
        {
            _printer.PrintLine("Nie udało się utworzyć magazynu, niepoprawne dane");
        }
    }

    private void Exit()
    {
        try
        {
            _fileManager.ExportData(_library);
            _printer.PrintLine("Export danych do pliku zakończony powodzeniem");
        }
        catch (DataExportException e)
        {
            _printer.PrintLine(e.Message);
        }

        _printer.PrintLine("Koniec programu, papa!");
    }

    private void PrintBooks()
    {
        _printer.PrintBooks(_library.GetSortedPublications(Comparer<Publication>.Create((x, y) =>
            StringComparer.OrdinalIgnoreCase.Compare(x.Title, y.Title))));
    }

    private void AddBook()
    {
        try
        {
            Book book = _dataReader.ReadAndCreateBook();
            _library.AddPublication(book);
        }
        catch (System.Exception)
        {
            _printer.PrintLine("Nie udało się utworzyć książki, niepoprawne dane.");
        }
    }

    private void DeleteBook()
    {
        try
        {
            Book book = _dataReader.ReadAndCreateBook();
            _printer.PrintLine(_library.RemovePublication(book) ? "Usunięto książkę" : "Brak wskazanej książki");
        }
        catch (System.Exception)
        {
            _printer.PrintLine("Nie udało się utworzyć książki, niepoprawne dane");
        }
    }

    private void PrintOptions()
    {
        _printer.PrintLine("Wybierz opcje:");
        foreach (Option value in Enum.GetValues(typeof(Option)))
        {
            _printer.PrintLine($"{(int)value} - {GetEnumDescription(value)}");
        }
    }

    private static string GetEnumDescription(Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attributes = (DescriptionAttribute[])fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false)!;
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }

    private enum Option
    {
        [Description("wyjście z programu")] Exit = 0,
        [Description("dodanie nowej książki")] AddBook = 1,

        [Description("dodanie nowego magazynu")]
        AddMagazine = 2,

        [Description("wyświetl dostępne książki")]
        PrintBooks = 3,

        [Description("wyświetl dostępne magazyny")]
        PrintMagazines = 4,
        [Description("Usuń książkę")] DeleteBook = 5,
        [Description("Usuń magazyn")] DeleteMagazine = 6,
        [Description("Dodaj czytelnika")] AddUser = 7,
        [Description("Wyświetl czytelników")] PrintUsers = 8,
        [Description("Wyszukaj książkę")] FindBook = 9
    }
}