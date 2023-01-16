namespace library_management_system.model;

public record Borrow : ICsvConvertible
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

    // private bool Equals(Borrow other)
    // {
    //     return Pesel == other.Pesel && Title == other.Title;
    // }
    //
    // public override bool Equals(object? obj)
    // {
    //     if (ReferenceEquals(null, obj)) return false;
    //     if (ReferenceEquals(this, obj)) return true;
    //     if (obj.GetType() != GetType()) return false;
    //     return Equals((Borrow)obj);
    // }

    public virtual bool Equals(Borrow? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Pesel == other.Pesel && Title == other.Title;
    }

    private sealed class PeselTitleEqualityComparer : IEqualityComparer<Borrow>
    {
        public bool Equals(Borrow x, Borrow y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Pesel == y.Pesel && x.Title == y.Title;
        }

        public int GetHashCode(Borrow obj)
        {
            return HashCode.Combine(obj.Pesel, obj.Title);
        }
    }

    public static IEqualityComparer<Borrow> PeselTitleComparer { get; } = new PeselTitleEqualityComparer();

    public override int GetHashCode()
    {
        return HashCode.Combine(Pesel, Title);
    }
}