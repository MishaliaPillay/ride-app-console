using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
            newRide.rideID = rideID;
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
        public bool HasPreviousRides(int id)
        {
            return rideRepository.HasPreviousRides(id);
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

        public bool GetDriverAvailability(int id)
        {
            try
            {
                User driverUser = userRepository.FindUser(id);
                if (driverUser is Driver driver)
                {
                    return driver._isAvailable;
                }
                else
                {
                    { return false; }
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool VerifyPassengerWalletBalance(int activeID, float xStart, float yStart, float xEnd, float yEnd)
        {
            Location start = new Location(xStart, yStart);
            Location end = new Location(xEnd, yEnd);
            Ride tempRide = new Ride(start, end);
            decimal rideCost = rideController.CalculateRidePrice(tempRide);

            User user = userRepository.FindUser(activeID);
            if (user._wallet < rideCost)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void CompleteRide(Ride ride)
        {
            User driverUser = userRepository.FindUser(ride._driverID);
            User passengerUser = userRepository.FindUser(ride._passengerID);

            int passengerID = ride._passengerID;
            int driverID = ride._driverID;

            passengerUser._wallet -= ride._rate;
            driverUser._wallet += ride._rate;
            UpdateDriverLocation(ride._dropOff._latitude, ride._dropOff._longitude, ride._driverID);
            ride.isComplete = true;

            if (driverUser is Driver driver)
            {
                driver._isAvailable = true;
                userRepository.UpdateDriver(driver, driverID);
            }
            if (passengerUser is Passenger passenger)
            {
                userRepository.UpdatePassenger(passenger, passengerID);
            }

            rideRepository.UpdateRide(ride, ride.rideID);
        }
        public void AssignDriverToRide(int id, Ride ride)
        {
            User user = userRepository.FindUser(id);

            if (user is Driver driver)
            {
                driver.rideIDs.Add(ride.rideID);
                driver._isAvailable = false;
                userRepository.UpdateDriver(driver, id);
                userRepository.AddUserRide(id, ride.rideID);
                rideController.AssignRideDriver(id, ride.rideID);
            }
        }
        public void AssignDriverRating(int userID, int ratingValue)
        {
            User passengerUser = userRepository.FindUser(userID);
            //Console.WriteLine("1 Adding rating");
            if (passengerUser is Passenger passenger)
            {
                //  Console.WriteLine("2 - User is apssenger");
                if (passenger.rideIDs.Count >= 1)
                {
                    //    Console.WriteLine("3 - The passenger has rides");
                    Ride ride = rideRepository.GetLatestRide(userID);
                    if (ride._driverID != -1)
                    {
                        //Console.WriteLine("4 - Passenger has had driver assigned");
                        int driverID = ride._driverID;
                        User driverUser = userRepository.FindUser(driverID);

                        if (driverUser is Driver driver)
                        {
                            //Console.WriteLine("5 driver is a driver");
                            driver._rating.Add(ratingValue);
                            userRepository.UpdateDriver(driver, driverID);
                            rideController.AssignRideRating(ride, ratingValue);
                        }
                    }
                }
            }
        }
        public List<Ride> GetRideSummary(int userID)
        {
            return rideController.GetRideSummary(userID);
        }
    }
}
