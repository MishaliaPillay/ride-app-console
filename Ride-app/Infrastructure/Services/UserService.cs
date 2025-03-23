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
        public int userID = 0;
        public int rideID = 0;
        UserRepository userRepository = new UserRepository();
        DriverRepository driverRepository = new DriverRepository();
        RideRepository rideRepository = new RideRepository();
        PassengerRepository passengerRepository = new PassengerRepository();
        RideController rideController = new RideController();

        public UserService()
        {
            if (UserRepository.users.Any())
            {
                userID = UserRepository.users.Count();
            }
            else
            {
                userID = 0;
            }
            if (RideRepository.rides.Any())
            {
                rideID = RideRepository.rides.Count();
            }
            else
            {
                rideID = 0;
            }
        }

        public void CreateDriver(string username, string password, decimal wallet, Location location)
        {
            User user = new User(wallet, location, username, password, userID, true);
            Driver driver = new Driver(user);

            userRepository.AddNewDriver(driver);
            userID++;
        }
        public void CreatePassengerRideRequest(float xStart, float yStart, float xEnd, float yEnd, int id)
        {
            Console.WriteLine("userservice as " + id);
            Location pickup = new Location(xStart, yStart);
            Location dropoff = new Location(xEnd, yEnd);
            Ride newRide = new Ride(pickup, dropoff);
            newRide._passengerID = id;
            newRide._rate = rideController.CalculateRidePrice(newRide);
            rideRepository.AddNewRide(newRide);
            userRepository.AddUserRide(id, rideID);

            rideID++;
        }
        public decimal GetUserWallet(int id)
        {
            return userRepository.GetUserWallet(id);
        }
        public void CreatePassenger(string username, string password, decimal wallet, Location location)
        {
            User user = new User(wallet, location, username, password, userID, false);
            Passenger passenger = new Passenger(user);
            userRepository.AddNewPassenger(passenger);
            userID++;
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

        public string GetUsername(int id)
        {
            return userRepository.FindUser(id)._name;
        }
        public List<Ride> GetDriverlessRides(int id)
        {
            //return rideRepository.GetDriverlessRides();
            List<Ride> allRides = rideRepository.GetDriverlessRides();
            Location driverLocation = GetUserLocation(id);

            List<Ride> inRangeRides = allRides.Where(r => rideController.GetRideDistance(r._pickUp, driverLocation) < 10f).ToList();
            return inRangeRides;
        }

        public Location GetUserLocation(int id)
        {
            return userRepository.GetUserLocation(id);
        }
    }
}
