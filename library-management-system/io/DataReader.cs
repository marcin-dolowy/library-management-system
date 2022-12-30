using library_management_system.model;

namespace library_management_system.io;

public class DataReader
{
    private ConsolePrinter printer;

    public DataReader(ConsolePrinter printer) {
        this.printer = printer;
    }

    public Book readAndCreateBook() {
        printer.PrintLine("Tytuł:");
        string title = Console.ReadLine();
        printer.PrintLine("Autor:");
        string author = Console.ReadLine();
        printer.PrintLine("Wydawnictwo:");
        string publisher = Console.ReadLine();
        printer.PrintLine("ISBN:");
        string isbn = Console.ReadLine();
        printer.PrintLine("Rok wydania:");
        int releaseDate = int.Parse(Console.ReadLine());
        printer.PrintLine("Liczba stron:");
        int pages = int.Parse(Console.ReadLine());
        return new Book(title, author, releaseDate, pages, publisher, isbn);
    }

    public Magazine readAndCreateMagazine() {
        printer.PrintLine("Tytuł:");
        string title = Console.ReadLine();
        printer.PrintLine("Wydawnictwo:");
        string publisher = Console.ReadLine();
        printer.PrintLine("Język:");
        string language = Console.ReadLine();
        printer.PrintLine("Rok wydania:");
        int year = int.Parse(Console.ReadLine());
        printer.PrintLine("Miesiąc:");
        int month = int.Parse(Console.ReadLine());
        printer.PrintLine("Dzień:");
        int day = int.Parse(Console.ReadLine());
        return new Magazine(title, publisher, language, year, month, day);
    }

    public LibraryUser createLibraryUser() {
        printer.PrintLine("Imię");
        string firstName = Console.ReadLine();
        printer.PrintLine("Nazwisko");
        string lastName = Console.ReadLine();
        printer.PrintLine("Pesel");
        string pesel = Console.ReadLine();
        return new LibraryUser(firstName, lastName, pesel);
    }
    
    public String GetString() {
        return Console.ReadLine();
    }
}
