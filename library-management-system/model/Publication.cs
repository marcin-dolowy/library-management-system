namespace library_management_system.model;

public abstract class Publication : CsvConvertible, IComparable<Publication>
{
    public int Year { get; set; }
    public string Title { get; set; }
    public string Publisher { get; set; }

    protected Publication(int year, string title, string publisher)
    {
        this.Year = year;
        this.Title = title;
        this.Publisher = publisher;
    }

    public override string ToString()
    {
        return Title + "; " + Publisher + "; " + Year;
    }

    public abstract string toCsv();

    protected bool Equals(Publication other)
    {
        return Year == other.Year && Title == other.Title && Publisher == other.Publisher;
    }

    public int CompareTo(Publication? other)
    {
        if (string.Equals(Title, other.Title, StringComparison.OrdinalIgnoreCase))
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
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Publication)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Year, Title, Publisher);
    }
}