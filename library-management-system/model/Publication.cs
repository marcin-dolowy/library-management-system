namespace library_management_system.model;

public abstract class Publication : ICsvConvertible, IComparable<Publication>
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

    protected bool Equals(Publication other)
    {
        return Year == other.Year && Title == other.Title && Publisher == other.Publisher;
    }

    public int CompareTo(Publication? other)
    {
        if (string.Equals(Title, other?.Title, StringComparison.OrdinalIgnoreCase))
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Publication)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Year, Title, Publisher);
    }
}