namespace library_management_system_login.model;

public class User
{
    private string FirstName { get; }
    private string LastName { get; }
    public string Pesel { get; }
    public string Password { get; }

    public User(string firstName, string lastName, string pesel, string password)
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

    public string ToCsv()
    {
        return FirstName + ";" + LastName + ";" + Pesel + ";" + Password;
    }

    public override string ToString()
    {
        return FirstName + ";" + LastName + ";" + Pesel;
    }
}