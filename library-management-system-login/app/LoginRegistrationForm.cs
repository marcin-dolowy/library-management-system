using System.IO.Pipes;
using library_management_system_login.model;

namespace library_management_system_login.app;

public class LoginRegistrationForm
{
    private static readonly string usersFileName =
        @"C:\Users\huber\RiderProjects\library-management-system\library-management-system\io\file\Library_users.csv";

    private static List<User> _users;
    private static User _admin = new("admin", "admin", "00000000000", "admin");

    public static void Main()
    {
        var pipeClient = new NamedPipeClientStream(".", "PipeExample", PipeDirection.InOut);
        pipeClient.Connect();

        var streamWriter = new StreamWriter(pipeClient);
        var streamReader = new StreamReader(pipeClient);
        
        int option = -1;
        do
        {
            if (option != 1)
            {
                _users = ImportDataByPipe(streamReader);
            }
            Console.WriteLine("Wybierz opcje:");
            Console.WriteLine("0 - Wyjdź z systemu");
            Console.WriteLine("1 - Zaloguj się do systemu");
            Console.WriteLine("2 - Zarejestruj się do systemu");
            option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 0:
                    try
                    {
                        ExportData();
                        Console.WriteLine("Export danych do pliku zakończony powodzeniem");
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    Console.WriteLine("Koniec programu, papa!");
                    pipeClient.Close();
                    break;
                case 1:
                    User user = ValidateUser();
                    if (user != null)
                    {
                        Console.WriteLine("Zalogowano pomyślnie.");

                        var message = "logged:" + user.ToCsv();

                        streamWriter.WriteLine(message);
                        streamWriter.Flush();

                        pipeClient.Close();
                        return;
                    }

                    Console.WriteLine("Brak takiego użytkownika. Sprawdź wprowadzone dane.");

                    break;
                case 2:
                    Console.WriteLine("Podaj imię:");
                    string firstName = GetInput();
                    Console.WriteLine("Podaj nazwisko:");
                    string lastName = GetInput();
                    Console.WriteLine("Podaj pesel:");
                    string pesel = GetInput();
                    Console.WriteLine("Podaj hasło:");
                    string password = GetInput();

                    User newUser = new User(firstName, lastName, pesel, password);

                    if (_users.Any(p => p.Pesel.Equals(pesel)))
                    {
                        Console.WriteLine("Użytkownik o podanym peselu już istnieje!");
                        break;
                    }

                    _users.Add(newUser);

                    string usersString = "";
                    foreach (User u in _users)
                    {
                        usersString += u.ToCsv() + '#';
                    }

                    usersString = usersString.Remove(usersString.Length - 1);

                    streamWriter.WriteLine(usersString);
                    streamWriter.Flush();

                    Console.WriteLine("Zarejestrowano pomyślnie. Możesz się teraz zalogować.");

                    break;
                default:
                    Console.WriteLine("Brak takiej opcji.");
                    break;
            }
        } while (option != 0);
    }

    private static string GetInput()
    {
        string input = Console.ReadLine();
        while (input is null or "")
        {
            Console.WriteLine("Wprowadź poprawne dane!");
            input = Console.ReadLine();
        }

        return input;
    }

    private static List<User> ImportDataByPipe(StreamReader streamReader)
    {
        var stringUsers = streamReader.ReadLine();

        string[] users = stringUsers.Split('#');
        //convert from string to users list
        return users.Select(user => user.Split(';'))
            .Select(userData => new User(userData[0], userData[1], userData[2], userData[3])).ToList();
    }

    private static void ExportData()
    {
        try
        {
            using StreamWriter file = new(usersFileName);
            foreach (User user in _users)
            {
                file.WriteLine(user.ToCsv());
            }
        }
        catch (IOException)
        {
            throw new IOException("Błąd zapisu danych do pliku " + usersFileName);
        }
    }

    private static User ValidateUser()
    {
        Console.WriteLine("Podaj pesel:");
        string pesel = GetInput();
        Console.WriteLine("Podaj hasło:");
        string password = GetInput();
        if (pesel.Equals(_admin.Pesel) && password.Equals(_admin.Password))
        {
            return _admin;
        }

        return _users.FirstOrDefault(user => pesel.Equals(user.Pesel) && password.Equals(user.Password));
    }
}