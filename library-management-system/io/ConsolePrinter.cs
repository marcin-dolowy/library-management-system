using library_management_system.model;

namespace library_management_system.io;

public class ConsolePrinter
{
    public void PrintBooks(IEnumerable<Publication> publications)
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

    public void PrintMagazines(IEnumerable<Publication> publications)
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

    public void PrintUsers(IEnumerable<LibraryUser> users)
    {
        users
            .Select(user => user.ToString().ChangeSpacesToDash())
            .ToList()
            .ForEach(PrintLine);
    }

    public void PrintLine(string text)
    {
        Console.WriteLine(text.ToUpper());
    }
}