using System.Diagnostics;
using System.IO.Pipes;
using library_management_system.model;

namespace library_management_system.app;

class LibraryApp
{
    private const string AppName = "Biblioteka";

    public static void Main()
    {
        // Process.Start(@"C:\Users\huber\RiderProjects\library-management-system\library-management-system-login\bin\Debug\net6.0\library-management-system-login.exe");
        // var pipeServer = new NamedPipeServerStream("PipeExample", PipeDirection.InOut, 1);
        //
        // pipeServer.WaitForConnection();
        //
        // var streamReader = new StreamReader(pipeServer);
        // var streamWriter = new StreamWriter(pipeServer);
        //
        // var message = streamReader.ReadLine();
        //
        // Console.WriteLine("To jest: " + message);
        //
        // pipeServer.Close();
        //
        // if (message.Equals("a"))
        // {
        LibraryUser loggedUser = new LibraryUser("Marcin", "Filipczuk", "8576232"); // to z drugiego programu dostaniemy
        bool isAdmin = false; //to samo co wyzej
        Console.WriteLine(AppName);
        LibraryControl libControl = new LibraryControl(loggedUser, isAdmin);
        libControl.ControlLoop();
        // }
    }
}