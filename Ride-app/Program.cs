using Ride_app.Presentation.Menus;

namespace Ride_app
{
    public class Program
    {
        MainMenu menu = new MainMenu();
        static void Main(string[] args)
        {
            Program program = new Program();
            Console.WriteLine("Hello, World!");
            program.menu.MainMenuOptions();
        }
    }
}
