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
        public static List<User> users = new List<User>();

        private static readonly string JsonFilePath = "C:\\Users\\Mishalia Pillay\\Desktop\\ride-app-console\\Ride-app\\Data\\data.json";

        public UserRepository()
        {
            LoadFromFile();
        }

        private void LoadFromFile()
        {
            try
            {
                if (File.Exists(JsonFilePath))
                {
                    string jsonData = File.ReadAllText(JsonFilePath);
                    users = JsonConvert.DeserializeObject<List<User>>(jsonData/*, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }*/) ?? new List<User>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading from JSON file: " + ex.Message);
                users = new List<User>();
            }
        }

        public Decimal GetUserWallet(int id)
        {
            try
            {
                User user = users.Where(u => u._id == id).FirstOrDefault();
                return user._wallet;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return -1;
            }
        }

        public void AddNewDriver(Driver driver)
        {
            try
            {
                if (File.Exists(JsonFilePath))
                {
                    users.Add(driver);
                    SaveToFile();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        public void AddNewPassenger(Passenger passenger)
        {
            try
            {
                if (File.Exists(JsonFilePath))
                {
                    users.Add(passenger);
                    SaveToFile();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        public void SaveToFile()
        {
            try
            {
                string JsonData = JsonConvert.SerializeObject(users, Formatting.Indented/*, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }*/);
                File.WriteAllText(JsonFilePath, JsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to JSON file: {ex.Message}");
            }
        }
        public void ToggleDriverAvailability(int id)
        {
            Console.WriteLine("Reposiroty " + id);
            User userToUpdate = users.FirstOrDefault(u => u._id == id);
            Console.WriteLine(userToUpdate._name);
            Console.WriteLine(userToUpdate._isDriver);
            if (userToUpdate is Driver driverToUpdate)
            {
                driverToUpdate._isAvailable = !driverToUpdate._isAvailable; SaveToFile();
            }
            else
            {
                Console.WriteLine("User not found for some reason");
            }
        }
        public void UpdateDriver(Driver driver, int id)
        {
            try
            {
                Console.WriteLine("Repo");
                User userToUpdate = users.Where(u => u._id == id).FirstOrDefault();

                if (userToUpdate is Driver driverToUpdate)
                {
                    driverToUpdate._wallet = driver._wallet;
                    driverToUpdate._password = driver._password;
                    driverToUpdate._location = driver._location;
                    driverToUpdate._name = driver._name;
                    driverToUpdate._rating = driver._rating;
                    driverToUpdate._isAvailable = driver._isAvailable;

                    SaveToFile();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void UpdatePassenger(Passenger passenger, int id)
        {
            try
            {
                User userToUpdate = users.Where(u => u._id == id).FirstOrDefault();

                if (userToUpdate is Passenger passengerToUpdate)
                {
                    passengerToUpdate._wallet = passenger._wallet;
                    passengerToUpdate._name = passenger._name;
                    passengerToUpdate._location = passenger._location;
                    passengerToUpdate._password = passenger._password;

                    SaveToFile();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void AddUserRide(int id, int rideID)
        {
            try
            {
                User user = users.Where(u => u._id == id).FirstOrDefault();
                if (user is Passenger passenger)
                {
                    passenger.rideIDs.Add(rideID);
                }
                SaveToFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        public User FindUser(int id)
        {
            return users.Where(u => u._id == id).FirstOrDefault();
        }

        public bool FindUsername(string username)
        {
            User user = users.Where(u => u._name == username).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckPassword(string username, string password)
        {
            User user = users.Where(u => u._name == username).FirstOrDefault();
            if (user._password == password)
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
            User user = users.Where(u => u._name == username).FirstOrDefault();
            return user._id;
        }
        public Location GetUserLocation(int id)
        {
            try
            {
                User user = users.Where(u => u._id == id).FirstOrDefault();
                return user._location;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
