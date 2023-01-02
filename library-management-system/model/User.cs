namespace library_management_system.model;

public abstract class User : CsvConvertible
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Pesel { get; set; }

    protected User(string firstName, string lastName, string pesel)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Pesel = pesel;
    }

    protected bool Equals(User other)
    {
        return FirstName == other.FirstName && LastName == other.LastName && Pesel == other.Pesel;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
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

    public abstract string toCsv();
}