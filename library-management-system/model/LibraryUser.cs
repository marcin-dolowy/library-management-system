namespace library_management_system.model;

public class LibraryUser : User
{
    private readonly List<Publication> _publicationHistory = new();
    private readonly List<Publication> _borrowedPublications = new();

    public LibraryUser(string firstName, string lastName, string pesel, string password) : base(firstName, lastName, pesel, password)
    {
    }

    public void AddPublicationToHistory(Publication pub)
    {
        _publicationHistory.Add(pub);
    }

    public void BorrowPublication(Publication pub)
    {
        _borrowedPublications.Add(pub);
    }

    public bool ReturnPublication(Publication pub)
    {
        bool result = false;
        if (_borrowedPublications.Contains(pub))
        {
            _borrowedPublications.Remove(pub);
            AddPublicationToHistory(pub);
            result = true;
        }

        return result;
    }

    private bool Equals(LibraryUser other)
    {
        return base.Equals(other) && _publicationHistory.Equals(other._publicationHistory) &&
               _borrowedPublications.Equals(other._borrowedPublications);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((LibraryUser)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), _publicationHistory, _borrowedPublications);
    }

    public override string ToCsv()
    {
        return FirstName + ";" + LastName + ";" + Pesel + ";" + Password;
    }
}