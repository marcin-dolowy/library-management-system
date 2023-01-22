using library_management_system.Exception;

namespace library_management_system.model;

public class Library
{
    public Dictionary<string, Publication> Publications { get; } = new();
    public Dictionary<string, LibraryUser> Users { get; set; } = new();
    public List<Borrow> Borrows { get; } = new();

    public ICollection<Publication> GetSortedPublications(IComparer<Publication> comparer)
    {
        List<Publication> list = new List<Publication>(Publications.Values);
        list.Sort(comparer);
        return list;
    }

    public ICollection<LibraryUser> GetSortedUsers(IComparer<LibraryUser> comparer)
    {
        List<LibraryUser> list = new List<LibraryUser>(Users.Values);
        list.Sort(comparer);
        return list;
    }

    public void AddPublication(Publication publication)
    {
        if (Publications.ContainsKey(publication.Title))
        {
            throw new PublicationAlreadyExistsException("Publikacja o takim tytule już istnieje " + publication.Title);
        }

        Publications.Add(publication.Title, publication);
    }

    public void AddUser(LibraryUser user)
    {
        if (Users.ContainsKey(user.Pesel))
        {
            throw new UserAlreadyExistsException("Użytkownik ze wskazanym peselem już istnieje " + user.Pesel);
        }

        Users.Add(user.Pesel, user);
    }

    public bool RemovePublication(Publication pub)
    {
        if (!Publications.ContainsValue(pub)) return false;
        Publications.Remove(pub.Title);
        return true;
    }

    public void AddBorrowed(Borrow borrow)
    {
        if (Borrows.Contains(borrow))
        {
            throw new BorrowAlreadyExistsException("Dane wypożyczenie już istnieje: " + borrow);
        }

        if (!Users.ContainsKey(borrow.Pesel))
        {
            throw new NotALibraryUserException("Nie ma takiego użytkownika jak " + borrow.Pesel);
        }

        if (!Publications.ContainsKey(borrow.Title))
        {
            throw new NoSuchTitleException("Brak takiego tytułu jak " + borrow.Title);
        }

        Borrows.Add(borrow);
    }

    public bool RemoveBorrow(Borrow borrow)
    {
        if (!Borrows.Contains(borrow)) return false;
        Borrows.Remove(borrow);
        return true;
    }
}