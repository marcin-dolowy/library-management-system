using library_management_system.Exception;

namespace library_management_system.model;

public record Library
{
    public Dictionary<string, Publication> Publications { get; } = new();
    public Dictionary<string, LibraryUser> Users { get; set; } = new();
    public List<Borrow> Borrows { get; } = new();

    public IEnumerable<Publication> GetSortedPublications(IComparer<Publication> comparer)
    {
        List<Publication> list = new List<Publication>(Publications.Values);
        list.Sort(comparer);
        return list;
    }

    public IEnumerable<LibraryUser> GetSortedUsers(IComparer<LibraryUser> comparer)
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

    public virtual bool Equals(Library? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Publications.Equals(other.Publications) && Users.Equals(other.Users) && Borrows.Equals(other.Borrows);
    }

    private sealed class PublicationsUsersBorrowsEqualityComparer : IEqualityComparer<Library>
    {
        public bool Equals(Library x, Library y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Publications.Equals(y.Publications) && x.Users.Equals(y.Users) && x.Borrows.Equals(y.Borrows);
        }

        public int GetHashCode(Library obj)
        {
            return HashCode.Combine(obj.Publications, obj.Users, obj.Borrows);
        }
    }

    public static IEqualityComparer<Library> PublicationsUsersBorrowsComparer { get; } = new PublicationsUsersBorrowsEqualityComparer();

    public override int GetHashCode()
    {
        return HashCode.Combine(Publications, Users, Borrows);
    }
}