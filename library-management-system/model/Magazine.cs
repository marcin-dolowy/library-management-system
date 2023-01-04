namespace library_management_system.model;

public class Magazine : Publication
{
    public const string Type = "Magazyn";
    private int Month { get; }
    private int Day { get; }
    private string Language { get; }

    public Magazine(string title, string publisher, string language, int year, int month, int day) : base(year, title,
        publisher)
    {
        Month = month;
        Day = day;
        Language = language;
    }

    public override string ToCsv()
    {
        return Type + ";" +
               Title + ";" +
               Publisher + ";" +
               Year + ";" +
               Month + ";" +
               Day + ";" +
               Language;
    }

    public override string ToString()
    {
        return base.ToString() + "; " + Month + "; " + Day + "; " + Language;
    }

    private bool Equals(Magazine other)
    {
        return base.Equals(other) && Month == other.Month && Day == other.Day && Language == other.Language;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Magazine)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Month, Day, Language);
    }
}