using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ride_app.Application.Interfacse;
using Ride_app.Enities;

namespace Ride_app.Infrastructure.Repositories
{
    public class UserRepository
    {
        List<User> users = new List<User>();

        private static readonly string JsonFilePath = "C:\\Users\\Mishalia Pillay\\Desktop\\ride-app-console\\Ride-app\\Data\\data.json";

        public void AddNewUser(User user)
        {
            if (File.Exists(JsonFilePath))
            {
                users.Add(user);

                SaveToFile();
            }
        }

        public void AddNewDriver(Driver driver)
        {
            if (File.Exists(JsonFilePath))
            {
                users.Add(driver);

                SaveToFile();
            }
        }

        public void AddNewPassenger(Passenger passenger)
        {
            if (File.Exists(JsonFilePath))
            {
                users.Add(passenger);

                SaveToFile();
            }
        }

        public void SaveToFile()
        {
            try
            {
                string JsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
                File.WriteAllText(JsonFilePath, JsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to JSON file: {ex.Message}");
            }
        }
    }
}
