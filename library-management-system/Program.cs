// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Collections.ObjectModel;
using library_management_system.io;
using library_management_system.model;

public class TestMethod
{
    public static void Main(string[] args)
    {
        ConsolePrinter consolePrinter = new ConsolePrinter();

        ICollection<LibraryUser> users = new List<LibraryUser>();
        users.Add(new LibraryUser("a", "b", "253432432342"));
        users.Add(new LibraryUser("a", "b", "253432432342"));
        users.Add(new LibraryUser("a", "b", "253432432342"));
        users.Add(new LibraryUser("a", "b", "253432432342"));
        users.Add(new LibraryUser("a", "b", "253432432342"));
        Console.WriteLine("===============");

        consolePrinter.PrintUsers(users);

        DataReader dataReader = new DataReader(consolePrinter);
        LibraryUser user = dataReader.createLibraryUser();
        users.Add(user);
        Console.WriteLine("=========================");
        consolePrinter.PrintUsers(users);
        Console.WriteLine("=========================");

        Book book = dataReader.readAndCreateBook();
        ICollection<Publication> publications = new List<Publication>();
        publications.Add(book);
        
        consolePrinter.PrintBooks(publications);
    }
}