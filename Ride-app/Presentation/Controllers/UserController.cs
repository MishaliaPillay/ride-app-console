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
        public void UpdateUser()
        {
            Console.Write("GetUserID");
            int id = int.Parse(Console.ReadLine());

            if (userService.IsDriver(id))
            {
                UpdateDriver(id);
            }
            else
            {
                UpdatePassenger(id);
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
                Console.WriteLine("Signed in");
                return userService.GetUserID(username);
            }


        }
        public void UpdateDriver(int id)
        {
            Console.WriteLine("Update Availability");
            int result = int.Parse(Console.ReadLine());
            bool available;
            if (result == 0)
            {
                available = true;
            }
            else
            {
                available = false;
            }
            Console.WriteLine("Update wallet: ");
            decimal walletChange = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Change xPos: ");
            float xPos = int.Parse(Console.ReadLine());
            Console.WriteLine("Change yPos: ");
            float yPos = int.Parse(Console.ReadLine());

            //userService.UpdateDriver(walletChange, xPos, yPos, available, id);

        }
        public void UpdatePassenger(int id)
        {
            Console.WriteLine("Update wallet: ");
            decimal walletChange = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Change xPos: ");
            float xPos = int.Parse(Console.ReadLine());
            Console.WriteLine("Change yPos: ");
            float yPos = int.Parse(Console.ReadLine());

            //userService.UpdatePassenger(walletChange, xPos, yPos, id);

        }
    }
}
