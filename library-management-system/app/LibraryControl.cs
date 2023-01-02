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
                case 0:
                    addBook();
                    break;
                case 1:
                    addMagazine();
                    break;
                case 2:
                    printBooks();
                    break;
                case 3:
                    printMagazines();
                    break;
                case 4:
                    deleteBook();
                    break;
                case 5:
                    deleteMagazine();
                    break;
                case 6:
                    addUser();
                    break;
                case 7:
                    printUsers();
                    break;
                case 8:
                    findBook();
                    break;
                case 9:
                    exit();
                    break;
                default:
                    printer.PrintLine("Brak takiej opcji. Wybierz poprawną.");
                    break;
            }
        } while (option != 9);
    }

    private void findBook() // DONE
    {
        printer.PrintLine("Podaj tytuł publikacji:");
        String title = dataReader.GetString();
        String notFoundMessage = "Brak publikacji o takim tytule";

        // Publication publication = library.findPublicationByTitle(title);
        // if (publication != null)
        // {
        //     printer.PrintLine(publication.ToString());
        // }
        // else
        // {
        //     printer.PrintLine(notFoundMessage);
        // }

        if (library.Publications.ContainsKey(title))
        {
            printer.PrintLine(library.Publications[title].ToString());
        }
        else
        {
            printer.PrintLine(notFoundMessage);
        }

        // library.findPublicationByTitle(title)
        //     .map(Publication::toString)
        //     .ifPresentOrElse(System.out::println, () -> System.out.println(notFoundMessage));
    }

    private void printUsers() // NIE WIEM JAK TO ZROBIC
    {
        // printer.printUsers(library.getSortedUsers(
        //         (p1, p2) -> p1.getLastName().compareToIgnoreCase(p2.getLastName())
        //     Comparator.comparing(User::getLastName, String.CASE_INSENSITIVE_ORDER)
        // ));

        // printer.PrintUsers(library.getSortedUsers((p1, p2) => p1.LastName.CompareTo
        // (p2.LastName, StringComparison.OrdinalIgnoreCase)));
        
        //DO SPRAWDZENIA!!!!!!!!!!
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
//         printer.PrintMagazines(library.getSortedPublications(
// //                (p1, p2) -> p1.getTitle().compareToIgnoreCase(p2.getTitle())
//             IComparator.comparing(Publication::getTitle, String.CASE_INSENSITIVE_ORDER)
//         ));


        //??????????????
        // printer.PrintUsers(library.getSortedUsers(
        //     (x, y) => string.Compare(x.LastName, y.LastName, StringComparison.OrdinalIgnoreCase)
        // ));

        // DO SPRAWDZENIA!!!!!!!!!
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
//         printer.PrintLine(library.getSortedPublications(
// //                (p1, p2) -> p1.getTitle().compareToIgnoreCase(p2.getTitle())
//             Comparator.comparing(Publication::getTitle, String.CASE_INSENSITIVE_ORDER)
//         ));


        // DO SPRAWDZENIA!!!!!!!!!
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
        string[] options =
        {
            "wyjście z programu", "dodanie nowej książki", "dodanie nowego magazynu", "wyświetl dostępne książki",
            "wyświetl dostępne magazyny", "Usuń książkę", "Usuń magazyn", "Dodaj czytelnika", "Wyświetl czytelników",
            "Wyszukaj książkę"
        };
        foreach (var option in options)
        {
            printer.PrintLine(option);
        }
    }
}