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
        private Location _pickUp { get; set; }
        private Location _dropOff { get; set; }
        private Passenger _passenger { get; set; }
        private Driver _driver { get; set; }
        private int _rating { get; set; }
        private decimal _rate { get; set; }
    }
}
