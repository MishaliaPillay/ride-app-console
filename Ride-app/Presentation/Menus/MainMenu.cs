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
        DriverMenu driverMenu = new DriverMenu();
        PassengerMenu passengerMenu = new PassengerMenu();

        public void MainMenuOptions()
        {
            while (true)
            {

                Console.WriteLine("Welcome to Ride App. Please select an option below.");
                Console.WriteLine("1 - Sign in");
                Console.WriteLine("2 - Add account");
                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        {
                            Console.Clear();
                            MainMenuHelpers.activeID = userController.SignIn();
                            if (MainMenuHelpers.activeID == -1)
                            {
                                Console.WriteLine("Login Failed");
                                Console.WriteLine("---------------------------------------------------------------------");
                                Console.WriteLine("");
                                break;
                            }
                            if (!userController.CheckUserDriver())
                            {
                                passengerMenu.ShowPassengerMenu();
                            }
                            else
                            {
                                driverMenu.ShowDriverMenu();
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            userController.CreateUser();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Not a valid option");
                            break;
                        }
                }
            }
        }
    }
}
