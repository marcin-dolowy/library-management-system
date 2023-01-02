namespace library_management_system.model;

public class Book : Publication
{
    public const string Type = "Książka";
    private string Author { get; }
    private int Pages { get; }
    private string Isbn { get; }

    public Book(string title, string author, int year, int pages, string publisher, string isbn) : base(year, title,
        publisher)
    {
        Author = author;
        Pages = pages;
        Isbn = isbn;
    }
    
    public override string ToString()
    {
        return base.ToString() + "; " + Author + "; " + Pages + "; " + Isbn;
    }

    public override string ToCsv()
    {
        return Type + ";" +
               Title + ";" +
               Publisher + ";" +
               Year + ";" +
               Author + ";" +
               Pages + ";" +
               Isbn;
    }

    private bool Equals(Book other)
    {
        return base.Equals(other) && Author == other.Author && Pages == other.Pages && Isbn == other.Isbn;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Book)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Author, Pages, Isbn);
    }
}