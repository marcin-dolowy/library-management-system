using System.Collections;

namespace library_management_system.model;

public class LibraryUser : User
{
    private List<Publication> publicationHistory = new List<Publication>();
    private List<Publication> borrowedPublications = new List<Publication>();

    public LibraryUser(string firstName, string lastName, string pesel) : base(firstName, lastName, pesel)
    {
    }
    
    public void addPublicationToHistory(Publication pub) {
        publicationHistory.Add(pub);
    }

    public void borrowPublication(Publication pub) {
        borrowedPublications.Add(pub);
    }

    public bool returnPublication(Publication pub) {
        bool result = false;
        if (borrowedPublications.Contains(pub)) {
            borrowedPublications.Remove(pub);
            addPublicationToHistory(pub);
            result = true;
        }
        return result;
    }

    protected bool Equals(LibraryUser other)
    {
        return base.Equals(other) && publicationHistory.Equals(other.publicationHistory) && borrowedPublications.Equals(other.borrowedPublications);
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
        return HashCode.Combine(base.GetHashCode(), publicationHistory, borrowedPublications);
    }

    public override string toCsv()
    {
        return FirstName + ";" + LastName + ";" + Pesel;
    }
}