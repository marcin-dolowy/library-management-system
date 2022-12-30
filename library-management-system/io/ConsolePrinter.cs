using library_management_system.model;

namespace library_management_system.io;

public class ConsolePrinter
{
    public void PrintBooks(ICollection<Publication> publications)
    {
        long count = publications.Where(p => p is Book).Select(p => p.ToString()).Select(i =>
        {
            PrintLine(i);
            return i;
        }).Count();

        if (count == 0)
        {
            PrintLine("Brak ksiazek w bibliotece.");
        }
    }

    public void PrintMagazines(ICollection<Publication> publications)
    {
        long count = publications.Where(p => p is Magazine).Select(p => p.ToString()).Select(i =>
        {
            PrintLine(i);
            return i;
        }).Count();

        if (count == 0)
        {
            PrintLine("Brak magazynow w bibliotece.");
        }
    }

    public void PrintUsers(ICollection<LibraryUser> users)
    {
        users
            .Select(user => user.ToString())
            .ToList()
            .ForEach(PrintLine);
    }

    public void PrintLine(string text)
    {
        Console.WriteLine(text.ToUpper());
    }
}