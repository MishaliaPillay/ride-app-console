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
using Ride_app.Presentation.Menus;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ride_app.Presentation.Controllers
{
    public class UserController
    {
        public static int activeID = -1;
        UserService userService = new UserService();

        public void CreateUser()
        {
            Console.WriteLine("User Registration:");
            Console.WriteLine("");
            Console.Write("Input username: ");
            string username = Console.ReadLine();
            if (userService.CheckUsernameExists(username))
            {
                Console.WriteLine("");
                Console.WriteLine("This user already exists!");
                Console.WriteLine("");
                Console.WriteLine("Please try again.");
                Console.WriteLine("");

                CreateUser();
            }

            Console.Write("Input password: ");
            string password = Console.ReadLine();

            decimal wallet = 100.50M;
            Location location = new Location(0, 0);
            Console.WriteLine("");
            Console.WriteLine("Please select role below:  ");
            Console.WriteLine("");
            Console.WriteLine("1 - Passenger  ");
            Console.WriteLine("2 - Driver ");
            int role = int.Parse(Console.ReadLine());

            Console.Clear();

            switch (role)
            {
                case 1:
                    {
                        userService.CreatePassenger(username, password, wallet, location);
                        Console.WriteLine("");
                        Console.WriteLine("--- Successfully created passenger account ---");
                        Console.WriteLine("");
                        break;
                    }
                case 2:
                    {
                        userService.CreateDriver(username, password, wallet, location);
                        Console.WriteLine("");
                        Console.WriteLine("--- Successfully created driver account ---");
                        Console.WriteLine("");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Not a valid option");
                        break;
                    }
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

            Console.WriteLine("User Sign in: ");
            Console.WriteLine("");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            if (!userService.CheckUsernameExists(username))
            {
                Console.WriteLine("This user does not exist");
                return -1;
            }


            Console.Write("Password: ");
            string password = Console.ReadLine();
            try
            {
                if (!userService.CheckPassword(username, password))
                {
                    Console.WriteLine("Incorrect Password!");
                    throw new Exception();
                }
                else
                {
                    Console.WriteLine("Correct password");
                    activeID = userService.GetUserID(username);

                    Console.Clear();
                    return userService.GetUserID(username);
                }

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return -1;

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


        //This allows passengers to create rides
        public void CreateRideRequest()
        {


            Console.WriteLine("Please set the positions of the start location: ");
            Console.Write("x Start : ");
            float xStart = float.Parse(Console.ReadLine()!);

            Console.Write("y Start : ");
            float yStart = float.Parse(Console.ReadLine()!);
            Console.WriteLine("Please set the positions of the dropoff location: ");
            Console.Write("x DropOff : ");
            float xEnd = float.Parse(Console.ReadLine()!);

            Console.Write("y DropOff: ");
            float yEnd = float.Parse(Console.ReadLine()!);

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
        //This shows the users the amount in their wallet - for passnger and driver
        public Decimal GetUserWallet()
        {
            return userService.GetUserWallet(activeID);
        }
        //When the user wants to add funds 
        public void UpdatePassengerWallet()
        {
            Console.WriteLine("Update wallet: ");
            decimal walletChange = decimal.Parse(Console.ReadLine());
            decimal userCurrentAmount = GetUserWallet();
            userService.UpdatePassengerWallet(userCurrentAmount + walletChange, activeID);
            Console.Clear();
        }
        public void UpdatePassengerLocation()
        {
            Console.WriteLine("Change xPos: ");
            float xPos = int.Parse(Console.ReadLine());
            Console.WriteLine("Change yPos: ");
            float yPos = int.Parse(Console.ReadLine());

            userService.UpdatePassengerLocation(xPos, yPos, activeID);
        }

        //This fucntion gets called after the ride to take money from the passenger and add it to the drivers wallet
        public void UpdateDriverWallet()
        {
            Console.WriteLine("Update wallet: ");
            decimal walletChange = decimal.Parse(Console.ReadLine());


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
                Console.WriteLine(index + " - " + passengerName + " is " + Math.Round(ridePrice / 10, 0) + "km" + " away" + " ,  R" + ridePrice);
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
                Console.WriteLine("Price: " + "R" + Math.Round(r._rate, 2) + " , " + "Location: " + "(" + r._pickUp._latitude.ToString() + ", "
                    + r._pickUp._longitude.ToString() + ") tp (" + r._dropOff._latitude.ToString() + ", "
                    + r._dropOff._longitude.ToString() + ") ,  " + "Rating: " + r._rating);
            }
            Console.WriteLine("0 - Exit");
            Console.ReadLine();
        }
    }
}

