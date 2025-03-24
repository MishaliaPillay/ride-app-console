using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Ride_app.Enities;
using Ride_app.Infrastructure.Repositories;

namespace Ride_app.Infrastructure.Services
{
    public class RideController
    {
        public RideRepository rideRepository = new RideRepository();
        public decimal GetRideDistance(Ride ride)
        {
            return (decimal)Math.Sqrt(Math.Pow(ride._pickUp._longitude - ride._dropOff._longitude, 2) + Math.Pow(ride._pickUp._latitude - ride._dropOff._latitude, 2));
        }
        public decimal CalculateRidePrice(Ride ride)
        {
            return GetRideDistance(ride) * 10;
        }
        public float GetRideDistance(Location location1, Location location2)
        {
            return (float)Math.Sqrt(Math.Pow(location1._latitude - location2._latitude, 2) + Math.Pow(location1._longitude - location2._longitude, 2));
        }
        public void AssignRideDriver(int userID, int rideID)
        {
            Ride ride = rideRepository.GetRideByID(rideID);
            ride._driverID = userID;
            rideRepository.UpdateRide(ride, rideID);
        }
        public Ride GetLatestRide(int id)
        {
            return rideRepository.GetLatestRide(id);
        }
        public void AssignRideRating(Ride ride, int rating)
        {
            ride._rating = rating;
            rideRepository.UpdateRide(ride, ride.rideID);
        }
        public List<Ride> GetRideSummary(int userID)
        {
            return rideRepository.GetRideSummary(userID);
        }
    }
}
