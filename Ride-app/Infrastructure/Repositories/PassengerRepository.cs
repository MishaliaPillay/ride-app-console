using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ride_app.Enities;

namespace Ride_app.Infrastructure.Repositories
{
    public class PassengerRepository
    {
        readonly List<Passenger> passengers = new List<Passenger>();

        private static readonly string JsonFilePath = "Data\\Passengers.json";

        public void AddNewPassenger(Passenger newPassenger)
        {
            if (File.Exists(JsonFilePath))
            {
                passengers.Add(newPassenger);

                SaveToFile();
            }
        }

        public void SaveToFile()
        {
            try
            {
                string JsonData = JsonConvert.SerializeObject(passengers, Formatting.Indented);
                File.WriteAllText(JsonFilePath, JsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to JSON file: {ex.Message}");
            }
        }
    }
}
