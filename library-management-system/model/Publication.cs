namespace library_management_system.model;

public abstract class Publication : CsvConvertible, IComparable<Publication>
{
    public int year { get; set; }
    public string title { get; set; }
    public string publisher { get; set; }

    protected Publication(int year, string title, string publisher)
    {
        this.year = year;
        this.title = title;
        this.publisher = publisher;
    }

    public override string ToString()
    {
        return title + "; " + publisher + "; " + year;
    }

    public abstract string toCsv();

    protected bool Equals(Publication other)
    {
        return year == other.year && title == other.title && publisher == other.publisher;
    }

    public int CompareTo(Publication? other)
    {
        if (string.Equals(title, other.title, StringComparison.OrdinalIgnoreCase))
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
        return HashCode.Combine(year, title, publisher);
    }
}