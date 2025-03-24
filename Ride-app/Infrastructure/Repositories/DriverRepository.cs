using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ride_app.Enities;

namespace Ride_app.Infrastructure.Repositories
{
    public class DriverRepository
    {

        List<Driver> drivers = new List<Driver>();

        private static readonly string JsonFilePath = "C:\\Users\\Mishalia Pillay\\Desktop\\ride-app-console\\Ride-app\\Data\\Drivers.json";

        public void AddNewDriver(Driver newDriver)
        {
            if (File.Exists(JsonFilePath))
            {
                drivers.Add(newDriver);

                SaveToFile();
            }
        }

        public void SaveToFile()
        {
            try
            {
                string JsonData = JsonConvert.SerializeObject(drivers, Formatting.Indented);
                File.WriteAllText(JsonFilePath, JsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to JSON file: {ex.Message}");
            }
        }

    }
}
