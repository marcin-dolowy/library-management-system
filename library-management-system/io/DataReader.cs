using library_management_system.model;

namespace library_management_system.io;

public class DataReader
{
    private readonly ConsolePrinter _printer;

    public DataReader(ConsolePrinter printer) {
        this._printer = printer;
    }

    public Book ReadAndCreateBook() {
        _printer.PrintLine("Tytuł:");
        string title = Console.ReadLine();
        _printer.PrintLine("Autor:");
        string author = Console.ReadLine();
        _printer.PrintLine("Wydawnictwo:");
        string publisher = Console.ReadLine();
        _printer.PrintLine("ISBN:");
        string isbn = Console.ReadLine();
        _printer.PrintLine("Rok wydania:");
        int releaseDate = int.Parse(Console.ReadLine());
        _printer.PrintLine("Liczba stron:");
        int pages = int.Parse(Console.ReadLine());
        return new Book(title, author, releaseDate, pages, publisher, isbn);
    }

    public Magazine ReadAndCreateMagazine() {
        _printer.PrintLine("Tytuł:");
        string title = Console.ReadLine();
        _printer.PrintLine("Wydawnictwo:");
        string publisher = Console.ReadLine();
        _printer.PrintLine("Język:");
        string language = Console.ReadLine();
        _printer.PrintLine("Rok wydania:");
        int year = int.Parse(Console.ReadLine());
        _printer.PrintLine("Miesiąc:");
        int month = int.Parse(Console.ReadLine());
        _printer.PrintLine("Dzień:");
        int day = int.Parse(Console.ReadLine());
        return new Magazine(title, publisher, language, year, month, day);
    }

    public LibraryUser CreateLibraryUser() {
        _printer.PrintLine("Imię");
        string firstName = Console.ReadLine();
        _printer.PrintLine("Nazwisko");
        string lastName = Console.ReadLine();
        _printer.PrintLine("Pesel");
        string pesel = Console.ReadLine();
        return new LibraryUser(firstName, lastName, pesel);
    }
    
    public string GetString() {
        return Console.ReadLine();
    }
    
    public int GetInt() {
        return int.Parse(Console.ReadLine());
    }
}
