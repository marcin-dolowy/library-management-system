namespace library_management_system.model;

public class Book : Publication
{
    public static string TYPE = "Książka";
    public string author { get; set; }
    public int pages { get; set; }
    public string isbn { get; set; }

    public Book(string title, string author, int year, int pages, string publisher, string isbn) : base(year, title,
        publisher)
    {
        this.author = author;
        this.pages = pages;
        this.isbn = isbn;
    }

    public override string ToString()
    {
        return base.ToString() + "; " + author + "; " + pages + "; " + isbn;
    }

    public override string toCsv()
    {
        return TYPE + ";" +
               title + ";" +
               publisher + ";" +
               year + ";" +
               author + ";" +
               pages + ";" +
               isbn;
    }

    protected bool Equals(Book other)
    {
        return base.Equals(other) && author == other.author && pages == other.pages && isbn == other.isbn;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Book)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), author, pages, isbn);
    }
}