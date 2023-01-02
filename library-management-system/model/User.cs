namespace library_management_system.model;

public abstract class User : ICsvConvertible
{
    public string FirstName { get; }
    public string LastName { get; }
    public string Pesel { get; }

    protected User(string firstName, string lastName, string pesel)
    {
        FirstName = firstName;
        LastName = lastName;
        Pesel = pesel;
    }

    protected bool Equals(User other)
    {
        return FirstName == other.FirstName && LastName == other.LastName && Pesel == other.Pesel;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((User)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FirstName, LastName, Pesel);
    }

    public override string ToString()
    {
        return FirstName + " " + LastName + " " + Pesel;
    }

    public abstract string ToCsv();
}