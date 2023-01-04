namespace library_management_system.model;

public class LibraryUser : User
{
    public LibraryUser(string firstName, string lastName, string pesel, string password) : base(firstName, lastName,
        pesel, password)
    {
    }

    public override string ToCsv()
    {
        return FirstName + ";" + LastName + ";" + Pesel + ";" + Password;
    }
}