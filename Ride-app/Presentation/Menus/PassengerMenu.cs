using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
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

            string userName = userController.GetUsername();
            Console.Clear();
            Console.WriteLine("--- Passenger Dashboard --- " + userName);
            Console.WriteLine("1 - Request a ride");
            Console.WriteLine("2 - Rate previous ride");
            Console.WriteLine("3 - View Wallet");
            Console.WriteLine("4 - View Completed Rides");
            Console.WriteLine("5 - Sign Out");
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
                        RateDriver();
                        break;
                    }
                case 3:
                    {
                        ViewWallet();
                        break;
                    }
                case 4:
                    {
                        ViewRideHistory();
                        break;
                    }
                case 5:
                    {
                        return;
                    }
                default:
                    {
                        Console.WriteLine("Not a valid option");
                        break;
                    }
            }
        }
        public void ViewRideHistory()
        {
            userController.GetRideSummary();
        }
        public void RateDriver()
        {
            if (userController.HasPreviousRides())
            {
                userController.RateDriver();
            }
            else
            {
                NoPreviousRides();
            }
            ShowPassengerMenu();
        }

        public void NoPreviousRides()
        {
            Console.Clear();
            Console.WriteLine("You have not completed an rides yet. Come back when you have");
            Console.WriteLine(" 0 - Return ");
            int response = int.Parse(Console.ReadLine());
            if (response == 0)
            {
                return;
            }
        }
        public void ViewWallet()
        {
            //Console.Clear();
            decimal walletValue = userController.GetUserWallet();
            Console.WriteLine("Balance: " + " R " + Math.Round(walletValue, 2));
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
            ShowPassengerMenu();

        }
        public void RequestRide()
        {
            //Console.Clear();
            userController.CreateRideRequest();
            ShowPassengerMenu();
        }
    }
}
