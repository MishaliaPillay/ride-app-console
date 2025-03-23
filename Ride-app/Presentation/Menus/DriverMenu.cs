using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ride_app.Presentation.Controllers;
using Ride_app.Enities;

namespace Ride_app.Presentation.Menus
{
    public class DriverMenu
    {
        UserController userController = new UserController();
        public void ShowDriverMenu()
        {
            //Console.Clear();
            string userName = userController.GetUsername();
            Console.WriteLine("--- Driver Dashboard --- " + userName);
            Console.WriteLine("1 - View passenger rides");
            Console.WriteLine("2 - View Wallet");
            Console.WriteLine("3 - Sign Out");
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
                //case 3:
                //    {
                //        return;
                //    }
                default:
                    {
                        Console.WriteLine("Not a valid option");
                        break;
                    }
            }

        }
        public void ViewWallet()
        {
            //Console.Clear();
            decimal walletValue = userController.GetUserWallet();
            Console.WriteLine("Show balance here: " + walletValue);
            Console.WriteLine("1 - Exit");
            int action = int.Parse(Console.ReadLine());

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
        }
    }
}
