﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ride_app.Application.Interfacse;
using Ride_app.Enities;

namespace Ride_app.Infrastructure.Repositories
{
    public class RideRepository
    {
        public static List<Ride> rides = new List<Ride>();

        private static readonly string JsonFilePath = "Data\\rides.json";

        public RideRepository()
        {
            LoadFromFile();
        }
        public List<Ride> GetRideSummary(int id)
        {
            return rides.Where(r => (r._passengerID == id) && r.isComplete).ToList();
        }
        public Ride GetLatestRide(int id)
        {
            return rides.Where(r => r._passengerID == id).Last();
        }
        public bool HasPreviousRides(int userID)
        {
            try
            {
                List<Ride> completedRides = rides.Where(r => (r._passengerID == userID) && (r.isComplete)).ToList();
                if (completedRides.Count == 0)
                {
                    return false;
                }
                else if (completedRides.Count > 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception();
                    return false;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public List<Ride> GetDriverlessRides()
        {
            return rides.Where(r => r._driverID == -1).ToList();
        }
        private void LoadFromFile()
        {
            try
            {
                if (File.Exists(JsonFilePath))
                {
                    string jsonData = File.ReadAllText(JsonFilePath);
                    rides = JsonConvert.DeserializeObject<List<Ride>>(jsonData) ?? new List<Ride>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading from JSON file: " + ex.Message);
                rides = new List<Ride>();
            }
        }

        public void AddNewRide(Ride ride)
        {
            if (File.Exists(JsonFilePath))
            {
                rides.Add(ride);

                SaveToFile();
            }
        }
        public void SaveToFile()
        {
            try
            {
                string JsonData = JsonConvert.SerializeObject(rides, Formatting.Indented);
                File.WriteAllText(JsonFilePath, JsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to JSON file: {ex.Message}");
            }
        }
        public void UpdateRide(Ride newRide, int id)
        {
            try
            {
                Ride rideToUpdate = rides.Where(r => r.rideID == id).FirstOrDefault();
                rideToUpdate._driverID = newRide._driverID;
                rideToUpdate._rating = newRide._rating;
                rideToUpdate._rate = newRide._rate;
                SaveToFile();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error updating ride: " + ex.ToString());
            }

        }
        public Ride GetRideByID(int id)
        {
            return rides.Where(r => r.rideID == id).FirstOrDefault();
        }
    }
}
