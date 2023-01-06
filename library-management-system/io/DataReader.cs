using System.Text.RegularExpressions;
using library_management_system.model;
using InvalidDataException = library_management_system.Exception.InvalidDataException;

namespace library_management_system.io;

public class DataReader
{
    private readonly ConsolePrinter _printer;

    public DataReader(ConsolePrinter printer)
    {
        _printer = printer;
    }

    public Book ReadAndCreateBook()
    {
        _printer.PrintLine("Tytuł:");
        string title = GetString();
        _printer.PrintLine("Autor:");
        string author = GetString();
        _printer.PrintLine("Wydawnictwo:");
        string publisher = GetString();
        _printer.PrintLine("ISBN:");
        string isbn = GetString();
        if (!IsValidIsbn(isbn))
            throw new InvalidDataException("ISBN musi składać się z 10 lub 13 cyfr!");
        _printer.PrintLine("Rok wydania:");
        int releaseDate = GetInt();
        _printer.PrintLine("Liczba stron:");
        int pages = GetInt();
        return new Book(title, author, releaseDate, pages, publisher, isbn);
    }

    public Magazine ReadAndCreateMagazine()
    {
        _printer.PrintLine("Tytuł:");
        string title = GetString();
        _printer.PrintLine("Wydawnictwo:");
        string publisher = GetString();
        _printer.PrintLine("Język:");
        string language = GetString();
        _printer.PrintLine("Rok wydania:");
        int year = GetInt();
        _printer.PrintLine("Miesiąc:");
        int month = GetInt();
        _printer.PrintLine("Dzień:");
        int day = GetInt();
        return new Magazine(title, publisher, language, year, month, day);
    }

    public LibraryUser CreateLibraryUser()
    {
        _printer.PrintLine("Imię");
        string firstName = GetString();
        _printer.PrintLine("Nazwisko");
        string lastName = GetString();
        _printer.PrintLine("Pesel");
        string pesel = GetString();
        _printer.PrintLine("Hasło");
        string password = GetString();
        return new LibraryUser(firstName, lastName, pesel, password);
    }
    
    public Borrow CreateBorrow()
    {
        _printer.PrintLine("Pesel");
        string pesel = GetString();
        _printer.PrintLine("Tytuł");
        string title = GetString();
        return new Borrow(pesel, title);
    }

    public string ReadIsbnFromBook()
    {
        _printer.PrintLine("Wprowadź ISBN książki, którą chcesz usunąć");
        return GetString();
    }

    public string ReadTitleFromMagazine()
    {
        _printer.PrintLine("Podaj tytul magazynu, który chcesz usunąć");
        return GetString();
    }

    public string GetString()
    {
        return Console.ReadLine()!;
    }

    public int GetInt()
    {
        return int.Parse(Console.ReadLine()!);
    }

    private static bool IsValidIsbn(string isbn)
    {
        if (isbn.Length != 10 && isbn.Length != 13)
        {
            return false;
        }

        return Regex.IsMatch(isbn, @"^\d{9}[\d|X]$|^\d{12}[\d|X]$");
    }

    public bool ContainsOnlyDigits(string input)
    {
        return Regex.IsMatch(input, @"^\d+$");
    }
    
}