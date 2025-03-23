using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ride_app.Enities;
using Ride_app.Infrastructure.Services;

namespace Ride_app.Presentation.Controllers
{
    public class UserController
    {
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
    }
}
