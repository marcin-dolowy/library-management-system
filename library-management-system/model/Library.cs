using System.Collections.ObjectModel;

namespace library_management_system.model;

public class Library
{
    private Dictionary<string, Publication> publications = new Dictionary<string, Publication>();
    private Dictionary<string, LibraryUser> users = new Dictionary<string, LibraryUser>();

    public Dictionary<string, Publication> Publications
    {
        get { return publications; }
    }

    public Dictionary<string, LibraryUser> Users
    {
        get { return users; }
    }

    public ICollection<Publication> getSortedPublications(IComparer<Publication> comparer)
    {
        List<Publication> list = new List<Publication>(publications.Values);
        list.Sort(comparer);
        return list;
    }

    public Publication findPublicationByTitle(String title)
    {
        return publications[title];
    }

    public ICollection<LibraryUser> getSortedUsers(IComparer<LibraryUser> comparator)
    {
        List<LibraryUser> list = new List<LibraryUser>(users.Values);
        list.Sort(comparator);
        return list;
    }
}