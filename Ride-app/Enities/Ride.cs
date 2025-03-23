using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ride_app.Enities;

namespace Ride_app.Enities
{
    public class Ride
    {
        public int rideID;
        public Location _pickUp { get; set; }
        public Location _dropOff { get; set; }
        public int _passengerID { get; set; }
        public int _driverID { get; set; }
        public int _rating { get; set; }
        public decimal _rate { get; set; }

        public Ride(Location pickUp, Location dropOff)
        {
            _pickUp = pickUp;
            _dropOff = dropOff;
            _passengerID = -1;
            _driverID = -1;
            _rating = 0;
            _rate = 0M;
        }

    }
}
