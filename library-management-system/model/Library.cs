using System.Collections.ObjectModel;
using library_management_system.Exception;

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

    // public Publication findPublicationByTitle(String title)
    // {
    //     return publications[title];
    // }

    public ICollection<LibraryUser> getSortedUsers(IComparer<LibraryUser> comparator)
    {
        List<LibraryUser> list = new List<LibraryUser>(users.Values);
        list.Sort(comparator);
        return list;
    }
    
    public void addPublication (Publication publication) {
        if (publications.ContainsKey(publication.Title)) {
            throw new PublicationAlreadyExistsException("Publikacja o takim tytule już istnieje " + publication.Title);
        }
        publications.Add(publication.Title, publication);
    }

    public void addUser(LibraryUser user) {
        if (users.ContainsKey(user.Pesel)) {
            throw new UserAlreadyExistsException("Użytkownik ze wskazanym peselem już istnieje " + user.Pesel);
        }
        users.Add(user.Pesel, user);
    }
    
    public bool removePublication(Publication pub) {
        if (publications.ContainsValue(pub)) {
            publications.Remove(pub.Title);
            return true;
        } else {
            return false;
        }
    }
}