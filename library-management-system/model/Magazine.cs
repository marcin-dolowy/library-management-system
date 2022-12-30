namespace library_management_system.model;

public class Magazine : Publication
{
    public const string TYPE = "Magazyn";
    private uint monthDay { get; set; }
    private string language { get; set; }

    public Magazine(int year, string title, string publisher, uint monthDay, string language) : base(year, title,
        publisher)
    {
        this.monthDay = monthDay;
        this.language = language;
    }

    public override string toCsv()
    {
        return TYPE + ";" +
               title + ";" +
               publisher + ";" +
               year + ";" +
               monthDay + ";" +
               monthDay + ";" +
               language;
    }

    public override string ToString()
    {
        return base.ToString() + "; " + monthDay + "; " + monthDay + "; " + language;
    }

    protected bool Equals(Magazine other)
    {
        return base.Equals(other) && monthDay == other.monthDay && language == other.language;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Magazine)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), monthDay, language);
    }
}