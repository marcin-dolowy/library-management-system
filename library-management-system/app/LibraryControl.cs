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

    public Library Library { get; set; }
    public LibraryUser CurrentUser { get; set; }
    public bool IsAdmin { get; set; }

    public LibraryControl(LibraryUser currentUser, bool isAdmin)
    {
        CurrentUser = currentUser;
        IsAdmin = isAdmin;
        _dataReader = new DataReader(_printer);
        _fileManager = new FileManagerBuilder(_printer, _dataReader).Build();
        try
        {
            Library = _fileManager.ImportData();
            _printer.PrintLine("Zaimportowano dane z pliku");
        }
        catch (System.Exception e) when (e is DataImportException or InvalidDataException)
        {
            _printer.PrintLine(e.Message);
            _printer.PrintLine("Zainicjowano nową bazę.");
            Library = new Library();
        }
    }

    public void ControlLoop()
    {
        int option;
        do
        {
            PrintOptions();
            option = GetOption();
            if (IsAdmin)
            {
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
                    case (int)Option.PrintBorrowed:
                        PrintBorrowed();
                        break;
                    case (int)Option.BorrowPublication:
                        BorrowPublication();
                        break;
                    case (int)Option.ReturnPublication:
                        ReturnPublication();
                        break;
                    default:
                        _printer.PrintLine("Brak takiej opcji. Wybierz poprawną.");
                        break;
                }
            }
            else
            {
                switch (option)
                {
                    case (int)Option.PrintBooks:
                        PrintBooks();
                        break;
                    case (int)Option.PrintMagazines:
                        PrintMagazines();
                        break;
                    case (int)Option.FindBook:
                        FindBook();
                        break;
                    case (int)Option.Exit:
                        Exit();
                        break;
                    case (int)Option.PrintBorrowed:
                        PrintBorrowed();
                        break;
                    case (int)Option.BorrowPublication:
                        BorrowPublication();
                        break;
                    case (int)Option.ReturnPublication:
                        ReturnPublication();
                        break;
                    default:
                        _printer.PrintLine("Brak takiej opcji. Wybierz poprawną.");
                        break;
                }
            }
        } while (option != (int)Option.Exit);
    }

    private void ReturnPublication()
    {
        try
        {
            if (IsAdmin)
            {
                _printer.PrintLine("Podaj pesel: ");
                string pesel = _dataReader.GetString();
                _printer.PrintLine("Podaj tytuł: ");
                string title = _dataReader.GetString();

                Borrow? borrow = Library.Borrows.FirstOrDefault(b => b.Title == title && b.Pesel == pesel);

                _printer.PrintLine(Library.RemoveBorrow(borrow!)
                    ? "Usunięto wypożyczenie"
                    : "Brak wskazanego wypożyczenia");
            }
            else
            {
                _printer.PrintLine("Podaj tytuł: ");
                string title = _dataReader.GetString();

                Borrow? borrow = Library.Borrows.FirstOrDefault(b => b.Title == title && b.Pesel == CurrentUser.Pesel);

                _printer.PrintLine(Library.RemoveBorrow(borrow!)
                    ? "Usunięto wypożyczenie"
                    : "Brak wskazanego wypożyczenia");
            }
        }
        catch (System.Exception)
        {
            _printer.PrintLine("Niepoprawne dane");
        }
    }

    private void BorrowPublication()
    {
        if (IsAdmin)
        {
            Borrow borrow = _dataReader.CreateBorrow();
            try
            {
                Library.AddBorrowed(borrow);
            }
            catch (NoSuchTitleException e)
            {
                _printer.PrintLine(e.Message);
            }
            catch (NotALibraryUserException e)
            {
                _printer.PrintLine(e.Message);
            }
            catch (BorrowAlreadyExistsException e)
            {
                _printer.PrintLine(e.Message);
            }
        }
        else
        {
            _printer.PrintLine("Podaj tytuł publikacji:");
            String title = _dataReader.GetString();
            try
            {
                Library.AddBorrowed(new Borrow(CurrentUser.Pesel, title));
                _printer.PrintLine("Pomyślnie wypożyczono");
            }
            catch (NoSuchTitleException e)
            {
                _printer.PrintLine(e.Message);
            }
            catch (NotALibraryUserException e)
            {
                _printer.PrintLine(e.Message);
            }
            catch (BorrowAlreadyExistsException e)
            {
                _printer.PrintLine(e.Message);
            }
        }
    }

    private void PrintBorrowed()
    {
        if (IsAdmin)
        {
            foreach (Borrow borrow in Library.Borrows)
            {
                _printer.PrintLine(borrow.ToString());
            }
        }
        else
        {
            foreach (Borrow borrow in Library.Borrows.Where(b => b.Pesel == CurrentUser.Pesel))
            {
                _printer.PrintLine(borrow.ToString());
            }
        }
    }

    private void FindBook()
    {
        _printer.PrintLine("Podaj tytuł publikacji:");
        String title = _dataReader.GetString();
        String notFoundMessage = "Brak publikacji o takim tytule";

        if (Library.Publications.ContainsKey(title))
        {
            _printer.PrintLine(Library.Publications[title].ToString());
        }
        else
        {
            _printer.PrintLine(notFoundMessage);
        }
    }

    private void PrintUsers()
    {
        _printer.PrintUsers(Library.GetSortedUsers(Comparer<LibraryUser>.Create((x, y) =>
            StringComparer.OrdinalIgnoreCase.Compare(x.LastName, y.LastName))));
    }

    private void AddUser()
    {
        LibraryUser libraryUser = _dataReader.CreateLibraryUser();
        try
        {
            Library.AddUser(libraryUser);
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
        _printer.PrintMagazines(Library.GetSortedPublications(Comparer<Publication>.Create((x, y) =>
            StringComparer.OrdinalIgnoreCase.Compare(x.Title, y.Title))));
    }

    private void AddMagazine()
    {
        try
        {
            Magazine magazine = _dataReader.ReadAndCreateMagazine();
            Library.AddPublication(magazine);
        }
        catch (PublicationAlreadyExistsException e)
        {
            _printer.PrintLine(e.Message);
        }
    }

    private void DeleteMagazine()
    {
        try
        {
            string title = _dataReader.ReadTitleFromMagazine();
            Publication magazine = Library.Publications.Values.Where(p => p is Magazine)
                .SingleOrDefault(b => b.Title == title)!;

            _printer.PrintLine(Library.RemovePublication(magazine!) ? "Usunięto magazyn" : "Brak wskazanego magazynu");
        }
        catch (System.Exception)
        {
            _printer.PrintLine("Nie udało się utworzyć magazynu, niepoprawne dane");
        }
    }

    public void Exit()
    {
        try
        {
            _fileManager.ExportData(Library);
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
        _printer.PrintBooks(Library.GetSortedPublications(Comparer<Publication>.Create((x, y) =>
            StringComparer.OrdinalIgnoreCase.Compare(x.Title, y.Title))));
    }

    private void AddBook()
    {
        try
        {
            Book book = _dataReader.ReadAndCreateBook();
            Library.AddPublication(book);
        }
        catch (System.Exception e)
        {
            _printer.PrintLine(e.Message);
        }
    }

    private void DeleteBook()
    {
        try
        {
            var booksFromPublications =
                Library.Publications.Values.Where(p => p is Book).ToList().Cast<Book>().ToList();
            var isbn = _dataReader.ReadIsbnFromBook();
            var book = booksFromPublications.FirstOrDefault(b => b.Isbn == isbn);

            _printer.PrintLine(Library.RemovePublication(book!) ? "Usunięto książkę" : "Brak wskazanej książki");
        }
        catch (System.Exception)
        {
            _printer.PrintLine("Nie udało się utworzyć książki, niepoprawne dane");
        }
    }

    private void PrintOptions()
    {
        _printer.PrintLine("Wybierz opcje:");
        if (IsAdmin)
        {
            foreach (Option value in Enum.GetValues(typeof(Option)))
            {
                _printer.PrintLine($"{(int)value} - {GetEnumDescription(value)}");
            }
        }
        else
        {
            int i = 0;
            foreach (Option value in Enum.GetValues(typeof(Option)))
            {
                _printer.PrintLine($"{(int)value} - {GetEnumDescription(value)}");
                i++;
                if (i == 7)
                {
                    break;
                }
            }
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
        [Description("Wyjście z programu")] Exit = 0,

        [Description("Wyświetl dostępne książki")]
        PrintBooks = 1,

        [Description("Wyświetl dostępne magazyny")]
        PrintMagazines = 2,
        [Description("Wypożycz publikacje")] BorrowPublication = 3,
        [Description("Zwróć publikacje")] ReturnPublication = 4,
        [Description("Wyszukaj publikacje")] FindBook = 5,

        [Description("Wyświetl wypożyczone publikacje")]
        PrintBorrowed = 6,

        [Description("Dodanie nowej książki")] AddBook = 7,

        [Description("Dodanie nowego magazynu")]
        AddMagazine = 8,
        [Description("Usuń książkę")] DeleteBook = 9,
        [Description("Usuń magazyn")] DeleteMagazine = 10,
        [Description("Dodaj czytelnika")] AddUser = 11,
        [Description("Wyświetl czytelników")] PrintUsers = 12
    }
}