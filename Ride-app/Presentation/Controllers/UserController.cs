using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Ride_app.Enities;
using Ride_app.Infrastructure.Repositories;
using Ride_app.Infrastructure.Services;

namespace Ride_app.Presentation.Controllers
{
    public class UserController
    {
        public static int activeID = -1;
        UserService userService = new UserService();

        public void CreateUser()
        {
            Console.Write("Input username: ");
            string username = Console.ReadLine();
            Console.Write("Input password: ");
            string password = Console.ReadLine();

            decimal wallet = 100.50M;
            Location location = new Location(0, 0);

            Console.Write("Passenger or driver: ");
            string role = Console.ReadLine();

            if (role == "Driver")
            {
                userService.CreateDriver(username, password, wallet, location);
            }
            else if (role == "Passenger")
            {
                userService.CreatePassenger(username, password, wallet, location);
            }

        }
        public void UpdateUser()
        {

            if (userService.IsDriver(activeID))
            {
                UpdateDriver(activeID);
            }
            else
            {
                UpdatePassenger(activeID);
            }
        }
        public int SignIn()
        {
            Console.WriteLine("Username: ");
            string username = Console.ReadLine();
            if (!userService.CheckUsernameExists(username))
            {
                Console.WriteLine("This user does not exist");
                return -1;
            }


            Console.WriteLine("Password: ");
            string password = Console.ReadLine();
            if (!userService.CheckPassword(username, password))
            {
                Console.WriteLine("Incorrect Password");
                return -1;
            }
            else
            {
                Console.WriteLine("Correct password");
                activeID = userService.GetUserID(username);

                Console.Write("UserController as " + activeID);
                return userService.GetUserID(username);
            }
        }
        public void UpdateDriver(int id)
        {
            Console.WriteLine("1 - Update balance");
            Console.WriteLine("2 - Update location");
            Console.WriteLine("3 - Update Availability");
            int action = int.Parse(Console.ReadLine());

            switch (action)
            {
                case 1:
                    {
                        UpdateDriverWallet();
                        break;
                    }
                case 2:
                    {
                        UpdateDriverLocation();
                        break;
                    }
                case 3:
                    {
                        UpdateDriverAvailability(false);
                        break;
                    }
            }

        }
        public void UpdatePassenger(int id)
        {
            Console.WriteLine("1 - Update balance");
            Console.WriteLine("2 - Update location");
            int action = int.Parse(Console.ReadLine());

            switch (action)
            {
                case 1:
                    {
                        UpdatePassengerWallet();
                        break;
                    }
                case 2:
                    {
                        UpdatePassengerLocation();
                        break;
                    }
            }
        }
        public bool GetAvailability()
        {
            return userService.GetDriverAvailability(activeID);
        }
        //public void ToggleDriverAvailability()
        //{
        //    Console.WriteLine("Controller");
        //    userService.ToggleDriverAvailability(activeID);
        //}
        public void CreateRideRequest()
        {

            Console.Write("UserController as " + activeID);
            ////Console.Clear
            Console.WriteLine("x - Select start location: ");
            float xStart = float.Parse(Console.ReadLine());

            Console.WriteLine("y - Select start location: ");
            float yStart = float.Parse(Console.ReadLine());

            Console.WriteLine("x - Select dropoff location: ");
            float xEnd = float.Parse(Console.ReadLine());

            Console.WriteLine("y - Select dropoff location: ");
            float yEnd = float.Parse(Console.ReadLine());

            bool canAfford = userService.VerifyPassengerWalletBalance(activeID, xStart, yStart, xEnd, yEnd);

            if (canAfford)
            {
                userService.CreatePassengerRideRequest(xStart, yStart, xEnd, yEnd, activeID);
                Console.WriteLine("Ride requested successfully");
            }
            else
            {
                Console.WriteLine("Insufficient balance - please topup wallet");
            }
        }
        public Decimal GetUserWallet()
        {
            return userService.GetUserWallet(activeID);
        }
        public void UpdatePassengerWallet()
        {
            Console.WriteLine("Update wallet: ");
            decimal walletChange = decimal.Parse(Console.ReadLine());
            //Console.Clear();

            userService.UpdatePassengerWallet(walletChange, activeID);
        }
        public void UpdatePassengerLocation()
        {
            Console.WriteLine("Change xPos: ");
            float xPos = int.Parse(Console.ReadLine());
            Console.WriteLine("Change yPos: ");
            float yPos = int.Parse(Console.ReadLine());

            userService.UpdatePassengerLocation(xPos, yPos, activeID);
        }
        public void UpdateDriverWallet()
        {
            Console.WriteLine("Update wallet: ");
            decimal walletChange = decimal.Parse(Console.ReadLine());
            //Console.Clear();

            userService.UpdateDriverWallet(walletChange, activeID);
        }
        public void UpdateDriverLocation()
        {
            Console.WriteLine("Change xPos: ");
            float xPos = int.Parse(Console.ReadLine());
            Console.WriteLine("Change yPos: ");
            float yPos = int.Parse(Console.ReadLine());

            userService.UpdateDriverLocation(xPos, yPos, activeID);
        }
        public void UpdateDriverAvailability(bool isAvailable)
        {
            userService.UpdateDriverAvailability(isAvailable, activeID);
        }
        public bool HasPreviousRides()
        {
            return userService.HasPreviousRides(activeID);
        }
        public bool CheckUserDriver()
        {
            return userService.IsDriver(activeID);
        }
        public string GetUsername()
        {
            return userService.GetUsername(activeID);
        }
        public List<Ride> GetDriverlessRides()
        {
            return userService.GetDriverlessRides(activeID);
        }
        public int ViewDriverRides()
        {
            int index = 1;
            List<Ride> availableRides = GetDriverlessRides();

            Console.WriteLine("--- Start a ride ---");

            foreach (Ride ride in availableRides)
            {
                string passengerName = userService.GetUsername(ride._passengerID);
                decimal ridePrice = Math.Round(ride._rate, 2);
                Console.WriteLine(index + " - " + passengerName + " : R" + ridePrice + ", is this far away: " + Math.Round(ridePrice / 10, 0));
                index++;
            }

            Console.WriteLine("0 - Exit");


            int action = int.Parse(Console.ReadLine());
            if (action == 0)
            {
                Console.WriteLine("Returning to driver dashboard");
            }
            else if (availableRides[action - 1] != null)
            {
                userService.AssignDriverToRide(activeID, availableRides[action - 1]);
                UpdateRideStatus(availableRides[action - 1]);
            }
            else
            {
                Console.WriteLine("Error: Not an option");
            }
            return action;
        }
        public void UpdateRideStatus(Ride ride)
        {
            Console.Clear();
            Console.WriteLine("Press anything to complete ride");
            Console.ReadLine();
            userService.CompleteRide(ride);
        }
        public void RateDriver()
        {
            Console.Clear();
            Console.WriteLine("Give your last driver a rating");
            int rating = int.Parse(Console.ReadLine());
            userService.AssignDriverRating(activeID, rating);
        }
        public void GetRideSummary()
        {
            List<Ride> userRides = userService.GetRideSummary(activeID);

            foreach (Ride r in userRides)
            {
                Console.WriteLine(r._rate.ToString() + " : " + "(" + r._pickUp._latitude.ToString() + ", "
                    + r._pickUp._longitude.ToString() + ") tp (" + r._dropOff._latitude.ToString() + ", "
                    + r._dropOff._longitude.ToString() + ") : " + r._rating);
            }
            Console.WriteLine("0 - Exit");
            Console.ReadLine();
        }
    }
}

