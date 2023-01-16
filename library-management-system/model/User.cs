namespace library_management_system.model;

public abstract record User : ICsvConvertible
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

    // private bool Equals(User other)
    // {
    //     return FirstName == other.FirstName && LastName == other.LastName && Pesel == other.Pesel &&
    //            Password == other.Password;
    // }
    //
    // public override bool Equals(object? obj)
    // {
    //     if (ReferenceEquals(null, obj)) return false;
    //     if (ReferenceEquals(this, obj)) return true;
    //     if (obj.GetType() != GetType()) return false;
    //     return Equals((User)obj);
    // }

    public virtual bool Equals(User? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return FirstName == other.FirstName && LastName == other.LastName && Pesel == other.Pesel && Password == other.Password;
    }

    private sealed class UserEqualityComparer : IEqualityComparer<User>
    {
        public bool Equals(User x, User y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.FirstName == y.FirstName && x.LastName == y.LastName && x.Pesel == y.Pesel && x.Password == y.Password;
        }

        public int GetHashCode(User obj)
        {
            return HashCode.Combine(obj.FirstName, obj.LastName, obj.Pesel, obj.Password);
        }
    }

    public static IEqualityComparer<User> UserComparer { get; } = new UserEqualityComparer();

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