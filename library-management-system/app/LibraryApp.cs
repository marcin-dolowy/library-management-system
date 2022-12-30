namespace library_management_system.app;

class LibraryApp {
    private static String APP_NAME = "Biblioteka";

    public static void Main(String[] args) {
        Console.WriteLine(APP_NAME);
        LibraryControl libControl = new LibraryControl();
        libControl.controlLoop();
    }
}