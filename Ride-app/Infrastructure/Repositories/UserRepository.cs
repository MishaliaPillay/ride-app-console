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
        public void UpdateDriver(Driver driver, int id)
        {
            try
            {
                User userToUpdate = users.Where(u => u._id == id).First();

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
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }


        public void UpdatePassenger(Passenger passenger, int id)
        {
            try
            {
                User userToUpdate = users.Where(u => u._id == id).First();

                if (userToUpdate is Passenger passengerToUpdate)
                {
                    passengerToUpdate._wallet = passenger._wallet;
                    passengerToUpdate._name = passenger._name;
                    passengerToUpdate._location = passenger._location;
                    passengerToUpdate._password = passenger._password;
                    //passengerToUpdate._rides = passenger._rides;
                    //passengerToUpdate._rides.Add(passenger._rides.First());
                    SaveToFile();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }

        public User FindUser(int id)
        {
            return users.Where(u => u._id == id).First();
        }

        public bool FindUsername(string username)
        {
            User user = users.Where(u => u._name == username).First();
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
            User user = users.Where(u => u._name == username).First();
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
            User user = users.Where(u => u._name == username).First();
            return user._id;
        }
    }
}
