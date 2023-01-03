﻿using System.Diagnostics;
using System.IO.Pipes;
using library_management_system.model;

namespace library_management_system.app;

class LibraryApp
{
    private const string AppName = "Biblioteka";

    public static void Main()
    {
        LibraryUser admin = new("admin", "admin", "00000000000", "admin");
        Console.WriteLine(AppName);
        
        LibraryUser defaultUser = new LibraryUser("default", "default", "default", "default");
        LibraryControl libControl = new LibraryControl(defaultUser, false);

        Process.Start(
            @"C:\Users\huber\RiderProjects\library-management-system\library-management-system-login\bin\Debug\net6.0\library-management-system-login.exe");
        var pipeServer = new NamedPipeServerStream("PipeExample", PipeDirection.InOut, 1);
        
        pipeServer.WaitForConnection();
        var streamReader = new StreamReader(pipeServer);
        var streamWriter = new StreamWriter(pipeServer);

        string message = "";
        while (message != null && !message.Contains("logged:"))
        {
            string users = "";

            if (libControl.Library.Users.Count != 0)
            {
                foreach (KeyValuePair<string,LibraryUser> keyValuePair in libControl.Library.Users)
                {
                    users += keyValuePair.Value.ToCsv() + '#';
                }
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
                    
                        LibraryUser libraryUser = new LibraryUser(eachUserData[0], eachUserData[1], eachUserData[2], eachUserData[3]);
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
            Console.WriteLine("Zalogowany użytkowanik: " + message);

            LibraryUser loggedUser = new LibraryUser(userData[0], userData[1], userData[2], userData[3]);

            libControl.CurrentUser = loggedUser;
            libControl.IsAdmin = loggedUser.Pesel.Equals(admin.Pesel) && loggedUser.Password.Equals(admin.Password);
            libControl.ControlLoop();
        }
    }
}