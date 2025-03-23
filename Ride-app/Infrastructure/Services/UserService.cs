using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ride_app.Enities;
using Ride_app.Infrastructure.Repositories;

namespace Ride_app.Infrastructure.Services
{
    public class UserService
    {
        UserRepository userRepository = new UserRepository();
        DriverRepository driverRepository = new DriverRepository();
        PassengerRepository passengerRepository = new PassengerRepository();

        public void CreateDriver(string username, string password, decimal wallet, Location location)
        {
            User user = new User(wallet, location, username, password);
            Driver driver = new Driver(user);
            //driverRepository.AddNewDriver(driver);
            userRepository.AddNewDriver(driver);
        }
        public void CreatePassenger(string username, string password, decimal wallet, Location location)
        {
            User user = new User(wallet, location, username, password);
            Passenger passenger = new Passenger(user);
            //passengerRepository.AddNewPassenger(passenger);
            userRepository.AddNewPassenger(passenger);
        }
    }
}
