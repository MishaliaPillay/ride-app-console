using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ride_app.Presentation.Controllers;
using Ride_app.Enities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ride_app.Presentation.Menus
{
    public class DriverMenu
    {
        readonly UserController userController = new UserController();
        public void ShowDriverMenu()
        {

            string userName = userController.GetUsername();
            bool isAvailable = userController.GetAvailability();
            Console.Clear();
            Console.WriteLine("--- Driver Dashboard --- " + userName);
            Console.WriteLine("1 - View passenger rides");
            Console.WriteLine("2 - View Wallet");
            if (isAvailable)
            {
                Console.WriteLine("3 - Toggle availability - Available");
            }
            else
            {
                Console.WriteLine("3 - Toggle availability - Unavailable");
            }
            Console.WriteLine("0 - Sign Out");
            int action = int.Parse(Console.ReadLine());

            switch (action)
            {
                case 1:
                    {
                        ViewRides();
                        break;
                    }
                case 2:
                    {
                        ViewWallet();
                        break;
                    }
                case 0:
                    {
                        return;
                    }
                case 3:
                    {
                        ToggleAvailability(!isAvailable);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Not a valid option");
                        break;
                    }
            }

        }
        public void ViewWallet()
        {
            decimal walletValue = userController.GetUserWallet();
            Console.WriteLine("Balance: " + "R" + Math.Round(walletValue, 2));
            Console.WriteLine("1 - Exit");
            int action = int.Parse(Console.ReadLine()!);

            switch (action)
            {
                case 1:
                    {
                        ShowDriverMenu();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Not a valid option");
                        break;
                    }
            }
            ShowDriverMenu();

        }
        public void ViewRides()
        {
            userController.ViewDriverRides();
            ShowDriverMenu();
        }
        public void ToggleAvailability(bool isAvailable)
        {
            Console.WriteLine("Menu");
            userController.UpdateDriverAvailability(isAvailable);
            ShowDriverMenu();
        }
    }
}
