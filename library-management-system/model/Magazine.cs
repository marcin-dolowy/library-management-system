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

    // private bool Equals(Magazine other)
    // {
    //     return base.Equals(other) && Month == other.Month && Day == other.Day && Language == other.Language;
    // }
    //
    // public override bool Equals(object? obj)
    // {
    //     if (ReferenceEquals(null, obj)) return false;
    //     if (ReferenceEquals(this, obj)) return true;
    //     if (obj.GetType() != GetType()) return false;
    //     return Equals((Magazine)obj);
    // }

    public virtual bool Equals(Magazine? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return base.Equals(other) && Month == other.Month && Day == other.Day && Language == other.Language;
    }

    private sealed class MonthDayLanguageEqualityComparer : IEqualityComparer<Magazine>
    {
        public bool Equals(Magazine x, Magazine y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Month == y.Month && x.Day == y.Day && x.Language == y.Language;
        }

        public int GetHashCode(Magazine obj)
        {
            return HashCode.Combine(obj.Month, obj.Day, obj.Language);
        }
    }

    public static IEqualityComparer<Magazine> MonthDayLanguageComparer { get; } = new MonthDayLanguageEqualityComparer();

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Month, Day, Language);
    }
}