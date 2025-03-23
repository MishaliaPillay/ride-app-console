using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ride_app.Infrastructure.Services;
using Ride_app.Presentation.Controllers;

namespace Ride_app.Presentation.Menus
{
    public class PassengerMenu
    {
        UserController userController = new UserController();
        public PassengerMenu()
        {
            this.userController = userController;
        }
        public void ShowPassengerMenu()
        {
            //Console.Clear();
            Console.WriteLine("1 - Request a ride");
            Console.WriteLine("2 - View Wallet");
            int action = int.Parse(Console.ReadLine());

            switch (action)
            {
                case 1:
                    {
                        RequestRide();
                        break;
                    }
                case 2:
                    {
                        ViewWallet();
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
            //Console.Clear();
            Console.WriteLine("Show balance here");
            Console.WriteLine("1 - Add to wallet");
            Console.WriteLine("2 - Exit");
            int action = int.Parse(Console.ReadLine());

            switch (action)
            {
                case 1:
                    {
                        userController.UpdatePassengerWallet();
                        break;
                    }
                case 2:
                    {
                        ShowPassengerMenu();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Not a valid option");
                        break;
                    }
            }

        }
        public void RequestRide()
        {
            userController.CreateRideRequest();
        }
    }
}
