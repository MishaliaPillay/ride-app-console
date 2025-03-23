using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ride_app.Presentation.Controllers;

namespace Ride_app.Presentation.Menus
{
    public class MainMenu
    {
        UserController userController = new UserController();
        public void MainMenuOptions()
        {
            while (true)
            {
                Console.WriteLine("1 - Sign in /n 2 - Sign Up");
                string input = Console.ReadLine();
                if (input == "2")
                {
                    userController.CreateUser();
                }
            }
        }
    }
}
