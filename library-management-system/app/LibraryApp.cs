using System.Diagnostics;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using library_management_system.model;

namespace library_management_system.app;

public static class LibraryApp
{
    private const string AppName = "Biblioteka";

    public static void Main()
    {
        string currentDirectory = TryGetSolutionDirectoryInfo().ToString();
        string correctFile = CorrectExePath(currentDirectory);

        LibraryUser admin = new("admin", "admin", "00000000000", "admin");
        Console.WriteLine(AppName);

        LibraryUser defaultUser = new LibraryUser("default", "default", "default", "default");
        LibraryControl libControl = new LibraryControl(defaultUser, false);

        Process.Start(correctFile);

        var pipeServer = new NamedPipeServerStream("PipeExample", PipeDirection.InOut, 1);

        pipeServer.WaitForConnection();
        var streamReader = new StreamReader(pipeServer);
        var streamWriter = new StreamWriter(pipeServer);

        string? message = "";
        while (message != null && !message.Contains("logged:"))
        {
            string users = "";

            if (libControl.Library.Users.Count != 0)
            {
                users = libControl.Library.Users.Aggregate(users,
                    (current, keyValuePair) => current + (keyValuePair.Value.ToCsv() + '#'));
                users = users.Remove(users.Length - 1);

                streamWriter.WriteLine(users);
                streamWriter.Flush();

                message = streamReader.ReadLine();

                if (message != null && !message.Contains("logged:"))
                {
                    string[] eachUser = message.Split('#');
                    libControl.Library.Users = new Dictionary<string, LibraryUser>();
                    foreach (var u in eachUser)
                    {
                        string[] eachUserData = u.Split(';');

                        LibraryUser libraryUser = new LibraryUser(eachUserData[0], eachUserData[1],
                            eachUserData[2], eachUserData[3]);
                        libControl.Library.Users.Add(libraryUser.Pesel, libraryUser);
                    }
                }
            }
            else
            {
                libControl.Library.Users.Add(admin.Pesel, admin);
            }
        }

        pipeServer.Close();

        if (message != null)
        {
            message = message.Substring(7);
            string[] userData = message.Split(";");
            Console.WriteLine("Zalogowany użytkownik: " + message.ChangeSemicolonsToDash());

            LibraryUser loggedUser = new LibraryUser(userData[0], userData[1], userData[2],
                userData[3]);

            libControl.CurrentUser = loggedUser;
            libControl.IsAdmin = loggedUser.Pesel.Equals(admin.Pesel) && loggedUser.Password.Equals(admin.Password);
            libControl.ControlLoop();
        }
    }

    public static DirectoryInfo TryGetSolutionDirectoryInfo(string? currentPath = null)
    {
        var directory = new DirectoryInfo(
            currentPath ?? Directory.GetCurrentDirectory());
        while (directory != null && !directory.GetDirectories("library-management-system-login").Any())
        {
            directory = directory.Parent;
        }

        return directory;
    }

    public static string CorrectExePath(string currentDirectory)
    {
        string fileexe;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            fileexe = "library-management-system-login.exe";
        }
        else
        {
            fileexe = "library-management-system-login";
        }

        bool exeFound = false;
        var correct = Directory.GetDirectories(currentDirectory)
            .FirstOrDefault(s1 => s1.Contains("library-management-system-login"));
        string correctDirectory = null;
        while (!exeFound)
        {
            string[] subDirectories = Directory.GetDirectories(correct);
            foreach (string subDirectory in subDirectories)
            {
                string[] files = Directory.GetFiles(subDirectory, "*", SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    if (Path.GetFileName(file) == fileexe)
                    {
                        correctDirectory = file;
                        exeFound = true;
                        break;
                    }
                }

                if (!exeFound)
                    correct = subDirectory;
            }
        }

        return correctDirectory;
    }
}