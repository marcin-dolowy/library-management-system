namespace library_management_system.model;

public abstract class User : ICsvConvertible
{
    protected string FirstName { get; }
    public string LastName { get; }
    public string Pesel { get; }
    public string Password { get; }

    protected User(string firstName, string lastName, string pesel, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Pesel = pesel;
        Password = password;
    }

    private bool Equals(User other)
    {
        return FirstName == other.FirstName && LastName == other.LastName && Pesel == other.Pesel &&
               Password == other.Password;
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
        return HashCode.Combine(FirstName, LastName, Pesel, Password);
    }

    public override string ToString()
    {
        return FirstName + " " + LastName + " " + Pesel;
    }

    public abstract string ToCsv();
}