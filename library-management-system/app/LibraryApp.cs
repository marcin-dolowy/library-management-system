namespace library_management_system.app;

class LibraryApp
{
    private static String APP_NAME = "Biblioteka";

    public static void Main(string[] args)
    {
        // Console.WriteLine(APP_NAME);
        // LibraryControl libControl = new LibraryControl();
        // libControl.controlLoop();


        // string currentDirectory = Directory.GetCurrentDirectory();
        // for (int i = 0; i < 3; i++)
        // {
        //     DirectoryInfo parentDirectory = Directory.GetParent(currentDirectory);
        //     if (parentDirectory == null)
        //     {
        //         break;
        //     }
        //     currentDirectory = parentDirectory.FullName;
        // }
        // Console.WriteLine(currentDirectory);

        Console.WriteLine("=========================");
        string currentDirectory = Directory.GetCurrentDirectory();
        string directory3LevelsUp = Directory
            .GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName)!.FullName)!.FullName;
        string goodDirectory = Path.Combine(directory3LevelsUp, "file", "Library.csv");
        Console.WriteLine(goodDirectory);
    }
}