namespace library_management_system.model;

public abstract record Publication : ICsvConvertible, IComparable<Publication>
{
    protected int Year { get; }
    public string Title { get; }
    protected string Publisher { get; }

    protected Publication(int year, string title, string publisher)
    {
        Year = year;
        Title = title;
        Publisher = publisher;
    }

    public override string ToString()
    {
        return Title + "; " + Publisher + "; " + Year;
    }

    public abstract string ToCsv();
    

    public int CompareTo(Publication? other)
    {
        if (string.Equals(Title, other?.Title, StringComparison.OrdinalIgnoreCase))
        {
            return 1;
        }
        return -1;
    }
    
    // public bool Equals(Publication other)
    // {
    //     return Year == other.Year && Title == other.Title && Publisher == other.Publisher;
    // }

    // public override bool Equals(object? obj)
    // {
    //     if (ReferenceEquals(null, obj)) return false;
    //     if (ReferenceEquals(this, obj)) return true;
    //     if (obj.GetType() != GetType()) return false;
    //     return Equals((Publication)obj);
    // }

    public virtual bool Equals(Publication? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Year == other.Year && Title == other.Title && Publisher == other.Publisher;
    }

    private sealed class YearTitlePublisherEqualityComparer : IEqualityComparer<Publication>
    {
        public bool Equals(Publication x, Publication y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Year == y.Year && x.Title == y.Title && x.Publisher == y.Publisher;
        }

        public int GetHashCode(Publication obj)
        {
            return HashCode.Combine(obj.Year, obj.Title, obj.Publisher);
        }
    }

    public static IEqualityComparer<Publication> YearTitlePublisherComparer { get; } = new YearTitlePublisherEqualityComparer();

    public override int GetHashCode()
    {
        return HashCode.Combine(Year, Title, Publisher);
    }
}