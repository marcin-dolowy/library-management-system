namespace library_management_system.model;

public class Borrow : ICsvConvertible
{
    public string Pesel { get; }
    public string Title { get; }

    public Borrow(string pesel, string title)
    {
        Pesel = pesel;
        Title = title;
    }

    public override string ToString()
    {
        return Pesel + "; " + Title;
    }

    public string ToCsv()
    {
        return Pesel + ";" + Title;
    }

    private bool Equals(Borrow other)
    {
        return Pesel == other.Pesel && Title == other.Title;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Borrow)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Pesel, Title);
    }
}