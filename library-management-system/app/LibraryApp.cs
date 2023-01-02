namespace library_management_system.app;

class LibraryApp
{
    private const string AppName = "Biblioteka";

    public static void Main()
    {
        Console.WriteLine(AppName);
        LibraryControl libControl = new LibraryControl();
        libControl.ControlLoop();
    }
}