using Ride_app.Presentation.Menus;

namespace Ride_app
{
    public class Program
    {
        MainMenu menu = new MainMenu();
        static void Main(string[] args)
        {
            Program program = new Program();

            program.menu.MainMenuOptions();
        }
    }
}
