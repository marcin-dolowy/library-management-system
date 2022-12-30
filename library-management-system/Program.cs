// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Collections.ObjectModel;
using library_management_system.io;
using library_management_system.model;

public class TestMethod
{
    public static void Main(string[] args)
    {
        ICollection<Publication> publications = new List<Publication>();
        publications.Add(new Book(1,"a","a", "a", 5, "abc"));
        publications.Add(new Book(2,"a","a", "a", 5, "abc"));
        publications.Add(new Magazine(3, "abc", "abc", 6, "polish"));
        publications.Add(new Magazine(4, "abc", "abc", 6, "abc"));
        publications.Add(new Magazine(5, "abc", "abc", 6, "abve"));
        publications.Add(new Book(6,"a","a", "a", 5, "abc"));
        publications.Add(new Book(7,"a","a", "a", 5, "abc"));
        publications.Add(new Book(8,"a","a", "a", 5, "abc"));

        ConsolePrinter consolePrinter = new ConsolePrinter();
        consolePrinter.PrintBooks(publications);
        Console.WriteLine("===============");
        consolePrinter.PrintMagazines(publications);

        ICollection<LibraryUser> users = new List<LibraryUser>();
        users.Add(new LibraryUser("a", "b", "253432432342"));
        users.Add(new LibraryUser("a", "b", "253432432342"));
        users.Add(new LibraryUser("a", "b", "253432432342"));
        users.Add(new LibraryUser("a", "b", "253432432342"));
        users.Add(new LibraryUser("a", "b", "253432432342"));
        Console.WriteLine("===============");

        consolePrinter.PrintUsers(users);
    }
}