namespace library_management_system.model;

public class Magazine : Publication
{
    public const string TYPE = "Magazyn";
    private int month { get; set; }
    private int day { get; set; }
    private string language { get; set; }

    public Magazine(string title, string publisher, string language, int year, int month, int day) : base(year, title,
        publisher)
    {
        this.month = month;
        this.day = day;
        this.language = language;
    }

    public override string ToCsv()
    {
        return TYPE + ";" +
               Title + ";" +
               Publisher + ";" +
               Year + ";" +
               month + ";" +
               day + ";" +
               language;
    }

    public override string ToString()
    {
        return base.ToString() + "; " + month + "; " + day + "; " + language;
    }

    protected bool Equals(Magazine other)
    {
        return base.Equals(other) && month == other.month && day == other.day && language == other.language;
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
        return HashCode.Combine(base.GetHashCode(), month, day, language);
    }
}