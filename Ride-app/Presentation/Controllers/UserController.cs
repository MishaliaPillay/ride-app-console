﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

            Console.Write("User or driver: ");
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
        public void CreateRideRequest()
        {

            Console.Write("UserController as " + activeID);
            //Console.Clear();

            Console.WriteLine("x - Select start location: ");
            float xStart = float.Parse(Console.ReadLine());

            Console.WriteLine("y - Select start location: ");
            float yStart = float.Parse(Console.ReadLine());

            Console.WriteLine("x - Select dropoff location: ");
            float xEnd = float.Parse(Console.ReadLine());

            Console.WriteLine("y - Select dropoff location: ");
            float yEnd = float.Parse(Console.ReadLine());

            Console.Write("UserController as " + activeID);

            userService.CreatePassengerRideRequest(xStart, yStart, xEnd, yEnd, activeID);
        }
        public void UpdatePassengerWallet()
        {
            Console.WriteLine("Update wallet: ");
            decimal walletChange = decimal.Parse(Console.ReadLine());
            Console.Clear();

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
            Console.Clear();

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
        public bool CheckUserDriver()
        {
            return userService.IsDriver(activeID);
        }
    }
}

