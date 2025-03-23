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
        public static int activeID;
        UserController userController = new UserController();
        DriverMenu driverMenu = new DriverMenu();
        PassengerMenu passengerMenu = new PassengerMenu();

        public void MainMenuOptions()
        {
            while (true)
            {
                //Console.Clear();
                Console.WriteLine("1 - Sign in");
                Console.WriteLine("2 - Add account");
                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        {
                            activeID = userController.SignIn();
                            Console.WriteLine("Signed in as user #" + activeID);
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
