using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ride_app.Application.Interfacse;
using Ride_app.Enities;
using Ride_app.Infrastructure.Repositories;

namespace Ride_app.Infrastructure.Services
{
    public class UserService
    {
        public int id = 0;
        UserRepository userRepository = new UserRepository();
        DriverRepository driverRepository = new DriverRepository();
        PassengerRepository passengerRepository = new PassengerRepository();

        public void CreateDriver(string username, string password, decimal wallet, Location location)
        {
            User user = new User(wallet, location, username, password, id, true);
            Driver driver = new Driver(user);

            //driverRepository.AddNewDriver(driver);
            userRepository.AddNewDriver(driver);
            id++;
        }
        public void CreatePassengerRideRequest(float xStart, float yStart, float xEnd, float yEnd, int id)
        {
            Console.WriteLine("userservice as " + id);
            Location pickup = new Location(xStart, yStart);
            Location dropoff = new Location(xEnd, yEnd);
            Ride newRide = new Ride(pickup, dropoff);
            userRepository.AddUserRide(id, newRide);

            //Console.WriteLine("Getting user ref from storage");
            //User userToCheck = userRepository.FindUser(id);

            //Console.WriteLine("Gotten user ref from storage");
            //if (userToCheck is Passenger passengerToCheck)
            //{
            //    passengerToCheck._rides.Add(newRide);
            //    userRepository.UpdatePassenger(passengerToCheck, id);
            //}
        }
        public void CreatePassenger(string username, string password, decimal wallet, Location location)
        {
            User user = new User(wallet, location, username, password, id, false);
            Passenger passenger = new Passenger(user);
            //passengerRepository.AddNewPassenger(passenger);
            userRepository.AddNewPassenger(passenger);
            id++;
        }

        public void UpdatePassengerWallet(decimal wallet, int id)
        {
            User userToCheck = userRepository.FindUser(id);
            if (userToCheck is Passenger passengerToCheck)
            {
                passengerToCheck._wallet = wallet;
                userRepository.UpdatePassenger(passengerToCheck, id);
            }
        }
        public void UpdatePassengerLocation(float xPos, float yPos, int id)
        {
            Location location = new Location(xPos, yPos);
            User userToCheck = userRepository.FindUser(id);
            if (userToCheck is Passenger passengerToCheck)
            {
                passengerToCheck._location = location;
                userRepository.UpdatePassenger(passengerToCheck, id);
            }
        }

        public bool CheckUsernameExists(string username)
        {
            return userRepository.FindUsername(username);
        }
        public bool CheckPassword(string username, string password)
        {
            return userRepository.CheckPassword(username, password);
        }

        public void UpdateDriverWallet(decimal wallet, int id)
        {

            User userToCheck = userRepository.FindUser(id);
            if (userToCheck is Driver driverToCheck)
            {
                driverToCheck._wallet = wallet;

                userRepository.UpdateDriver(driverToCheck, id);
            }
        }
        public void UpdateDriverLocation(float xPos, float yPos, int id)
        {
            Location location = new Location(xPos, yPos);
            User userToCheck = userRepository.FindUser(id);
            if (userToCheck is Driver driverToCheck)
            {
                driverToCheck._location = location;
                userRepository.UpdateDriver(driverToCheck, id);
            }
        }
        public void UpdateDriverAvailability(bool isAvailable, int id)
        {
            User userToCheck = userRepository.FindUser(id);
            if (userToCheck is Driver driverToCheck)
            {
                driverToCheck._isAvailable = isAvailable;
                userRepository.UpdateDriver(driverToCheck, id);
            }
        }
        public bool IsDriver(int id)
        {
            User userToCheck = userRepository.FindUser(id);
            if (userToCheck._isDriver)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetUserID(string username)
        {
            return userRepository.GetUserID(username);
        }
    }
}
