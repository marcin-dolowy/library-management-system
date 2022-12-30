namespace library_management_system.model;

public abstract class User : CsvConvertible
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string pesel { get; set; }

    protected User(string firstName, string lastName, string pesel)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.pesel = pesel;
    }

    protected bool Equals(User other)
    {
        return firstName == other.firstName && lastName == other.lastName && pesel == other.pesel;
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
        return HashCode.Combine(firstName, lastName, pesel);
    }

    public override string ToString()
    {
        return firstName + " " + lastName + " " + pesel;
    }

    public abstract string toCsv();
}